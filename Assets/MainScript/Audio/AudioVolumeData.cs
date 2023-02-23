using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AudioVolumeData
{
    [Tooltip("全体の音量")]
    public float _masterVolume;

    [Tooltip("BGMの音量")]
    public float _bgmVolume;

    [Tooltip("ゲーム音の音量")]
    public float _gameEffectVolume;

    [Tooltip("システムの音量")]
    public float _systemEffectVolume;

    [Tooltip("音量設定をしているかどうか")]
    public bool _isCustumSetting;


    public AudioVolumeData(float master, float bgm, float game, float system,bool isChange)
    {
        _masterVolume = master;
        _bgmVolume = bgm;
        _gameEffectVolume = game;
        _systemEffectVolume = system;
        _isCustumSetting = isChange;
    }
}
