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

        //�~�b�V�����̎�t��Ԃɂ���āA��b�̓��e��ς���

        //�~�b�V�������Ȃ���
        if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.NoMission)
        {
            _nowTalkText.Clear();
            _nowTalkText.Add("���A�~�b�V�����͂Ȃ���");
        }  //�~�b�V���������邪�A�󂯂Ă��Ȃ�
        else if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.NoAcceptMission)
        {
            _nowTalkText = _acceptMissionText;
        }  //�~�b�V�����i�s��
        else if (_missionManager._mainMissionSituation == MissionManager.MainMissionSituation.RecebedMission)
        {
            _nowTalkText = _receivedMissionText;
        }
        else  //�~�b�V�����������Ă���
        {
            _nowTalkText = _endMissionText;
        }
    }

    protected override void TalkInEvent(int talkNum)
    {
        if (_missionManager.NowMainMission != null)
        {
            foreach (var a in _missionManager.NowMainMission.TalkEvent)
            {
                if (a.MissionSituation == _missionManager._mainMissionSituation)
                {
                    if (a.Num == talkNum)
                    {
                        _isEvent = true;
                        a.UnityEvent?.Invoke();
                    }
                }
            }
        }
    }

    protected override void TalkEndEvent()
    {
        _missionManager.TalkEnd();
    }


}
