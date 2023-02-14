using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>�ʏ�̉�b�̍ۂɎg���N���X</summary>
public class NomalTalk : TalkBase
{
    [Header("��ԍŏ��ɘb�����t")]
    [SerializeField] private List<string> _firstContactText = new List<string>();

    [Header("�����_���ɘb�����ǂ���")]
    [SerializeField] private bool _isRandamTalk = false;

    [Header("�����_���ɘb�����e")]
    [SerializeField] private List<RandomTalk> _randomTalk = new List<RandomTalk>();

    [Header("��ԍŏ��ɘb�����Ƃ��ɂ��邱��")]
    [SerializeField] private UnityEvent _event;

    /// <summary>�ŏ��̉�b�Ɍ���A�b���I�����Ƃ��ɓo�^���Ă���C�x���g�����s����</summary>
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

    /// <summary>��b�̓��e��o�^����</summary>
    protected override void TalkSet()
    {
        //�����_���ɉ�b����ꍇ
        if (_isRandamTalk)
        {
            //�ŏ��͌Œ�̘b
            if (_talkedNum == 0)
            {
                _nowTalkText = _firstContactText;
            }
            else //���̂��Ƃ̓����_���ɘb��
            {
                var r = Random.Range(0, _randomTalk.Count);
                _nowTalkText = _randomTalk[r].Talk;
            }
        }
        else�@//�����_����b���Ȃ��ꍇ�́A�Œ�̘b����������
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
