using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSettingSaveManager : MonoBehaviour
{
    [Tooltip("�Z�[�u���������e��ێ�����N���X")]
    private GameSettingData _save;

    public GameSettingData SaveData => _save;


    //�t�@�C���̃p�X
    string filePath;

    void Awake()
    {

    }

    public void FirstLode()
    {
        filePath = Application.persistentDataPath + "/" + ".gamesettingsavedata.json";
        Load();
    }


    /// <summary>�Z�[�u���e���X�V���A�Z�[�u����</summary>
    public void Save(float mouseSensitivity, bool runSetting, int showPanel, bool isSave, bool isShowAsistUI, int operationLevel)
    {
        Debug.Log($"�Q�[���ݒ���Z�[�u�B�}�E�X���x:{mouseSensitivity}/Run�ݒ�:{runSetting}/SwinuUI:{showPanel}/�Z�[�u:{isSave}/UI:{isShowAsistUI}/���샌�x��:{operationLevel}");

        _save = new GameSettingData(mouseSensitivity, runSetting, showPanel, isSave, isShowAsistUI, operationLevel);

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
            _save = JsonUtility.FromJson<GameSettingData>(data);

            Debug.Log($"�Q�[���ݒ�����[�h�B�}�E�X���x:{_save._mouseSensitivity}/Run�ݒ�:{_save._runSettingIsPushChange}/SwinuUI:{_save._showSwingUI}/�Z�[�u:{_save._isSaveData}/UI:{_save._isShowAsistUI} / ���샌�x��:{_save._isOperationLevel}");
        }
    }
}
