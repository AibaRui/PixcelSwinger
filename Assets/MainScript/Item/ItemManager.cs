using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour, ISave
{
    [Header("スクリタブルオブジェクト")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("インベントリマネージャー")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("初期状態のアイテム")]
    [SerializeField] private List<string> _firstGetItem = new List<string>();

    [Header("アイテムのセーブマネージャー")]
    [SerializeField] private ItemDataSaveManager _itemDataSaveManager;


    [Header("セーブマネージャー")]
    [SerializeField] private SaveManager _saveManager;

    public List<string> FirstGetItem => _firstGetItem;

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

    ItemDatas ItemData = ItemDatas.Karaage;

    //SaveData用
    private HashSet<string> _getItemSaveData = new HashSet<string>();

    //SaveData用
    public HashSet<string> GetItemSaveData => _getItemSaveData;

    [Header("初回のアイテム追加はセーブしない")]
    private bool _isFirstLoad = true;

    private void Start()
    {

    }
    /// <summary>インターフェイス。データのロードを揃えるための関数</summary>
    public void FistDataLodeOnGameStart()
    {
        CheckSaveData();
    }

    /// <summary>ゲームのはじめに呼ぶ。NewGameかコンティニューによってロードするデータを変える</summary>
    public void CheckSaveData()
    {
        ///NewGameは初期アイテムをロード
        if (CheckSaveDataExistence.s_isNewGame)
        {
            foreach (var item in _firstGetItem)
            {
                AddItem(item);
            }
            Debug.Log("初回_ItemManager初期化:" + _firstGetItem);
        }
        else        ///Contunueはアイテムをロード
        {
            var data = _itemDataSaveManager.GetSaveData().Split(",");

            foreach (var item in data)
            {
                AddItem(item);
            }
            Debug.Log("初回_ItemManagerコンティニュー:" + _getItemSaveData);
        }

        _isFirstLoad = false;
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

                _itemDataSaveManager.SaveDataUpDate(_getItemSaveData.ToArray());
            //データをセーブ(初回のセーブ確認時はセーブしない)
            if (!_isFirstLoad)
            {

                _saveManager.DaveSave();
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
        Debug.LogWarning($"{name}は無いか、名前が違います");
        return false;
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}