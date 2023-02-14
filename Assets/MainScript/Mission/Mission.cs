using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mission : MonoBehaviour
{
    [Header("�~�b�V�����̊Ȍ��ȓ��e")]
    [SerializeField] private string _missionDetail;

    [Header("�C���x���g���ɕ\�L���鎟�̃~�b�V�������e")]
    [SerializeField] private string _missionLongDetail;



    [Header("�~�b�V�����J�n���ɏo����������")]
    [SerializeField]
    private UnityEvent _firstEvent;

    [Header("�~�b�V�����N���A���ɏo����������")]
    [SerializeField]
    private UnityEvent _endEvent;

    [Header("�󂯕t����Ƃ��̃Z���t")]
    [SerializeField] protected List<string> _acceptMissionText = new List<string>();

    [Header("�󂯕t������̃Z���t")]
    [SerializeField] protected List<string> _receivedMissionText = new List<string>();

    [Header("�Z���t���ɏo���������o��ݒ�ł���")]
    [SerializeField] private List<TalkEvents> _talkEvent = new List<TalkEvents>();

    public List<TalkEvents> TalkEvent { get => _talkEvent; }

    [Header("�N���A�����Ƃ��̃Z���t")]
    [SerializeField] protected List<string> _endMissionText = new List<string>();



    [Header("�~�b�V�����̔ԍ�")]
    [SerializeField]
    private int _missionNum;


    private int _missionClearNum;

    protected MissionManager _missionManager = null;

    protected bool _isMissionCompleted = false;

    public bool IsMissionCompleted { get => _isMissionCompleted; }


    protected int _enterMissionCount = 0;


    /// <summary>MissionManager���Z�b�g����֐�</summary>
    /// <param name="missionManager"></param>
    public void Init(MissionManager missionManager)
    {
        _missionManager = missionManager;
        _missionManager.CheckMission.AcceptMissionText = _acceptMissionText;
        _missionManager.CheckMission.ReceivedText = _receivedMissionText;
        _missionManager.CheckMission.EndText = _endMissionText;

    }

    public void StartMission()
    {
        _firstEvent?.Invoke();
        _missionManager.SettingMissionText(_missionDetail, _missionLongDetail);
    }

    public void CheckMission()
    {
        _missionClearNum++;

        if (_missionClearNum == _missionNum)
        {
            _endEvent?.Invoke();
            _isMissionCompleted = true;
            MissionClear();
        }
    }

    /// <summary>�N�G�X�g�����������Ɏ��s</summary>
    public void MissionClear()
    {
        Debug.Log("Clear");
    }

}

[System.Serializable]
 public class TalkEvents
{
    [SerializeField] private UnityEvent _unityEvent;

    [SerializeField] private int _num;

    [SerializeField] private MissionManager.MainMissionSituation _missionSituation;

    public MissionManager.MainMissionSituation MissionSituation => _missionSituation;

    public UnityEvent UnityEvent => _unityEvent;

    public int Num => _num;


}

