using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnterZone : MonoBehaviour
{
    [Header("元に戻す位置")]
    [SerializeField] private Transform _reSpownPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            other.gameObject.transform.position = _reSpownPos.position;
        }
    }

}
