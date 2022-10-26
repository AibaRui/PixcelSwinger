using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManue : MonoBehaviour
{
    [SerializeField] GameObject _settingCanvas;
    bool _isOpen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }




    void Open()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isOpen = !_isOpen;
        }
        _settingCanvas.SetActive(_isOpen);
        Cursor.visible = _isOpen;
    }

}
