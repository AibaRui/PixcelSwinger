using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GunSkinDataSaveManager : MonoBehaviour
{

    [SerializeField] GunSkinManager _gunSkinManager;

    //�t�@�C���̃p�X
    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".gunskinsavedata.json";

        //NewGame�̍ۂ́A������������������
        if (CheckSaveDataExistence.s_isNewGame) NewGameLoad();
        //Continu�̏ꍇ�́A�Z�[�u�f�[�^����ǂݍ���
        else Load();
    }

    // �ȗ��B�ȉ���Save�֐���Load�֐����Ăяo���Ďg�p���邱��

    public void Save()
    {
        var data = string.Join(',', _gunSkinManager.GetGunSkinSaveData.ToArray());
        GunSkinSaveData _save = new GunSkinSaveData(data);
        Debug.Log("�������Ă���e�̃X�L�����Z�[�u���܂���");

        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();


    }

    public void Load()
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
            GunSkinSaveData _save = JsonUtility.FromJson<GunSkinSaveData>(data);
            var lodeDta = _save._getGunSkinNameSaveData.Split(",");
            //�����Ă���X�L����ǉ�����
            _gunSkinManager.DataLode(lodeDta);

            Debug.Log("�������Ă���e�̃X�L�������[�h���܂���");
        }
    }

    /// <summary>NewGame�̍ۂ̃f�[�^���[�h
    /// �����Ŏ����Ă�����̂�ǉ�����</summary>
    public void NewGameLoad()
    {
        _gunSkinManager.DataLode(_gunSkinManager.FirstSkinData.ToArray());
    }

}
