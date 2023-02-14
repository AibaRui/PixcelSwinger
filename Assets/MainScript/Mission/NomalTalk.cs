using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>通常の会話の際に使うクラス</summary>
public class NomalTalk : TalkBase
{
    [Header("一番最初に話す言葉")]
    [SerializeField] private List<string> _firstContactText = new List<string>();

    [Header("ランダムに話すかどうか")]
    [SerializeField] private bool _isRandamTalk = false;

    [Header("ランダムに話す内容")]
    [SerializeField] private List<RandomTalk> _randomTalk = new List<RandomTalk>();

    [Header("一番最初に話したときにすること")]
    [SerializeField] private UnityEvent _event;

    /// <summary>最初の会話に限り、話し終えたときに登録してあるイベントを実行する</summary>
    protected override void TalkEndEvent()
    {
        if(_talkedNum==1)
        {
            _event?.Invoke();
        }
    }

    protected override void TalkInEvent(int talkNum)
    {

    }

    /// <summary>会話の内容を登録する</summary>
    protected override void TalkSet()
    {
        //ランダムに会話する場合
        if (_isRandamTalk)
        {
            //最初は固定の話
            if (_talkedNum == 0)
            {
                _nowTalkText = _firstContactText;
            }
            else //そのあとはランダムに話す
            {
                var r = Random.Range(0, _randomTalk.Count);
                _nowTalkText = _randomTalk[r].Talk;
            }
        }
        else　//ランダム会話しない場合は、固定の話をし続ける
        {
            _nowTalkText = _firstContactText;
        }
    }
}

[System.Serializable]
public class RandomTalk
{
    [SerializeField]
    private List<string> _talk = new List<string>();

    public List<string> Talk => _talk;
}
