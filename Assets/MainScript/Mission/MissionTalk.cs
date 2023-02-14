using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionTalk : TalkBase
{
    [Header("メインミッションの番号")]
    [SerializeField] private int _missionNumber;

    [Header("ミッションの詳細番号")]
    [SerializeField] private int _missionDetailNumber;

    [Header("デバッグ用のミッションの簡潔な内容")]
    [SerializeField] private string _missionDetail;

    [Header("インベントリに表記する次のミッション内容")]
    [SerializeField] private string _missionLongDetail;

    [Header("開始時に話す言葉")]
    [SerializeField] private List<string> _firstContactText = new List<string>();

    [Header("再度、話したときの言葉")]
    [SerializeField] private List<string> _receivedMissionText = new List<string>();

    [Header("会話中に実行したい内容と、会話の行を登録出来る")]
    [SerializeField] private List<Events> _talkEvents = new List<Events>();

    [Header("最初の一回の会話が終わった時に実行するもの")]
    [SerializeField] private UnityEvent _endEvent;

    private MissionManager _missionManager;

    MissionSituation _missionSituation = MissionSituation.First;
    public enum MissionSituation
    {
        //ミッションを受けはじめ
        First,

        //ミッションを受けている
        RecebedMission,
    }

    private void Start()
    {
        _missionManager = GameObject.FindObjectOfType<MissionManager>();

        //ミッションの詳細をデバッグ
        Debug.Log($"{_missionDetail} : {_missionNumber}_{_missionDetailNumber}目のタスク");
    }


    /// <summary>会話の内容を決める関数</summary>
    protected override void TalkSet()
    {
        //会話内容をリセット
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

    /// <summary>会話中に実行するイベントがある場合に実行</summary>
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

    /// <summary>会話が終了した時にするイベント</summary>
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
    [Header("イベントを実行するタイミング")]
    [SerializeField] private MissionTalk.MissionSituation _missionSituation;

    public MissionTalk.MissionSituation MissionSituation => _missionSituation;

    [Header("実行する、会話の行")]
    [SerializeField]
    private int _number;

    public int Number => _number;

    [Header("実行する処理")]
    [SerializeField] private UnityEvent _events;

    public UnityEvent TalkEvent => _events;

}