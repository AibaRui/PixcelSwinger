using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>スクリタブルオブジェクトの情報を持ち
/// 銃のスキンの追加をするクラス
/// インベントリへの実装は、InventoryManagerを参照し
///向こうで実装</summary>
public class GunSkinManager : MonoBehaviour
{
    [Header("スクリタブルオブジェクト")]
    [SerializeField] private List<GunSkinDataInformation> _gunSkinDatas = new List<GunSkinDataInformation>();

    [Header("インベントリマネージャー")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("現在持っているスキンのnameIDを持つ")]
    private HashSet<string> _getGunSkins = new HashSet<string>();

    [Header("初期で持っている銃のスキン")]
    [SerializeField] List<GunSkinDataInformation> _firstgunSkinDatas = new List<GunSkinDataInformation>();


    private void Start()
    {
        foreach(var skin in _firstgunSkinDatas)
        {
            AddGunSkin(skin.NameId);
        }
    }

    //アイテムを追加する
    public void AddGunSkin(string nameID)
    {
        //IDが正しいかどうかチェック
        if (!CheckItem(nameID))
        {
            return;
        }

        //名前があっているか確認
        foreach (var gunsSkin in _gunSkinDatas)
        {
            if (gunsSkin.NameId == nameID)
            {
                //持って居なかったら追加
                if (!_getGunSkins.Contains(nameID))
                {
                    _inventoryManager.InventoryAddGunSkin(gunsSkin.Name, gunsSkin.GunSkinImage);
                    _getGunSkins.Add(nameID);
                }
            }
        }
    }

    public bool CheckItem(string name)
    {
        foreach (var a in _gunSkinDatas)
        {
            if (a.NameId == name)
            {
                return true;
            }
        }
        Debug.LogError($"{name}は無いか、名前が違います");
        return false;
    }
}
