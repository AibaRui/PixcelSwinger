using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>�C���x���g������UI�̏�������
/// �A�C�e�����A����X�L�����ɐV����
/// �A�C�e���A�X�L����ǉ�����֐�������</summary>
public class InventoryManager : MonoBehaviour
{
    [Header("�C���x���g���̃p�l��")]
    [SerializeField] private GameObject _inventoryPanel;

    [Header("�A�C�e����")]
    [SerializeField] private LayoutGroup _itemlayoutGroup;

    [Header("�A�C�e���̃p�l���̂�������")]
    [SerializeField] private GameObject _itemPanelOrizin;

    [Header("�A�C�e�����̑傫���A�C�R��")]
    [SerializeField] private Image _itemBigImage;

    [SerializeField] private GameObject _big;

    [Header("�A�C�e�����̏��")]
    [SerializeField] private Text _itemInforText;  

    [Header("�A�C�e�����̖��O")]
    [SerializeField] private Text _itemNameText;

    
    [Header("����ꗓ��LayoutGroup")]
    [SerializeField] private LayoutGroup _weaponLayoutGroup;

    [Header("����̃X�L����\������Image")]
    [SerializeField] private Image _gunSkinBigImage;

    [Header("����ꗓ�ɒǉ�����{�^��")]
    [SerializeField] private GameObject _weaponButtun;

    [Header("�����SpriteLendrer")]
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;


    /// <summary>�C���x���g�����J�������̏���</summary>
    public void InventoryOpen()
    {
        //�C���x���g���̃p�l�����J��
        _inventoryPanel.SetActive(true);
        //�}�E�X�J�[�\�����o��
        Cursor.visible = true;
    }

    /// <summary>�C���x���g����������̏���</summary>
    public void InventoryClose()
    {
        //�C���x���g���̃p�l�������
        _inventoryPanel.SetActive(false);
        //�}�E�X�J�[�\��������
        Cursor.visible = false;
    }

    /// <summary>�C���x���g�����̕���X�L�����ɁA�V�����X�L���̒ǉ�����</summary>
    /// <param name="name"></param>
    public void InventoryAddGunSkin(string name,Sprite gunSkinSprite)
    {
        //�ꗗ�ɒǉ�����{�^���𐶐�
        var go = Instantiate(_weaponButtun);
        //���������{�^����Text��ύX
        go.transform.GetChild(0).GetComponent<Text>().text = name;

        //�����ݒ�
        GunSkinUIInformation info = go.GetComponent<GunSkinUIInformation>();
        info.BigItemImage = _gunSkinBigImage;
        info.DefultSprite = _gunSkinBigImage.sprite;
        info.GunSkinSprite = gunSkinSprite;
        info.GunSpriteRendere = _gunSpriteRenderer;

        go.transform.SetParent(_weaponLayoutGroup.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>�C���x���g�����ɃA�C�e����ǉ�����</summary>
    /// <param name="name">�A�C�e���̖��O</param>
    /// <param name="itemPanel">�\������A�C�e���̃p�l��</param>
    public void InventoryAddItem(string name,string information, Sprite itemPanel)
    {
        //�ꗗ�ɒǉ�����A�C�e���̃p�l���̂������Ƃ�ǉ�
        var go = Instantiate(_itemPanelOrizin);
        //���������p�l����Image��ύX
        go.transform.GetChild(0).GetComponent<Image>().sprite = itemPanel;

        //�����ݒ�
        ItemUIShowInformation info = go.GetComponent<ItemUIShowInformation>();
        info.Name = name;
        info.Information = information;
        info.InforMationText = _itemInforText;
        info.NameText = _itemNameText;
        info.BigItemImage = _itemBigImage;
        info.ItemImage = itemPanel;
        info.DefultSprite = _itemBigImage.sprite;

        go.transform.SetParent(_itemlayoutGroup.transform);
    }

}
