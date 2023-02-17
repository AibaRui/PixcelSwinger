using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GunSkinDataSaveManager : SaveManagerBase
{
    [Tooltip("セーブしたい内容")]
    private string _saveDataString;

    [Tooltip("セーブしたい内容を保持するクラス")]
    GunSkinSaveData _save;

    //ファイルのパス
    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".gunskinsavedata.json";
    }

    /// <summary>セーブする内容を更新する</summary>
    /// <param name="saveData">セーブしたいデータ</param>
    public void SaveDataUpDate(string[] saveData)
    {
        _saveDataString = string.Join(",", saveData);
        _saveDataString = string.Join(",", saveData);
        Debug.Log("アイテムの保存データを更新" + _saveDataString);
    }

    /// <summary>保存していた内容を返す</summary>
    /// <returns></returns>
    public string GetSaveData()
    {
        return _save._getGunSkinNameSaveData;
    }

    // 省略。以下のSave関数やLoad関数を呼び出して使用すること
    public override bool Save()
    {
        _save = new GunSkinSaveData(_saveDataString);
        Debug.Log("所持している銃のスキンをセーブしました" + _save._getGunSkinNameSaveData);

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
            //ファイルの読み込みを行うクラス
            StreamReader streamReader;

            //多分、場所を入れてインスタンスを確保している
            streamReader = new StreamReader(filePath);

            //ファイルを末尾まで一度に読み込むための「ReadToEndメソッド」
            string data = streamReader.ReadToEnd();
            //閉じる
            streamReader.Close();

            //データを復元
            _save = JsonUtility.FromJson<GunSkinSaveData>(data);

            _saveDataString = _save._getGunSkinNameSaveData;

            Debug.Log("所持している銃のスキンをロードしました" + _save._getGunSkinNameSaveData);
        }
        return true;
    }
}
