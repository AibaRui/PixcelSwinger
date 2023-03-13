using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionManager : MonoBehaviour, ISave
{
    [Header("�������Ă���~�b�V����")]
    [SerializeField] private List<Mission> _missionBases = new List<Mission>();

    [Header("�Q�[����ʂ̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _missionDetailFromMainUIText;

    [Header("�C���x���g���̃~�b�V�����̓��e��Text")]
    [SerializeField] private Text _inventoryMissionText;

    [Header("�C���x���g���̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _inventoryMissionDetailText;

    [Header("�~�b�V�������Ȃ��Ƃ��̏ڍׂ̓��e")]
    [SerializeField] private string _noMissionTextDetail;


    [Header("�~�b�V�������܂����鎞�AUI�ɕ\��������e")]
    [SerializeField] private string _missionMidle;

    [Header("�~�b�V�������܂����鎞�́AUI�ɕ\��������e�̏ڍ�"), TextArea]
    [SerializeField] private string _missionMidleDetail;

    [Header("�~�b�V�������Ȃ����́AUI�ɕ\��������e")]
    [SerializeField] private string _noMissionMidle;

    [Header("�~�b�V�������Ȃ����́AUI�ɕ\��������e�̏ڍ�"), TextArea]
    [SerializeField] private string _noMissionMidleDetail;

    [SerializeField] private CheckMission _checkMission;

    [SerializeField] private MissionDataSaveManager _missionDataSaveManager;

    [SerializeField] SaveManager _saveManager;

    [Tooltip("���݂̃~�b�V�����̔ԍ���\��")]
    private int _nowMissionNum = 0;
    [Tooltip("���݂̃~�b�V�����̏ڍהԍ���\��")]
    private int _nowDetailMissionNum = 0;


    private int _clearMissionNum = 0;

    private int _clearMissionDetailNum = 0;

    private bool _isClearMission = false;


    private Mission _nowMainMission;

    [Tooltip("���݃~�b�V�������󂯕t���Ă��邩�ǂ���")]
    private bool _isAcceptMission = false;
    [Tooltip("�~�b�V�������̂����Ă��邩�ǂ���")]
    private bool _isCompletedMission = false;


    public int NowDetailMissionNum { get => _nowDetailMissionNum; set => _nowDetailMissionNum = value; }
    public int ClearMissionNum { get => _clearMissionNum; set => _clearMissionNum = value; }
    public int ClearMissionDetailNum { get => _clearMissionDetailNum; set => _clearMissionDetailNum = value; }
    public bool IsClear { get => _isClearMission; set => _isClearMission = value; }
    public Mission NowMainMission => _nowMainMission;
    public bool IsAcceptMission { get => _isAcceptMission; }
    public CheckMission CheckMission { get => _checkMission; }
    public bool IsCompletedMission { get => _isCompletedMission; }

    public MainMissionSituation _mainMissionSituation = MainMissionSituation.NoAcceptMission;

    public enum MainMissionSituation
    {
        //�o����~�b�V����������
        NoMission,

        //�~�b�V�������󂯕t���Ă��Ȃ�
        NoAcceptMission,

        //�~�b�V�������󂯂Ă���
        RecebedMission,

        //�~�b�V�����N���A
        ClearMission,
    }

    /// <summary>�Q�[����ʂ̃~�b�V�����̏ڍׂ�Text������������</summary>
    public void SettingMissionText(string mission, string detail)
    {
        _inventoryMissionText.text = mission;
        _missionDetailFromMainUIText.text = mission;
        _inventoryMissionDetailText.text = detail;
    }

    public void ReSetMissionText()
    {
        _inventoryMissionText.text = _noMissionTextDetail;
        _missionDetailFromMainUIText.text = _noMissionTextDetail;
        _inventoryMissionDetailText.text = "";
    }

    public void Save()
    {
        SaveMission();
        _saveManager.DataSave();
    }

    /// <summary>�C���^�[�t�F�C�X�B�f�[�^�̃��[�h�𑵂��邽�߂̊֐�</summary>
    public void FistDataLodeOnGameStart()
    {
        if (CheckSaveDataExistence.s_isNewGame)
        {
            SetNewSetting();
        }
        else
        {
            DataLodeStart();
        }
    }

    /// <summary>�~�b�V�����̐i�s�󋵂̃f�[�^���X�V</summary>
    public void SaveMission()
    {
        _missionDataSaveManager.SaveDataUpDate(_nowMissionNum, _nowDetailMissionNum, _isClearMission);
    }

    /// <summary>NewGame�̏����ݒ�</summary>
    public void SetNewSetting()
    {
        //���̃~�b�V�����̎�t�̃Z���t�����炩���ߓo�^���Ă���
        _missionBases[_nowMissionNum].SetTalks();
        _clearMissionNum = 0;
        _clearMissionDetailNum = 0;
        _isClearMission = false;
    }

    /// <summary>�~�b�V�����̐i�s�x���Z�[�u�f�[�^�̏��܂Ŗ߂�</summary>
    public void DataLodeStart()
    {
        if (_missionBases.Count > 0)
        {
            _clearMissionNum = _missionDataSaveManager.GetSaveDataMissionNum();
            _clearMissionDetailNum = _missionDataSaveManager.GetSaveDataMissionDetailNum();
            _isClearMission = _missionDataSaveManager.GetSaveDataIsClear();

            //�N���A���Ă�~�b�V�����́A�S��V�𓾂�
            for (int i = 0; i < _clearMissionNum - 1; i++)
            {
                foreach (var a in _missionBases[i].MissionDetails)
                {
                    a.CheckReward();
                }
                _missionBases[i].CheckReward();
            }

               _nowMissionNum = _clearMissionNum;
                _nowMainMission = _missionBases[_nowMissionNum - 1];

            //���݂̃~�b�V�����̎󂯂Ă���ԍ���ݒ�
            try
            {
 
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log($"�G���[�l :{_nowMissionNum - 1}");
            }

            //���̃~�b�V�������N���A���Ă�����B�~�b�V������V���󂯎��
            if (_isClearMission)
            {
                foreach (var a in _missionBases[_clearMissionNum - 1].MissionDetails)
                {
                    a.CheckReward();
                }


                ClearNowMission();
            }
            else //�~�b�V�������N���A���Ă��Ȃ�������~�b�V������V�͂Ȃ�
            {

                _missionBases[_nowMissionNum - 1].SetTalks();
                _checkMission.TalkNum = 1;

                for (int i = 0; i < _clearMissionDetailNum; i++)
                {
                    if (_nowMainMission.MissionDetails.Count > i)
                    {
                        _nowMainMission.MissionDetails[i].CheckReward();
                    }
                }

                _nowMainMission.NowDetailMissionNum = _clearMissionDetailNum;
                _nowMainMission.GoNextMission();

                _isAcceptMission = true;
                _isClearMission = false;
            }
        }
    }


    /// <summary>���̃~�b�V�������Z�b�g
    /// ���̃~�b�V�������󂯕t�������ɌĂ�</summary>
    public void GoNextMission()
    {
        _missionBases[_nowMissionNum - 1].StartMission();

    }

    /// <summary>���݂̃~�b�V�������N���A
    /// �~�b�V�����N���A��ɗ������Ƃ��ɌĂ�</summary>
    public void ClearNowMission()
    {
        _nowMainMission.ClearMission();
        _nowMainMission = null;

        _isAcceptMission = false;
        _isClearMission = true;

        if (_nowMissionNum == _missionBases.Count)
        {
            //�~�b�V�������ׂĊ���
            _isCompletedMission = true;
            SettMissionUIText(true);
        }
        else
        {
            //���̃~�b�V�����̎�t�̃Z���t�����炩���ߓo�^���Ă���
            _missionBases[_nowMissionNum].SetTalks();
            SettMissionUIText(false);
        }

        Save();
    }


    /// <summary>�~�b�V�����̊m�F�Řb�����Ƃ��ɌĂ�
    /// �~�b�V�����̏󋵂𔻕�</summary>
    public void CheckMissionToTalk()
    {
        if (_isCompletedMission)   //�~�b�V�������Ȃ�
        {
            _mainMissionSituation = MainMissionSituation.NoMission;
            return;
        }
        //�~�b�V����������ꍇ
        else if (_missionBases.Count >= _nowMissionNum)
        {
            //�~�b�V�������󂯂Ă���Œ�
            if (_isAcceptMission)
            {
                //�~�b�V�����N���A���Ă���
                if (_missionBases[_nowMissionNum - 1].IsMissionCompleted)
                {
                    _mainMissionSituation = MainMissionSituation.ClearMission;
                    // Debug.Log("�N���A");
                    return;
                }
                else�@//�~�b�V�����N���A���Ă��Ȃ�
                {
                    _mainMissionSituation = MainMissionSituation.RecebedMission;

                    //Debug.Log("�󂯂Ă���");
                    return;
                }
            }
            else  //�~�b�V�������󂯂Ă��Ȃ�
            {
                _mainMissionSituation = MainMissionSituation.NoAcceptMission;

                //�~�b�V������o�^
                _isAcceptMission = true;
                _isClearMission = false;

                _nowMissionNum++;
                _clearMissionNum = _nowMissionNum;
                _nowMainMission = _missionBases[_nowMissionNum - 1];

                // Debug.Log("�󂯂ĂȂ�");
                return;
            }
        }
    }


    public void SettMissionUIText(bool isMission)
    {

        if (isMission)
        {
            _missionDetailFromMainUIText.text = _missionMidle;
            _inventoryMissionText.text = _missionMidle;
            _inventoryMissionDetailText.text = _missionMidleDetail;
        }
        else
        {
            _missionDetailFromMainUIText.text = _noMissionMidle;
            _inventoryMissionText.text = _noMissionMidle;
            _inventoryMissionDetailText.text = _noMissionMidleDetail;
        }

    }
}
