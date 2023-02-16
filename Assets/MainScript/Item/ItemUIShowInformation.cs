using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>�A�C�e�����̃A�C�e����
/// EentTrigger�ɐݒ肷��֐��ƁA��������</summary>
public class ItemUIShowInformation : MonoBehaviour
{
    [Tooltip("�A�C�e���̃C���[�W")]
    private Sprite _itemImage;

    public Sprite ItemImage { get => _itemImage; set => _itemImage = value; }

    [Tooltip("�傫���\������A�C�e���̃C���[�W")]
    private Image _bigItemImage;

    public Image BigItemImage { get => _bigItemImage; set => _bigItemImage = value; }

    [Tooltip("�A�C�e���̏���Text")]
    private Text _informationText;

    public Text InforMationText { get => _informationText; set => _informationText = value; }

    [Tooltip("�A�C�e���̖��O��Text")]
    private Text _nameText;

    public Text NameText { get => _nameText; set => _nameText = value; }

    [Tooltip("�A�C�e���̏��")]
    private string _information;

    public string Information { get => _information; set => _information = value; }

    [Tooltip("�A�C�e���̖��O")]
    private string _name;

    public string Name { get => _name; set => _name = value; }

    private Color _color;
    
    public Color ColorBig { get => _color; set => _color = value; }

    private Sprite _defultSprite;
    public Sprite DefultSprite { get => _defultSprite; set => _defultSprite = value; }


    /// <summary>�}�E�X�J�[�\������������̏���</summary>
    public void On()
    {
        //�A�C�e�����̏ڍׂ�؂�ւ���
        _informationText.text = _information;
        //�A�C�e�����̖��O��؂�ւ���
        _nameText.text = _name;
        //�A�C�e�����̃C���[�W��؂�ւ���
        _bigItemImage.sprite = _itemImage;
    }

    /// <summary>�}�E�X�J�[�\�������ꂽ���̏���</summary>
    public void Of()
    {
        //�A�C�e�����̏ڍׂ�؂�ւ���
        _informationText.text = "";
        //�A�C�e�����̖��O��؂�ւ���
        _nameText.text = "";
        //�A�C�e�����̃C���[�W��؂�ւ���
        _bigItemImage.sprite = _defultSprite;
    }

}