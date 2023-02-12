using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDirControl : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    private void FixedUpdate()
    {
        Vector3 dir = _player.transform.position - transform.position;
        dir.y = 0;
        transform.forward = -dir;
    }
}
