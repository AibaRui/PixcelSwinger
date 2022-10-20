using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatanaAttack : MonoBehaviour
{

    [SerializeField] GameObject _katana;

    bool _okAttack = false;
    bool _nowAttacking = false;


    PlayerMove _playerMove;
    [SerializeField] Animator m_anim;

    P_Control _control;

    [SerializeField] Animator _animKatana;
    Rigidbody m_rb;
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
        m_rb = GetComponent<Rigidbody>();
        if (m_anim)
        {
            m_anim = m_anim.gameObject.GetComponent<Animator>();
        }
        _control = FindObjectOfType<P_Control>();
        _animKatana = _animKatana.GetComponent<Animator>();
    }


    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (_katana.activeSelf)
        {
            if (Input.GetMouseButton(0))
            {
                if (_nowAttacking || _control._isWallRun)
                {
                    return;
                }
                _nowAttacking = true;
                _animKatana.Play("katanaAttack1");
            }
            CoolTime();
        }

    }

    void CoolTime()
    {
        var a = _animKatana.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (a >= 1)
        {
            _nowAttacking = false;
        }

    }

}
