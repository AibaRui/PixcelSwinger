using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSettingSaveManager : MonoBehaviour
{
    [Tooltip("セーブしたい内容を保持するクラス")]
    private GameSettingData _save;

    public GameSettingData SaveData => _save;


    //ファイルのパス
    string filePath;

    void Awake()
    {

    }

    public void FirstLode()
    {
        filePath = Application.persistentDataPath + "/" + ".gamesettingsavedata.json";
        Load();
    }


    /// <summary>セーブ内容を更新し、セーブする</summary>
    public void Save(float mouseSensitivity, bool runSetting, int showPanel, bool isSave, bool isShowAsistUI, int operationLevel)
    {
        Debug.Log($"ゲーム設定をセーブ。マウス感度:{mouseSensitivity}/Run設定:{runSetting}/SwinuUI:{showPanel}/セーブ:{isSave}/UI:{isShowAsistUI}/操作レベル:{operationLevel}");

        _save = new GameSettingData(mouseSensitivity, runSetting, showPanel, isSave, isShowAsistUI, operationLevel);

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
            _save = JsonUtility.FromJson<GameSettingData>(data);

            Debug.Log($"ゲーム設定をロード。マウス感度:{_save._mouseSensitivity}/Run設定:{_save._runSettingIsPushChange}/SwinuUI:{_save._showSwingUI}/セーブ:{_save._isSaveData}/UI:{_save._isShowAsistUI} / 操作レベル:{_save._isOperationLevel}");
        }
    }
}
