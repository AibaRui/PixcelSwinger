using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("�����ł��Ă���A�C�e��")]
    [SerializeField] private List<ItemDataInformation> _firstGetItem = new List<ItemDataInformation>();

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

     ItemDatas ItemData = ItemDatas.Karaage;

    private void Start()
    {
        foreach(var item in _firstGetItem)
        {
            AddItem(item.NameId);
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

            //���O�������Ă�����ǉ�
            foreach(var item in _itemDatas)
            {
                if(item.NameId== name)
                {
                    _inventoryManager.InventoryAddItem(item.Name,item.Information, item.ItemUIPanel);
                }
            }
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
        Debug.LogError($"{name}�͖������A���O���Ⴂ�܂�");
        return false;
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}