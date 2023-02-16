using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "ItemData")]
public class ItemDataInformation : ScriptableObject
{
    //    [SerializeField] private ItemManager.ItemDatas itemData;

    [Header("�A�C�e���̃C���[�W")]
    [SerializeField] private Sprite _itemUIImage;

    [Header("�A�C�e���̎��ʖ�")]
    [SerializeField] private string _nameID;

    [Header("�C���x���g���ɕ\������A�C�e�����O")]
    [SerializeField] private string _name;

    [Header("�A�C�e���̐���")]
    [SerializeField] private string _information;

    public string Name => _name;

    public string NameId => _nameID;

    public Sprite ItemUIPanel { get => _itemUIImage; }

    public string Information => _information;
}
