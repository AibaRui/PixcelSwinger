using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [SerializeField] private List<MissionBase> _missionBases = new List<MissionBase>();

    MissionBase _nowMainMission;

    public MissionBase NowMainMission => _nowMainMission;

    int _nowMissionNum = 0;

    private bool _isAcceptMission = false;

    public bool IsAcceptMission { get => _isAcceptMission; }

    [SerializeField] private CheckMission _checkMission;

    public CheckMission CheckMission { get => _checkMission; }


    private bool _isNoMission = false;

    public bool IsNoMission { get => _isNoMission; }



    public MissionSituation _missionSituation = MissionSituation.NoAcceptMission;

    public enum MissionSituation
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
                    _missionSituation = MissionSituation.ClearMission;
                }
                else�@//�~�b�V�����N���A���Ă��Ȃ�
                {
                    _missionSituation = MissionSituation.RecebedMission;
                }
            }
            else  //�~�b�V�������󂯂Ă��Ȃ�
            {
                _missionSituation = MissionSituation.NoAcceptMission;
            }
        }
        else    //�~�b�V�������Ȃ�
        {
            _missionSituation = MissionSituation.NoMission;
        }
    }
}
