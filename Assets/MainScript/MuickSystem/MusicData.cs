using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MusicData")]
public class MusicData : ScriptableObject
{

    // [Header("�A�C�e���̃C���[�W")]
    //[SerializeField] private Sprite _itemUIImage;

    [Header("�Ȃ̎��ʖ�")]
    [SerializeField] private string _nameID;

    [Header("�Ȗ�")]
    [SerializeField] private string _name;

    [Header("���")]
    [SerializeField] private string _madeHuman;

    public string Name => _name;

    public string NameId => _nameID;

   // public Sprite ItemUIPanel { get => _itemUIImage; }

    public string Information => _madeHuman;

}
