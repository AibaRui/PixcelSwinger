using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionTalk : TalkBase
{
    [Header("�J�n���ɘb�����t")]
    [SerializeField, TextArea(3, 20)] private List<string> _firstContactText = new List<string>();

    [Header("�ēx�A�b�����Ƃ��̌��t")]
    [SerializeField] private List<string> _receivedMissionText = new List<string>();

    [Header("��b���Ɏ��s���������e�ƁA��b�̍s��o�^�o����_�ŏ��ɘb�����Ƃ�����")]
    [SerializeField] private List<Events> _talkEvents = new List<Events>();


    [SerializeField] private MissionManager _missionManager;


    private void Start()
    {

    }


    /// <summary>��b�̓��e�����߂�֐�</summary>
    protected override void TalkSet()
    {
        //��b���e�����Z�b�g
        _nowTalkText.Clear();

        //�ŏ��ɉ�b����
        if (_talkedNum == 0)
        {
           
            _nowTalkText = _firstContactText;
        }
        else
        {
            _nowTalkText = _receivedMissionText;
        }
    }

    /// <summary>��b���Ɏ��s����C�x���g������ꍇ�Ɏ��s</summary>
    /// <param name="talkNum"></param>
    protected override void TalkInEvent(int talkNum)
    {
        foreach (var a in _talkEvents)
        {
            if (_talkedNum == 1)
            {
                if (a.Number == talkNum)
                {
                    _isEvent = true;
                    a.TalkEvent?.Invoke();
                }
            }
        }

    }

    /// <summary>��b���I���������ɂ���C�x���g</summary>
    protected override void TalkEndEvent()
    {
        if (_talkedNum == 1)
        {
            _missionManager.NowMainMission.NowMissionDetail.ClearMissionTask();
        }


    }


}

[System.Serializable]
public class Events
{
    [Header("���s����A��b�̍s")]
    [SerializeField]
    private int _number;

    public int Number => _number;

    [Header("���s���鏈��")]
    [SerializeField] private UnityEvent _events;

    public UnityEvent TalkEvent => _events;

}