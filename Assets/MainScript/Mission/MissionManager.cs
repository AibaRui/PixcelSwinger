using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionManager : MonoBehaviour
{

    [SerializeField] private List<Mission> _missionBases = new List<Mission>();

    [Header("ゲーム画面のミッションの詳細のText")]
    [SerializeField] private Text _missionDetailFromMainUIText;

    [Header("インベントリのミッションの内容のText")]
    [SerializeField] private Text _inventoryMissionText;

    [Header("インベントリのミッションの詳細のText")]
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
        //出来るミッションが無い
        NoMission,

        //ミッションを受け付けていない
        NoAcceptMission,

        //ミッションを受けている
        RecebedMission,

        //ミッションクリア
        ClearMission,

    }

    void Start()
    {
        MissionSet();
    }

    /// <summary>ゲーム画面のミッションの詳細のTextを書き換える</summary>
    public void SettingMissionText(string mission,string detail)
    {
        _inventoryMissionText.text = mission;
        _missionDetailFromMainUIText.text = mission;
        _inventoryMissionDetailText.text = detail;
    }


    public void EndMission()
    {
        //ミッションクリア後の、したい処理を実行
        _missionBases[_nowMissionNum].MissionClear();
        _nowMainMission = null;

        //現在、ミッションを受けていない状態に変更
        _isAcceptMission = false;


        _nowMissionNum++;

        //ミッションが余っていたら、次のミッションをセット。
        if (_missionBases.Count != _nowMissionNum)
        {
            MissionSet();
        }
        else
        {
            _isNoMission = true;
        }
    }

    /// <summary>話が終わった後の処理</summary>
    public void TalkEnd()
    {
        if (_nowMissionNum < _missionBases.Count)
        {
            //ミッションを受け付けている
            if (_isAcceptMission)
            {
                //ミッションクリアしていたら
                if (_missionBases[_nowMissionNum].IsMissionCompleted)
                {
                    //ミッションクリア処理
                    EndMission();
                }
            }
            else//ミッションをしていない
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

    /// <summary>ミッションの確認で話したときに呼ぶ</summary>
    public void CheckMissionToTalk()
    {
        //ミッションがある場合
        if (_missionBases.Count != _nowMissionNum)
        {
            //ミッションを受けている最中
            if (_isAcceptMission)
            {
                //ミッションクリアしている
                if (_missionBases[_nowMissionNum].IsMissionCompleted)
                {
                    _mainMissionSituation = MainMissionSituation.ClearMission;
                }
                else　//ミッションクリアしていない
                {
                    _mainMissionSituation = MainMissionSituation.RecebedMission;
                }
            }
            else  //ミッションを受けていない
            {
                _mainMissionSituation = MainMissionSituation.NoAcceptMission;
                _nowMainMission.StartMission();
            }
        }
        else    //ミッションがない
        {
            _mainMissionSituation = MainMissionSituation.NoMission;
        }
    }
}
