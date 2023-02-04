using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CheckMission : MonoBehaviour
{
    [SerializeField] GameObject _weapons;

    [SerializeField] CinemachineVirtualCamera _camera;

    [SerializeField] GameObject _showPanel;

    [SerializeField] GameObject _talkPanel;

    [SerializeField] Text _talkText;

    private List<string> _acceptMissionText = new List<string>();

    private List<string> _receivedMissionText = new List<string>();

    private List<string> _endMissionText = new List<string>();

    private List<string> _nowTalkText = new List<string>();

    public List<string> AcceptMissionText { set => _acceptMissionText = value; get => _acceptMissionText; }

    public List<string> ReceivedText { set => _receivedMissionText = value; get => _receivedMissionText; }

    public List<string> EndText { set => _endMissionText = value; get => _endMissionText; }

    public List<string> NowTalkText { set => _nowTalkText = value; get => _nowTalkText; }

    private bool _isEnter;

    private bool _isTalkNow = false;

    public bool IsTalkNow { get => _isTalkNow; }

    [SerializeField] MissionManager _missionManager;

    [SerializeField] PlayerInput _playerInput;


    public void CheckPlayerToTalk()
    {
        //��t�͈͂ɓ����Ă��邩�ǂ���
        if (_isEnter)
        {
            //�X�y�[�X������
            if (_playerInput.IsJumping && _talkPanel.activeSelf == false)
            {
                _talkPanel.SetActive(true);
                _camera.Priority = 1000;
                _weapons.SetActive(false);

                _missionManager.CheckMissionToTalk();
                _isTalkNow = true;
                StartCoroutine(Talk());
            }
        }
    }


    IEnumerator Talk()
    {
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

        int num = 0;
        
        //�b���Ă���e�L�X�g���X�V�B
        while (num != _nowTalkText.Count)
        {
            _talkText.text = _nowTalkText[num];

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            num++;
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        yield return new WaitForSeconds(0.2f);

        _isTalkNow = false;

        //��b�̃p�l�������
        CloseMissionPanel();

        //��b�I�����̏���
        _missionManager.TalkEnd();

    }

    public void CloseMissionPanel()
    {
        _talkPanel.SetActive(false);
        _weapons.SetActive(true);
        _camera.Priority = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _showPanel.activeSelf == false)
        {
            _showPanel.SetActive(true);
            _isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _showPanel.SetActive(false);
            _isEnter = false;
        }
    }


}
