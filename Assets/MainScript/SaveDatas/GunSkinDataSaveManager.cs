using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GunSkinDataSaveManager : SaveManagerBase
{
    [Tooltip("�Z�[�u���������e")]
    private string _saveDataString;

    [Tooltip("�Z�[�u���������e��ێ�����N���X")]
    GunSkinSaveData _save;

    //�t�@�C���̃p�X
    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".gunskinsavedata.json";
    }

    /// <summary>�Z�[�u������e���X�V����</summary>
    /// <param name="saveData">�Z�[�u�������f�[�^</param>
    public void SaveDataUpDate(string[] saveData)
    {
        _saveDataString = string.Join(",", saveData);
        _saveDataString = string.Join(",", saveData);
        Debug.Log("�A�C�e���̕ۑ��f�[�^���X�V" + _saveDataString);
    }

    /// <summary>�ۑ����Ă������e��Ԃ�</summary>
    /// <returns></returns>
    public string GetSaveData()
    {
        return _save._getGunSkinNameSaveData;
    }

    // �ȗ��B�ȉ���Save�֐���Load�֐����Ăяo���Ďg�p���邱��
    public override bool Save()
    {
        _save = new GunSkinSaveData(_saveDataString);
        Debug.Log("�������Ă���e�̃X�L�����Z�[�u���܂���" + _save._getGunSkinNameSaveData);

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    public override bool Load()
    {
        if (File.Exists(filePath))
        {
            //�t�@�C���̓ǂݍ��݂��s���N���X
            StreamReader streamReader;

            //�����A�ꏊ�����ăC���X�^���X���m�ۂ��Ă���
            streamReader = new StreamReader(filePath);

            //�t�@�C���𖖔��܂ň�x�ɓǂݍ��ނ��߂́uReadToEnd���\�b�h�v
            string data = streamReader.ReadToEnd();
            //����
            streamReader.Close();

            //�f�[�^�𕜌�
            _save = JsonUtility.FromJson<GunSkinSaveData>(data);

            _saveDataString = _save._getGunSkinNameSaveData;

            Debug.Log("�������Ă���e�̃X�L�������[�h���܂���" + _save._getGunSkinNameSaveData);
        }
        return true;
    }
}
