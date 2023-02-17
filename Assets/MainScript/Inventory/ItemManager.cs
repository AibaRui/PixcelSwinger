using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("スクリタブルオブジェクト")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("インベントリマネージャー")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("初期状態のアイテム")]
    [SerializeField] private List<string> _firstGetItem = new List<string>();

    [Header("セーブマネージャー")]
    [SerializeField] private SaveManager _saveManager;

    public List<string> FirstGetItem => _firstGetItem;

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

    ItemDatas ItemData = ItemDatas.Karaage;

    //SaveData用
    private HashSet<string> _getItemSaveData = new HashSet<string>();

    //SaveData用
    public HashSet<string> GetItemSaveData => _getItemSaveData;



    private void Start()
    {
        //foreach(var item in _firstGetItem)
        //{
        //    AddItem(item);
        //}
    }


    /// <summary>
    /// SaveManagerから呼ぶ。
    /// データを読み込んでセットする</summary>
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

            //SaveData用
            _getItemSaveData.Add(name);

            //名前があっていたら追加
            foreach (var item in _itemDatas)
            {
                if (item.NameId == name)
                {
                    _inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }
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

            //SaveData用
            _getItemSaveData.Add(name);

            //名前があっていたら追加
            foreach (var item in _itemDatas)
            {
                if (item.NameId == name)
                {
                    _inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }
            //データをセーブ
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
        Debug.LogWarning($"{name}は無いか、名前が違います");
        return false;
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}