using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunSkinData")]
public class GunSkinDataInformation : ScriptableObject
{
    [Header("e‚ÌImage")]
    [SerializeField] private Sprite _gunSkinSprite;

    [Header("e‚ÌŽ¯•ÊName")]
    [SerializeField] private string _nameID;

    [Header("ƒCƒ“ƒxƒ“ƒgƒŠ‚É•\Ž¦‚·‚é–¼‘O")]
    [SerializeField] private string _name;

    [Header("e‚Ìà–¾(¡‚ÍŽg‚í‚È‚¢)")]
    [SerializeField] private string _information;

    public string Name => _name;

    public string NameId => _nameID;

    public Sprite GunSkinImage { get => _gunSkinSprite; }

    public string Information => _information;
}
