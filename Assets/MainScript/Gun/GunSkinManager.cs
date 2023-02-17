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

    [Header("初期状態の持ってる銃のスキン")]
    [SerializeField] private List<string> _firstgunSkinDatas = new List<string>();

    [Header("セーブマネージャー")]
    [SerializeField] private SaveManager _saveManager;


    public List<string> FirstSkinData => _firstgunSkinDatas;

    //SaveData用
    private HashSet<string> _getGunSkinSaveData = new HashSet<string>();

    //SaveData用
    public HashSet<string> GetGunSkinSaveData => _getGunSkinSaveData;


    private void Start()
    {
        //foreach(var skin in _firstgunSkinDatas)
        //{
        //    AddGunSkin(skin);
        //}
    }

    /// <summary>
    /// SaveManagerから呼ぶ。
    /// データを読み込んでセットする</summary>
    /// <param name="data"></param>
    public void DataLode(string[] data)
    {
        Debug.Log("ロード開始");
        if (data != null)
        {
            foreach (var item in data)
            {
                _getGunSkinSaveData.Add(item);
                FirstAddGunSkin(item);
            }
        }
    }


    public void FirstAddGunSkin(string nameID)
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

                    _getGunSkinSaveData.Add(nameID);
                }
            }
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

                    _getGunSkinSaveData.Add(nameID);
                }
            }
        }
            //データをセーブ
            _saveManager.DaveSave();
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
