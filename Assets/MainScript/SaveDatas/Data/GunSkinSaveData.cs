using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GunSkinSaveData
{
    /// <summary>�e�̃X�L����ID��,��؂��string�Ƃ��Ď����Ă���</summary>
    public string _getGunSkinNameSaveData;


    public GunSkinSaveData(string nameData)
    {
        this._getGunSkinNameSaveData = nameData;
    }
}
