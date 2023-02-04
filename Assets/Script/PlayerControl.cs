using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{

    public PlayerAction playerAction = PlayerAction.OnGround;

    [SerializeField] Text _xSpeed;
    [SerializeField] Text _xSpeedLimit;
    [SerializeField] Text _zSpeed;
    [SerializeField] Text _zSpeedLimit;
    [SerializeField] Text _Action;

    [Header("�ڒn����̍ہA���S (Pivot) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���")]
    [Tooltip("�ڒn����̍ہA���S (Pivot) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���")]
    [SerializeField] float _isGroundedLength = 1.1f;

    [SerializeField] float _groundMove = 12;
    [SerializeField] float _swingMove = 15;
    [SerializeField] float _slidingMove;

    [SerializeField] float _wallRunMove;

    [SerializeField] float _airMove;
    [SerializeField] float _swingAirMove;

    [SerializeField] float _jumpMove;
    [SerializeField] float _gravity = 3;

    public bool _isWapon = false;

    public bool _isSwing;
    public bool _isSwingAir;

    public bool _isGrapple;
    public bool _isWallRun;

    public bool _isSliding;

    public bool _isSquat;
    public bool _isGround;
    public bool _isJump;

    PlayerMoveing _playerMove;
    Swing _swing;
    Grapple _grapple;



    
    private float _limitSpeedX;
    private float _limitSpeedZ;

    Rigidbody m_rb;
    void Start()
    {
        _playerMove = GetComponent<PlayerMoveing>();
        _swing = GetComponent<Swing>();
        _grapple = GetComponent<Grapple>();

        m_rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Check();
        SpeedLimit();
        T();

        _isGround = IsGrounded();
    }



    void T()
    {
        _xSpeed.text = m_rb.velocity.x.ToString("00.0");
        _xSpeedLimit.text = _limitSpeedX.ToString();

        _zSpeed.text = m_rb.velocity.x.ToString("00.0");
        _zSpeedLimit.text = _limitSpeedZ.ToString();

        _Action.text = playerAction.ToString();

    }



    void SpeedLimit()
    {

        if (m_rb.velocity.x >= _limitSpeedX)
        {
            m_rb.velocity = new Vector3(_limitSpeedX, m_rb.velocity.y, m_rb.velocity.z);
        }
        if (m_rb.velocity.x < -_limitSpeedX)
        {
            m_rb.velocity = new Vector3(-_limitSpeedX, m_rb.velocity.y, m_rb.velocity.z);
        }

        if (m_rb.velocity.z >= _limitSpeedZ)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, _limitSpeedZ);
        }
        if (m_rb.velocity.z < -_limitSpeedZ)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, -_limitSpeedZ);
        }

    }


    void Action()
    {

        if(_isGround)
        {
            _playerMove.Move();
        }
    }

    void Check()
    {
        //if (FindObjectOfType<TimeAvirity>()._isSlashing)
        //{
        //    playerAction = PlayerAction.Slow;
        //    _limitSpeedX = 50;
        //    _limitSpeedZ = 50;
        //    _isJump = false;
        //    return;
        //}

        if (_isSwing)
        {
            _isSwingAir = true;

            playerAction = PlayerAction.Swing;
            _limitSpeedX = _swingMove;
            _limitSpeedZ = _swingMove;

            _isJump = false;
            return;
        }

        if (_isGrapple)
        {
            _isSwingAir = true;

            playerAction = PlayerAction.Grapple;
            _limitSpeedX = _swingMove;
            _limitSpeedZ = _swingMove;

            _isJump = false;
            return;
        }


        if (_isWallRun)
        {
            playerAction = PlayerAction.WallRun;
            _limitSpeedX = _wallRunMove;
            _limitSpeedZ = _wallRunMove;

            _isSwingAir = false;
            _isJump = false;
            return;
        }




        if (_isSliding)
        {
            playerAction = PlayerAction.Sliding;

            _limitSpeedX = _slidingMove;
            _limitSpeedZ = _slidingMove;
            return;
        }

        //if (_isJump)
        //{
        //    playerAction = PlayerAction.JumpAir;

        //    _limitSpeedX = _jumpMove;
        //    _limitSpeedZ = _jumpMove;
        //    return;
        //}


        if (_isGround)
        {
            playerAction = PlayerAction.OnGround;
            _isWallRun = false;
            _isSwingAir = false;
            _limitSpeedX = _groundMove;
            _limitSpeedZ = _groundMove;
            return;
        }

        if (_isSwingAir)
        {
            playerAction = PlayerAction.SwingAir;
            _limitSpeedX = _swingAirMove;
            _limitSpeedZ = _swingAirMove;

            _isJump = false;
            return;
        }


        if (!_isGround)
        {
            playerAction = PlayerAction.Air;

            _limitSpeedX = _airMove;
            _limitSpeedZ = _airMove;
            return;
        }



    }

    public bool IsGrounded()
    {
        //  Debug.Log("kk");
        // Physics.Linecast() ���g���đ���������𒣂�A�����ɉ������Փ˂��Ă����� true �Ƃ���
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center + new Vector3(0, 0, -0.3f);   // start: �̂̒��S
        Vector3 end = start + Vector3.down * _isGroundedLength;  // end: start ����^���̒n�_
        Debug.DrawLine(start, end, Color.green); // ����m�F�p�� Scene �E�B���h�E��Ő���\������
        bool isGrounded = Physics.Linecast(start, end); // ���������C���ɉ������Ԃ����Ă����� true �Ƃ���
        return isGrounded;
    }

    public enum PlayerAction
    {
        OnGround,
        JumpAir,
        Air,
        Sliding,
        Swing,
        Grapple,
        WallRun,
        SwingAir,
        Slow,
    }
}
