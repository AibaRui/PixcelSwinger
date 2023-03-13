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

    protected override void TalkSet()
    {
        _missionManager.CheckMissionToTalk();

        //ミッションの受付状態によって、会話の内容を変える

        //ミッションがない時
        if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.NoMission)
        {
            _nowTalkText.Clear();
            _nowTalkText.Add("今、ミッションはないよ");
            return;
        }  //ミッションがあるが、受けていない
        else if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.NoAcceptMission)
        {
            _nowTalkText = _acceptMissionText;
            return;
        }  //ミッション進行中
        else if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.RecebedMission)
        {
            _nowTalkText = _receivedMissionText;
            return;
        }
        else  //ミッション完了している
        {
            _nowTalkText = _endMissionText;
        }
    }

    protected override void TalkInEvent(int talkNum)
    {
        if (_missionManager.NowMainMission != null)
        {
            Debug.Log("よばれた");
            foreach (var a in _missionManager.NowMainMission.TalkEvent)
            {
                if (a.MissionSituation == _missionManager._mainMissionSituation)
                {
                    Debug.Log("あってる");
                    if (a.Num == talkNum)
                    {
                        Debug.Log("発生");
                        _isEvent = true;
                        a.UnityEvent?.Invoke();
                    }
                }
            }
        }
    }

    protected override void TalkEndEvent()
    {
        if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.NoAcceptMission)
        {
            _missionManager.GoNextMission();
        }
        else if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.ClearMission)
        {
            _missionManager.ClearNowMission();
        }
    }


}
