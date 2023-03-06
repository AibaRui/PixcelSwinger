using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [Header("�S�̂̉���")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Master��Group�ɐݒ肵��Volume�p�����[�^�̖��O")]
    [SerializeField] private string _masterVolumeName;

    [Header("�ݒ�̌��ʉ���Group�ɐݒ肵��Volume�p�����[�^�̖��O")]
    [SerializeField] private string _systemEffectVolumeName;

    [Header("BGM��Group�ɐݒ肵��Volume�p�����[�^�̖��O")]
    [SerializeField] private string _bgmVolumeName;

    [Header("�Q�[�����ʉ���Group�ɐݒ肵��Volume�p�����[�^�̖��O")]
    [SerializeField] private string _gameEffectVolumeName;



    [Header("�S�̂̉��ʂ�Slider")]
    [SerializeField] private Slider _masterAudioSlider;

    [Header("�Q�[���̌��ʉ���Slider")]
    [SerializeField] private Slider _gameEffectSlider;

    [Header("�V�X�e���̌��ʉ���Slider")]
    [SerializeField] private Slider _systemEffectSlider;

    [Header("BGM��Slider")]
    [SerializeField] private Slider _bgmSlider;

    [Header("�X���C�_�[�̕�����")]
    [SerializeField] private float _maxVolume = 20;

    [Header("�{�����[�������l")]
    [SerializeField] private float _firstVolume = 0;

    [Header("�ݒ���")]
    [SerializeField] private GameObject _settingPanel;

    [Header("�x���p�l��")]
    [SerializeField] private GameObject _warningPanel;



    [SerializeField] AudioSettingSaveManager _audioSettingSaveManager;

    private float _nowMasterVolume;

    private float _nowBGMVolume;

    private float _nowSystemVolume;

    private float _nowGameEffectVolume;

    private bool _isChangeSetting = false;

    public bool IsChange => _isChangeSetting;

    private void Start()
    {

        FirstSliderSetting();

        if (_audioSettingSaveManager.SaveData == null)
        {
            ReSetSetting(true);
            Debug.Log("NULL");
        }
        else if (!_audioSettingSaveManager.SaveData._isCustumSetting)
        {
            Debug.Log("A" + _audioSettingSaveManager.SaveData._isCustumSetting);
            ReSetSetting(true);
        }
        //�ݒ��ύX���Ă�����A�Z�[�u�f�[�^����ǂݍ���ŕύX
        else if (_audioSettingSaveManager.SaveData._isCustumSetting)
        {
            SetMasterVolume(_audioSettingSaveManager.SaveData._masterVolume);
            SetBGMVolume(_audioSettingSaveManager.SaveData._bgmVolume);
            SetGameEffectVolume(_audioSettingSaveManager.SaveData._gameEffectVolume);
            SetSystemEffectVolume(_audioSettingSaveManager.SaveData._systemEffectVolume);
            Debug.Log($"���ʐݒ��ǂݍ��݁B�S��:{_nowMasterVolume}/BGM:{_nowBGMVolume}/System:{_nowSystemVolume}/�Q�[��:{_nowGameEffectVolume}");
        }
        //�ݒ�����Ă��Ȃ�������A�����l�ŕύX
    }
    private void Awake()
    {


    }


    /// <summary>�X���C�_�[�Őݒ肵�����e��K�p����֐�
    /// �ݒ��ʂŁA�K�p�{�^���������Ď��s</summary>
    public void SetSetting()
    {
        SetMasterVolume(_masterAudioSlider.value);
        SetBGMVolume(_bgmSlider.value);
        SetGameEffectVolume(_gameEffectSlider.value);
        SetSystemEffectVolume(_systemEffectSlider.value);
        _audioSettingSaveManager.Save(_nowMasterVolume, _nowBGMVolume, _nowSystemVolume, _nowSystemVolume, true);
    }


    /// <summary>�ݒ��ʂŕύX�������e��K�p�������ǂ������m�F����</summary>
    public void CheckDoSetting()
    {
        if (_masterAudioSlider.value != _nowMasterVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("�}�X�^�[���ʂ̕ύX��K�p���Ă��܂���");
        }
        else if (_bgmSlider.value != _nowBGMVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("BGM���ʂ̕ύX��K�p���Ă��܂���");
        }
        else if (_systemEffectSlider.value != _nowSystemVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("System���ʂ̕ύX��K�p���Ă��܂���");
        }
        else if (_gameEffectSlider.value != _nowGameEffectVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("gameEF���ʂ̕ύX��K�p���Ă��܂���");
        }
        else
        {
            _settingPanel.SetActive(false);
            _isChangeSetting = false;
        }
    }

    /// <summary>�ݒ�ŁA�K�p���Ȃ������ۂɃX���C�_�[�̒l�����ɖ߂�</summary>
    public void NoSetSetting()
    {
        SetMasterVolume(_nowMasterVolume);
        SetBGMVolume(_nowBGMVolume);
        SetGameEffectVolume(_nowGameEffectVolume);
        SetSystemEffectVolume(_nowSystemVolume);
        Debug.Log($"���ʐݒ�̕ύX���e��K�p���Ȃ������B�S��:{_nowMasterVolume}/BGM:{_nowBGMVolume}/System:{_nowSystemVolume}/�Q�[��:{_nowGameEffectVolume}");
    }

    /// <summary>���ʐݒ������������֐�</summary>
    public void ReSetSetting(bool isFirstLode)
    {

        SetMasterVolume(_firstVolume);
        SetBGMVolume(_firstVolume);
        SetGameEffectVolume(_firstVolume);
        SetSystemEffectVolume(_firstVolume);
        Debug.Log($"���ʐݒ���������B�S��:{_nowMasterVolume}/BGM:{_nowBGMVolume}/System:{_nowSystemVolume}/�Q�[��:{_nowGameEffectVolume}");

        if (!isFirstLode)
            _audioSettingSaveManager.Save(_nowMasterVolume, _nowBGMVolume, _nowSystemVolume, _nowSystemVolume, false);
    }


    /// <summary>�S�̂̉��ʂ̐ݒ�</summary>
    /// <param name="value">�ݒ肷�鉹��</param>
    public void SetMasterVolume(float value)
    {
        //���݂̐ݒ肵�Ă���l���X�V
        _nowMasterVolume = value;

        //�Z�[�u�����f�[�^����ǂݍ��񂾍ۂɂ́A�ݒ��ʂ̃X���C�_�[�̒l���ς��Ă���
        if (value != _masterAudioSlider.value) _masterAudioSlider.value = value;

        //5�i�K�␳
        value /= 5;

        //���Ηʂ�dB�ɕϊ����鎮�BMathf.Clamp�Łu-80~0�v�̊ԂɎ��܂�悤�ɂ��Ă���
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixer��Group��Volume�̒l��ύX
        _audioMixer.SetFloat(_masterVolumeName, volume);
    }

    /// <summary>BGM�̉��ʂ̐ݒ�</summary>
    /// <param name="value">�ݒ肷�鉹��</param>
    public void SetBGMVolume(float value)
    {
        //���݂̐ݒ肵�Ă���l���X�V
        _nowBGMVolume = value;

        //�Z�[�u�����f�[�^����ǂݍ��񂾍ۂɂ́A�ݒ��ʂ̃X���C�_�[�̒l���ς��Ă���
        if (value != _bgmSlider.value) _bgmSlider.value = value;

        //5�i�K�␳
        value /= 5;

        //���Ηʂ�dB�ɕϊ����鎮�BMathf.Clamp�Łu-80~0�v�̊ԂɎ��܂�悤�ɂ��Ă���
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixer��Group��Volume�̒l��ύX
        _audioMixer.SetFloat(_bgmVolumeName, volume);
    }

    /// <summary>�V�X�e�����̉��ʂ̐ݒ�</summary>
    /// <param name="value">�ݒ肷�鉹��</param>
    public void SetSystemEffectVolume(float value)
    {
        //���݂̐ݒ肵�Ă���l���X�V
        _nowSystemVolume = value;

        //�Z�[�u�����f�[�^����ǂݍ��񂾍ۂɂ́A�ݒ��ʂ̃X���C�_�[�̒l���ς��Ă���
        if (value != _systemEffectSlider.value) _systemEffectSlider.value = value;

        //5�i�K�␳
        value /= 5;

        //���Ηʂ�dB�ɕϊ����鎮�BMathf.Clamp�Łu-80~0�v�̊ԂɎ��܂�悤�ɂ��Ă���
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixer��Group��Volume�̒l��ύX
        _audioMixer.SetFloat(_systemEffectVolumeName, volume);
    }

    /// <summary>�Q�[�����ʉ��̉��ʂ̐ݒ�</summary>
    /// <param name="value">�ݒ肷�鉹��</param>
    public void SetGameEffectVolume(float value)
    {
        //���݂̐ݒ肵�Ă���l���X�V
        _nowGameEffectVolume = value;

        //�Z�[�u�����f�[�^����ǂݍ��񂾍ۂɂ́A�ݒ��ʂ̃X���C�_�[�̒l���ς��Ă���
        if (value != _gameEffectSlider.value) _gameEffectSlider.value = value;

        //5�i�K�␳
        value /= 5;

        //���Ηʂ�dB�ɕϊ����鎮�BMathf.Clamp�Łu-80~0�v�̊ԂɎ��܂�悤�ɂ��Ă���
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixer��Group��Volume�̒l��ύX
        _audioMixer.SetFloat(_gameEffectVolumeName, volume);

    }


    /// <summary>�X���C�_�[�̍ő�A�ŏ��l��ݒ肷��</summary>
    public void FirstSliderSetting()
    {
        _masterAudioSlider.maxValue = _maxVolume;
        _masterAudioSlider.minValue = 0;

        _systemEffectSlider.maxValue = _maxVolume;
        _systemEffectSlider.minValue = 0;

        _gameEffectSlider.maxValue = _maxVolume;
        _gameEffectSlider.minValue = 0;

        _bgmSlider.maxValue = _maxVolume;
        _bgmSlider.minValue = 0;
    }

}
