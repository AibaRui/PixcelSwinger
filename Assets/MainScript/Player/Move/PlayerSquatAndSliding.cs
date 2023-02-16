using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSquatAndSliding : MonoBehaviour
{
    [Header("POV�̃J����")]
    [SerializeField] CinemachineVirtualCamera _camera;

    CinemachineTransposer _cameraTransposer;

    [Header("�X���C�f�B���O�̑��x")]
    [Tooltip("�X���C�f�B���O�̑��x")] [SerializeField] float _slidingPower = 10f;

    [Header("�X���C�f�B���O�̎��s����")]
    [Tooltip("�X���C�f�B���O�̎��s����")] [SerializeField] float _slidingTime = 0.5f;

    [Header("�X���C�f�B���O�̃N�[���^�C��")]
    [Tooltip("�X���C�f�B���O�̃N�[���^�C��")] [SerializeField] float _slidingCoolTime = 3f;


    [Header("���Ⴊ�݈ړ��̑���")]
    [Tooltip("���Ⴊ�݈ړ��̑���")] [SerializeField] float _movingSpeedOnGround = 5f;

    [Header("�ʏ�̃J�����̍���")]
    [Tooltip("�ʏ�̃J�����̍���")] [SerializeField] private float _defultCameraHigh = 1.5f;

    [Header("���Ⴊ�݂̃J�����̍���")]
    [Tooltip("���Ⴊ�݂̃J�����̍���")] [SerializeField] private float _squatCameraHigh = 0.5f;


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

    /// <summary>�L�����̐��ʂ����߂�</summary>
    public void SetDir()
    {
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���
        transform.forward = dir;
    }


    /// <summary>�X���C�f�B���O�̎��Ԃ𐔂���</summary>
    /// <returns></returns>
    IEnumerator CountSlidingTime()
    {
        _isSlidingEnd = false;
        yield return new WaitForSeconds(_slidingTime);
        _isSlidingEnd = true;
    }

    /// <summary>�X���C�f�B���O�̃N�[���^�C�����Ԃ𐔂���</summary>
    /// <returns></returns>
    IEnumerator CountSlidingCoolTime()
    {
        _isSliding = false;
        yield return new WaitForSeconds(_slidingTime);
        _isSliding = true;
    }

    /// <summary>�X���C�f�B���O�J�n���ɌĂ�</summary>
    public void StartSliding()
    {
        //�X���C�f�B���O�̎��s���Ԃ𐔂���
        StartCoroutine(CountSlidingTime());

        /// <summary>�J�����̍�����������</summary>
        _cameraTransposer.m_FollowOffset.y = _squatCameraHigh;

    }


    /// <summary>�X���C�f�B���O���̑��x�ݒ�</summary>
    public void SlidingMove()
    {
        Vector3 dir = Camera.main.transform.forward;

        _countSlidingTime += Time.deltaTime;

        if (_countSlidingTime < _slidingTime / 2)
        {
            _rb.velocity = dir.normalized * _slidingPower;
        }

    }

    /// <summary>�X���C�f�B���O�I�����ɌĂ�</summary>
    public void StopSliding()
    {
        //�X���C�f�B���O�̃N�[���^�C���𐔂���
        StartCoroutine(CountSlidingCoolTime());

        /// <summary>�J�����̍�����߂�</summary>
        _cameraTransposer.m_FollowOffset.y = _defultCameraHigh;

        _countSlidingTime = 0;
    }


    public void StartSquat()
    {
        /// <summary>�J�����̍�����߂�</summary>
        _cameraTransposer.m_FollowOffset.y = _squatCameraHigh;
    }

    public void StopSquat()
    {
        /// <summary>�J�����̍�����߂�</summary>
        _cameraTransposer.m_FollowOffset.y = _defultCameraHigh;
    }


    /// <summary>�n��ł̓����Bvelocity��ݒ�</summary>
    public void SquatMove()
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
            _anim.SetFloat("Speed", 1);
        }
    }
}
