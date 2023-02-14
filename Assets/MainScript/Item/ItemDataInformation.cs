using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "ItemData")]
public class ItemDataInformation : ScriptableObject
{
//    [SerializeField] private ItemManager.ItemDatas itemData;

    [SerializeField]
    private Sprite _itemUIImage;

    [SerializeField]
    private string _nameID;

    [SerializeField]
    private string _name;



    [SerializeField]
    private string _information;

    public string Name => _name;

    public string NameId => _nameID;

    public Sprite ItemUIPanel { get => _itemUIImage; }

    public string Information => _information;
}
