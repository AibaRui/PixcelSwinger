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

    [Header("�J�n���ɘb�����t")]
    [SerializeField] protected List<string> _firstContactText = new List<string>();

    [Header("�ēx�A�b�����Ƃ��̌��t")]
    [SerializeField] protected List<string> _receivedMissionText = new List<string>();

    [Header("�I��������̌��t")]
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
        //��t�͈͂ɓ����Ă��邩�ǂ���
        if (_isEnter && !_isTalkNow)
        {
            //�X�y�[�X������
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
        Debug.Log($"{_missionDetail} : {_missionNumber}_{_missionDetailNumber}�ڂ̃^�X�N");
    }


    /// <summary>��b���Ɏ��s����C�x���g������ꍇ�Ɏ��s</summary>
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
        //Event�����s
        
    }

    /// <summary>��b���I���������ɂ���C�x���g</summary>
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