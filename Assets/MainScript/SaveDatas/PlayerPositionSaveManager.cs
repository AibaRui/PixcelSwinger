using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class PlayerPositionSaveManager : SaveManagerBase
{
    [Header("Player")] 
    [SerializeField] private GameObject _player;

    [Header("初期の位置")]
    [SerializeField] private Transform _firstPos;

    private PlayerPositionSaveData _save;

    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".playerpositionsavedata.json";
    }


    public override bool Save()
    {
        _save = new PlayerPositionSaveData(_player.transform.position);

        Debug.Log($"プレイヤーの位置座標をセーブしました。{_save._savePos}");

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    public override bool Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<PlayerPositionSaveData>(data);
            Debug.Log($"プレイヤーの位置座標をロードしました。{_save._savePos}");
        }
        return true;
    }

    public void SetPlayerPosition()
    {
        if (CheckSaveDataExistence.s_isNewGame)
        {
            _player.transform.position = _firstPos.position;
        }
        else
        {
            _player.transform.position = _save._savePos;
        }

    }

}
