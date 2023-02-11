using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MissionBase
{

    private bool _isEnter = false;


    public override void EnterMission()
    {
        _enterMissionCount++;
    }

    public override void FirstSetting()
    {

    }

    public override void MissionClear()
    {
        Debug.Log("Clear");
    }



}
