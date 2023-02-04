using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBase : MonoBehaviour
{

    [SerializeField] protected List<MissionEnterZone> _missionEnterZones = new List<MissionEnterZone>();

    [SerializeField] protected List<string> _acceptMissionText = new List<string>();

    [SerializeField] protected List<string> _receivedMissionText = new List<string>();

    [SerializeField] protected List<string> _endMissionText = new List<string>();


    protected MissionManager _missionManager = null;

    protected bool _isMissionCompleted = false;

    public bool IsMissionCompleted { get => _isMissionCompleted; }


    protected int _enterMissionCount = 0;


    /// <summary>MissionManager���Z�b�g����֐�</summary>
    /// <param name="missionManager"></param>
    public void Init(MissionManager missionManager)
    {
        _missionManager = missionManager;
        _missionManager.CheckMission.AcceptMissionText = _acceptMissionText;
        _missionManager.CheckMission.ReceivedText = _receivedMissionText;
        _missionManager.CheckMission.EndText = _endMissionText;

        FirstSetting();
    }

    public abstract void FirstSetting();


    /// <summary>�N�G�X�g�̐�������</summary>
    public abstract void MissionDetail();


    /// <summary>�N�G�X�g�����������Ɏ��s</summary>
    public abstract void MissionClear();



    public abstract void EnterMission();

}

