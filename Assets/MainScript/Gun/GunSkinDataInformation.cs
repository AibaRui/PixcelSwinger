using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunSkinData")]
public class GunSkinDataInformation : ScriptableObject
{
    [Header("�e��Image")]
    [SerializeField] private Sprite _gunSkinSprite;

    [Header("�e�̎���Name")]
    [SerializeField] private string _nameID;

    [Header("�C���x���g���ɕ\�����閼�O")]
    [SerializeField] private string _name;

    [Header("�e�̐���(���͎g��Ȃ�)")]
    [SerializeField] private string _information;

    public string Name => _name;

    public string NameId => _nameID;

    public Sprite GunSkinImage { get => _gunSkinSprite; }

    public string Information => _information;
}
