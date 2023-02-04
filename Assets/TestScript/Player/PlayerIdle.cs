using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    [SerializeField] Animator _gunAnim;


    public void Idle()
    {
        _gunAnim.SetFloat("Speed", 0);
    }

}
