using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionManager : MonoBehaviour, ISave
{
    [Header("実装してあるミッション")]
    [SerializeField] private List<Mission> _missionBases = new List<Mission>();

    [Header("ゲーム画面のミッションの詳細のText")]
    [SerializeField] private Text _missionDetailFromMainUIText;

    [Header("インベントリのミッションの内容のText")]
    [SerializeField] private Text _inventoryMissionText;

    [Header("インベントリのミッションの詳細のText")]
    [SerializeField] private Text _inventoryMissionDetailText;

    [SerializeField] private CheckMission _checkMission;

    [SerializeField] private MissionDataSaveManager _missionDataSaveManager;

    [SerializeField] SaveManager _saveManager;

    [Tooltip("現在のミッションの番号を表す")]
    private int _nowMissionNum = 0;
    [Tooltip("現在のミッションの詳細番号を表す")]
    private int _nowDetailMissionNum = 0;


    private int _clearMissionNum = 0;

    private int _clearMissionDetailNum = 0;

    private bool _isClearMission = false;


    private Mission _nowMainMission;

    [Tooltip("現在ミッションを受け付けているかどうか")]
    private bool _isAcceptMission = false;
    [Tooltip("ミッションがのこっているかどうか")]
    private bool _isCompletedMission = false;


    public int NowDetailMissionNum { get => _nowDetailMissionNum; set => _nowDetailMissionNum = value; }
    public int ClearMissionNum { get => _clearMissionNum; set => _clearMissionNum = value; }
    public int ClearMissionDetailNum { get => _clearMissionDetailNum; set => _clearMissionDetailNum = value; }
    public bool IsClear { get => _isClearMission; set => _isClearMission = value; }
    public Mission NowMainMission => _nowMainMission;
    public bool IsAcceptMission { get => _isAcceptMission; }
    public CheckMission CheckMission { get => _checkMission; }
    public bool IsCompletedMission { get => _isCompletedMission; }

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

    /// <summary>ゲーム画面のミッションの詳細のTextを書き換える</summary>
    public void SettingMissionText(string mission, string detail)
    {
        _inventoryMissionText.text = mission;
        _missionDetailFromMainUIText.text = mission;
        _inventoryMissionDetailText.text = detail;
    }

    private void Start()
    {

    }

    public void Save()
    {
        SaveMission();
        _saveManager.DaveSave();
    }

    /// <summary>インターフェイス。データのロードを揃えるための関数</summary>
    public void FistDataLodeOnGameStart()
    {
        if (CheckSaveDataExistence.s_isNewGame)
        {
            SetNewSetting();
        }
        else
        {
            DataLodeStart();
        }
    }

    /// <summary>ミッションの進行状況のデータを更新</summary>
    public void SaveMission()
    {
        _missionDataSaveManager.SaveDataUpDate(_nowMissionNum, _nowDetailMissionNum, _isClearMission);
    }

    /// <summary>NewGameの初期設定</summary>
    public void SetNewSetting()
    {
        //次のミッションの受付のセリフをあらかじめ登録しておく
        _missionBases[_nowMissionNum].SetTalks();
        _clearMissionNum = 0;
        _clearMissionDetailNum = 0;
        _isClearMission = false;
    }

    /// <summary>ミッションの進行度をセーブデータの所まで戻す</summary>
    public void DataLodeStart()
    {
        _clearMissionNum = _missionDataSaveManager.GetSaveDataMissionNum();
        _clearMissionDetailNum = _missionDataSaveManager.GetSaveDataMissionDetailNum();
        _isClearMission = _missionDataSaveManager.GetSaveDataIsClear();

        //クリアしてるミッションの、全報酬を得る
        for (int i = 0; i < _clearMissionNum - 1; i++)
        {
            foreach (var a in _missionBases[i].MissionDetails)
            {
                a.CheckReward();
            }
            _missionBases[i].CheckReward();
        }

        //現在のミッションの受けている番号を設定
        _nowMissionNum = _clearMissionNum;
        _nowMainMission = _missionBases[_nowMissionNum - 1];

        //そのミッションをクリアしていたら。ミッション報酬も受け取る
        if (_isClearMission)
        {
            foreach (var a in _missionBases[_clearMissionNum - 1].MissionDetails)
            {
                a.CheckReward();
            }


            ClearNowMission();
        }
        else　//ミッションをクリアしていなかったらミッション報酬はなし
        {

            _missionBases[_nowMissionNum - 1].SetTalks();
            _checkMission.TalkNum = 1;

            for (int i = 0; i < _clearMissionDetailNum; i++)
            {
                if (_nowMainMission.MissionDetails.Count > i)
                {
                    _nowMainMission.MissionDetails[i].CheckReward();
                }
            }

            _nowMainMission.NowDetailMissionNum = _clearMissionDetailNum;
            _nowMainMission.GoNextMission();

            _isAcceptMission = true;
            _isClearMission = false;
        }
    }


    /// <summary>次のミッションをセット
    /// 次のミッションを受け付けた時に呼ぶ</summary>
    public void GoNextMission()
    {
        _isAcceptMission = true;
        _isClearMission = false;

        _nowMissionNum++;
        _clearMissionNum = _nowMissionNum;

        _missionBases[_nowMissionNum - 1].StartMission();
        _nowMainMission = _missionBases[_nowMissionNum - 1];
    }

    /// <summary>現在のミッションをクリア
    /// ミッションクリア後に離したときに呼ぶ</summary>
    public void ClearNowMission()
    {
        _nowMainMission.ClearMission();
        _nowMainMission = null;

        _isAcceptMission = false;
        _isClearMission = true;

        if (_nowMissionNum == _missionBases.Count)
        {
            //ミッションすべて完了
            _isCompletedMission = true;
        }
        else
        {
            //次のミッションの受付のセリフをあらかじめ登録しておく
            _missionBases[_nowMissionNum].SetTalks();
        }

        Save();
    }


    /// <summary>ミッションの確認で話したときに呼ぶ
    /// ミッションの状況を判別</summary>
    public void CheckMissionToTalk()
    {
        if (_isCompletedMission)   //ミッションがない
        {
            _mainMissionSituation = MainMissionSituation.NoMission;
            return;
        }
        //ミッションがある場合
        else if (_missionBases.Count >= _nowMissionNum)
        {
            //ミッションを受けている最中
            if (_isAcceptMission)
            {
                //ミッションクリアしている
                if (_missionBases[_nowMissionNum - 1].IsMissionCompleted)
                {
                    _mainMissionSituation = MainMissionSituation.ClearMission;
                    // Debug.Log("クリア");
                    return;
                }
                else　//ミッションクリアしていない
                {
                    _mainMissionSituation = MainMissionSituation.RecebedMission;
                    //Debug.Log("受けている");
                    return;
                }
            }
            else  //ミッションを受けていない
            {
                _mainMissionSituation = MainMissionSituation.NoAcceptMission;
                // Debug.Log("受けてない");
                return;
            }
        }
    }
}
