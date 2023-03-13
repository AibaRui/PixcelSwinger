using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMoveing : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    [Header("歩きの速さ")]
    [Tooltip("歩きの速さ")] [SerializeField] float _walkSpeed = 5f;

    [Header("走りの速さ")]
    [Tooltip("走りの速さ")] [SerializeField] float _runSpeed = 10f;

    [Header("空中でのAddする動きの速さ")]
    [Tooltip("空中でのAddする動きの速さ")] [SerializeField] float _movingSpeedOnAir = 5f;

    [Header("簡単_空中で下向きに加速する速さ")]
    [SerializeField] float _addDownAirEazy = 5;

    [Header("普通_空中で下向きに加速する速さ")]
    [SerializeField] float _addDownAirNomal = 5;

    [Header("空中で下向きに加速する速さ")]
    [SerializeField] bool _isDownSpeed = false;

    [Header("最初の設定")]
    [SerializeField] private bool _isFirstPushChange = true;

    [Header("銃")]
    [SerializeField] private Animator _anim;

    [Header("足音")]
    [SerializeField] private AudioClip _walkSound;

    [Header("走りの足音")]
    [SerializeField] private AudioClip _runSound;

    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] PlayerVelocityLimitControl _playerVelocityLimitControl;

    private float _moveSpeed = 5;

    /// <summary>trueの時は</summary>
    private bool _moveTypeIsWalk = true;

    private bool _isPushChange = true;

    private Vector3 _airVelo;



    public bool IsPushChange { get => _isPushChange; set => _isPushChange = value; }
    public bool IsFirstPushChange => _isFirstPushChange;
    public bool IsWalk => _moveTypeIsWalk;
    Rigidbody _rb;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _moveSpeed = _walkSpeed;
    }



    /// <summary>走り方の方法を変える</summary>
    /// <param name="isPush">Trueで切り替え/Falseで押している間</param>
    public void ChangeRunWay(bool isPush)
    {
        _moveSpeed = _walkSpeed;

        _isPushChange = isPush;

        //trueだったら切り替え
        if (_isPushChange)
        {
            _moveTypeIsWalk = true;
        }
    }

    public void SpeedChange()
    {
        if (_isPushChange)
        {
            if (_playerInput.IsLeftShiftDown)
            {
                _moveTypeIsWalk = !_moveTypeIsWalk;

                if (_moveTypeIsWalk)
                {
                    _moveSpeed = _walkSpeed;
                }
                else
                {
                    _moveSpeed = _runSpeed;
                }
            }
        }
        else
        {
            if (_playerInput.IsLeftShift)
            {
                _moveSpeed = _runSpeed;
            }
            else
            {
                _moveSpeed = _walkSpeed;
            }

        }
    }


    /// <summary>キャラの正面を決める</summary>
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

            Vector3 velo = dir.normalized * _moveSpeed; // 入力した方向に移動する
            velo.y = _rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            _rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }

        // 水平方向の速度を Speed にセットする
        //Vector3 velocity = _rb.velocity;
        // velocity.y = 0f;

        _anim.SetFloat("Speed", _moveSpeed);
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

        if (_isDownSpeed)
        {
            var downSpeed = 0f;

            if(_playerController.OperationLevel == PlayerController.OperationsLevel.Eazy)
            {
                downSpeed = _addDownAirEazy;
            }
            else
            {
                downSpeed = _addDownAirNomal;
            }


            _rb.AddForce(-transform.up * downSpeed);
        }


    }

    public void WalkSoundAndAnimation()
    {
        _playerController.AudioManager.PlayerSE(_walkSound, true);


    }

    public void RunSoundAndAnimation()
    {
        _playerController.AudioManager.PlayerSE(_runSound, true);


    }
}


