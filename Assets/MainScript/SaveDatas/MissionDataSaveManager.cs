using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


public class MissionDataSaveManager : SaveManagerBase
{
    [Tooltip("セーブしたい内容")]
    private int _saveDataMissionNum;
    private int _saveDataMissionDetailNum;
    private bool _saveDataIsClear;

    [Tooltip("セーブしたい内容を保持するクラス")]
    MissionSaveData _save;

    string filePath;


    [SerializeField] MissionManager _missionManager;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".missionsavedata.json";

        //NewGameの際は、初期装備を持たせる
        if (!CheckSaveDataExistence.s_isNewGame) Load();
        else _missionManager.SetNewSetting();
    }

    /// <summary>セーブする内容を更新する</summary>
    /// <param name="saveData">セーブしたいデータ</param>
    public void SaveDataUpDate(int missioNum, int detailNum, bool isClear)
    {
        _saveDataMissionNum = missioNum;
        _saveDataMissionDetailNum = detailNum;
        _saveDataIsClear = isClear;
        Debug.Log($"アイテムの保存データを更新。{_saveDataMissionNum}/{_saveDataMissionDetailNum}/{_saveDataIsClear}");
    }

    /// <summary>保存していた内容を返す_MissionNum</summary>
    /// <returns></returns>
    public int GetSaveDataMissionNum()
    {
        return _save._missionNun;
    }

    /// <summary>保存していた内容を返す_DetailNum</summary>
    /// <returns></returns>
    public int GetSaveDataMissionDetailNum()
    {
        return _save._detailNum;
    }

    /// <summary>保存していた内容を返す_IsClear</summary>
    /// <returns></returns>
    public bool GetSaveDataIsClear()
    {
        return _save._isClear;
    }

    /// <summary>データをセーブ</summary>
    public override bool Save()
    {
        _save = new MissionSaveData(_saveDataMissionNum, _saveDataMissionDetailNum, _saveDataIsClear);
        Debug.Log($"ミッションの進行状況をセーブしました。{_saveDataMissionNum}/{_saveDataMissionDetailNum}/{_saveDataIsClear}");

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    /// <summary>データをロード</summary>
    public override bool Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<MissionSaveData>(data);
            Debug.Log($"ミッションの進行状況をロードしました。{_save._missionNun}/{_save._detailNum}/{_save._isClear}");

            _saveDataMissionNum = _save._missionNun;
            _saveDataMissionDetailNum = _save._detailNum;
            _saveDataIsClear = _save._isClear;
        }
        return true;
    }

}
