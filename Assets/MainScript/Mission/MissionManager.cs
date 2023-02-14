using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionManager : MonoBehaviour
{

    [SerializeField] private List<Mission> _missionBases = new List<Mission>();

    [Header("�Q�[����ʂ̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _missionDetailFromMainUIText;

    [Header("�C���x���g���̃~�b�V�����̓��e��Text")]
    [SerializeField] private Text _inventoryMissionText;

    [Header("�C���x���g���̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _inventoryMissionDetailText;

    Mission _nowMainMission;

    public Mission NowMainMission => _nowMainMission;

    int _nowMissionNum = 0;

    private bool _isAcceptMission = false;

    public bool IsAcceptMission { get => _isAcceptMission; }

    [SerializeField] private CheckMission _checkMission;

    public CheckMission CheckMission { get => _checkMission; }


    private bool _isNoMission = false;

    public bool IsNoMission { get => _isNoMission; }



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

    void Start()
    {
        MissionSet();
    }

    /// <summary>�Q�[����ʂ̃~�b�V�����̏ڍׂ�Text������������</summary>
    public void SettingMissionText(string mission,string detail)
    {
        _inventoryMissionText.text = mission;
        _missionDetailFromMainUIText.text = mission;
        _inventoryMissionDetailText.text = detail;
    }


    public void EndMission()
    {
        //�~�b�V�����N���A��́A���������������s
        _missionBases[_nowMissionNum].MissionClear();
        _nowMainMission = null;

        //���݁A�~�b�V�������󂯂Ă��Ȃ���ԂɕύX
        _isAcceptMission = false;


        _nowMissionNum++;

        //�~�b�V�������]���Ă�����A���̃~�b�V�������Z�b�g�B
        if (_missionBases.Count != _nowMissionNum)
        {
            MissionSet();
        }
        else
        {
            _isNoMission = true;
        }
    }

    /// <summary>�b���I�������̏���</summary>
    public void TalkEnd()
    {
        if (_nowMissionNum < _missionBases.Count)
        {
            //�~�b�V�������󂯕t���Ă���
            if (_isAcceptMission)
            {
                //�~�b�V�����N���A���Ă�����
                if (_missionBases[_nowMissionNum].IsMissionCompleted)
                {
                    //�~�b�V�����N���A����
                    EndMission();
                }
            }
            else//�~�b�V���������Ă��Ȃ�
            {
                _isAcceptMission = true;
            }
        }


    }

    void MissionSet()
    {
        _missionBases[_nowMissionNum].Init(this);
        _nowMainMission = _missionBases[_nowMissionNum];
    }

    /// <summary>�~�b�V�����̊m�F�Řb�����Ƃ��ɌĂ�</summary>
    public void CheckMissionToTalk()
    {
        //�~�b�V����������ꍇ
        if (_missionBases.Count != _nowMissionNum)
        {
            //�~�b�V�������󂯂Ă���Œ�
            if (_isAcceptMission)
            {
                //�~�b�V�����N���A���Ă���
                if (_missionBases[_nowMissionNum].IsMissionCompleted)
                {
                    _mainMissionSituation = MainMissionSituation.ClearMission;
                }
                else�@//�~�b�V�����N���A���Ă��Ȃ�
                {
                    _mainMissionSituation = MainMissionSituation.RecebedMission;
                }
            }
            else  //�~�b�V�������󂯂Ă��Ȃ�
            {
                _mainMissionSituation = MainMissionSituation.NoAcceptMission;
                _nowMainMission.StartMission();
            }
        }
        else    //�~�b�V�������Ȃ�
        {
            _mainMissionSituation = MainMissionSituation.NoMission;
        }
    }
}
