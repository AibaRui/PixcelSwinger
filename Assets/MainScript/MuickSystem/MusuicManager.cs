using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusuicManager : MonoBehaviour
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<MusicData> _musicDatas = new List<MusicData>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("������Ԃ̃A�C�e��")]
    [SerializeField] private List<string> _firstGetItem = new List<string>();

    [Header("�Z�[�u�}�l�[�W���[")]
    [SerializeField] private SaveManager _saveManager;

    public List<string> FirstGetItem => _firstGetItem;

    //���ݎ����Ă����
    private HashSet<string> _getMusics = new HashSet<string>();

    public HashSet<string> GetMusics => _getMusics;

    //SaveData�p
    private HashSet<string> _getItemSaveData = new HashSet<string>();

    //SaveData�p
    public HashSet<string> GetItemSaveData => _getItemSaveData;

    private bool _isGetWalkMan = false;

    public bool IsGetWalkMan => _isGetWalkMan;


    // private bool _isMusicPlaying

    public bool IsWalkMan => _isGetWalkMan;


    [SerializeField] PlayerInput _playerInput;

    [SerializeField] private MusicPlayControler _musicPlayControler;



    


    /// <summary>
    /// SaveManager����ĂԁB
    /// �f�[�^��ǂݍ���ŃZ�b�g����</summary>
    /// <param name="data"></param>
    public void DataLode(string[] data)
    {
        if (data != null)
        {
            foreach (var music in data)
            {
                _getItemSaveData.Add(music);
                AddMusic(music);
            }
        }
    }


    //�A�C�e����ǉ�����
    public void AddMusic(string name)
    {
        //ID�����������ǂ����`�F�b�N
        if (!CheckMusicNameID(name))
        {
            return;
        }

        if (!_getMusics.Contains(name))
        {
            _getMusics.Add(name);

            //SaveData�p
            _getItemSaveData.Add(name);

            //ID�������T��
            foreach (var item in _musicDatas)
            {
                if (item.NameId == name)
                {
                    //_inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }
            //�f�[�^���Z�[�u
            _saveManager.DaveSave();
        }
    }

    public bool CheckMusicNameID(string name)
    {
        foreach (var a in _musicDatas)
        {
            if (a.NameId == name)
            {
                return true;
            }
        }
        Debug.LogWarning($"{name}�͖������A���O���Ⴂ�܂�");
        return false;
    }

}
