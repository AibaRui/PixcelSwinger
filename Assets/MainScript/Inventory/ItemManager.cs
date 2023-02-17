using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("������Ԃ̃A�C�e��")]
    [SerializeField] private List<string> _firstGetItem = new List<string>();

    [Header("�Z�[�u�}�l�[�W���[")]
    [SerializeField] private SaveManager _saveManager;

    public List<string> FirstGetItem => _firstGetItem;

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

    ItemDatas ItemData = ItemDatas.Karaage;

    //SaveData�p
    private HashSet<string> _getItemSaveData = new HashSet<string>();

    //SaveData�p
    public HashSet<string> GetItemSaveData => _getItemSaveData;



    private void Start()
    {
        //foreach(var item in _firstGetItem)
        //{
        //    AddItem(item);
        //}
    }


    /// <summary>
    /// SaveManager����ĂԁB
    /// �f�[�^��ǂݍ���ŃZ�b�g����</summary>
    /// <param name="data"></param>
    public void DataLode(string[] data)
    {
        if (data != null)
        {
            foreach (var item in data)
            {
                _getItemSaveData.Add(item);
                AddFirstItem(item);
            }
        }
    }


    public void AddFirstItem(string name)
    {
        //ID�����������ǂ����`�F�b�N
        if (!CheckItem(name))
        {
            return;
        }

        //���ɂ���������𑝂₷(���̃V�X�e���͌��ݎ������Ă��Ȃ�)
        if (_getItems.ContainsKey(name))
        {
            _getItems[name]++;
        }
        else
        {
            //Dictionary�ɒǉ�
            _getItems.Add(name, 1);

            //SaveData�p
            _getItemSaveData.Add(name);

            //���O�������Ă�����ǉ�
            foreach (var item in _itemDatas)
            {
                if (item.NameId == name)
                {
                    _inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }
        }
    }

    //�A�C�e����ǉ�����
    public void AddItem(string name)
    {
        //ID�����������ǂ����`�F�b�N
        if (!CheckItem(name))
        {
            return;
        }

        //���ɂ���������𑝂₷(���̃V�X�e���͌��ݎ������Ă��Ȃ�)
        if (_getItems.ContainsKey(name))
        {
            _getItems[name]++;
        }
        else
        {
            //Dictionary�ɒǉ�
            _getItems.Add(name, 1);

            //SaveData�p
            _getItemSaveData.Add(name);

            //���O�������Ă�����ǉ�
            foreach (var item in _itemDatas)
            {
                if (item.NameId == name)
                {
                    _inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }
            //�f�[�^���Z�[�u
            _saveManager.DaveSave();
        }
    }

    public bool CheckItem(string name)
    {
        foreach (var a in _itemDatas)
        {
            if (a.NameId == name)
            {
                return true;
            }
        }
        Debug.LogWarning($"{name}�͖������A���O���Ⴂ�܂�");
        return false;
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}