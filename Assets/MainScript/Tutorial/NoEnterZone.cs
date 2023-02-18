using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnterZone : MonoBehaviour
{
    [Header("���ɖ߂��ʒu")]
    [SerializeField] private Transform _reSpownPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            other.gameObject.transform.position = _reSpownPos.position;
        }
    }

}
