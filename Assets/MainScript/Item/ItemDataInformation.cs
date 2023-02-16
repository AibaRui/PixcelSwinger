using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "ItemData")]
public class ItemDataInformation : ScriptableObject
{
    //    [SerializeField] private ItemManager.ItemDatas itemData;

    [Header("アイテムのイメージ")]
    [SerializeField] private Sprite _itemUIImage;

    [Header("アイテムの識別名")]
    [SerializeField] private string _nameID;

    [Header("インベントリに表示するアイテム名前")]
    [SerializeField] private string _name;

    [Header("アイテムの説明")]
    [SerializeField] private string _information;

    public string Name => _name;

    public string NameId => _nameID;

    public Sprite ItemUIPanel { get => _itemUIImage; }

    public string Information => _information;
}
