using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventEriaCheck : MonoBehaviour
{
    private bool _isEnterEventEria = false;

    public bool IsEnterEventEria { get => _isEnterEventEria; set => _isEnterEventEria = value; }

    private GameObject _charactor;

    public GameObject Charactor => _charactor;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EventEria")
        {
            _isEnterEventEria = true;
            _charactor = other.gameObject;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EventEria")
        {
            _isEnterEventEria = true;
            _charactor = other.gameObject;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EventEria")
        {
            _isEnterEventEria = false;
            return;
        }
    }
}
