using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>インベントリ内のUIの情報を持つ
/// アイテム欄、武器スキン欄に新しく
/// アイテム、スキンを追加する関数を持つ</summary>
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

    
    [Header("武器一欄のLayoutGroup")]
    [SerializeField] private LayoutGroup _weaponLayoutGroup;

    [Header("武器のスキンを表示するImage")]
    [SerializeField] private Image _gunSkinBigImage;

    [Header("武器一欄に追加するボタン")]
    [SerializeField] private GameObject _weaponButtun;

    [Header("武器のSpriteLendrer")]
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;


    /// <summary>インベントリを開いた時の処理</summary>
    public void InventoryOpen()
    {
        //インベントリのパネルを開く
        _inventoryPanel.SetActive(true);
        //マウスカーソルを出す
        Cursor.visible = true;
    }

    /// <summary>インベントリを閉じた時の処理</summary>
    public void InventoryClose()
    {
        //インベントリのパネルを閉じる
        _inventoryPanel.SetActive(false);
        //マウスカーソルを消す
        Cursor.visible = false;
    }

    /// <summary>インベントリ内の武器スキン欄に、新しいスキンの追加する</summary>
    /// <param name="name"></param>
    public void InventoryAddGunSkin(string name,Sprite gunSkinSprite)
    {
        //一覧に追加するボタンを生成
        var go = Instantiate(_weaponButtun);
        //生成したボタンのTextを変更
        go.transform.GetChild(0).GetComponent<Text>().text = name;

        //初期設定
        GunSkinUIInformation info = go.GetComponent<GunSkinUIInformation>();
        info.BigItemImage = _gunSkinBigImage;
        info.DefultSprite = _gunSkinBigImage.sprite;
        info.GunSkinSprite = gunSkinSprite;
        info.GunSpriteRendere = _gunSpriteRenderer;

        go.transform.SetParent(_weaponLayoutGroup.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>インベントリ欄にアイテムを追加する</summary>
    /// <param name="name">アイテムの名前</param>
    /// <param name="itemPanel">表示するアイテムのパネル</param>
    public void InventoryAddItem(string name,string information, Sprite itemPanel)
    {
        //一覧に追加するアイテムのパネルのおおもとを追加
        var go = Instantiate(_itemPanelOrizin);
        //生成したパネルのImageを変更
        go.transform.GetChild(0).GetComponent<Image>().sprite = itemPanel;

        //初期設定
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
