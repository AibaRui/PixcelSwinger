using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSkinUIInformation : MonoBehaviour
{
    [Tooltip("銃のスキンのスプライト")]
    private Sprite _gunSkin;

    public Sprite GunSkinSprite { get => _gunSkin; set => _gunSkin = value; }

    [Tooltip("ゲーム画面の銃のスプライトレンダラー")]
    private SpriteRenderer _gunSpriteRendere;

    public SpriteRenderer GunSpriteRendere { get => _gunSpriteRendere; set => _gunSpriteRendere = value; }

    [Tooltip("大きく表示するアイテムのイメージ")]
    private Image _bigItemImage;

    public Image BigItemImage { get => _bigItemImage; set => _bigItemImage = value; }

    private Sprite _defultSprite;
    public Sprite DefultSprite { get => _defultSprite; set => _defultSprite = value; }

    /// <summary>マウスカーソルが乗った時の処理</summary>
    public void On()
    {
        _bigItemImage.sprite = _gunSkin;
    }

    /// <summary>マウスカーソルが離れた時の処理</summary>
    public void Of()
    {
        //アイテム欄のイメージを切り替える
        _bigItemImage.sprite = _defultSprite;
    }

    public void ChangeGunSkin()
    {
        _gunSpriteRendere.sprite = _gunSkin;
    }


}
