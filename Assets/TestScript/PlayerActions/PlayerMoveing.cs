using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveing : MonoBehaviour
{
    [Header("動きの速さ")]
    [Tooltip("動きの速さ")] [SerializeField] float _movingSpeed = 5f;

    private float _moveSpeed = 0;

    PlayerInput _playerInput;

    Rigidbody _rb;
    [SerializeField] Animator _anim;
    private void Awake()
    {
        _moveSpeed = _movingSpeed;
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        // 方向の入力を取得し、方向を求める
        // 入力方向のベクトルを組み立てる
        Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
            //_airVelo = Vector3.zero;
            //_animKatana.SetBool("Move", false);
        }
        else
        {
            //_animKatana.SetBool("Move", true);

            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

            Vector3 velo = dir.normalized * _moveSpeed; // 入力した方向に移動する
            velo.y = _rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            _rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }

        // 水平方向の速度を Speed にセットする
        //Vector3 velocity = _rb.velocity;
       // velocity.y = 0f;
        //_anim.SetFloat("Speed", velocity.magnitude);

    }

    public void ChangeMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public void ReSetMoveSpeed()
    {
        _moveSpeed = _movingSpeed;
    }

}
