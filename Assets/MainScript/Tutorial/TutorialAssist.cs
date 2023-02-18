using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialAssist : MonoBehaviour
{
    [Header("一番最初にエリアに入ったらすること")]
    [SerializeField] private UnityEvent _firstMove;

    [Header("アシストパネル")]
    [SerializeField] private GameObject _assistPanel;

    private bool _isFirstEnter = false;  


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _assistPanel.SetActive(true);

            if(!_isFirstEnter)
            {
                _firstMove?.Invoke();
                _isFirstEnter = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _assistPanel.SetActive(false);
        }
    }

}
