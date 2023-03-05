using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMissionDetailChange : MonoBehaviour
{
    [Header("ミッションの内容")]
    [SerializeField] private string _mission;

    [Header("ミッションの詳細")]
    [TextArea(2,10)]
    [SerializeField] private string _missionDetail;

    [Header("ミッションの内容のText")]
    [SerializeField] private Text _main;

    [Header("インベントリのミッションの内容のText")]
    [SerializeField] private Text _inventoryMission;

    [Header("インベントリのミッションの詳細のText")]
    [SerializeField] private Text _inventoryMissionDetail;

    public void Change()
    {
        _main.text = _mission;
        _inventoryMission.text = _mission;
        _inventoryMissionDetail.text = _missionDetail;
    }

}
