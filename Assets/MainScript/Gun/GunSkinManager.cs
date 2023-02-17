using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>�X�N���^�u���I�u�W�F�N�g�̏�������
/// �e�̃X�L���̒ǉ�������N���X
/// �C���x���g���ւ̎����́AInventoryManager���Q�Ƃ�
///�������Ŏ���</summary>
public class GunSkinManager : MonoBehaviour, ISave
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<GunSkinDataInformation> _gunSkinDatas = new List<GunSkinDataInformation>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("���ݎ����Ă���X�L����nameID������")]
    private HashSet<string> _getGunSkins = new HashSet<string>();

    [Header("������Ԃ̎����Ă�e�̃X�L��")]
    [SerializeField] private List<string> _firstgunSkinDatas = new List<string>();

    [Header("�Z�[�u�}�l�[�W���[")]
    [SerializeField] private GunSkinDataSaveManager _gunSkinDataSaveManager;


    [Header("�Z�[�u�}�l�[�W���[")]
    [SerializeField] private SaveManager _saveManager;

    [Header("����̃A�C�e���ǉ��̓Z�[�u���Ȃ�")]
    private bool _isFirstLoad = true;


    public List<string> FirstSkinData => _firstgunSkinDatas;

    //SaveData�p
    private HashSet<string> _getGunSkinSaveData = new HashSet<string>();

    //SaveData�p
    public HashSet<string> GetGunSkinSaveData => _getGunSkinSaveData;


    private void Start()
    {

    }
    /// <summary>�C���^�[�t�F�C�X�B�f�[�^�̃��[�h�𑵂��邽�߂̊֐�</summary>
    public void FistDataLodeOnGameStart()
    {
        CheckSaveData();
    }
    /// <summary>�Q�[���̂͂��߂ɌĂԁBNewGame���R���e�B�j���[�ɂ���ă��[�h����f�[�^��ς���</summary>
    public void CheckSaveData()
    {
        ///NewGame�͏����A�C�e�������[�h
        if (CheckSaveDataExistence.s_isNewGame)
        {
            foreach (var skin in _firstgunSkinDatas)
            {
                AddGunSkin(skin);
            }
            Debug.Log("����_GunSkinManager������:" + _firstgunSkinDatas);
        }
        else        ///Contunue�̓A�C�e�������[�h
        {
            var data = _gunSkinDataSaveManager.GetSaveData().Split(",");

            foreach (var skin in data)
            {
                AddGunSkin(skin);
            }
            Debug.Log("����_GunSkinManager�R���e�B�j���[:" + _getGunSkinSaveData);
        }

        _isFirstLoad = false;
    }



    //�A�C�e����ǉ�����
    public void AddGunSkin(string nameID)
    {
        //ID�����������ǂ����`�F�b�N
        if (!CheckItem(nameID))
        {
            return;
        }

        //���O�������Ă��邩�m�F
        foreach (var gunsSkin in _gunSkinDatas)
        {
            if (gunsSkin.NameId == nameID)
            {
                //�����ċ��Ȃ�������ǉ�
                if (!_getGunSkins.Contains(nameID))
                {
                    _inventoryManager.InventoryAddGunSkin(gunsSkin.Name, gunsSkin.GunSkinImage);
                    _getGunSkins.Add(nameID);

                    _getGunSkinSaveData.Add(nameID);
                }
            }
        }

        //�f�[�^���Z�[�u(����̃Z�[�u�m�F���̓Z�[�u���Ȃ�)
        if (!_isFirstLoad)
        {
            _gunSkinDataSaveManager.SaveDataUpDate(_getGunSkinSaveData.ToArray());
            _saveManager.DaveSave();
        }
    }

    public bool CheckItem(string name)
    {
        foreach (var a in _gunSkinDatas)
        {
            if (a.NameId == name)
            {
                return true;
            }
        }
        Debug.Log($"{name}�͖������A���O���Ⴂ�܂�");
        return false;
    }
}
