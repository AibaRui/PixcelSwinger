using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerWallRunning : MonoBehaviour
{
    [Header("プレイヤー視点のカメラ")]
    [SerializeField] private CinemachineVirtualCamera _playerPOVCamera;

    [Header("壁走りの速さ")]
    [SerializeField] private float m_runSpeed = 8;

    [Header("Rayの長さ")]
    [SerializeField] private float m_checkWallRayDistace = 2f;

    [Header("壁のレイヤー")]
    [SerializeField] private LayerMask _wallLayer;

    [Header("カメラを傾かせる最大角度")]
    [SerializeField] private float _cameraDutch = 5;

    [Header("カメラを傾かせる速度")]
    [SerializeField] private float _changeCameraSpeed = 5;

    [Header("ジャンプのパワー")]
    [SerializeField] private float _jumpPower = 7;

    [Header("WallRunのクールタイム")]
    [SerializeField] private float _wallRunCoolTime = 0.5f;

    [Header("ジャンプの方向")]
    [SerializeField] private Vector2 _addJumpDir;

    [Header("正面の壁の判定の長さ")]
    [SerializeField] private float _layLongOfCheckForwardWallOfEndWallRun = 5;


    /// <summary>壁RUNの進む方向</summary>
    private Vector3 _wallForward;

    /// <summary>現在WallrRunが出来るかどうか</summary>
    private bool _isCanWallRun = false;

    /// <summary>CoolTimeが終わったかどうか</summary>
    private bool _isCoolTime = true;

    /// <summary>右の壁に当たっているかどうか</summary>
    private bool _rightWallHit = false;
    /// <summary>右の壁に当たっているかどうか</summary>
    private bool _leftWallHit = false;

    /// <summary>プレイヤーの視点の方向に壁があるかどうか</summary>
    private bool _fowardWallHit = false;

    /// <summary>カメラを切り替えたかどうか</summary>
    private bool _isChangeCamera = false;

    /// <summary>WallRunの始まりの、壁の方向。true:右/false:左</summary>
    private bool _startHitRight;

    /// <summary>自身の右側の壁を検索するRaycast</summary>
    private RaycastHit _rightWall;
    /// <summary>自身の左側の壁を検索するRaycast</summary>
    private RaycastHit _leftWall;
    /// <summary>自身の正面の壁を検索するRaycast</summary>
    private RaycastHit _fowardWall;

    public bool IsCanWallRun { get => _isCanWallRun; }

    public bool IsCoolTime => _isCoolTime;

    public bool IsWallRunCoolTime { get => _isCoolTime; }

    public Vector3 WallForward => _wallForward;

    [SerializeField] Animator _anim;
    [SerializeField] AudioSource _aud;
    private Rigidbody _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>WallRunのクールタイムを数える関数</summary>
    /// <returns></returns>
    IEnumerator WallRunCoolTime()
    {
        yield return new WaitForSeconds(_wallRunCoolTime);
        _isCoolTime = true;
    }

    /// <summary>WallRun開始時の初期の設定</summary>
    public void WallRunStartSet()
    {
        //WallRunの始まりの壁の方向を検索
        _startHitRight = CheckWallRight();

        //方向に応じた銃のAnimationをさせる
        if (_startHitRight)
        {
            _anim.Play("GunWallRunStart_R");
        }
        else
        {
            _anim.Play("GunWallRunStart_L");
        }
        _anim.SetBool("IsWallRun", true);
    }

    /// <summary>WallRun時に、カメラを傾かせる関数</summary>
    private void ChangeDutch()
    {
        //壁が右にある場合
        if (_startHitRight)
        {
            //カメラを傾ける
            _playerPOVCamera.m_Lens.Dutch += Time.deltaTime * _changeCameraSpeed;

            //設定角度まで達したら終了。カメラを傾け終えた事にする
            if (_playerPOVCamera.m_Lens.Dutch >= _cameraDutch)
            {
                _isChangeCamera = true;
                _playerPOVCamera.m_Lens.Dutch = _cameraDutch;
            }
        }
        //壁が左にある場合
        else
        {
            //カメラを傾ける
            _playerPOVCamera.m_Lens.Dutch -= Time.deltaTime * _changeCameraSpeed;

            //設定角度まで達したら終了。カメラを傾け終えた事にする
            if (_playerPOVCamera.m_Lens.Dutch <= -_cameraDutch)
            {
                _isChangeCamera = true;
                _playerPOVCamera.m_Lens.Dutch = -_cameraDutch;
            }
        }
    }

    /// <summary>WallRunの動きをさせる関数</summary>
    public void DoWallRun()
    {
        //壁の方向に速度を加え、Y速度を0にする
        _rb.AddForce(_wallForward * m_runSpeed);
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
    }

    /// <summary>WallRunで走っている方向に壁があるかどうかを確認する関数
    /// 壁に詰まらせないようにするため、進行方向にあったら強制的にWallRunを終わらせる</summary>
    /// <returns>進行方向に壁があるかどうか</returns>
    public bool CheckForwardWall()
    {
        bool hit = Physics.Raycast(transform.position, _wallForward, out var raycastHit, _layLongOfCheckForwardWallOfEndWallRun, _wallLayer);
        return hit;
    }

    /// <summary>WallRun実行させる関数
    /// 1:外積を用いて、進行方向を決める
    /// 2:カメラが傾かせる
    /// 3:速度を加える関数を呼ぶ</summary>
    public void WallRuning()
    {
        //右の壁に当たり続けていたら
        if (CheckWallRight())
        {
            _rb.useGravity = false;

            //法線を取る
            Vector3 wallNomal = _rightWall.normal;
            //外積を使い、進行方向を取る
            _wallForward = Vector3.Cross(wallNomal, transform.up);

            Debug.Log("外積:"+_wallForward);

            //プラスとマイナスの外積ベクトルと自身の向いている方向を比べる。
            //近い方を進む方向とする
            if ((transform.forward - _wallForward).magnitude > (transform.forward - -_wallForward).magnitude)
            {
                _wallForward = -_wallForward;
            }
        }
        //右の壁に当たり続けていたら
        else if (CheckWallLeft())
        {
            _rb.useGravity = false;

            //法線を取る
            Vector3 wallNomal = _leftWall.normal;
            //外積を使い、進行方向を取る
            _wallForward = Vector3.Cross(wallNomal, transform.up);

            //プラスとマイナスの外積ベクトルと自身の向いている方向を比べる。
            //近い方を進む方向とする
            if ((transform.forward - _wallForward).magnitude > (transform.forward - -_wallForward).magnitude)
            {
                _wallForward = -_wallForward;
            }
        }

        if (!_isChangeCamera)
        {
            ChangeDutch();
        }


        DoWallRun();
    }

    public void WallRunJumpAuto()
    {
        if (_startHitRight)
        {
            Vector3 dir = _wallForward + -transform.right + transform.up;
            _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
        }
        else
        {
            Vector3 dir = _wallForward + transform.right + transform.up;
            _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
        }
    }

    public void WallRunJump()
    {
        //壁に向かってとんでいなければ
        if (!CheckfowardWallofPlayerLook())
        {
            if (_startHitRight)
            {
                Vector3 dir = Camera.main.transform.forward + transform.up * _addJumpDir.y + (-transform.right * _addJumpDir.x);
                _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
            }
            else
            {
                Vector3 dir = Camera.main.transform.forward + transform.up * _addJumpDir.y + (transform.right * _addJumpDir.x);
                _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
            }
        }
        else //自動的にとぶ
        {
            if (_startHitRight)
            {
                Vector3 dir = _wallForward + -transform.right * _addJumpDir.x + transform.up * _addJumpDir.y;
                _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
            }
            else
            {
                Vector3 dir = _wallForward + transform.right * _addJumpDir.x + transform.up * _addJumpDir.y;
                _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
            }
        }
    }


    public void EndWallRun()
    {
        _playerPOVCamera.m_Lens.Dutch = 0;
        _rb.useGravity = true;
        _anim.SetBool("IsWallRun", false);
        _isChangeCamera = false;
        _isCoolTime = false;
        StartCoroutine(WallRunCoolTime());
    }



    private bool CheckfowardWallofPlayerLook()
    {
        _fowardWallHit = Physics.Raycast(transform.position, Camera.main.transform.forward, out _fowardWall, 1, _wallLayer);
        return _fowardWallHit;
    }

    public bool CheckWallLeft()
    {
        _leftWallHit = Physics.Raycast(transform.position, -transform.right, out _leftWall, m_checkWallRayDistace, _wallLayer);
        return _leftWallHit;
    }


    /// <summary>壁に近いかどうかを判断する</summary>
    public bool CheckWallRight()
    {
        _rightWallHit = Physics.Raycast(transform.position, transform.right, out _rightWall, m_checkWallRayDistace, _wallLayer);
        return _rightWallHit;
    }




}
