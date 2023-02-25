using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameSetting : MonoBehaviour
{
    [Header("�Q�[���ݒ��ʂ̃p�l��")]
    [SerializeField] private GameObject _settingPanel;

    [Header("�ύX�����l��K�p���Ȃ������ۂɏo�����ӊ��N�̃p�l��")]
    [SerializeField] private GameObject _warningPanel;


    [SerializeField] private GameSettingSaveManager _gameSettingSaveManager;
    [SerializeField] private MouseSensitivity _mouseSensitivitySetting;
    [SerializeField] private PlayerGrappleAndSwingSetting _playerGrappleAndSwingSetting;
    [SerializeField] private PlayerMoveing _playerMoveing;
    [SerializeField] private UIShowSystem _uIShowSystem;
    [SerializeField] private PlayerVelocityLimitControl _playerVelocityLimitControl;

    //tmp�`�B�ݒ��ʂŕύX�������ʂ̒l��ۑ����Ă����p�̒l

    /// <summary>Run�ݒ��tmp</summary>
    private bool _tmpRunSettingIsPushChange;
    /// <summary>SwingUI�ݒ��tmp</summary>
    private int _tmpSwingHitUI;
    /// <summary>���샌�x���̐ݒ��tmp</summary>
    private int _tmpOperationLevel;
    /// <summary>�A�V�X�gUI�ݒ��tmp</summary>
    private bool _tmpIsShowAsistUI;

    private void Awake()
    {
        _gameSettingSaveManager.FirstLode();

        if (_gameSettingSaveManager.SaveData == null)
        {
            ReSetSetting(true);
            Debug.Log("�Q�[���ݒ�:Null");
        }
        //�ݒ�����Ă��Ȃ�������A�����l�ŕύX
        else if (!_gameSettingSaveManager.SaveData._isSaveData)
        {
            ReSetSetting(true);
            Debug.Log("�Q�[���ݒ�:�Ł[�^�Ȃ�");
        }
        //�ݒ��ύX���Ă�����A�Z�[�u�f�[�^����ǂݍ���ŕύX
        else if (_gameSettingSaveManager.SaveData._isSaveData)
        {
            LodeSetting();
            Debug.Log("�Q�[���ݒ�:���[�h");
        }
    }

    /// <summary>�ۑ������f�[�^���A�e�ݒ�ɑ���</summary>
    public void LodeSetting()
    {
        //�}�E�X���x
        _mouseSensitivitySetting.ChangeSensitivity(_gameSettingSaveManager.SaveData._mouseSensitivity);

        //�A�V�X�gUI
        _uIShowSystem.IsShowUI = _gameSettingSaveManager.SaveData._isShowAsistUI;

        //Run�ݒ�
        _playerMoveing.IsPushChange = _gameSettingSaveManager.SaveData._runSettingIsPushChange;
        _tmpRunSettingIsPushChange = _gameSettingSaveManager.SaveData._runSettingIsPushChange;

        //Swing��UI�ݒ�
        _playerGrappleAndSwingSetting.SwingHitUISetting = (PlayerGrappleAndSwingSetting.SwingHitUI)Enum.ToObject(typeof(PlayerGrappleAndSwingSetting.SwingHitUI), _gameSettingSaveManager.SaveData._showSwingUI);
        _tmpSwingHitUI = _gameSettingSaveManager.SaveData._showSwingUI;

        //���샌�x����ݒ�Atmp�p�̒l�����݂̒l�ɍX�V
        _playerVelocityLimitControl.OperationLevel = (PlayerVelocityLimitControl.OperationsLevel)Enum.ToObject(typeof(PlayerVelocityLimitControl.OperationsLevel), _gameSettingSaveManager.SaveData._isOperationLevel);
        _tmpOperationLevel = _gameSettingSaveManager.SaveData._isOperationLevel;
    }

    /// <summary>�ݒ��ʂŕύX�������e��K�p����</summary>
    /// tmp�̒l�ŁA�e�ݒ�̒l���X�V����
    public void SetGameSetting()
    {
        //�}�E�X���x
        _mouseSensitivitySetting.ChangeSensitivity(_mouseSensitivitySetting.MouseSensivitySlider.value);

        //�A�V�X�gUI
        _uIShowSystem.IsShowUI = _tmpIsShowAsistUI;

        //Run�ݒ�
        _playerMoveing.ChangeRunWay(_tmpRunSettingIsPushChange);

        //Swing��UI�ݒ�
        //enum��int�^�ɕϊ����ĕۑ�
        _playerGrappleAndSwingSetting.SwingHitUISetting = (PlayerGrappleAndSwingSetting.SwingHitUI)Enum.ToObject(typeof(PlayerGrappleAndSwingSetting.SwingHitUI), _tmpSwingHitUI);
        var numberOfSwingHItUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;

        //���샌�x��
        //tmp��int�^����enum�^�ɕϊ����ĕύX�B���샌�x�����X�V���āA�Ă�int���^�ɕϊ����ۑ�
        _playerVelocityLimitControl.OperationLevel = (PlayerVelocityLimitControl.OperationsLevel)Enum.ToObject(typeof(PlayerVelocityLimitControl.OperationsLevel), _tmpOperationLevel);
        var numberOfOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;


        //�Z�[�u����
        _gameSettingSaveManager.Save(_mouseSensitivitySetting.NowSensivity, _playerMoveing.IsPushChange, numberOfSwingHItUI, true, _uIShowSystem.IsShowUI, numberOfOperationLevel);
    }

    /// <summary>
    /// �ݒ�̕ύX���K�p����Ă��邩�ǂ����m�F����֐�
    /// tmp�̒l�ƁA�e�ݒ�̌��݂̒l���ׂāA�Ⴂ�����邩���m�F����
    /// </summary>
    public void CheckChange()
    {
        //Swing��enum��int�Ƃ��ĕϊ�
        int nowSwingHitUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;
        //���샌�x����enum��int�Ƃ��ĕϊ�
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

    /// <summary>�ݒ�̕ύX���e��K�p���Ȃ��������ɌĂԊ֐�</summary>
    /// tmp�̒l��ύX�O�ɖ߂��B
    public void NoSetSetting()
    {
        //�}�E�X���x
        _mouseSensitivitySetting.ChangeSensitivity(_mouseSensitivitySetting.NowSensivity);

        //�A�V�X�gUI
        _tmpIsShowAsistUI = _uIShowSystem.IsShowUI;

        //Run�ݒ�
        _tmpRunSettingIsPushChange = _playerMoveing.IsPushChange;

        //Swing��UI�\��
        _tmpSwingHitUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;

        //���샌�x��
        _tmpOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;
    }


    /// <summary>�ݒ�������l�ɖ߂��֐�</summary>
    /// �e�N���X�ɐݒ肵�Ă���A�����l�����ɒl������������
    public void ReSetSetting(bool isFirstSet)
    {
        //�}�E�X���x�̏�����
        _mouseSensitivitySetting.ChangeSensitivity(_mouseSensitivitySetting.FirstMouseSensivity);

        //�A�V�X�gUI
        _uIShowSystem.IsShowUI = _uIShowSystem.FirstIsShowUI;
        _tmpIsShowAsistUI = _uIShowSystem.IsShowUI;

        //Run�ݒ�
        _playerMoveing.ChangeRunWay(_playerMoveing.IsFirstPushChange);
        _tmpRunSettingIsPushChange = _playerMoveing.IsPushChange;

        //Swing��UI�\���̐ݒ�
        _playerGrappleAndSwingSetting.SwingHitUISetting = _playerGrappleAndSwingSetting.FirstSwingHitUISetting;
        _tmpSwingHitUI = (int)_playerGrappleAndSwingSetting.SwingHitUISetting;

        //���샌�x��
        _playerVelocityLimitControl.OperationLevel = _playerVelocityLimitControl.FirstOperationLevel;
        _tmpOperationLevel = (int)_playerVelocityLimitControl.OperationLevel;

        //����̃Z�b�g�Ŗ���������Z�[�u����
        if (!isFirstSet)
        {
            _gameSettingSaveManager.Save(_mouseSensitivitySetting.NowSensivity, _playerMoveing.IsPushChange, _tmpSwingHitUI, false, _uIShowSystem.IsShowUI, _tmpOperationLevel);
        }
    }




    /// <summary>�{�^������ĂԁB����̕��@��ς���</summary>
    /// <param name="isPush">true�Ő؂�ւ�/false�ŉ����Ă����</param>
    public void ChangeRunSetting(bool isPush)
    {
        _tmpRunSettingIsPushChange = isPush;
    }

    /// <summary>�{�^������ĂԁBSwing����UI�̕\���̎d����ς���</summary>
    /// <param name="num">0�Ŕ�\��/1�ōŏ�����/2�ł�����</param>
    public void ChangeSwingUI(int num)
    {
        _tmpSwingHitUI = num;
    }


    /// <summary>�{�^������ĂԁBSwing����UI�̕\���̎d����ς���</summary>
    /// <param name="isAsist">true�ŕ\������/false�ŕ\�����Ȃ�</param>
    public void ChangeIsAsistUI(bool isAsist)
    {
        _tmpIsShowAsistUI = isAsist;
    }

    /// <summary>�{�^������ĂԁB���샌�x����ς���</summary>
    /// <param name="level">���샌�x��/0:���S��/1:����/2:�㋉</param>
    public void ChangeOperationLevel(int level)
    {
        _tmpOperationLevel = level;
    }

}
