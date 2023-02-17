using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SaveManager : MonoBehaviour
{
    [SerializeField] UnityEvent _save;

    [Tooltip("�f�o�b�O���̓Z�[�u�f�[�^���Ȃ�")]
    [SerializeField] bool _isSave = false;


    public void DaveSave()
    {
        if (_isSave)
        {
            Debug.Log("�Z�[�u���J�n���܂�");
            CheckSaveDataExistence.Instance.SetIsSave(true);
            CheckSaveDataExistence.Instance.Save();
            _save?.Invoke();
        }
    }

}
