using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Mission : MonoBehaviour
{

    [Header("�~�b�V�����̔ԍ�")]
    [SerializeField] private int _missionNum;


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

    [Header("���̃~�b�V�����̃^�X�N")]
    [SerializeField] private List<MissionDetail> _missionDetails = new List<MissionDetail>();


    [Header("�~�b�V�����I�����ɏ�������")]
    [SerializeField] private List<GameObject> _endActiveFalses = new List<GameObject>();


    public List<MissionDetail> MissionDetails => _missionDetails;

    private MissionDetail _nowMissionDetail;

    public MissionDetail NowMissionDetail => _nowMissionDetail;



    public int MissionNum => _missionNum;


    private int _nowDetailMissionNum = 0;

    public int NowDetailMissionNum { get => _nowDetailMissionNum; set => _nowDetailMissionNum = value; }

    [SerializeField] MissionManager _missionManager = null;

    protected bool _isMissionCompleted = false;

    public bool IsMissionCompleted { get => _isMissionCompleted; }


    protected int _enterMissionCount = 0;


    public void SetTalks()
    {
        _missionManager.CheckMission.AcceptMissionText = _acceptMissionText;
        _missionManager.CheckMission.ReceivedText = _receivedMissionText;
        _missionManager.CheckMission.EndText = _endMissionText;
    }

    /// <summary>Mission���n�߂�֐�</summary>
    /// <param name="missionManager"></param>
    public void StartMission()
    {
        Debug.Log($"�~�b�V����:{_missionNum}�J�n");
        GoNextMission();
    }
    /// <summary>���̃^�X�N��o�^����</summary>
    public void GoNextMission()
    {
        _missionManager.ClearMissionDetailNum = _nowDetailMissionNum;


        _missionManager.NowDetailMissionNum = _nowDetailMissionNum;
        _nowDetailMissionNum++;

        if (_missionDetails.Count >= _nowDetailMissionNum)
        {

            _missionDetails[_nowDetailMissionNum - 1].Init(this);
            _nowMissionDetail = _missionDetails[_nowDetailMissionNum - 1];
        }
        else
        {
            Debug.Log($"{_missionDetails.Count}>={_nowDetailMissionNum}");
            //_missionManager.ClearNowMission();
            _isMissionCompleted = true;
        }

        _missionManager.Save();
    }

    public void ClearMission()
    {
        _endActiveFalses.ForEach(i => i.SetActive(false));
        _endEvent?.Invoke();
    }


    public void CheckReward()
    {
        _endActiveFalses.ForEach(i => i.SetActive(false));
        _endEvent?.Invoke();
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

