using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [Header("全体の音量")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("MasterのGroupに設定したVolumeパラメータの名前")]
    [SerializeField] private string _masterVolumeName;

    [Header("設定の効果音のGroupに設定したVolumeパラメータの名前")]
    [SerializeField] private string _systemEffectVolumeName;

    [Header("BGMのGroupに設定したVolumeパラメータの名前")]
    [SerializeField] private string _bgmVolumeName;

    [Header("ゲーム効果音のGroupに設定したVolumeパラメータの名前")]
    [SerializeField] private string _gameEffectVolumeName;



    [Header("全体の音量のSlider")]
    [SerializeField] private Slider _masterAudioSlider;

    [Header("ゲームの効果音のSlider")]
    [SerializeField] private Slider _gameEffectSlider;

    [Header("システムの効果音のSlider")]
    [SerializeField] private Slider _systemEffectSlider;

    [Header("BGMのSlider")]
    [SerializeField] private Slider _bgmSlider;

    [Header("スライダーの分割数")]
    [SerializeField] private float _maxVolume = 20;

    [Header("ボリューム初期値")]
    [SerializeField] private float _firstVolume = 0;

    [Header("設定画面")]
    [SerializeField] private GameObject _settingPanel;

    [Header("警告パネル")]
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
        //設定を変更していたら、セーブデータから読み込んで変更
        else if (_audioSettingSaveManager.SaveData._isCustumSetting)
        {
            SetMasterVolume(_audioSettingSaveManager.SaveData._masterVolume);
            SetBGMVolume(_audioSettingSaveManager.SaveData._bgmVolume);
            SetGameEffectVolume(_audioSettingSaveManager.SaveData._gameEffectVolume);
            SetSystemEffectVolume(_audioSettingSaveManager.SaveData._systemEffectVolume);
            Debug.Log($"音量設定を読み込み。全体:{_nowMasterVolume}/BGM:{_nowBGMVolume}/System:{_nowSystemVolume}/ゲーム:{_nowGameEffectVolume}");
        }
        //設定をしていなかったら、初期値で変更
    }
    private void Awake()
    {


    }


    /// <summary>スライダーで設定した内容を適用する関数
    /// 設定画面で、適用ボタンを押して実行</summary>
    public void SetSetting()
    {
        SetMasterVolume(_masterAudioSlider.value);
        SetBGMVolume(_bgmSlider.value);
        SetGameEffectVolume(_gameEffectSlider.value);
        SetSystemEffectVolume(_systemEffectSlider.value);
        _audioSettingSaveManager.Save(_nowMasterVolume, _nowBGMVolume, _nowSystemVolume, _nowSystemVolume, true);
    }


    /// <summary>設定画面で変更した内容を適用したかどうかを確認する</summary>
    public void CheckDoSetting()
    {
        if (_masterAudioSlider.value != _nowMasterVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("マスター音量の変更を適用していません");
        }
        else if (_bgmSlider.value != _nowBGMVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("BGM音量の変更を適用していません");
        }
        else if (_systemEffectSlider.value != _nowSystemVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("System音量の変更を適用していません");
        }
        else if (_gameEffectSlider.value != _nowGameEffectVolume)
        {
            _warningPanel.SetActive(true);
            _isChangeSetting = true;
            Debug.Log("gameEF音量の変更を適用していません");
        }
        else
        {
            _settingPanel.SetActive(false);
            _isChangeSetting = false;
        }
    }

    /// <summary>設定で、適用しなかった際にスライダーの値を元に戻す</summary>
    public void NoSetSetting()
    {
        SetMasterVolume(_nowMasterVolume);
        SetBGMVolume(_nowBGMVolume);
        SetGameEffectVolume(_nowGameEffectVolume);
        SetSystemEffectVolume(_nowSystemVolume);
        Debug.Log($"音量設定の変更内容を適用しなかった。全体:{_nowMasterVolume}/BGM:{_nowBGMVolume}/System:{_nowSystemVolume}/ゲーム:{_nowGameEffectVolume}");
    }

    /// <summary>音量設定を初期化する関数</summary>
    public void ReSetSetting(bool isFirstLode)
    {

        SetMasterVolume(_firstVolume);
        SetBGMVolume(_firstVolume);
        SetGameEffectVolume(_firstVolume);
        SetSystemEffectVolume(_firstVolume);
        Debug.Log($"音量設定を初期化。全体:{_nowMasterVolume}/BGM:{_nowBGMVolume}/System:{_nowSystemVolume}/ゲーム:{_nowGameEffectVolume}");

        if (!isFirstLode)
            _audioSettingSaveManager.Save(_nowMasterVolume, _nowBGMVolume, _nowSystemVolume, _nowSystemVolume, false);
    }


    /// <summary>全体の音量の設定</summary>
    /// <param name="value">設定する音量</param>
    public void SetMasterVolume(float value)
    {
        //現在の設定している値を更新
        _nowMasterVolume = value;

        //セーブしたデータから読み込んだ際には、設定画面のスライダーの値も変えておく
        if (value != _masterAudioSlider.value) _masterAudioSlider.value = value;

        //5段階補正
        value /= 5;

        //相対量をdBに変換する式。Mathf.Clampで「-80~0」の間に収まるようにしている
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixerのGroupのVolumeの値を変更
        _audioMixer.SetFloat(_masterVolumeName, volume);
    }

    /// <summary>BGMの音量の設定</summary>
    /// <param name="value">設定する音量</param>
    public void SetBGMVolume(float value)
    {
        //現在の設定している値を更新
        _nowBGMVolume = value;

        //セーブしたデータから読み込んだ際には、設定画面のスライダーの値も変えておく
        if (value != _bgmSlider.value) _bgmSlider.value = value;

        //5段階補正
        value /= 5;

        //相対量をdBに変換する式。Mathf.Clampで「-80~0」の間に収まるようにしている
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixerのGroupのVolumeの値を変更
        _audioMixer.SetFloat(_bgmVolumeName, volume);
    }

    /// <summary>システム音の音量の設定</summary>
    /// <param name="value">設定する音量</param>
    public void SetSystemEffectVolume(float value)
    {
        //現在の設定している値を更新
        _nowSystemVolume = value;

        //セーブしたデータから読み込んだ際には、設定画面のスライダーの値も変えておく
        if (value != _systemEffectSlider.value) _systemEffectSlider.value = value;

        //5段階補正
        value /= 5;

        //相対量をdBに変換する式。Mathf.Clampで「-80~0」の間に収まるようにしている
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixerのGroupのVolumeの値を変更
        _audioMixer.SetFloat(_systemEffectVolumeName, volume);
    }

    /// <summary>ゲーム効果音の音量の設定</summary>
    /// <param name="value">設定する音量</param>
    public void SetGameEffectVolume(float value)
    {
        //現在の設定している値を更新
        _nowGameEffectVolume = value;

        //セーブしたデータから読み込んだ際には、設定画面のスライダーの値も変えておく
        if (value != _gameEffectSlider.value) _gameEffectSlider.value = value;

        //5段階補正
        value /= 5;

        //相対量をdBに変換する式。Mathf.Clampで「-80~0」の間に収まるようにしている
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);

        //AudioMixerのGroupのVolumeの値を変更
        _audioMixer.SetFloat(_gameEffectVolumeName, volume);

    }


    /// <summary>スライダーの最大、最小値を設定する</summary>
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
