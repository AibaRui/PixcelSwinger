using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemSaveData
{
   public string _getItemNameSaveData = "";

   public ItemSaveData(string nameData)
    {
        this._getItemNameSaveData = nameData;
    }
}
