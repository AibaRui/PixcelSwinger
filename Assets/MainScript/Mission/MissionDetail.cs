using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionDetail : MonoBehaviour
{
    Mission _mission;

    [Header("ミッションの簡潔な内容")]
    [SerializeField] private string _missionDetail;

    [Header("インベントリに表記するミッション内容")]
    [SerializeField,TextArea(20,5)] private string _missionLongDetail;
    


    [Header("最初に必要な関数を呼ぶ")]
    [SerializeField]
    private UnityEvent _missionSettingEvent;

    [Header("最初に必要なものを出す")]
    [SerializeField]
    private　List<GameObject> _missionSettingObject = new List<GameObject>();

    [Header("クリア後に消すもの")]
    [SerializeField]
    private List<GameObject> _endTaskActiveFalseObject = new List<GameObject>();

    [Header("報酬として行うもの")]
    [SerializeField]
    private UnityEvent _rewardEvent;

    [Header("詳細番号")]
    [SerializeField] private int _missionDetailNum;


    [Header("会話のタスクの時のみ参照させる")]
    [SerializeField]
    TalkBase _talkBase;

    /// <summary>登録される</summary>
    /// <param name="mission"></param>
    public void Init(Mission mission)
    {
        _mission = mission;

        Debug.Log($"タスク開始:{_mission.MissionNum}-{_missionDetailNum}");

        _missionSettingEvent?.Invoke();
        _missionSettingObject.ForEach(i => i?.SetActive(true));
        _mission.MissionManager.SettingMissionText(_missionDetail, _missionLongDetail);
    }

    /// <summary>クリアしたことを通知される</summary>
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


