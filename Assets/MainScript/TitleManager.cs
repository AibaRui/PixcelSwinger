using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("�Q�[���V�[��")]
    [SerializeField] string _gameSceneName = "";

    [Header("NewGame�̃Q�[���V�[��")]
    [SerializeField] string _newGameSceneName = "";

    [Header("FREE�̃Q�[���V�[��")]
    [SerializeField] string _freeSceneName = "";

    [Header("�Z�[�u�f�[�^���������Ƃ��x������p�l��")]
    [SerializeField] GameObject _panelNoSaveData;




    /// <summary>�R���e�B�j���[</summary>
    public void Continue()
    {
        //�Z�[�u�f�[�^������
        if (CheckSaveDataExistence.Instance.SaveDataCheck())
        {
            //Couninue���Ďn�߂�Ƃ�����������
            CheckSaveDataExistence.s_isNewGame = false;

            //�Z�[�u�f�[�^������
            CheckSaveDataExistence.Instance.SetIsSave(true);
            CheckSaveDataExistence.Instance.Save();

            //�V�[����ǂݍ���
            SceneManager.LoadScene(_gameSceneName);
        }
        //�Z�[�u�f�[�^���Ȃ��ꍇ�A�x���p�l�����o��
        else 
        {
            _panelNoSaveData.SetActive(true);
        }
    }

    /// <summary>NewGame�Ŏn�߂�</summary>
    public void NewGame()
    {
        //NewGame�Ŏn�߂邱�Ƃ�����
        CheckSaveDataExistence.s_isNewGame = true;

        //�Z�[�u�f�[�^���Ȃ�
        CheckSaveDataExistence.Instance.SetIsSave(false);
        CheckSaveDataExistence.Instance.Save();

        //�V�[����ǂݍ���
        SceneManager.LoadScene(_newGameSceneName);
    }

    public void FreeGame()
    {
        //�V�[����ǂݍ���
        SceneManager.LoadScene(_freeSceneName);
    }
}
