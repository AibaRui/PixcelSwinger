using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMoveing : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    [Header("�����̑���")]
    [Tooltip("�����̑���")] [SerializeField] float _walkSpeed = 5f;

    [Header("����̑���")]
    [Tooltip("����̑���")] [SerializeField] float _runSpeed = 10f;

    [Header("�󒆂ł�Add���铮���̑���")]
    [Tooltip("�󒆂ł�Add���铮���̑���")] [SerializeField] float _movingSpeedOnAir = 5f;

    [Header("�ȒP_�󒆂ŉ������ɉ������鑬��")]
    [SerializeField] float _addDownAirEazy = 5;

    [Header("����_�󒆂ŉ������ɉ������鑬��")]
    [SerializeField] float _addDownAirNomal = 5;

    [Header("�󒆂ŉ������ɉ������鑬��")]
    [SerializeField] bool _isDownSpeed = false;

    [Header("�ŏ��̐ݒ�")]
    [SerializeField] private bool _isFirstPushChange = true;

    [Header("�e")]
    [SerializeField] private Animator _anim;

    [Header("����")]
    [SerializeField] private AudioClip _walkSound;

    [Header("����̑���")]
    [SerializeField] private AudioClip _runSound;

    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] PlayerVelocityLimitControl _playerVelocityLimitControl;

    private float _moveSpeed = 5;

    /// <summary>true�̎���</summary>
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



    /// <summary>������̕��@��ς���</summary>
    /// <param name="isPush">True�Ő؂�ւ�/False�ŉ����Ă����</param>
    public void ChangeRunWay(bool isPush)
    {
        _moveSpeed = _walkSpeed;

        _isPushChange = isPush;

        //true��������؂�ւ�
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


    /// <summary>�L�����̐��ʂ����߂�</summary>
    public void SetDir()
    {
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���
        transform.forward = dir;

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        // ���͕����̃x�N�g����g�ݗ��Ă�
        _airVelo = Vector3.forward * v + Vector3.right * h;
        _airVelo = Camera.main.transform.TransformDirection(_airVelo);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
        _airVelo.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���
    }


    /// <summary>�n��ł̑��铮���Bvelocity��ݒ�</summary>
    public void Move()
    {
        // �����̓��͂��擾���A���������߂�
        // ���͕����̃x�N�g����g�ݗ��Ă�
        Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;

        if (dir == Vector3.zero)
        {
            // �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ����邾��
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
            //_airVelo = Vector3.zero;
            //_animKatana.SetBool("Move", false);
        }
        else
        {
            //_animKatana.SetBool("Move", true);

            // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
            dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
            dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���

            Vector3 velo = dir.normalized * _moveSpeed; // ���͂��������Ɉړ�����
            velo.y = _rb.velocity.y;   // �W�����v�������� y �������̑��x��ێ�����
            _rb.velocity = velo;   // �v�Z�������x�x�N�g�����Z�b�g����
        }

        // ���������̑��x�� Speed �ɃZ�b�g����
        //Vector3 velocity = _rb.velocity;
        // velocity.y = 0f;

        _anim.SetFloat("Speed", _moveSpeed);
    }

    /// <summary>�󒆂ł̓����BAddForce�Ő���</summary>
    public void MoveAir()
    {
        // �����̓��͂��擾���A���������߂�
        // ���͕����̃x�N�g����g�ݗ��Ă�
        Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;

        if (dir == Vector3.zero)
        {
            // �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ����邾��
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            //_airVelo = Vector3.zero;
            //_animKatana.SetBool("Move", false);
        }
        else
        {
            //_animKatana.SetBool("Move", true);

            // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
            dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
            dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���

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


