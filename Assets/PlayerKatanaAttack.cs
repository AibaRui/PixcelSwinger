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
        if (_katana.activeSelf)
        {
            Attack();
            ScondAvirity();
        }
    }

    void ScondAvirity()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力方向のベクトルを組み立てる
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
        dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
        dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

        if (Input.GetMouseButtonDown(1) && !_control._isAvirity)
        {
            _animKatana.Play("KatanaDash");
            _control._isAvirity = true;
            Debug.Log("!!!");
            m_rb.velocity = Vector3.zero;
            m_rb.AddForce(dir.normalized * 50, ForceMode.Impulse);
            StartCoroutine(a());
        }

        if (Input.GetMouseButtonUp(1))
        {

        }
    }

    IEnumerator a()
    {
        yield return new WaitForSeconds(0.3f);
        _control._isAvirity = false;
    }

    void Attack()
    {
        //刀を装備している
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
          StartCoroutine(CoolTime());
        }
    }
    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        _nowAttacking = false;

        //var a = _animKatana.GetCurrentAnimatorStateInfo(0).normalizedTime;

        //if (a >= 1)
        //{
        //    _nowAttacking = false;
        //}

    }


}
