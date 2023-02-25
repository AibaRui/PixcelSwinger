using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowSystem : MonoBehaviour
{
    [SerializeField] float _setTime = 5f;

    private float _countTime;

    [SerializeField] private List<GameObject> _panels = new List<GameObject>();

    [SerializeField] private bool _firstIsShowUI;

    public bool FirstIsShowUI=>_firstIsShowUI;

    private bool _isShowUI = true;

    public bool IsShowUI { get => _isShowUI; set => _isShowUI = value; }




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
