using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MissionBase
{
    [Header("èoåªÇ≥ÇπÇÈï®")]
    [SerializeField] private GameObject _spawnBlock;

    private bool _isEnter = false;


    public override void EnterMission()
    {
        _enterMissionCount++;
        MissionDetail();

    }

    public override void FirstSetting()
    {
        if (_missionEnterZones.Count != 0)
        {
            _missionEnterZones[0].Init(this);
        }
    }

    public override void MissionClear()
    {
        Debug.Log("Clear");
        _spawnBlock.SetActive(true);
    }

    public override void MissionDetail()
    {

        if (_enterMissionCount == 1)
        {
            _isMissionCompleted = true;
        }
    }



}
