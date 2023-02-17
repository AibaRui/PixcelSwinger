using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class ItemDataSaveManager : SaveManagerBase
{
    [Tooltip("セーブしたい内容")]
    private string _saveDataString;

    [Tooltip("セーブしたい内容を保持するクラス")]
    private ItemSaveData _save;

    private string filePath;

    public ItemSaveData SaveData => _save;


    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".itemsavedata.json";
    }


    /// <summary>セーブする内容を更新する</summary>
    /// <param name="saveData">セーブしたいデータ</param>
    public void SaveDataUpDate(string[] saveData)
    {
        _saveDataString = string.Join(",", saveData);
        Debug.Log("アイテムの保存データを更新" + _saveDataString);
    }

    /// <summary>保存していた内容を返す</summary>
    /// <returns></returns>
    public string GetSaveData()
    {
        return _save._getItemNameSaveData;
    }


    /// <summary>セーブする</summary>
    public override bool Save()
    {
        //Listの中身を、,区切りの文字列で保存
        _save = new ItemSaveData(_saveDataString);
        Debug.Log("所持しているアイテムをセーブしました" + _save._getItemNameSaveData);

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    /// <summary>ロードする</summary>
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

            Debug.Log("所持しているアイテムをロードしました" + _save._getItemNameSaveData);

        }
        return true;
    }
}
