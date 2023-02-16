using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunSkinData")]
public class GunSkinDataInformation : ScriptableObject
{
    [Header("銃のImage")]
    [SerializeField] private Sprite _gunSkinSprite;

    [Header("銃の識別Name")]
    [SerializeField] private string _nameID;

    [Header("インベントリに表示する名前")]
    [SerializeField] private string _name;

    [Header("銃の説明(今は使わない)")]
    [SerializeField] private string _information;

    public string Name => _name;

    public string NameId => _nameID;

    public Sprite GunSkinImage { get => _gunSkinSprite; }

    public string Information => _information;
}
