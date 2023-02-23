using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class AudioSettingSaveManager : MonoBehaviour
{

    [Tooltip("セーブしたい内容を保持するクラス")]
    private AudioVolumeData _save;

    public AudioVolumeData SaveData => _save;


    //ファイルのパス
    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".ausiosettingsavedata.json";
        Load();
    }

    /// <summary>セーブ内容を更新し、セーブする</summary>
    public void Save(float masterVolume, float bgmVolume, float systemVolume, float gameEffectVolume, bool isChange)
    {
        Debug.Log($"音量設定をセーブ。全体:{masterVolume}/BGM:{bgmVolume}/System:{systemVolume}/ゲーム:{gameEffectVolume}");

        _save = new AudioVolumeData(masterVolume, bgmVolume, gameEffectVolume, systemVolume, isChange);

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
            _save = JsonUtility.FromJson<AudioVolumeData>(data);

            Debug.Log($"音量設定をロード。全体:{_save._masterVolume}/BGM:{_save._bgmVolume}/System:{_save._systemEffectVolume}/ゲーム:{_save._gameEffectVolume}");
        }
    }
}
