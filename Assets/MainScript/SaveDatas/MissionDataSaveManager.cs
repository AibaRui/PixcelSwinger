using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


public class MissionDataSaveManager : SaveManagerBase
{
    [Tooltip("�Z�[�u���������e")]
    private int _saveDataMissionNum;
    private int _saveDataMissionDetailNum;
    private bool _saveDataIsClear;

    [Tooltip("�Z�[�u���������e��ێ�����N���X")]
    MissionSaveData _save;

    string filePath;


    [SerializeField] MissionManager _missionManager;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".missionsavedata.json";

        //NewGame�̍ۂ́A������������������
        if (!CheckSaveDataExistence.s_isNewGame) Load();
        else _missionManager.SetNewSetting();
    }

    /// <summary>�Z�[�u������e���X�V����</summary>
    /// <param name="saveData">�Z�[�u�������f�[�^</param>
    public void SaveDataUpDate(int missioNum, int detailNum, bool isClear)
    {
        _saveDataMissionNum = missioNum;
        _saveDataMissionDetailNum = detailNum;
        _saveDataIsClear = isClear;
        Debug.Log($"�A�C�e���̕ۑ��f�[�^���X�V�B{_saveDataMissionNum}/{_saveDataMissionDetailNum}/{_saveDataIsClear}");
    }

    /// <summary>�ۑ����Ă������e��Ԃ�_MissionNum</summary>
    /// <returns></returns>
    public int GetSaveDataMissionNum()
    {
        return _save._missionNun;
    }

    /// <summary>�ۑ����Ă������e��Ԃ�_DetailNum</summary>
    /// <returns></returns>
    public int GetSaveDataMissionDetailNum()
    {
        return _save._detailNum;
    }

    /// <summary>�ۑ����Ă������e��Ԃ�_IsClear</summary>
    /// <returns></returns>
    public bool GetSaveDataIsClear()
    {
        return _save._isClear;
    }

    /// <summary>�f�[�^���Z�[�u</summary>
    public override bool Save()
    {
        _save = new MissionSaveData(_saveDataMissionNum, _saveDataMissionDetailNum, _saveDataIsClear);
        Debug.Log($"�~�b�V�����̐i�s�󋵂��Z�[�u���܂����B{_saveDataMissionNum}/{_saveDataMissionDetailNum}/{_saveDataIsClear}");

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    /// <summary>�f�[�^�����[�h</summary>
    public override bool Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<MissionSaveData>(data);
            Debug.Log($"�~�b�V�����̐i�s�󋵂����[�h���܂����B{_save._missionNun}/{_save._detailNum}/{_save._isClear}");

            _saveDataMissionNum = _save._missionNun;
            _saveDataMissionDetailNum = _save._detailNum;
            _saveDataIsClear = _save._isClear;
        }
        return true;
    }

}
