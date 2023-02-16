using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSquatAndSliding : MonoBehaviour
{
    [Header("POVのカメラ")]
    [SerializeField] CinemachineVirtualCamera _camera;

    CinemachineTransposer _cameraTransposer;

    [Header("スライディングの速度")]
    [Tooltip("スライディングの速度")] [SerializeField] float _slidingPower = 10f;

    [Header("スライディングの実行時間")]
    [Tooltip("スライディングの実行時間")] [SerializeField] float _slidingTime = 0.5f;

    [Header("スライディングのクールタイム")]
    [Tooltip("スライディングのクールタイム")] [SerializeField] float _slidingCoolTime = 3f;


    [Header("しゃがみ移動の速さ")]
    [Tooltip("しゃがみ移動の速さ")] [SerializeField] float _movingSpeedOnGround = 5f;

    [Header("通常のカメラの高さ")]
    [Tooltip("通常のカメラの高さ")] [SerializeField] private float _defultCameraHigh = 1.5f;

    [Header("しゃがみのカメラの高さ")]
    [Tooltip("しゃがみのカメラの高さ")] [SerializeField] private float _squatCameraHigh = 0.5f;


    private bool _isSlidingEnd = false;

    public bool IsSlidingEnd => _isSlidingEnd;

    private bool _isSliding = true;

    public bool IsSliding => _isSliding;


    private float _countSlidingTime = 0;

    [SerializeField] PlayerInput _playerInput;

    Rigidbody _rb;
    [SerializeField] Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Start()
    {

    }

    /// <summary>キャラの正面を決める</summary>
    public void SetDir()
    {
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする
        transform.forward = dir;
    }


    /// <summary>スライディングの時間を数える</summary>
    /// <returns></returns>
    IEnumerator CountSlidingTime()
    {
        _isSlidingEnd = false;
        yield return new WaitForSeconds(_slidingTime);
        _isSlidingEnd = true;
    }

    /// <summary>スライディングのクールタイム時間を数える</summary>
    /// <returns></returns>
    IEnumerator CountSlidingCoolTime()
    {
        _isSliding = false;
        yield return new WaitForSeconds(_slidingTime);
        _isSliding = true;
    }

    /// <summary>スライディング開始時に呼ぶ</summary>
    public void StartSliding()
    {
        //スライディングの実行時間を数える
        StartCoroutine(CountSlidingTime());

        /// <summary>カメラの高さを下げる</summary>
        _cameraTransposer.m_FollowOffset.y = _squatCameraHigh;

    }


    /// <summary>スライディング中の速度設定</summary>
    public void SlidingMove()
    {
        Vector3 dir = Camera.main.transform.forward;

        _countSlidingTime += Time.deltaTime;

        if (_countSlidingTime < _slidingTime / 2)
        {
            _rb.velocity = dir.normalized * _slidingPower;
        }

    }

    /// <summary>スライディング終了時に呼ぶ</summary>
    public void StopSliding()
    {
        //スライディングのクールタイムを数える
        StartCoroutine(CountSlidingCoolTime());

        /// <summary>カメラの高さを戻す</summary>
        _cameraTransposer.m_FollowOffset.y = _defultCameraHigh;

        _countSlidingTime = 0;
    }


    public void StartSquat()
    {
        /// <summary>カメラの高さを戻す</summary>
        _cameraTransposer.m_FollowOffset.y = _squatCameraHigh;
    }

    public void StopSquat()
    {
        /// <summary>カメラの高さを戻す</summary>
        _cameraTransposer.m_FollowOffset.y = _defultCameraHigh;
    }


    /// <summary>地上での動き。velocityを設定</summary>
    public void SquatMove()
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
            _anim.SetFloat("Speed", 1);
        }
    }
}
