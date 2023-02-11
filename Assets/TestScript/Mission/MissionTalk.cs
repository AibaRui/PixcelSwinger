using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionTalk : TalkBase
{
    [SerializeField] int _missionNumber;

    [SerializeField] int _missionDetailNumber;

    [SerializeField] string _missionDetail;

    [SerializeField]
    List<Events> _talkEvents = new List<Events>();

    private Dictionary<int, List<UnityEvent>> _events = new Dictionary<int, List<UnityEvent>>();

    [SerializeField]
    private UnityEvent _endEvent;

    [Header("開始時に話す言葉")]
    [SerializeField] protected List<string> _firstContactText = new List<string>();

    [Header("再度、話したときの言葉")]
    [SerializeField] protected List<string> _receivedMissionText = new List<string>();

    [Header("終わった時の言葉")]
    [SerializeField] protected List<string> _endMissionText = new List<string>();

    private MissionBase _missionBase;
    private MissionManager _missionManager;
    PlayerInput _playerInput;

    private void Start()
    {
        _missionBase = GameObject.FindObjectOfType<MissionBase>();
        _missionManager = GameObject.FindObjectOfType<MissionManager>();
        _playerInput = FindObjectOfType<PlayerInput>();
        MissionDetail();

        foreach(var a in _talkEvents)
        {
            _events.Add(a.Number, a.TalkEvent);
        }

    }

    private void Update()
    {
        //受付範囲に入っているかどうか
        if (_isEnter && !_isTalkNow)
        {
            //スペースを押す
            if (_playerInput.IsJumping)
            {
                _nowTalkText.Clear();
                if (_talkedNum == 0)
                {
                    _nowTalkText = _firstContactText;
                }
                else
                {
                    _nowTalkText = _receivedMissionText;
                }

                StartTalk();
            }
        }
    }

    public void MissionDetail()
    {
        Debug.Log($"{_missionDetail} : {_missionNumber}_{_missionDetailNumber}目のタスク");
    }


    /// <summary>会話中に実行するイベントがある場合に実行</summary>
    /// <param name="talkNum"></param>
    protected override void TalkInEvent(int talkNum)
    {
        if (_events.Count > 0)
        {
            foreach (var a in _events[talkNum])
            {
                a?.Invoke();
            }
        }
        //Eventを実行
        
    }

    /// <summary>会話が終了した時にするイベント</summary>
    protected override void TalkEndEvent()
    {
        _endEvent.Invoke();
        _missionManager.NowMainMission.CheckMission();
        gameObject.SetActive(false);
    }

}

[System.Serializable]
class Events
{
    [SerializeField]
    private int _number;

    public int Number => _number;

    [SerializeField] private List<UnityEvent> _events = new List<UnityEvent>();

    public List<UnityEvent> TalkEvent => _events;

}