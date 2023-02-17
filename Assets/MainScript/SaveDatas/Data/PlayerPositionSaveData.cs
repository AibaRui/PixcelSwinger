using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerPositionSaveData
{
    public Vector3 _savePos;

    public PlayerPositionSaveData(Vector3 pos)
    {
        this._savePos = pos;
    }

}
