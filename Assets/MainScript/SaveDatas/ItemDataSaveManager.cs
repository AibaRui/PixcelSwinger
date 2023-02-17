using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class ItemDataSaveManager : SaveManagerBase
{
    [Tooltip("�Z�[�u���������e")]
    private string _saveDataString;

    [Tooltip("�Z�[�u���������e��ێ�����N���X")]
    private ItemSaveData _save;

    private string filePath;

    public ItemSaveData SaveData => _save;


    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".itemsavedata.json";
    }


    /// <summary>�Z�[�u������e���X�V����</summary>
    /// <param name="saveData">�Z�[�u�������f�[�^</param>
    public void SaveDataUpDate(string[] saveData)
    {
        _saveDataString = string.Join(",", saveData);
        Debug.Log("�A�C�e���̕ۑ��f�[�^���X�V" + _saveDataString);
    }

    /// <summary>�ۑ����Ă������e��Ԃ�</summary>
    /// <returns></returns>
    public string GetSaveData()
    {
        return _save._getItemNameSaveData;
    }


    /// <summary>�Z�[�u����</summary>
    public override bool Save()
    {
        //List�̒��g���A,��؂�̕�����ŕۑ�
        _save = new ItemSaveData(_saveDataString);
        Debug.Log("�������Ă���A�C�e�����Z�[�u���܂���" + _save._getItemNameSaveData);

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    /// <summary>���[�h����</summary>
    public override bool Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<ItemSaveData>(data);

            _saveDataString = _save._getItemNameSaveData;

            Debug.Log("�������Ă���A�C�e�������[�h���܂���" + _save._getItemNameSaveData);

        }
        return true;
    }
}
