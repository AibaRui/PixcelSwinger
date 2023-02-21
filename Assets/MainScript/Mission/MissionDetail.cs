using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionDetail : MonoBehaviour
{
    Mission _mission;

    [Header("�~�b�V�����̊Ȍ��ȓ��e")]
    [SerializeField] private string _missionDetail;

    [Header("�C���x���g���ɕ\�L����~�b�V�������e")]
    [SerializeField,TextArea(20,5)] private string _missionLongDetail;
    


    [Header("�ŏ��ɕK�v�Ȋ֐����Ă�")]
    [SerializeField]
    private UnityEvent _missionSettingEvent;

    [Header("�ŏ��ɕK�v�Ȃ��̂��o��")]
    [SerializeField]
    private�@List<GameObject> _missionSettingObject = new List<GameObject>();

    [Header("�N���A��ɏ�������")]
    [SerializeField]
    private List<GameObject> _endTaskActiveFalseObject = new List<GameObject>();

    [Header("��V�Ƃ��čs������")]
    [SerializeField]
    private UnityEvent _rewardEvent;

    [Header("�ڍהԍ�")]
    [SerializeField] private int _missionDetailNum;


    [Header("��b�̃^�X�N�̎��̂ݎQ�Ƃ�����")]
    [SerializeField]
    TalkBase _talkBase;

    /// <summary>�o�^�����</summary>
    /// <param name="mission"></param>
    public void Init(Mission mission)
    {
        _mission = mission;

        Debug.Log($"�^�X�N�J�n:{_mission.MissionNum}-{_missionDetailNum}");

        _missionSettingEvent?.Invoke();
        _missionSettingObject.ForEach(i => i?.SetActive(true));
        _mission.MissionManager.SettingMissionText(_missionDetail, _missionLongDetail);
    }

    /// <summary>�N���A�������Ƃ�ʒm�����</summary>
    public void ClearMissionTask()
    {
        _rewardEvent?.Invoke();
        _endTaskActiveFalseObject.ForEach(i => i?.SetActive(false));
        _mission.GoNextMission();
    }

    public void CheckReward()
    {
        _rewardEvent?.Invoke();

        _missionSettingObject.ForEach(i => i?.SetActive(true));
        _endTaskActiveFalseObject.ForEach(i => i?.SetActive(false));

        if(_talkBase!=null)    _talkBase.TalkNum = 1;
    }

}


