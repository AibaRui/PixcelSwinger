using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMoveing : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    [Header("�n��ł̑���")]
    [Tooltip("�n��ł̑���")] [SerializeField] float _movingSpeedOnGround = 5f;

    [Header("�󒆂ł�Add���铮���̑���")]
    [Tooltip("�󒆂ł�Add���铮���̑���")] [SerializeField] float _movingSpeedOnAir = 5f;


    [SerializeField] float _addDownAir = 5;

    [SerializeField] bool _isDownSpeed = false;

    private Vector3 _airVelo;

    PlayerInput _playerInput;

    Rigidbody _rb;
    [SerializeField] Animator _anim;
    [SerializeField] Animator _legAnim;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

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

            Vector3 velo = dir.normalized * _movingSpeedOnGround; // ���͂��������Ɉړ�����
            velo.y = _rb.velocity.y;   // �W�����v�������� y �������̑��x��ێ�����
            _rb.velocity = velo;   // �v�Z�������x�x�N�g�����Z�b�g����
        }

        // ���������̑��x�� Speed �ɃZ�b�g����
        //Vector3 velocity = _rb.velocity;
        // velocity.y = 0f;

        if (_rb.velocity.x != 0 || _rb.velocity.z != 0)
        {
            _anim.SetFloat("Speed", 5);
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


