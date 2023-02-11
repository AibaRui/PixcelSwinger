using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CheckMission : TalkBase
{

    private List<string> _acceptMissionText = new List<string>();

    private List<string> _receivedMissionText = new List<string>();

    private List<string> _endMissionText = new List<string>();


    public List<string> AcceptMissionText { set => _acceptMissionText = value; get => _acceptMissionText; }

    public List<string> ReceivedText { set => _receivedMissionText = value; get => _receivedMissionText; }

    public List<string> EndText { set => _endMissionText = value; get => _endMissionText; }


    [SerializeField] MissionManager _missionManager;

    [SerializeField] PlayerInput _playerInput;


    public void CheckPlayerToTalk()
    {
        //受付範囲に入っているかどうか
        if (_isEnter && !_isTalkNow)
        {
            //スペースを押す
            if (_playerInput.IsJumping)
            {
                _missionManager.CheckMissionToTalk();

                //ミッションの受付状態によって、会話の内容を変える

                //ミッションがない時
                if (_missionManager._missionSituation == MissionManager.MissionSituation.NoMission)
                {
                    _nowTalkText.Clear();
                    _nowTalkText.Add("今、ミッションはないよ");
                }  //ミッションがあるが、受けていない
                else if (_missionManager._missionSituation == MissionManager.MissionSituation.NoAcceptMission)
                {
                    _nowTalkText = _acceptMissionText;
                }  //ミッション進行中
                else if (_missionManager._missionSituation == MissionManager.MissionSituation.RecebedMission)
                {
                    _nowTalkText = _receivedMissionText;
                }
                else  //ミッション完了している
                {
                    _nowTalkText = _endMissionText;
                }

                StartTalk();
            }
        }
    }


    protected override void TalkInEvent(int talkNum)
    {

    }

    protected override void TalkEndEvent()
    {
        _missionManager.TalkEnd();
    }
}
