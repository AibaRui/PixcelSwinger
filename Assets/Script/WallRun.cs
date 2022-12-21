using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WallRun : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    [Header("壁走りの速さ")]
    [SerializeField] float m_runSpeed = 8;

    [Header("ジャンプパワー")]
    [SerializeField] float m_jumpPower = 8;

    [Header("Rayの長さ")]
    [SerializeField] float m_checkWallRayDistace = 2f;

    [Header("壁のレイヤー")]
    [SerializeField] LayerMask _wallLayer;

    /// <summary>右の壁に当たっているかどうか</summary>
    bool m_rightWallHit = false;
    /// <summary>右の壁に当たっているかどうか</summary>
    bool m_leftWallHit = false;
    RaycastHit m_rightWall;
    RaycastHit m_leftWall;

    /// <summary>壁RUN中</summary>
    bool _isWallRun;
    /// <summary>壁RUNができるかどうか</summary>
    bool _okWallRun = true;

    /// <summary>壁RUNの進む方向</summary>
    Vector3 wallForward;


    Vector3 jumpFowrd;


    P_Control _control;

    // [SerializeField] AudioSource m_aud;
    Animator m_anim;
    Rigidbody _rb;
    void Start()
    {
        _control = GetComponent<P_Control>();
        _rb = gameObject.GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        //   m_aud = m_aud.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_control._isSwing && !_control._isGrapple)
        {
            Jump();
            if (_isWallRun)
            {
                _control._isWallRun = true;
            }
            CheckWall();
            WallRuning();
        }

    }

    private void FixedUpdate()
    {
        if (!_control._isSwing && !_control._isGrapple)
        {
            DoWallRun();
        }
    }

    void DoWallRun()
    {
        if (_isWallRun)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(wallForward * m_runSpeed);
        }
    }

    void WallRuning()
    {
        float v = Input.GetAxisRaw("Vertical");

        // 壁が近い　地面についていない
        if (!_control._isGround && _okWallRun && v > 0)
        {
 
            if (m_rightWallHit)
            {
                Debug.Log("rrr");
                _control._isWallRun = true;
                _camera.m_Lens.Dutch = 20;
                _isWallRun = true;

               _rb.useGravity = false;
                Vector3 wallNomal = m_rightWall.normal;
                wallForward = Vector3.Cross(wallNomal, transform.up);
 
                if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude)
                {
                    wallForward = -wallForward;
                }

                jumpFowrd = -transform.right + wallForward*2 + transform.up/2;
            }
            else if (m_leftWallHit)
            {
                Debug.Log("lll");
                _control._isWallRun = true;
                _camera.m_Lens.Dutch = -20;
                _isWallRun = true;

                _rb.useGravity = false;
                Vector3 wallNomal = m_leftWall.normal;
                wallForward = Vector3.Cross(wallNomal, transform.up);

                if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude)
                {
                    wallForward = -wallForward;
                }
                jumpFowrd = transform.right + wallForward*2+transform.up/2;
            }

            else if (_control._isGround)
            {
                _camera.m_Lens.Dutch = 0;
                _isWallRun = false;
                _rb.useGravity = true;
                _control._isWallRun = false;
            }
        }
        if (_isWallRun)
        {
            if (!m_rightWallHit && !m_leftWallHit)
            {
                _okWallRun = false;
                StopWallRun();

                StartCoroutine(CountCoolTime());

                _rb.velocity = Vector3.zero;
                //Vector3 dir = transform.forward;
                //dir.y = 0.5f;
                _rb.AddForce(jumpFowrd * m_jumpPower, ForceMode.Impulse);
            }
        }
        else if (_isWallRun && _control._isGround)
        {
            _camera.m_Lens.Dutch = 0;
            _isWallRun = false;
            _rb.useGravity = true;
            _control._isWallRun = false;
        }
    }


    IEnumerator a()
    {
        yield return new WaitForSeconds(0.2f);
        _control._isWallRun = false;
    }

    public void StopWallRun()
    {
        StartCoroutine(a());
        _camera.m_Lens.Dutch = 0;
        _isWallRun = false;
        _rb.useGravity = true;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isWallRun)
        {
            _okWallRun = false;
            StopWallRun();

            _rb.velocity = Vector3.zero;
            Vector3 dir = Camera.main.transform.forward;
            dir.y = 0.5f;
            _rb.AddForce(dir * m_jumpPower, ForceMode.Impulse);
            StartCoroutine(CountCoolTime());
        }
    }

    IEnumerator CountCoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        _okWallRun = true;
    }

    /// <summary>壁に近いかどうかを判断する</summary>
    void CheckWall()
    {
        m_rightWallHit = Physics.Raycast(transform.position, transform.right, out m_rightWall, m_checkWallRayDistace, _wallLayer);
        m_leftWallHit = Physics.Raycast(transform.position, -transform.right, out m_leftWall, m_checkWallRayDistace, _wallLayer);
    }


}
