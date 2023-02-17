using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


public class MissionDataSaveManager : MonoBehaviour
{
    string filePath;
    MissionSaveData _save;

    [SerializeField] MissionManager _missionManager;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".missionsavedata.json";

        //NewGameの際は、初期装備を持たせる
        if (!CheckSaveDataExistence.s_isNewGame) Load();
        else _missionManager.SetNewSetting();
    }


    public void Save()
    {
        _save = new MissionSaveData(_missionManager.ClearMissionNum,_missionManager.ClearMissionDetailNum,_missionManager.IsClear);

        Debug.Log($"ミッションの進行状況をセーブしました。{_save._missionNun}/{_save._detailNum}/{_save._isClear}");

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();

    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<MissionSaveData>(data);
            Debug.Log($"ミッションの進行状況をロードしました。{_save._missionNun}/{_save._detailNum}/{_save._isClear}");
            _missionManager.DataLode(_save._missionNun, _save._detailNum, _save._isClear);
        }
    }

}
