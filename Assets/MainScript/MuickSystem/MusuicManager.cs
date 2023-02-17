using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusuicManager : MonoBehaviour
{
    [Header("スクリタブルオブジェクト")]
    [SerializeField] private List<MusicData> _musicDatas = new List<MusicData>();

    [Header("インベントリマネージャー")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("初期状態のアイテム")]
    [SerializeField] private List<string> _firstGetItem = new List<string>();

    [Header("セーブマネージャー")]
    [SerializeField] private SaveManager _saveManager;

    public List<string> FirstGetItem => _firstGetItem;

    //現在持っている曲
    private HashSet<string> _getMusics = new HashSet<string>();

    public HashSet<string> GetMusics => _getMusics;

    //SaveData用
    private HashSet<string> _getItemSaveData = new HashSet<string>();

    //SaveData用
    public HashSet<string> GetItemSaveData => _getItemSaveData;

    private bool _isGetWalkMan = false;

    public bool IsGetWalkMan => _isGetWalkMan;


    // private bool _isMusicPlaying

    public bool IsWalkMan => _isGetWalkMan;


    [SerializeField] PlayerInput _playerInput;

    [SerializeField] private MusicPlayControler _musicPlayControler;



    


    /// <summary>
    /// SaveManagerから呼ぶ。
    /// データを読み込んでセットする</summary>
    /// <param name="data"></param>
    public void DataLode(string[] data)
    {
        if (data != null)
        {
            foreach (var music in data)
            {
                _getItemSaveData.Add(music);
                AddMusic(music);
            }
        }
    }


    //アイテムを追加する
    public void AddMusic(string name)
    {
        //IDが正しいかどうかチェック
        if (!CheckMusicNameID(name))
        {
            return;
        }

        if (!_getMusics.Contains(name))
        {
            _getMusics.Add(name);

            //SaveData用
            _getItemSaveData.Add(name);

            //IDから情報を探す
            foreach (var item in _musicDatas)
            {
                if (item.NameId == name)
                {
                    //_inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }
            //データをセーブ
            _saveManager.DaveSave();
        }
    }

    public bool CheckMusicNameID(string name)
    {
        foreach (var a in _musicDatas)
        {
            if (a.NameId == name)
            {
                return true;
            }
        }
        Debug.LogWarning($"{name}は無いか、名前が違います");
        return false;
    }

}
