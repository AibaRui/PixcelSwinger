using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class ItemDataSaveManager : MonoBehaviour
{
    string filePath;
    ItemSaveData _save;

    [SerializeField] ItemManager _itemManager;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".itemsavedata.json";

        //NewGame�̍ۂ́A������������������
        if (CheckSaveDataExistence.s_isNewGame) FirstStart();
        //Continue�̍ۂ́A�Z�[�u�f�[�^����ǂݍ���
        else Load();
    }


    public void Save()
    {   
        //List�̒��g���A,��؂�̕�����ɕϊ����ĕۑ�
        string data = string.Join(",", _itemManager.GetItemSaveData.ToArray());
        _save = new ItemSaveData(data);

        Debug.Log("�������Ă���A�C�e�����Z�[�u���܂���");

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
            _save = JsonUtility.FromJson<ItemSaveData>(data);

            Debug.Log("�������Ă���A�C�e�������[�h���܂���");
            var LodeData = _save._getItemNameSaveData.Split(",");
            _itemManager.DataLode(LodeData);
        }
    }


    public void FirstStart()
    {
        _itemManager.DataLode(_itemManager.FirstGetItem.ToArray());
    }

}
