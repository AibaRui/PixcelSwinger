using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMoveing : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    [Header("地上での速さ")]
    [Tooltip("地上での速さ")] [SerializeField] float _movingSpeedOnGround = 5f;

    [Header("空中でのAddする動きの速さ")]
    [Tooltip("空中でのAddする動きの速さ")] [SerializeField] float _movingSpeedOnAir = 5f;


    [SerializeField] float _addDownAir = 5;

    [SerializeField] bool _isDownSpeed = false;

    private Vector3 _airVelo;

    PlayerInput _playerInput;

    Rigidbody _rb;
    [SerializeField] Animator _anim;
    [SerializeField] Animator _legAnim;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    public void SetDir()
    {
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする
        transform.forward = dir;

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        // 入力方向のベクトルを組み立てる
        _airVelo = Vector3.forward * v + Vector3.right * h;
        _airVelo = Camera.main.transform.TransformDirection(_airVelo);    // メインカメラを基準に入力方向のベクトルを変換する
        _airVelo.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする
    }


    /// <summary>地上での走る動き。velocityを設定</summary>
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

            Vector3 velo = dir.normalized * _movingSpeedOnGround; // 入力した方向に移動する
            velo.y = _rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            _rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }

        // 水平方向の速度を Speed にセットする
        //Vector3 velocity = _rb.velocity;
        // velocity.y = 0f;

        if (_rb.velocity.x != 0 || _rb.velocity.z != 0)
        {
            _anim.SetFloat("Speed", 5);
        }


    }

    /// <summary>空中での動き。AddForceで制御</summary>
    public void MoveAir()
    {
        // 方向の入力を取得し、方向を求める
        // 入力方向のベクトルを組み立てる
        Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            //_airVelo = Vector3.zero;
            //_animKatana.SetBool("Move", false);
        }
        else
        {
            //_animKatana.SetBool("Move", true);

            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

            _rb.AddForce(dir.normalized * _movingSpeedOnAir, ForceMode.Force);
        }

        if (_isDownSpeed) _rb.AddForce(-transform.up * _addDownAir);
    }



    public void LegAnimation()
    {
        _legAnim.SetFloat("Speed", _rb.velocity.y);
    }


}


