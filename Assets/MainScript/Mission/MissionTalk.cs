using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionTalk : TalkBase
{
    [Header("���C���~�b�V�����̔ԍ�")]
    [SerializeField] private int _missionNumber;

    [Header("�~�b�V�����̏ڍהԍ�")]
    [SerializeField] private int _missionDetailNumber;

    [Header("�f�o�b�O�p�̃~�b�V�����̊Ȍ��ȓ��e")]
    [SerializeField] private string _missionDetail;

    [Header("�C���x���g���ɕ\�L���鎟�̃~�b�V�������e")]
    [SerializeField] private string _missionLongDetail;

    [Header("�J�n���ɘb�����t")]
    [SerializeField] private List<string> _firstContactText = new List<string>();

    [Header("�ēx�A�b�����Ƃ��̌��t")]
    [SerializeField] private List<string> _receivedMissionText = new List<string>();

    [Header("��b���Ɏ��s���������e�ƁA��b�̍s��o�^�o����")]
    [SerializeField] private List<Events> _talkEvents = new List<Events>();

    [Header("�ŏ��̈��̉�b���I��������Ɏ��s�������")]
    [SerializeField] private UnityEvent _endEvent;

    private MissionManager _missionManager;

    MissionSituation _missionSituation = MissionSituation.First;
    public enum MissionSituation
    {
        //�~�b�V�������󂯂͂���
        First,

        //�~�b�V�������󂯂Ă���
        RecebedMission,
    }

    private void Start()
    {
        _missionManager = GameObject.FindObjectOfType<MissionManager>();

        //�~�b�V�����̏ڍׂ��f�o�b�O
        Debug.Log($"{_missionDetail} : {_missionNumber}_{_missionDetailNumber}�ڂ̃^�X�N");
    }


    /// <summary>��b�̓��e�����߂�֐�</summary>
    protected override void TalkSet()
    {
        //��b���e�����Z�b�g
        _nowTalkText.Clear();

        if (_talkedNum == 0)
        {
            _nowTalkText = _firstContactText;
            _missionSituation = MissionSituation.First;
        }
        else
        {
            _nowTalkText = _receivedMissionText;
            _missionSituation = MissionSituation.RecebedMission;
        }
    }

    /// <summary>��b���Ɏ��s����C�x���g������ꍇ�Ɏ��s</summary>
    /// <param name="talkNum"></param>
    protected override void TalkInEvent(int talkNum)
    {
        foreach (var a in _talkEvents)
        {
            if (a.MissionSituation == _missionSituation)
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
            _endEvent.Invoke();
            _missionManager.NowMainMission.CheckMission();
            _missionManager.SettingMissionText(_missionDetail, _missionLongDetail);
        }


    }


}

[System.Serializable]
public class Events
{
    [Header("�C�x���g�����s����^�C�~���O")]
    [SerializeField] private MissionTalk.MissionSituation _missionSituation;

    public MissionTalk.MissionSituation MissionSituation => _missionSituation;

    [Header("���s����A��b�̍s")]
    [SerializeField]
    private int _number;

    public int Number => _number;

    [Header("���s���鏈��")]
    [SerializeField] private UnityEvent _events;

    public UnityEvent TalkEvent => _events;

}