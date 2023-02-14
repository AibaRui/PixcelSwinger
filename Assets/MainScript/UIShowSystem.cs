using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowSystem : MonoBehaviour
{
    [SerializeField] float _setTime = 5f;

    private float _countTime;

    [SerializeField] private List<GameObject> _panels = new List<GameObject>();

    void Start()
    {

    }


    public void ShowUI()
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

    public void CloseUI()
    {
        _countTime = 0;
        _panels.ForEach(i => i.SetActive(false));
    }

}
