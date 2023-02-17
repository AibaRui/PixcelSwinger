using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionManager : MonoBehaviour
{
    [Header("�������Ă���~�b�V����")]
    [SerializeField] private List<Mission> _missionBases = new List<Mission>();

    [Header("�Q�[����ʂ̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _missionDetailFromMainUIText;

    [Header("�C���x���g���̃~�b�V�����̓��e��Text")]
    [SerializeField] private Text _inventoryMissionText;

    [Header("�C���x���g���̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _inventoryMissionDetailText;

    [SerializeField] private CheckMission _checkMission;


    [Tooltip("���݂̃~�b�V�����̔ԍ���\��")]
    private int _nowMissionNum = 0;

    private int _nowDetailMissionNum = 0;

    public int NowDetailMissionNum { get => _nowDetailMissionNum; set => _nowDetailMissionNum = value; }


    private int _clearMissionNum = 0;

    private int _clearMissionDetailNum = 0;

    private bool _isClearMission = false;

    public int ClearMissionNum { get => _clearMissionNum; set => _clearMissionNum = value; }
    public int ClearMissionDetailNum { get => _clearMissionDetailNum; set => _clearMissionDetailNum = value; }


    public bool IsClear { get => _isClearMission; set => _isClearMission = value; }


    [SerializeField] SaveManager _saveManager;

    [Tooltip("���݃~�b�V�������󂯕t���Ă��邩�ǂ���")]
    private bool _isAcceptMission = false;
    [Tooltip("�~�b�V�������̂����Ă��邩�ǂ���")]
    private bool _isCompletedMission = false;

    private Mission _nowMainMission;

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

    private void Start()
    {

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

    public void Save()
    {
        _saveManager.DaveSave();
    }


    /// <summary>�~�b�V�����̐i�s�x���Z�[�u�f�[�^�̏��܂Ŗ߂�</summary>
    public void DataLode(int missionNum, int detailNum, bool isClear)
    {
        _clearMissionNum = missionNum;
        _clearMissionDetailNum = detailNum;
        _isClearMission = isClear;

        //�N���A���Ă�~�b�V�����́A�S��V�𓾂�
        for (int i = 0; i < missionNum - 1; i++)
        {
            foreach (var a in _missionBases[i].MissionDetails)
            {
                a.CheckReward();
            }
            _missionBases[i].CheckReward();
        }

        //���݂̃~�b�V�����̎󂯂Ă���ԍ���ݒ�
        _nowMissionNum = missionNum;
        _nowMainMission = _missionBases[_nowMissionNum - 1];

        //���̃~�b�V�������N���A���Ă�����B�~�b�V������V���󂯎��
        if (isClear)
        {
            foreach (var a in _missionBases[missionNum - 1].MissionDetails)
            {
                a.CheckReward();
            }


            ClearNowMission();
        }
        else�@//�~�b�V�������N���A���Ă��Ȃ�������~�b�V������V�͂Ȃ�
        {

            _missionBases[_nowMissionNum - 1].SetTalks();
            _checkMission.TalkNum = 1;

            for (int i = 0; i < detailNum; i++)
            {
                _nowMainMission.MissionDetails[i].CheckReward();
            }

                _nowMainMission.NowDetailMissionNum = detailNum;
                _nowMainMission.GoNextMission();

            _isAcceptMission = true;
            _isClearMission = false;
        }


    }


    /// <summary>���̃~�b�V�������Z�b�g
    /// ���̃~�b�V�������󂯕t�������ɌĂ�</summary>
    public void GoNextMission()
    {
        _isAcceptMission = true;
        _isClearMission = false;

        _nowMissionNum++;
        _clearMissionNum = _nowMissionNum;

        _missionBases[_nowMissionNum - 1].StartMission();
        _nowMainMission = _missionBases[_nowMissionNum - 1];


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
        }
        else
        {
            //���̃~�b�V�����̎�t�̃Z���t�����炩���ߓo�^���Ă���
            _missionBases[_nowMissionNum].SetTalks();
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
               // Debug.Log("�󂯂ĂȂ�");
                return;
            }
        }
    }
}
