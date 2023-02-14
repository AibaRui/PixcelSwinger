using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("スクリタブルオブジェクト")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("インベントリマネージャー")]
    [SerializeField] private InventoryManager _inventoryManager;

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

    private List<GameObject> _items = new List<GameObject>();

    public List<GameObject> Items => _items;

     ItemDatas ItemData = ItemDatas.Karaage;

 
    //アイテムを追加する
    public void AddItem(string name)
    {
        //既にあったら個数を増やす(個数のシステムは現在実装していない)
        if(_getItems.ContainsKey(name))
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
        Debug.LogError($"{name}は無いか、名前が違います");
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
