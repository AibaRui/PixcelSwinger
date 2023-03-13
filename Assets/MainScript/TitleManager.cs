using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("ゲームシーン")]
    [SerializeField] string _gameSceneName = "";

    [Header("NewGameのゲームシーン")]
    [SerializeField] string _newGameSceneName = "";

    [Header("FREEのゲームシーン")]
    [SerializeField] string _freeSceneName = "";

    [Header("セーブデータが無いことを警告するパネル")]
    [SerializeField] GameObject _panelNoSaveData;




    /// <summary>コンティニュー</summary>
    public void Continue()
    {
        //セーブデータがある
        if (CheckSaveDataExistence.Instance.SaveDataCheck())
        {
            //Couninueして始めるという事を示す
            CheckSaveDataExistence.s_isNewGame = false;

            //セーブデータがある
            CheckSaveDataExistence.Instance.SetIsSave(true);
            CheckSaveDataExistence.Instance.Save();

            //シーンを読み込む
            SceneManager.LoadScene(_gameSceneName);
        }
        //セーブデータがない場合、警告パネルを出す
        else 
        {
            _panelNoSaveData.SetActive(true);
        }
    }

    /// <summary>NewGameで始める</summary>
    public void NewGame()
    {
        //NewGameで始めることを示す
        CheckSaveDataExistence.s_isNewGame = true;

        //セーブデータがない
        CheckSaveDataExistence.Instance.SetIsSave(false);
        CheckSaveDataExistence.Instance.Save();

        //シーンを読み込む
        SceneManager.LoadScene(_newGameSceneName);
    }

    public void FreeGame()
    {
        //シーンを読み込む
        SceneManager.LoadScene(_freeSceneName);
    }
}
