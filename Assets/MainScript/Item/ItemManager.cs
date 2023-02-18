using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour, ISave
{
    [Header("�X�N���^�u���I�u�W�F�N�g")]
    [SerializeField] private List<ItemDataInformation> _itemDatas = new List<ItemDataInformation>();

    [Header("�C���x���g���}�l�[�W���[")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("������Ԃ̃A�C�e��")]
    [SerializeField] private List<string> _firstGetItem = new List<string>();

    [Header("�A�C�e���̃Z�[�u�}�l�[�W���[")]
    [SerializeField] private ItemDataSaveManager _itemDataSaveManager;


    [Header("�Z�[�u�}�l�[�W���[")]
    [SerializeField] private SaveManager _saveManager;

    public List<string> FirstGetItem => _firstGetItem;

    private Dictionary<string, int> _getItems = new Dictionary<string, int>();

    ItemDatas ItemData = ItemDatas.Karaage;

    //SaveData�p
    private HashSet<string> _getItemSaveData = new HashSet<string>();

    //SaveData�p
    public HashSet<string> GetItemSaveData => _getItemSaveData;

    [Header("����̃A�C�e���ǉ��̓Z�[�u���Ȃ�")]
    private bool _isFirstLoad = true;

    private void Start()
    {

    }
    /// <summary>�C���^�[�t�F�C�X�B�f�[�^�̃��[�h�𑵂��邽�߂̊֐�</summary>
    public void FistDataLodeOnGameStart()
    {
        CheckSaveData();
    }

    /// <summary>�Q�[���̂͂��߂ɌĂԁBNewGame���R���e�B�j���[�ɂ���ă��[�h����f�[�^��ς���</summary>
    public void CheckSaveData()
    {
        ///NewGame�͏����A�C�e�������[�h
        if (CheckSaveDataExistence.s_isNewGame)
        {
            foreach (var item in _firstGetItem)
            {
                AddItem(item);
            }
            Debug.Log("����_ItemManager������:" + _firstGetItem);
        }
        else        ///Contunue�̓A�C�e�������[�h
        {
            var data = _itemDataSaveManager.GetSaveData().Split(",");

            foreach (var item in data)
            {
                AddItem(item);
            }
            Debug.Log("����_ItemManager�R���e�B�j���[:" + _getItemSaveData);
        }

        _isFirstLoad = false;
    }

    //�A�C�e����ǉ�����
    public void AddItem(string name)
    {
        //ID�����������ǂ����`�F�b�N
        if (!CheckItem(name))
        {
            return;
        }

        //���ɂ���������𑝂₷(���̃V�X�e���͌��ݎ������Ă��Ȃ�)
        if (_getItems.ContainsKey(name))
        {
            _getItems[name]++;
        }
        else
        {
            //Dictionary�ɒǉ�
            _getItems.Add(name, 1);

            //SaveData�p
            _getItemSaveData.Add(name);

            //���O�������Ă�����ǉ�
            foreach (var item in _itemDatas)
            {
                if (item.NameId == name)
                {
                    _inventoryManager.InventoryAddItem(item.Name, item.Information, item.ItemUIPanel);
                }
            }

                _itemDataSaveManager.SaveDataUpDate(_getItemSaveData.ToArray());
            //�f�[�^���Z�[�u(����̃Z�[�u�m�F���̓Z�[�u���Ȃ�)
            if (!_isFirstLoad)
            {

                _saveManager.DaveSave();
            }

        }
    }

    public bool CheckItem(string name)
    {
        foreach (var a in _itemDatas)
        {
            if (a.NameId == name)
            {
                return true;
            }
        }
        Debug.LogWarning($"{name}�͖������A���O���Ⴂ�܂�");
        return false;
    }

    public enum ItemDatas
    {
        Karaage,
        Mouse,


    }

}