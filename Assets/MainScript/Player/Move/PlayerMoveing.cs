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

    [Header("�󒆂ŉ������ɉ������鑬��")]
    [SerializeField] float _addDownAir = 5;

    [SerializeField] bool _isDownSpeed = false;

    [Header("����_�������p�l��")]
    [Tooltip("����_�������p�l��")] [SerializeField] private GameObject _walkPanel;

    [Header("����_�������p�l��")]
    [Tooltip("����_�������p�l��")] [SerializeField] private GameObject _runPanel;

    private float _moveSpeed = 5;



    /// <summary>true�̎���</summary>
    private bool _moveTypeIsWalk = true;

    public bool IsWalk => _moveTypeIsWalk;

    private bool _isPushChangeOrPushing = true;


    private Vector3 _airVelo;

    PlayerInput _playerInput;

    Rigidbody _rb;
    [SerializeField] Animator _anim;
    [SerializeField] Animator _legAnim;

    private MoveSpeedChangeType _moveSpeedChangeType = MoveSpeedChangeType.Push;

    public MoveSpeedChangeType MoveSpeedChangeTypes => _moveSpeedChangeType;

    public enum MoveSpeedChangeType
    {
        Push,
        Pushing,
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _moveSpeed = _walkSpeed;
    }

    /// <summary>�{�^���ŌĂяo��</summary>
    /// <param name="type"></param>
    public void MoveSpeedChangeTypeChange(bool _isPush)
    {
        _moveSpeed = _walkSpeed;

        if (_isPush)
        {
            _moveSpeedChangeType = MoveSpeedChangeType.Push;
            _moveTypeIsWalk = true;
        }
        else
        {
            _moveSpeedChangeType = MoveSpeedChangeType.Pushing;
        }

    }

    public void SpeedChange()
    {
        if (_moveSpeedChangeType == MoveSpeedChangeType.Push)
        {
            if (_playerInput.IsLeftShiftDown)
            {
                _moveTypeIsWalk = !_moveTypeIsWalk;

                if (_moveTypeIsWalk)
                {
                    _moveSpeed = _walkSpeed;

                    _walkPanel?.SetActive(true);
                    _runPanel?.SetActive(false);
                }
                else
                {
                    _moveSpeed = _runSpeed;

                    _walkPanel?.SetActive(false);
                    _runPanel?.SetActive(true);
                }
            }
        }
        else
        {
            if (_playerInput.IsLeftShift)
            {
                _moveSpeed = _runSpeed;

                _walkPanel?.SetActive(false);
                _runPanel?.SetActive(true);
            }
            else
            {
                _moveSpeed = _walkSpeed;

                _walkPanel?.SetActive(true);
                _runPanel?.SetActive(false);
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

        if (_rb.velocity.x != 0 || _rb.velocity.z != 0)
        {
            _anim.SetFloat("Speed", _moveSpeed);
        }


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

        if (_isDownSpeed) _rb.AddForce(-transform.up * _addDownAir);
    }



    public void LegAnimation()
    {
        _legAnim.SetFloat("Speed", _rb.velocity.y);
    }


}


