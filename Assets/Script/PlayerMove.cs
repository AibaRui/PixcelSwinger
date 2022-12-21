using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{

    [Header("動きの速さ")]
    [Tooltip("動きの速さ")] [SerializeField] float _movingSpeed = 5f;

    [Header("空中でAddする速度")]
    [Tooltip("空中でAddする速度")] [SerializeField] float _airMoveSpeed = 1f;

    [Header("Jump空中でAddする速度")]
    [Tooltip("Jump空中でAddする速度")] [SerializeField] float _swingAirMoveSpeed = 1f;

    [Header("しゃがみの移動速度")]
    [Tooltip("しゃがみの移動速度")] [SerializeField] float _squatMoeSpeed = 4;

    [Header("ジャンプ力")]
    [Tooltip("ジャンプ力")] [SerializeField] float _jumpPower = 5f;

    [Header("ジャンプ後に加える追加の重力")]
    [Tooltip("ジャンプ後に加える追加の重力")] [SerializeField] float _gravity = 0.3f;


    [SerializeField] LayerMask _slope;

    /// <summary>しゃがんでいるかどうか</summary>
    public bool _squat = false;
    /// <summary>スライディングをしているかどうか</summary>
    public bool _isSliding = false;

    public bool _isSwing;

    P_Control _control;



    Vector3 angle;
    bool _isSlope;


    Vector3 _airVelo;

    /// <summary>キャラクターの Animator</summary>
    [SerializeField] Animator _anim;
    [SerializeField] Animator _animKatana;
    //  [SerializeField] AudioSource m_aud;
    Animator m_anim;
    Rigidbody _rb;
    void Start()
    {
        Cursor.visible = false;
        _control = GetComponent<P_Control>();

        _rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();

        Physics.gravity = Physics.gravity * 2;
        _animKatana = _animKatana.GetComponent<Animator>();
        //// m_aud = m_aud.GetComponent<AudioSource>();
    }

    void Update()
    {
        Dir();
        Jump();

        if (_control.playerAction == P_Control.PlayerAction.OnGround)
        {
            Move();
            DownSpeed();
        }


    }

    private void FixedUpdate()
    {
        MoveAir(_airVelo);
    }

    void Dir()
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

   public void Move()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力方向のベクトルを組み立てる
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
            //_airVelo = Vector3.zero;
            _animKatana.SetBool("Move", false);
        }
        else
        {
            _animKatana.SetBool("Move", true);
            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

            Vector3 velo;
            if (_control._isSquat)
            {
                velo = dir.normalized * _squatMoeSpeed; // 入力した方向に移動する
            }
            else
            {
                velo = dir.normalized * _movingSpeed; // 入力した方向に移動する
            }
            velo.y = _rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            _rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }

        // Animator Controller のパラメータをセットする
        if (_anim)
        {
            // 攻撃ボタンを押された時の処理
            if (Input.GetButtonDown("Fire1") && _control._isGround)
            {
                _anim.SetTrigger("Attack");
            }

            // 水平方向の速度を Speed にセットする
            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _anim.SetFloat("Speed", velocity.magnitude);

            // 地上/空中の状況に応じて IsGrounded をセットする
            if (_rb.velocity.y <= 0f && _control._isGround)
            {
                _anim.SetBool("IsGrounded", true);
            }
            else if (!_control._isGround)
            {
                _anim.SetBool("IsGrounded", false);
            }
        }
    }





    //public void CheckSlope()
    //{
    //    RaycastHit hit;
    //    CapsuleCollider col = GetComponent<CapsuleCollider>();
    //    Vector3 start = this.transform.position + col.center + new Vector3(0, 0, -0.3f);   // start: 体の中心
    //    Vector3 end = start + Vector3.down*5;  // end: start から真下の地点

    //    Debug.DrawLine(start, end, Color.red); // 動作確認用に Scene ウィンドウ上で線を表示する
    //    if (Physics.Linecast(start, end,out hit,_slope))
    //    {
    //        Debug.Log("yes");
    //        Vector3 hitNomal = hit.normal;
    //        angle = Vector3.Cross(hitNomal, transform.up);
    //        _isSlope = true;
    //    }
    //    else
    //    {
    //        Debug.Log("no");
    //        _isSlope = false;
    //    }

    //}





    void MoveAir(Vector3 velo)
    {
        if (_control.playerAction == P_Control.PlayerAction.SwingAir)
        {
            _rb.AddForce(velo.normalized * _swingAirMoveSpeed, ForceMode.Force);
        }
        else if (_control.playerAction == P_Control.PlayerAction.Air)
        {
            _rb.AddForce(velo.normalized * _airMoveSpeed, ForceMode.Force);
        }

        //if (_control._isWapon == false)
        //{
        //    if (velo == Vector3.zero)
        //    {

        //    }
        //    else
        //    {
        //        _rb.AddForce(velo.normalized * _airMoveSpeed, ForceMode.Force);
        //    }
        //}
        //else if (_control._isWapon == true)
        //{
        //    Vector3 vlo = velo * _airMoveSpeed;

        //    vlo.y = _rb.velocity.y;

        //    _rb.velocity = vlo;
        //}

       // _rb.AddForce(-transform.up * _gravity);
    }




    void Jump()
    {
        if (!_control._isGrapple && !_control._isSwing && !_control._isWallRun && _control._isGround)
        {
            // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
            if (Input.GetButtonDown("Jump") && _control._isGround)
            {
                FindObjectOfType<Sliding>().StopSquat();
                _control._isSliding = false;
                _control._isJump = true;
                _rb.velocity = new Vector3(_rb.velocity.x, _jumpPower, _rb.velocity.z);

                _animKatana.Play("KatanaJumpStart");
            }
        }
    }


    void DownSpeed()
    {
        if (_control._isJump)
        {
            if (_rb.velocity.y < 4)
            {
                _rb.AddForce(-transform.up * _gravity, ForceMode.Force);
            }

            if (_rb.velocity.y == 0)
            {

                _control._isJump = false;
            }
        }
    }






}