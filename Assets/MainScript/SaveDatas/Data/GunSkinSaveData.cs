using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GunSkinSaveData
{
    /// <summary>e‚ÌƒXƒLƒ“‚ÌID‚ğ,‹æØ‚è‚Åstring‚Æ‚µ‚Ä‚Á‚Ä‚¢‚é</summary>
    public string _getGunSkinNameSaveData;


    public GunSkinSaveData(string nameData)
    {
        this._getGunSkinNameSaveData = nameData;
    }
}
