using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowSystem : MonoBehaviour
{

    [Header("操作説明のUIを出すまでの時間")]
    [SerializeField] private float _setTime = 5f;

    [Header("操作説明のUI")]
    [SerializeField] private List<GameObject> _panels = new List<GameObject>();

    [Header("操作説明のUIを出すかどうか、初期設定")]
    [SerializeField] private bool _firstIsShowUI;

    [Header("設定画面の、[UIを出す]ボタンのバー")]
    [SerializeField] private GameObject _isShowButtunSetBar;

    [Header("設定画面の、[UIを出さない]ボタンのバー")]
    [SerializeField] private GameObject _isAnShowButtunSetBar;

    [Header("プレイヤーのRigidbody")]
    [SerializeField] private Rigidbody _playerigidBody;

    [SerializeField] private PlayerController _playerController;

    /// <summary>アシストパネルを出すかどうか。(設定で出す出さないの変更ができる)</summary>
    private bool _isShowUI = true;

    /// <summary>プレイヤーが動いていない時間を数える</summary>
    private float _countTime;

    public bool FirstIsShowUI => _firstIsShowUI;

    public bool IsShowUI { get => _isShowUI; set => _isShowUI = value; }



    private void Update()
    {
        if (_playerigidBody.velocity == Vector3.zero)
        {
            ShowUI();
        }
        else
        {
            if (_countTime != 0)
            {
                CloseUI();
            }
        }
    }

    /// <summary>現在有効にしている設定のボタンの下のバーを設定する</summary>
    public void SettingPanelButtunBarSetting()
    {
        if (_isShowUI)
        {
            _isShowButtunSetBar.SetActive(true);
            _isAnShowButtunSetBar.SetActive(false);
        }
        else
        {
            _isShowButtunSetBar.SetActive(false);
            _isAnShowButtunSetBar.SetActive(true);
        }
    }


    public void ShowUI()
    {
        if (_isShowUI)
        {
            if (_playerController.IsMove)
            {
                if (_countTime < _setTime)
                {
                    _countTime += Time.deltaTime;
                }
                else
                {
                    _panels.ForEach(i => i.SetActive(true));
                }
            }
        }
    }

    public void CloseUI()
    {
        _countTime = 0;
        _panels.ForEach(i => i.SetActive(false));
    }

}
