using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("スクリタブルオブジェクト")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("インベントリマネージャー")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("初期でもているアイテム")]
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

    //アイテムを追加する
    public void AddItem(string name)
    {
        //IDが正しいかどうかチェック
        if (!CheckItem(name))
        {
            return;
        }

        //既にあったら個数を増やす(個数のシステムは現在実装していない)
        if (_getItems.ContainsKey(name))
        {
            _getItems[name]++;
        }
        else
        {
            //Dictionaryに追加
            _getItems.Add(name, 1);

            //名前があっていたら追加
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
        Debug.LogError($"{name}は無いか、名前が違います");
        return false;
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}