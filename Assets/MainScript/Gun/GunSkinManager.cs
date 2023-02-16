using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�X�N���^�u���I�u�W�F�N�g�̏�������
/// �e�̃X�L���̒ǉ�������N���X
/// �C���x���g���ւ̎����́AInventoryManager���Q�Ƃ�
///�������Ŏ���</summary>
public class GunSkinManager : MonoBehaviour
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<GunSkinDataInformation> _gunSkinDatas = new List<GunSkinDataInformation>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("���ݎ����Ă���X�L����nameID������")]
    private HashSet<string> _getGunSkins = new HashSet<string>();

    [Header("�����Ŏ����Ă���e�̃X�L��")]
    [SerializeField] List<GunSkinDataInformation> _firstgunSkinDatas = new List<GunSkinDataInformation>();


    private void Start()
    {
        foreach(var skin in _firstgunSkinDatas)
        {
            AddGunSkin(skin.NameId);
        }
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
                }
            }
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
        Debug.LogError($"{name}�͖������A���O���Ⴂ�܂�");
        return false;
    }
}
