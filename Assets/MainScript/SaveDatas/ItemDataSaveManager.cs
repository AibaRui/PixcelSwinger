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

        //NewGameの際は、初期装備を持たせる
        if (CheckSaveDataExistence.s_isNewGame) FirstStart();
        //Continueの際は、セーブデータから読み込む
        else Load();
    }


    public void Save()
    {   
        //Listの中身を、,区切りの文字列に変換して保存
        string data = string.Join(",", _itemManager.GetItemSaveData.ToArray());
        _save = new ItemSaveData(data);

        Debug.Log("所持しているアイテムをセーブしました");

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

            Debug.Log("所持しているアイテムをロードしました");
            var LodeData = _save._getItemNameSaveData.Split(",");
            _itemManager.DataLode(LodeData);
        }
    }


    public void FirstStart()
    {
        _itemManager.DataLode(_itemManager.FirstGetItem.ToArray());
    }

}
