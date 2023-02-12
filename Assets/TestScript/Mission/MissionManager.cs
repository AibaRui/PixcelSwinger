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
                    _missionSituation = MissionSituation.ClearMission;
                }
                else　//ミッションクリアしていない
                {
                    _missionSituation = MissionSituation.RecebedMission;
                }
            }
            else  //ミッションを受けていない
            {
                _missionSituation = MissionSituation.NoAcceptMission;
            }
        }
        else    //ミッションがない
        {
            _missionSituation = MissionSituation.NoMission;
        }
    }
}
