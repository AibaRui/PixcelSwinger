using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CheckMission : TalkBase
{

    private List<string> _acceptMissionText = new List<string>();

    private List<string> _receivedMissionText = new List<string>();

    private List<string> _endMissionText = new List<string>();


    public List<string> AcceptMissionText { set => _acceptMissionText = value; get => _acceptMissionText; }

    public List<string> ReceivedText { set => _receivedMissionText = value; get => _receivedMissionText; }

    public List<string> EndText { set => _endMissionText = value; get => _endMissionText; }


    [SerializeField] MissionManager _missionManager;

    [SerializeField] PlayerInput _playerInput;


    public void CheckPlayerToTalk()
    {
        //��t�͈͂ɓ����Ă��邩�ǂ���
        if (_isEnter && !_isTalkNow)
        {
            //�X�y�[�X������
            if (_playerInput.IsJumping)
            {
                _missionManager.CheckMissionToTalk();

                //�~�b�V�����̎�t��Ԃɂ���āA��b�̓��e��ς���

                //�~�b�V�������Ȃ���
                if (_missionManager._missionSituation == MissionManager.MissionSituation.NoMission)
                {
                    _nowTalkText.Clear();
                    _nowTalkText.Add("���A�~�b�V�����͂Ȃ���");
                }  //�~�b�V���������邪�A�󂯂Ă��Ȃ�
                else if (_missionManager._missionSituation == MissionManager.MissionSituation.NoAcceptMission)
                {
                    _nowTalkText = _acceptMissionText;
                }  //�~�b�V�����i�s��
                else if (_missionManager._missionSituation == MissionManager.MissionSituation.RecebedMission)
                {
                    _nowTalkText = _receivedMissionText;
                }
                else  //�~�b�V�����������Ă���
                {
                    _nowTalkText = _endMissionText;
                }

                StartTalk();
            }
        }
    }


    protected override void TalkInEvent(int talkNum)
    {

    }

    protected override void TalkEndEvent()
    {
        _missionManager.TalkEnd();
    }
}
