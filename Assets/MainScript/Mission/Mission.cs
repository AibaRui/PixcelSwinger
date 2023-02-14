using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mission : MonoBehaviour
{
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



    [Header("ミッションの番号")]
    [SerializeField]
    private int _missionNum;


    private int _missionClearNum;

    protected MissionManager _missionManager = null;

    protected bool _isMissionCompleted = false;

    public bool IsMissionCompleted { get => _isMissionCompleted; }


    protected int _enterMissionCount = 0;


    /// <summary>MissionManagerをセットする関数</summary>
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

    /// <summary>クエスト完了した時に実行</summary>
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

