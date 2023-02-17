using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MissionSaveData
{
    public int _missionNun;

    public int _detailNum;

    public bool _isClear;


    public MissionSaveData(int missionNum,int detailNum,bool isClear)
    {
        this._missionNun = missionNum;
        this._detailNum = detailNum;
        this._isClear = isClear;
    }

}
