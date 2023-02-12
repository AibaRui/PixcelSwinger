using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [Header("ジャンプ力")]
    [Tooltip("ジャンプ力")] [SerializeField] float _jumpPower = 5f;

    [Header("空中のジャンプ力")]
    [Tooltip("空中のジャンプ力")] [SerializeField] float _airJumpPower = 10f;

    [Header("ジャンプ回数")]
    [Tooltip("ジャンプ回数")] [SerializeField] int _jumpNum = 1;

    [SerializeField] bool _isMultiJump = false;

    [SerializeField] Animator _anim;

    private bool _isCanJump = false;

    public bool IsCanJump { get => _isCanJump; }

    private int _jumpCount = 0;

    GroundCheck _groundCheck;
    PlayerInput _playerInput;

    Rigidbody _rb;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _groundCheck = GetComponent<GroundCheck>();
        _rb = GetComponent<Rigidbody>();
    }

    public void ReSetAirJump()
    {
        _jumpCount = 1;
        _isCanJump = true;
    }

    public void AirJump()
    {

        if (_jumpCount < _jumpNum)
        {
            Vector3 velo = new Vector3(_rb.velocity.x, _airJumpPower, _rb.velocity.z);
            _rb.velocity = velo;
        }


        _jumpCount++;

        if (_jumpCount == _jumpNum) _isCanJump = false;
    }

    public void Jump()
    {
        if (_isMultiJump)
        {
            if (_groundCheck.IsGround)
            {
                _isCanJump = true;
                _jumpCount = 0;
                Vector3 velo = new Vector3(_rb.velocity.x, _jumpPower, _rb.velocity.z);
                _rb.velocity = velo;
            }
            _jumpCount++;
        }
        else
        {
            if (_groundCheck.IsGround)
            {
                Vector3 velo = new Vector3(_rb.velocity.x, _jumpPower, _rb.velocity.z);
                _rb.velocity = velo;
            }
        }
        _anim.Play("GunJumpUp");
    }
}

