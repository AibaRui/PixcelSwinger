using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMissionDetailChange : MonoBehaviour
{
    [Header("�~�b�V�����̓��e")]
    [SerializeField] private string _mission;

    [Header("�~�b�V�����̏ڍ�")]
    [TextArea(2,10)]
    [SerializeField] private string _missionDetail;

    [Header("�~�b�V�����̓��e��Text")]
    [SerializeField] private Text _main;

    [Header("�C���x���g���̃~�b�V�����̓��e��Text")]
    [SerializeField] private Text _inventoryMission;

    [Header("�C���x���g���̃~�b�V�����̏ڍׂ�Text")]
    [SerializeField] private Text _inventoryMissionDetail;

    public void Change()
    {
        _main.text = _mission;
        _inventoryMission.text = _mission;
        _inventoryMissionDetail.text = _missionDetail;
    }

}
