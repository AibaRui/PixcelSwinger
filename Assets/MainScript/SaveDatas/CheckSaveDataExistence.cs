using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class CheckSaveDataExistence : SingletonMonoBehaviour<CheckSaveDataExistence>
{
    string filePath;

    SaveDataExistence _save;

    [Tooltip("�R���e�B�j���[���ŏ����炩�ǂ��������ʂ��邽�߂̕ϐ�")]
    public static bool s_isNewGame = true;

    [Tooltip("�Z�[�u�f�[�^�̗L��������")]
    public bool _isSave = false;

    protected override bool dontDestroyOnLoad { get { return true; } }



    private void Start()
    {
        //filePath�́A�Z�[�u����f�[�^���Ƃɕς���
        filePath = Application.persistentDataPath + "/" + ".savedata.json";

        //Data��ǂݍ���
        Load();
    }

    /// <summary>�{�^���ŌĂԁB</summary>
    /// <param name="isNewGame"></param>
    public void ChangeNewGame(bool isNewGame)
    {
        s_isNewGame = isNewGame;
    }


    /// <summary>�Z�[�u�f�[�^�����邩�ǂ������m�F����</summary>
    public bool SaveDataCheck()
    {
        if (_save == null)
        {
            Debug.Log("null�ł�");
            return false;

        }
        else
        {
            return _save._isSaveDAta;
        }
    }

    /// <summary>���̃N���X��Save�֐����ĂԑO�ɌĂԁB
    /// �Z�[�u�f�[�^�̗L�������߂邽��</summary>
    /// <param name="isSave"></param>
    public void SetIsSave(bool isSave)
    {
        _isSave = isSave;
    }



    //�Z�[�u�f�[�^�̗L���̏���ۑ�����B
    public void Save()
    {
        _save = new SaveDataExistence(_isSave);
        Debug.Log("Save���܂���:" + _save._isSaveDAta);

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();

    }

    //�Z�[�u�f�[�^�̗L���̏������[�h����B
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
