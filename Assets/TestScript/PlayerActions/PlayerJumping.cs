using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [Header("ジャンプ力")]
    [Tooltip("ジャンプ力")] [SerializeField] float _jumpPower = 5f;

    [Header("ジャンプ力")]
    [Tooltip("ジャンプ力")] [SerializeField] int _jumpNum = 1;

    [SerializeField] bool _isMultiJump = false;

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

    public void Jump()
    {
        if (_playerInput.IsJumping && _jumpCount<_jumpNum)
        {
            if(_groundCheck.IsGround)
            {
                //ジャンプした時に地面についていたから回数は0。
                //で、１回ジャンプするから1
                _jumpCount = 1;
            }
            else  if(_isMultiJump)
            {
                _jumpCount++;
            }
            
            Vector3 velo = new Vector3(_rb.velocity.x, _jumpPower, _rb.velocity.z);
            _rb.velocity = velo;
        }
    }

}
