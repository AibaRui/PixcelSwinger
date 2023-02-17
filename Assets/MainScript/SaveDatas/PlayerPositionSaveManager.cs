using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class PlayerPositionSaveManager : MonoBehaviour
{
    string filePath;
    PlayerPositionSaveData _save;

    [Header("Player")]
    [SerializeField] GameObject _player;

    [Header("初期の位置")]
    [SerializeField] Transform _pos;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".playerpositionsavedata.json";

        //NewGameの際は、初期位置
        if (CheckSaveDataExistence.s_isNewGame) SetPlayerPosition(_pos.position);
        else Load();
    }


    public void Save()
    {
        _save = new PlayerPositionSaveData(_player.transform.position);

        Debug.Log($"プレイヤーの位置座標をセーブしました。{_save._savePos}");

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
            _save = JsonUtility.FromJson<PlayerPositionSaveData>(data);
            Debug.Log($"プレイヤーの位置座標をロードしました。{_save._savePos}");
            SetPlayerPosition(_save._savePos);
        }
    }

    private void SetPlayerPosition(Vector3 pos)
    {
        _player.transform.position = pos;
    }

}
