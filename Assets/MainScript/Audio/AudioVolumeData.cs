using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AudioVolumeData
{
    [Tooltip("�S�̂̉���")]
    public float _masterVolume;

    [Tooltip("BGM�̉���")]
    public float _bgmVolume;

    [Tooltip("�Q�[�����̉���")]
    public float _gameEffectVolume;

    [Tooltip("�V�X�e���̉���")]
    public float _systemEffectVolume;

    [Tooltip("���ʐݒ�����Ă��邩�ǂ���")]
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
