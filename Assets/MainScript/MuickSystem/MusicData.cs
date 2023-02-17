using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MusicData")]
public class MusicData : ScriptableObject
{

    // [Header("アイテムのイメージ")]
    //[SerializeField] private Sprite _itemUIImage;

    [Header("曲の識別名")]
    [SerializeField] private string _nameID;

    [Header("曲名")]
    [SerializeField] private string _name;

    [Header("作者")]
    [SerializeField] private string _madeHuman;

    public string Name => _name;

    public string NameId => _nameID;

   // public Sprite ItemUIPanel { get => _itemUIImage; }

    public string Information => _madeHuman;

}
