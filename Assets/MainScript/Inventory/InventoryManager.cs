using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("�C���x���g���̃p�l��")]
    [SerializeField] private GameObject _inventoryPanel;

    [Header("�A�C�e����")]
    [SerializeField] private LayoutGroup _itemlayoutGroup;

    [Header("�A�C�e���̃p�l���̂�������")]
    [SerializeField] private GameObject _itemPanelOrizin;

    [Header("�A�C�e�����̑傫���A�C�R��")]
    [SerializeField] private Image _itemBigImage;
    [SerializeField] private GameObject _big;


    [Header("�A�C�e�����̏��")]
    [SerializeField] private Text _itemInforText;  

    [Header("�A�C�e�����̖��O")]
    [SerializeField] private Text _itemNameText;

    public LayoutGroup ItemlayoutGroup  { get => _itemlayoutGroup; set => _itemlayoutGroup = value; }




    public void InventoryOpen()
    {
        _inventoryPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void InventoryClose()
    {
        _inventoryPanel.SetActive(false);
        Cursor.visible = false;
    }


    /// <summary>�C���x���g�����ɃA�C�e����ǉ�����</summary>
    /// <param name="name">�A�C�e���̖��O</param>
    /// <param name="itemPanel">�\������A�C�e���̃p�l��</param>
    public void SetItemPanel(string name,string information, Sprite itemPanel)
    {
        var go = Instantiate(_itemPanelOrizin);
        
        Image sprite = go.transform.GetChild(0).GetComponent<Image>();
        sprite.sprite = itemPanel;

        ItemUIShowInformation info = go.GetComponent<ItemUIShowInformation>();
        info.Name = name;
        info.Information = information;
        info.InforMationText = _itemInforText;
        info.NameText = _itemNameText;
        info.BigItemImage = _itemBigImage;
        info.ItemImage = itemPanel;
        info.DefultSprite = _itemBigImage.sprite;

        go.transform.SetParent(_itemlayoutGroup.transform);
    }

}
