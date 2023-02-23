using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class AudioSettingSaveManager : MonoBehaviour
{

    [Tooltip("�Z�[�u���������e��ێ�����N���X")]
    private AudioVolumeData _save;

    public AudioVolumeData SaveData => _save;


    //�t�@�C���̃p�X
    string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".ausiosettingsavedata.json";
        Load();
    }

    /// <summary>�Z�[�u���e���X�V���A�Z�[�u����</summary>
    public void Save(float masterVolume, float bgmVolume, float systemVolume, float gameEffectVolume, bool isChange)
    {
        Debug.Log($"���ʐݒ���Z�[�u�B�S��:{masterVolume}/BGM:{bgmVolume}/System:{systemVolume}/�Q�[��:{gameEffectVolume}");

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
            //�t�@�C���̓ǂݍ��݂��s���N���X
            StreamReader streamReader;

            //�����A�ꏊ�����ăC���X�^���X���m�ۂ��Ă���
            streamReader = new StreamReader(filePath);

            //�t�@�C���𖖔��܂ň�x�ɓǂݍ��ނ��߂́uReadToEnd���\�b�h�v
            string data = streamReader.ReadToEnd();
            //����
            streamReader.Close();

            //�f�[�^�𕜌�
            _save = JsonUtility.FromJson<AudioVolumeData>(data);

            Debug.Log($"���ʐݒ�����[�h�B�S��:{_save._masterVolume}/BGM:{_save._bgmVolume}/System:{_save._systemEffectVolume}/�Q�[��:{_save._gameEffectVolume}");
        }
    }
}
