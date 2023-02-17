using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class CheckSaveDataExistence : SingletonMonoBehaviour<CheckSaveDataExistence>
{
    string filePath;

    SaveDataExistence _save;

    [Tooltip("コンティニューか最初からかどうかを識別するための変数")]
    public static bool s_isNewGame = true;

    [Tooltip("セーブデータの有無を示す")]
    public bool _isSave = false;

    protected override bool dontDestroyOnLoad { get { return true; } }



    private void Start()
    {
        //filePathは、セーブするデータごとに変える
        filePath = Application.persistentDataPath + "/" + ".savedata.json";

        //Dataを読み込む
        Load();
    }

    /// <summary>ボタンで呼ぶ。</summary>
    /// <param name="isNewGame"></param>
    public void ChangeNewGame(bool isNewGame)
    {
        s_isNewGame = isNewGame;
    }


    /// <summary>セーブデータがあるかどうかを確認する</summary>
    public bool SaveDataCheck()
    {
        if (_save == null)
        {
            Debug.Log("nullです");
            return false;

        }
        else
        {
            return _save._isSaveDAta;
        }
    }

    /// <summary>このクラスのSave関数を呼ぶ前に呼ぶ。
    /// セーブデータの有無を決めるため</summary>
    /// <param name="isSave"></param>
    public void SetIsSave(bool isSave)
    {
        _isSave = isSave;
    }



    //セーブデータの有無の情報を保存する。
    public void Save()
    {
        _save = new SaveDataExistence(_isSave);
        Debug.Log("Saveしました:" + _save._isSaveDAta);

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();

    }

    //セーブデータの有無の情報をロードする。
    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<SaveDataExistence>(data);
            Debug.Log(_save._isSaveDAta);
        }
    }



}
