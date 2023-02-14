using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDetail : MonoBehaviour
{
    [Header("�J�n���ɘb�����t")]
    [SerializeField] List<string> _acceptMissionText = new List<string>();
    [Header("�ēx�A�b�����Ƃ��̌��t")]
    [SerializeField] List<string> _receivedMissionText = new List<string>();
    [Header("�I��������̌��t")]
    [SerializeField] List<string> _endMissionText = new List<string>();

    [SerializeField] private int _talkMissionNum = 0;

    /// <summary>Talk��_�i�s�x��\��</summary>
    private int _talkMissionEndNum = 0;
    public int TalkMissionNum => _talkMissionEndNum;

    /// <summary>Enter��_�i�s�x��\��</summary>
    private int _enterMissionNum = 0;

    /// <summary>GetItem��_�i�s�x��\��</summary>
    private int _getItemMissionNum = 0;

    void Start()
    {

    }


    void Update()
    {

    }

    /// <summary>���݂̃~�b�V�����̐i�s�x�̊m�F������</summary>
    public bool CheckMission()
    {
        if (_talkMissionEndNum == _talkMissionNum)
        {
            return true;
        }
        return false;
    }


}


