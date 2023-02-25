using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameSettingData
{
    public float _mouseSensitivity;

    public bool _runSettingIsPushChange;

    public int _showSwingUI;

    public bool _isSaveData = false;

    public bool _isShowAsistUI;

    public int _isOperationLevel;

    public GameSettingData(float mouseSensitivity, bool runSetting, int showPanel, bool isSave, bool isAsistUI,int isOperationLevel)
    {
        _mouseSensitivity = mouseSensitivity;
        _runSettingIsPushChange = runSetting;
        _showSwingUI = showPanel;
        _isSaveData = isSave;
        _isShowAsistUI = isAsistUI;
        _isOperationLevel = isOperationLevel;
    }

}
