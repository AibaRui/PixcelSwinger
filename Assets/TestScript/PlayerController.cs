using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] PlayerMoveing _playerMoveing;

    void Start()
    {

    }


    void Update()
    {

    }


    public void Move()
    {
        _playerMoveing.Move();
    }

    public void Jump()
    {

    }

}
