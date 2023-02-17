using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SaveManager : MonoBehaviour
{
    [SerializeField] UnityEvent _save;

    [Tooltip("デバッグ中はセーブデータしない")]
    [SerializeField] bool _isSave = false;


    public void DaveSave()
    {
        if (_isSave)
        {
            Debug.Log("セーブを開始します");
            CheckSaveDataExistence.Instance.SetIsSave(true);
            CheckSaveDataExistence.Instance.Save();
            _save?.Invoke();
        }
    }

}
