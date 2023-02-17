using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SaveManager : MonoBehaviour
{
    [Tooltip("�f�o�b�O���̓Z�[�u�f�[�^���Ȃ�")]
    [SerializeField] bool _isSave = false;

    [Tooltip("�Z�[�u�}�l�[�W���[")]
    [SerializeField] private List<SaveManagerBase> _saveManagers = new List<SaveManagerBase>();

    [Tooltip("ISave���p�������A�ŏ��Ƀf�[�^�̃��[�h���K�v�Ȃ���")]
    [SerializeField] private List<GameObject> _iSaves = new List<GameObject>();

    [Tooltip("���[�h����������܂ő҂�ʂ̃p�l��")]
    [SerializeField] private GameObject _waitLodeingPanel;

    [Tooltip("�Z�[�u���Ă��鎖�������摜")]
    [SerializeField] private GameObject _savingPanel;

    private void Awake()
    {
        StartCoroutine(DataLode());
    }


    IEnumerator SaveControl()
    {
        //�Z�[�u���������p�l�����o��
        _savingPanel.SetActive(true);

        //���ׂẴf�[�^�����[�h����܂ő҂�
        foreach (var a in _saveManagers)
        {
            yield return new WaitUntil(() => a.Save());
        }

        _savingPanel.SetActive(false);
    }

    IEnumerator DataLode()
    {
        //�R���e�B�j���[�Ŏn�߂��ꍇ
        if (_isSave && !CheckSaveDataExistence.s_isNewGame)
        {
            _waitLodeingPanel.SetActive(true);
            //���ׂẴf�[�^�����[�h����܂ő҂�
            foreach (var a in _saveManagers)
            {
                yield return new WaitUntil(() => a.Load());
            }

            //�f�[�^���[�h���K�v�Ȃ��́A���[�h������
            foreach (var data in _iSaves)
            {
                ISave save = data.GetComponent<ISave>();
                save.FistDataLodeOnGameStart();
            }
            _waitLodeingPanel.SetActive(false);
        }
        else
        {
            //�f�[�^���[�h���K�v�Ȃ��́A���[�h������
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
            Debug.Log("�Z�[�u���J�n���܂�");
            CheckSaveDataExistence.Instance.SetIsSave(true);
            CheckSaveDataExistence.Instance.Save();

            StartCoroutine(SaveControl());
        }
    }

}
