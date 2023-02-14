using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

    private List<GameObject> _items = new List<GameObject>();

    public List<GameObject> Items => _items;

     ItemDatas ItemData = ItemDatas.Karaage;

 
    //�A�C�e����ǉ�����
    public void AddItem(string name)
    {
        //���ɂ���������𑝂₷(���̃V�X�e���͌��ݎ������Ă��Ȃ�)
        if(_getItems.ContainsKey(name))
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
                    _inventoryManager.SetItemPanel(item.Name,item.Information, item.ItemUIPanel);
                }
            }
        }
    }

    public void CheckItem(string name)
    {
        foreach(var a in _itemDatas)
        {
            if(a.NameId==name)
            {
                return;
            }
        }
        Debug.LogError($"{name}�͖������A���O���Ⴂ�܂�");
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}



class ItemInforMation
{
    private GameObject _itemPanel;

    public GameObject ItemPanel { get => _itemPanel; set => _itemPanel = value; }




}
