using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public abstract class TalkBase : MonoBehaviour
{
    [SerializeField] string _humanName;

    [Tooltip("話す内容を入れるリスト")]
    protected List<string> _nowTalkText = new List<string>();

    public List<string> NowTalkText { set => _nowTalkText = value; get => _nowTalkText; }

    protected bool _isTalkNow = false;

    public bool IsTalkNow { get => _isTalkNow; }

    protected bool _isEnter = false;

    /// <summary>会話の途中で演出をしたい際に、現在演出中かどうかを表す</summary>
    protected bool _isEvent;

    protected TalkManager _talkManager;
    protected EventController _eventController;
    protected ItemManager _itemManager;

    protected int _talkedNum = 0;

    public int TalkNum { get => _talkedNum; set => _talkedNum = value; }

    private void Awake()
    {
        _itemManager = GameObject.FindObjectOfType<ItemManager>();
        _talkManager = GameObject.FindObjectOfType<TalkManager>();
        _eventController = GameObject.FindObjectOfType<EventController>();
    }

    protected abstract void TalkSet();

    protected abstract void TalkInEvent(int talkNum);

    protected abstract void TalkEndEvent();


    public void StartTalk()
    {
        TalkSet();
        if (!_eventController.IsEventNow)
        {
            //もし会話範囲内に、２人居た場合に二つとも呼ばれるのを防ぐために会話のパネルがactiveかどうか確認
            if (_talkManager.TalkPanel.activeSelf == false)
            {
                //現在をイベント中とする
                _eventController.ChangeEventSituationTrue();

                //会話のパネルを表示。
                _talkManager.TalkPanel.SetActive(true);

                //アシスト表記を消す
                _talkManager.CloseAssistUI();

                _isTalkNow = true;
                StartCoroutine(Talk());
            }
        }
    }

    public void EventEnd()
    {
        _isEvent = false;
    }

    IEnumerator Talk()
    {
        //ミッションの受付状態によって、会話の内容を変える

        _talkManager.NameText.text = _humanName;

        //話しているテキストを更新。
        for (int i = 0; i < _nowTalkText.Count; i++)
        {
            //テキストを更新
            _talkManager.TalkText.text = _nowTalkText[i];
            //音を流す
            _talkManager.TalkToNext();

            //会話中のイベントがあったら呼ぶ
            TalkInEvent(i);

            yield return new WaitUntil(() => !_isEvent);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        //会話のパネルを閉じる
        EndTalk();
    }

    public void EndTalk()
    {
        //会話のパネルを非表示にする
        _talkManager.TalkPanel.SetActive(false);
        _talkedNum++;
        TalkEndEvent();

        _isTalkNow = false;

        //アシスト表記を出す
        _talkManager.OpenAssistUI();

        //Event状態を終える
        _eventController.ChangeEventSituationFalse();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _talkManager.AsistTalkPanel.activeSelf == false)
        {
            _talkManager.OpenAssistUI();
            _talkManager.TalkBase = this;
            _isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _talkManager.CloseAssistUI();
            _isEnter = false;
        }
    }


}
