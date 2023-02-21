using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionTalk : TalkBase
{
    [Header("開始時に話す言葉")]
    [SerializeField, TextArea(3, 20)] private List<string> _firstContactText = new List<string>();

    [Header("再度、話したときの言葉")]
    [SerializeField] private List<string> _receivedMissionText = new List<string>();

    [Header("会話中に実行したい内容と、会話の行を登録出来る_最初に話したときだけ")]
    [SerializeField] private List<Events> _talkEvents = new List<Events>();


    [SerializeField] private MissionManager _missionManager;


    private void Start()
    {

    }


    /// <summary>会話の内容を決める関数</summary>
    protected override void TalkSet()
    {
        //会話内容をリセット
        _nowTalkText.Clear();

        //最初に会話する
        if (_talkedNum == 0)
        {
           
            _nowTalkText = _firstContactText;
        }
        else
        {
            _nowTalkText = _receivedMissionText;
        }
    }

    /// <summary>会話中に実行するイベントがある場合に実行</summary>
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

    /// <summary>会話が終了した時にするイベント</summary>
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
    [Header("実行する、会話の行")]
    [SerializeField]
    private int _number;

    public int Number => _number;

    [Header("実行する処理")]
    [SerializeField] private UnityEvent _events;

    public UnityEvent TalkEvent => _events;

}