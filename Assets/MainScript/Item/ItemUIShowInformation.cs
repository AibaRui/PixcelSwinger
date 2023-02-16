using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>アイテム欄のアイテムの
/// EentTriggerに設定する関数と、情報を持つ</summary>
public class ItemUIShowInformation : MonoBehaviour
{
    [Tooltip("アイテムのイメージ")]
    private Sprite _itemImage;

    public Sprite ItemImage { get => _itemImage; set => _itemImage = value; }

    [Tooltip("大きく表示するアイテムのイメージ")]
    private Image _bigItemImage;

    public Image BigItemImage { get => _bigItemImage; set => _bigItemImage = value; }

    [Tooltip("アイテムの情報のText")]
    private Text _informationText;

    public Text InforMationText { get => _informationText; set => _informationText = value; }

    [Tooltip("アイテムの名前のText")]
    private Text _nameText;

    public Text NameText { get => _nameText; set => _nameText = value; }

    [Tooltip("アイテムの情報")]
    private string _information;

    public string Information { get => _information; set => _information = value; }

    [Tooltip("アイテムの名前")]
    private string _name;

    public string Name { get => _name; set => _name = value; }

    private Color _color;
    
    public Color ColorBig { get => _color; set => _color = value; }

    private Sprite _defultSprite;
    public Sprite DefultSprite { get => _defultSprite; set => _defultSprite = value; }


    /// <summary>マウスカーソルが乗った時の処理</summary>
    public void On()
    {
        //アイテム欄の詳細を切り替える
        _informationText.text = _information;
        //アイテム欄の名前を切り替える
        _nameText.text = _name;
        //アイテム欄のイメージを切り替える
        _bigItemImage.sprite = _itemImage;
    }

    /// <summary>マウスカーソルが離れた時の処理</summary>
    public void Of()
    {
        //アイテム欄の詳細を切り替える
        _informationText.text = "";
        //アイテム欄の名前を切り替える
        _nameText.text = "";
        //アイテム欄のイメージを切り替える
        _bigItemImage.sprite = _defultSprite;
    }

}