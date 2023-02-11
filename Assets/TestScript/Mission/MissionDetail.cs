using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDetail : MonoBehaviour
{
    [Header("開始時に話す言葉")]
    [SerializeField] List<string> _acceptMissionText = new List<string>();
    [Header("再度、話したときの言葉")]
    [SerializeField] List<string> _receivedMissionText = new List<string>();
    [Header("終わった時の言葉")]
    [SerializeField] List<string> _endMissionText = new List<string>();

    [SerializeField] private int _talkMissionNum = 0;

    /// <summary>Talkの_進行度を表す</summary>
    private int _talkMissionEndNum = 0;
    public int TalkMissionNum => _talkMissionEndNum;

    /// <summary>Enterの_進行度を表す</summary>
    private int _enterMissionNum = 0;

    /// <summary>GetItemの_進行度を表す</summary>
    private int _getItemMissionNum = 0;

    void Start()
    {

    }


    void Update()
    {

    }

    /// <summary>現在のミッションの進行度の確認をする</summary>
    public bool CheckMission()
    {
        if (_talkMissionEndNum == _talkMissionNum)
        {
            return true;
        }
        return false;
    }


}


