using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSkinUIInformation : MonoBehaviour
{
    [Tooltip("�e�̃X�L���̃X�v���C�g")]
    private Sprite _gunSkin;

    public Sprite GunSkinSprite { get => _gunSkin; set => _gunSkin = value; }

    [Tooltip("�Q�[����ʂ̏e�̃X�v���C�g�����_���[")]
    private SpriteRenderer _gunSpriteRendere;

    public SpriteRenderer GunSpriteRendere { get => _gunSpriteRendere; set => _gunSpriteRendere = value; }

    [Tooltip("�傫���\������A�C�e���̃C���[�W")]
    private Image _bigItemImage;

    public Image BigItemImage { get => _bigItemImage; set => _bigItemImage = value; }

    private Sprite _defultSprite;
    public Sprite DefultSprite { get => _defultSprite; set => _defultSprite = value; }

    /// <summary>�}�E�X�J�[�\������������̏���</summary>
    public void On()
    {
        _bigItemImage.sprite = _gunSkin;
    }

    /// <summary>�}�E�X�J�[�\�������ꂽ���̏���</summary>
    public void Of()
    {
        //�A�C�e�����̃C���[�W��؂�ւ���
        _bigItemImage.sprite = _defultSprite;
    }

    public void ChangeGunSkin()
    {
        _gunSpriteRendere.sprite = _gunSkin;
    }


}
