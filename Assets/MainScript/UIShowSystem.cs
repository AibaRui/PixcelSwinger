using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowSystem : MonoBehaviour
{

    [Header("���������UI���o���܂ł̎���")]
    [SerializeField] private float _setTime = 5f;

    [Header("���������UI")]
    [SerializeField] private List<GameObject> _panels = new List<GameObject>();

    [Header("���������UI���o�����ǂ����A�����ݒ�")]
    [SerializeField] private bool _firstIsShowUI;

    [Header("�ݒ��ʂ́A[UI���o��]�{�^���̃o�[")]
    [SerializeField] private GameObject _isShowButtunSetBar;

    [Header("�ݒ��ʂ́A[UI���o���Ȃ�]�{�^���̃o�[")]
    [SerializeField] private GameObject _isAnShowButtunSetBar;

    private bool _isShowUI = true;
    private float _countTime;

    [SerializeField] private Rigidbody _playerigidBody;
    public bool FirstIsShowUI => _firstIsShowUI;

    public bool IsShowUI { get => _isShowUI; set => _isShowUI = value; }



    private void Update()
    {
        if (_playerigidBody.velocity != Vector3.zero)
        {
            ShowUI();
        }
        else
        {
            if (_countTime == 0)
            {
                CloseUI();
            }
        }
    }

    /// <summary>���ݗL���ɂ��Ă���ݒ�̃{�^���̉��̃o�[��ݒ肷��</summary>
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

    public void CloseUI()
    {
        _countTime = 0;
        _panels.ForEach(i => i.SetActive(false));
    }

}
