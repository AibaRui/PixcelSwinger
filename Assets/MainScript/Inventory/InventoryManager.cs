using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("インベントリのパネル")]
    [SerializeField] private GameObject _inventoryPanel;

    [Header("アイテム欄")]
    [SerializeField] private LayoutGroup _itemlayoutGroup;

    [Header("アイテムのパネルのおおもと")]
    [SerializeField] private GameObject _itemPanelOrizin;

    [Header("アイテム欄の大きいアイコン")]
    [SerializeField] private Image _itemBigImage;
    [SerializeField] private GameObject _big;


    [Header("アイテム欄の情報")]
    [SerializeField] private Text _itemInforText;  

    [Header("アイテム欄の名前")]
    [SerializeField] private Text _itemNameText;

    public LayoutGroup ItemlayoutGroup  { get => _itemlayoutGroup; set => _itemlayoutGroup = value; }




    public void InventoryOpen()
    {
        _inventoryPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void InventoryClose()
    {
        _inventoryPanel.SetActive(false);
        Cursor.visible = false;
    }


    /// <summary>インベントリ欄にアイテムを追加する</summary>
    /// <param name="name">アイテムの名前</param>
    /// <param name="itemPanel">表示するアイテムのパネル</param>
    public void SetItemPanel(string name,string information, Sprite itemPanel)
    {
        var go = Instantiate(_itemPanelOrizin);
        
        Image sprite = go.transform.GetChild(0).GetComponent<Image>();
        sprite.sprite = itemPanel;

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
