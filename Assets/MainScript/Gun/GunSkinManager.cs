using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>スクリタブルオブジェクトの情報を持ち
/// 銃のスキンの追加をするクラス
/// インベントリへの実装は、InventoryManagerを参照し
///向こうで実装</summary>
public class GunSkinManager : MonoBehaviour, ISave
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
    [SerializeField] private GunSkinDataSaveManager _gunSkinDataSaveManager;


    [Header("セーブマネージャー")]
    [SerializeField] private SaveManager _saveManager;

    [Header("初回のアイテム追加はセーブしない")]
    private bool _isFirstLoad = true;


    public List<string> FirstSkinData => _firstgunSkinDatas;

    //SaveData用
    private HashSet<string> _getGunSkinSaveData = new HashSet<string>();

    //SaveData用
    public HashSet<string> GetGunSkinSaveData => _getGunSkinSaveData;


    private void Start()
    {

    }
    /// <summary>インターフェイス。データのロードを揃えるための関数</summary>
    public void FistDataLodeOnGameStart()
    {
        CheckSaveData();
    }
    /// <summary>ゲームのはじめに呼ぶ。NewGameかコンティニューによってロードするデータを変える</summary>
    public void CheckSaveData()
    {
        ///NewGameは初期アイテムをロード
        if (CheckSaveDataExistence.s_isNewGame)
        {
            foreach (var skin in _firstgunSkinDatas)
            {
                AddGunSkin(skin);
            }
            Debug.Log("初回_GunSkinManager初期化:" + _firstgunSkinDatas);
        }
        else        ///Contunueはアイテムをロード
        {
            var data = _gunSkinDataSaveManager.GetSaveData().Split(",");

            foreach (var skin in data)
            {
                AddGunSkin(skin);
            }
            Debug.Log("初回_GunSkinManagerコンティニュー:" + _getGunSkinSaveData);
        }

        _isFirstLoad = false;
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

        //データをセーブ(初回のセーブ確認時はセーブしない)
        if (!_isFirstLoad)
        {
            _gunSkinDataSaveManager.SaveDataUpDate(_getGunSkinSaveData.ToArray());
            _saveManager.DaveSave();
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
        Debug.Log($"{name}は無いか、名前が違います");
        return false;
    }
}
