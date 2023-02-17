using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GunSkinDataSaveManager : MonoBehaviour
{

    [SerializeField] GunSkinManager _gunSkinManager;

    //ファイルのパス
    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".gunskinsavedata.json";

        //NewGameの際は、初期装備を持たせる
        if (CheckSaveDataExistence.s_isNewGame) NewGameLoad();
        //Continuの場合は、セーブデータから読み込む
        else Load();
    }

    // 省略。以下のSave関数やLoad関数を呼び出して使用すること

    public void Save()
    {
        var data = string.Join(',', _gunSkinManager.GetGunSkinSaveData.ToArray());
        GunSkinSaveData _save = new GunSkinSaveData(data);
        Debug.Log("所持している銃のスキンをセーブしました");

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();


    }

    public void Load()
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
            GunSkinSaveData _save = JsonUtility.FromJson<GunSkinSaveData>(data);
            var lodeDta = _save._getGunSkinNameSaveData.Split(",");
            //持っているスキンを追加する
            _gunSkinManager.DataLode(lodeDta);

            Debug.Log("所持している銃のスキンをロードしました");
        }
    }

    /// <summary>NewGameの際のデータロード
    /// 初期で持っているものを追加する</summary>
    public void NewGameLoad()
    {
        _gunSkinManager.DataLode(_gunSkinManager.FirstSkinData.ToArray());
    }

}
