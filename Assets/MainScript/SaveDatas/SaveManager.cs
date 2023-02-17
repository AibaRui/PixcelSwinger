using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SaveManager : MonoBehaviour
{
    [Tooltip("デバッグ中はセーブデータしない")]
    [SerializeField] bool _isSave = false;

    [Tooltip("セーブマネージャー")]
    [SerializeField] private List<SaveManagerBase> _saveManagers = new List<SaveManagerBase>();

    [Tooltip("ISaveを継承した、最初にデータのロードが必要なもの")]
    [SerializeField] private List<GameObject> _iSaves = new List<GameObject>();

    [Tooltip("ロードが完了するまで待つ画面のパネル")]
    [SerializeField] private GameObject _waitLodeingPanel;

    [Tooltip("セーブしている事を示す画像")]
    [SerializeField] private GameObject _savingPanel;

    private void Awake()
    {
        StartCoroutine(DataLode());
    }


    IEnumerator SaveControl()
    {
        //セーブ中を示すパネルを出す
        _savingPanel.SetActive(true);

        //すべてのデータをロードするまで待つ
        foreach (var a in _saveManagers)
        {
            yield return new WaitUntil(() => a.Save());
        }

        _savingPanel.SetActive(false);
    }

    IEnumerator DataLode()
    {
        //コンティニューで始めた場合
        if (_isSave && !CheckSaveDataExistence.s_isNewGame)
        {
            _waitLodeingPanel.SetActive(true);
            //すべてのデータをロードするまで待つ
            foreach (var a in _saveManagers)
            {
                yield return new WaitUntil(() => a.Load());
            }

            //データロードが必要なもの、ロードをする
            foreach (var data in _iSaves)
            {
                ISave save = data.GetComponent<ISave>();
                save.FistDataLodeOnGameStart();
            }
            _waitLodeingPanel.SetActive(false);
        }
        else
        {
            //データロードが必要なもの、ロードをする
            foreach (var data in _iSaves)
            {
                ISave save = data.GetComponent<ISave>();
                save.FistDataLodeOnGameStart();
            }
        }
    }



    public void DaveSave()
    {
        if (_isSave)
        {
            Debug.Log("セーブを開始します");
            CheckSaveDataExistence.Instance.SetIsSave(true);
            CheckSaveDataExistence.Instance.Save();

            StartCoroutine(SaveControl());
        }
    }

}
