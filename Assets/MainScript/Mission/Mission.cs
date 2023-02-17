using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Mission : MonoBehaviour
{

    [Header("ミッションの番号")]
    [SerializeField] private int _missionNum;


    [Header("ミッションの簡潔な内容")]
    [SerializeField] private string _missionDetail;

    [Header("インベントリに表記する次のミッション内容")]
    [SerializeField] private string _missionLongDetail;


    [Header("ミッション開始時に出したいもの")]
    [SerializeField]
    private UnityEvent _firstEvent;

    [Header("ミッションクリア時に出したいもの")]
    [SerializeField]
    private UnityEvent _endEvent;

    [Header("受け付けるときのセリフ")]
    [SerializeField] protected List<string> _acceptMissionText = new List<string>();

    [Header("受け付けた後のセリフ")]
    [SerializeField] protected List<string> _receivedMissionText = new List<string>();

    [Header("セリフ中に出したい演出を設定できる")]
    [SerializeField] private List<TalkEvents> _talkEvent = new List<TalkEvents>();

    public List<TalkEvents> TalkEvent { get => _talkEvent; }

    [Header("クリアしたときのセリフ")]
    [SerializeField] protected List<string> _endMissionText = new List<string>();

    [Header("このミッションのタスク")]
    [SerializeField] private List<MissionDetail> _missionDetails = new List<MissionDetail>();


    [Header("ミッション終了時に消すもの")]
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

    /// <summary>Missionを始める関数</summary>
    /// <param name="missionManager"></param>
    public void StartMission()
    {
        Debug.Log($"ミッション:{_missionNum}開始");
        GoNextMission();
    }
    /// <summary>次のタスクを登録する</summary>
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

