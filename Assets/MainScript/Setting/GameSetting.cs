using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameSetting : MonoBehaviour
{
    [Header("ゲーム設定画面のパネル")]
    [SerializeField] private GameObject _settingPanel;

    [Header("変更した値を適用しなかった際に出す注意喚起のパネル")]
    [SerializeField] private GameObject _warningPanel;


    [SerializeField] private GameSettingSaveManager _gameSettingSaveManager;
    [SerializeField] private MouseSensitivity _mouseSensitivitySetting;
    [SerializeField] private PlayerGrappleAndSwingSetting _playerGrappleAndSwingSetting;
    [SerializeField] private PlayerMoveing _playerMoveing;
    [SerializeField] private UIShowSystem _uIShowSystem;
    [SerializeField] private PlayerVelocityLimitControl _playerVelocityLimitControl;

    //tmp～。設定画面で変更した結果の値を保存しておく用の値

    /// <summary>Run設定のtmp</summary>
    private bool _tmpRunSettingIsPushChange;
    /// <summary>SwingUI設定のtmp</summary>
    private int _tmpSwingHitUI;
    /// <summary>操作レベルの設定のtmp</summary>
    private int _tmpOperationLevel;
    /// <summary>アシストUI設定のtmp</summary>
    private bool _tmpIsShowAsistUI;

    private void Awake()
    {
        _gameSettingSaveManager.FirstLode();

        if (_gameSettingSaveManager.SaveData == null)
        {
            ReSetSetting(true);
            Debug.Log("ゲーム設定:Null");
        }
        //設定をしていなかったら、初期値で変更
        else if (!_gameSettingSaveManager.SaveData._isSaveData)
        {
            ReSetSetting(true);
            Debug.Log("ゲーム設定:でータなし");
        }
        //設定を変更していたら、セーブデータから読み込んで変更
        else if (_gameSettingSaveManager.SaveData._isSaveData)
        {
            LodeSetting();
            Debug.Log("ゲーム設定:ロード");
        }
    }

    /// <summary>保存したデータを、各設定に送る</summary>
    public void LodeSetting()
    {
        //マウス感度
        _mouseSensitivitySetting.ChangeSensitivity(_gameSettingSaveManager.SaveData._mouseSensitivity);

        //アシストUI
        _uIShowSystem.IsShowUI = _gameSettingSaveManager.SaveData._isShowAsistUI;

        //Run設定
        _playerMoveing.IsPushChange = _gameSettingSaveManager.SaveData._runSettingIsPushChange;
        _tmpRunSettingIsPushChange = _gameSettingSaveManager.SaveData._runSettingIsPushChange;

        //SwingのUI設定
        _playerGrappleAndSwingSetting.SwingHitUISetting = (PlayerGrappleAndSwingSetting.SwingHitUI)Enum.ToObject(typeof(PlayerGrappleAndSwingSetting.SwingHitUI), _gameSettingSaveManager.SaveData._showSwingUI);
        _tmpSwingHitUI = _gameSettingSaveManager.SaveData._showSwingUI;

        //操作レベルを設定、tmp用の値を現在の値に更新
        _playerVelocityLimitControl.OperationLevel = (PlayerVelocityLimitControl.OperationsLevel)Enum.ToObject(typeof(PlayerVelocityLimitControl.OperationsLevel), _gameSettingSaveManager.SaveData._isOperationLevel);
        _tmpOperationLevel = _gameSettingSaveManager.SaveData._isOperationLevel;
    }

    /// <summary>設定画面で変更した内容を適用する</summary>
    /// tmpの値で、各設定の値を更新する
    public void SetGameSetting()
    {
        //マウス感度
        _mouseSensitivitySetting.ChangeSensitivity(_mouseSensitivitySetting.MouseSensivitySlider.value);

        //アシストUI
        _uIShowSystem.IsShowUI = _tmpIsShowAsistUI;

        //Run設定
        _playerMoveing.ChangeRunWay(_tmpRunSettingIsPushChange);

        //SwingのUI設定
        //enumをint型に変換して保存
        _playerGrappleAndSwingSetting.SwingHitUISetting = (PlayerGrappleAndSwingSetting.SwingHitUI)Enum.ToObject(typeof(PlayerGrappleAndSwingSetting.SwingHitUI), _tmpSwingHitUI);
        var numberOfSwingHItUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;

        //操作レベル
        //tmpをint型からenum型に変換して変更。操作レベルを更新して、再びintが型に変換し保存
        _playerVelocityLimitControl.OperationLevel = (PlayerVelocityLimitControl.OperationsLevel)Enum.ToObject(typeof(PlayerVelocityLimitControl.OperationsLevel), _tmpOperationLevel);
        var numberOfOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;


        //セーブする
        _gameSettingSaveManager.Save(_mouseSensitivitySetting.NowSensivity, _playerMoveing.IsPushChange, numberOfSwingHItUI, true, _uIShowSystem.IsShowUI, numberOfOperationLevel);
    }

    /// <summary>
    /// 設定の変更が適用されているかどうか確認する関数
    /// tmpの値と、各設定の現在の値を比べて、違いがあるかを確認する
    /// </summary>
    public void CheckChange()
    {
        //Swingのenumをintとして変換
        int nowSwingHitUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;
        //操作レベルのenumをintとして変換
        int nowOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;

        if (_mouseSensitivitySetting.MouseSensivitySlider.value != _mouseSensitivitySetting.NowSensivity || _playerMoveing.IsPushChange != _tmpRunSettingIsPushChange ||
            nowSwingHitUI != _tmpSwingHitUI || _uIShowSystem.IsShowUI != _tmpIsShowAsistUI || nowOperationLevel != _tmpOperationLevel)
        {
            _warningPanel.SetActive(true);
        }
        else
        {
            _settingPanel.SetActive(false);
        }
    }

    /// <summary>設定の変更内容を適用しなかった時に呼ぶ関数</summary>
    /// tmpの値を変更前に戻す。
    public void NoSetSetting()
    {
        //マウス感度
        _mouseSensitivitySetting.ChangeSensitivity(_mouseSensitivitySetting.NowSensivity);

        //アシストUI
        _tmpIsShowAsistUI = _uIShowSystem.IsShowUI;

        //Run設定
        _tmpRunSettingIsPushChange = _playerMoveing.IsPushChange;

        //SwingのUI表示
        _tmpSwingHitUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;

        //操作レベル
        _tmpOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;
    }


    /// <summary>設定を初期値に戻す関数</summary>
    /// 各クラスに設定してある、初期値を元に値を初期化する
    public void ReSetSetting(bool isFirstSet)
    {
        //マウス感度の初期化
        _mouseSensitivitySetting.ChangeSensitivity(_mouseSensitivitySetting.FirstMouseSensivity);

        //アシストUI
        _uIShowSystem.IsShowUI = _uIShowSystem.FirstIsShowUI;
        _tmpIsShowAsistUI = _uIShowSystem.IsShowUI;

        //Run設定
        _playerMoveing.ChangeRunWay(_playerMoveing.IsFirstPushChange);
        _tmpRunSettingIsPushChange = _playerMoveing.IsPushChange;

        //SwingのUI表示の設定
        _playerGrappleAndSwingSetting.SwingHitUISetting = _playerGrappleAndSwingSetting.FirstSwingHitUISetting;
        _tmpSwingHitUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;

        //操作レベル
        _playerVelocityLimitControl.OperationLevel = _playerVelocityLimitControl.FirstOperationLevel;
        _tmpOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;

        //初回のセットで無かったらセーブする
        if (!isFirstSet)
        {
            _gameSettingSaveManager.Save(_mouseSensitivitySetting.NowSensivity, _playerMoveing.IsPushChange, _tmpSwingHitUI, false, _uIShowSystem.IsShowUI, _tmpOperationLevel);
        }
    }




    /// <summary>ボタンから呼ぶ。走りの方法を変える</summary>
    /// <param name="isPush">trueで切り替え/falseで押している間</param>
    public void ChangeRunSetting(bool isPush)
    {
        _tmpRunSettingIsPushChange = isPush;
    }

    /// <summary>ボタンから呼ぶ。Swing時のUIの表示の仕方を変える</summary>
    /// <param name="num">0で非表示/1で最初だけ/2でずっと</param>
    public void ChangeSwingUI(int num)
    {
        _tmpSwingHitUI = num;
    }


    /// <summary>ボタンから呼ぶ。Swing時のUIの表示の仕方を変える</summary>
    /// <param name="isAsist">trueで表示する/falseで表示しない</param>
    public void ChangeIsAsistUI(bool isAsist)
    {
        _tmpIsShowAsistUI = isAsist;
    }

    /// <summary>ボタンから呼ぶ。操作レベルを変える</summary>
    /// <param name="level">操作レベル/0:初心者/1:普通/2:上級</param>
    public void ChangeOperationLevel(int level)
    {
        _tmpOperationLevel = level;
    }

}
