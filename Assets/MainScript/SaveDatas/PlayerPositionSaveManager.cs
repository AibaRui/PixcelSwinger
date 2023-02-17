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

    [Header("�����̈ʒu")]
    [SerializeField] Transform _pos;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".playerpositionsavedata.json";

        //NewGame�̍ۂ́A�����ʒu
        if (CheckSaveDataExistence.s_isNewGame) SetPlayerPosition(_pos.position);
        else Load();
    }


    public void Save()
    {
        _save = new PlayerPositionSaveData(_player.transform.position);

        Debug.Log($"�v���C���[�̈ʒu���W���Z�[�u���܂����B{_save._savePos}");

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
            Debug.Log($"�v���C���[�̈ʒu���W�����[�h���܂����B{_save._savePos}");
            SetPlayerPosition(_save._savePos);
        }
    }

    private void SetPlayerPosition(Vector3 pos)
    {
        _player.transform.position = pos;
    }

}
