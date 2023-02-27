using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerWallRunning : MonoBehaviour
{
    [Header("�v���C���[���_�̃J����")]
    [SerializeField] private CinemachineVirtualCamera _playerPOVCamera;

    [Header("�Ǒ���̑���")]
    [SerializeField] private float m_runSpeed = 8;

    [Header("Ray�̒���")]
    [SerializeField] private float m_checkWallRayDistace = 2f;

    [Header("�ǂ̃��C���[")]
    [SerializeField] private LayerMask _wallLayer;

    [Header("�J�������X������ő�p�x")]
    [SerializeField] private float _cameraDutch = 5;

    [Header("�J�������X�����鑬�x")]
    [SerializeField] private float _changeCameraSpeed = 5;

    [Header("�W�����v�̃p���[")]
    [SerializeField] private float _jumpPower = 7;

    [Header("WallRun�̃N�[���^�C��")]
    [SerializeField] private float _wallRunCoolTime = 0.5f;

    [Header("�W�����v�̕���")]
    [SerializeField] private Vector2 _addJumpDir;

    [Header("���ʂ̕ǂ̔���̒���")]
    [SerializeField] private float _layLongOfCheckForwardWallOfEndWallRun = 5;


    /// <summary>��RUN�̐i�ޕ���</summary>
    private Vector3 _wallForward;

    /// <summary>����WallrRun���o���邩�ǂ���</summary>
    private bool _isCanWallRun = false;

    /// <summary>CoolTime���I��������ǂ���</summary>
    private bool _isCoolTime = true;

    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    private bool _rightWallHit = false;
    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    private bool _leftWallHit = false;

    /// <summary>�v���C���[�̎��_�̕����ɕǂ����邩�ǂ���</summary>
    private bool _fowardWallHit = false;

    /// <summary>�J������؂�ւ������ǂ���</summary>
    private bool _isChangeCamera = false;

    /// <summary>WallRun�̎n�܂�́A�ǂ̕����Btrue:�E/false:��</summary>
    private bool _startHitRight;

    /// <summary>���g�̉E���̕ǂ���������Raycast</summary>
    private RaycastHit _rightWall;
    /// <summary>���g�̍����̕ǂ���������Raycast</summary>
    private RaycastHit _leftWall;
    /// <summary>���g�̐��ʂ̕ǂ���������Raycast</summary>
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

    /// <summary>WallRun�̃N�[���^�C���𐔂���֐�</summary>
    /// <returns></returns>
    IEnumerator WallRunCoolTime()
    {
        yield return new WaitForSeconds(_wallRunCoolTime);
        _isCoolTime = true;
    }

    /// <summary>WallRun�J�n���̏����̐ݒ�</summary>
    public void WallRunStartSet()
    {
        //WallRun�̎n�܂�̕ǂ̕���������
        _startHitRight = CheckWallRight();

        //�����ɉ������e��Animation��������
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

    /// <summary>WallRun���ɁA�J�������X������֐�</summary>
    private void ChangeDutch()
    {
        //�ǂ��E�ɂ���ꍇ
        if (_startHitRight)
        {
            //�J�������X����
            _playerPOVCamera.m_Lens.Dutch += Time.deltaTime * _changeCameraSpeed;

            //�ݒ�p�x�܂ŒB������I���B�J�������X���I�������ɂ���
            if (_playerPOVCamera.m_Lens.Dutch >= _cameraDutch)
            {
                _isChangeCamera = true;
                _playerPOVCamera.m_Lens.Dutch = _cameraDutch;
            }
        }
        //�ǂ����ɂ���ꍇ
        else
        {
            //�J�������X����
            _playerPOVCamera.m_Lens.Dutch -= Time.deltaTime * _changeCameraSpeed;

            //�ݒ�p�x�܂ŒB������I���B�J�������X���I�������ɂ���
            if (_playerPOVCamera.m_Lens.Dutch <= -_cameraDutch)
            {
                _isChangeCamera = true;
                _playerPOVCamera.m_Lens.Dutch = -_cameraDutch;
            }
        }
    }

    /// <summary>WallRun�̓�����������֐�</summary>
    public void DoWallRun()
    {
        //�ǂ̕����ɑ��x�������AY���x��0�ɂ���
        _rb.AddForce(_wallForward * m_runSpeed);
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
    }

    /// <summary>WallRun�ő����Ă�������ɕǂ����邩�ǂ������m�F����֐�
    /// �ǂɋl�܂点�Ȃ��悤�ɂ��邽�߁A�i�s�����ɂ������狭���I��WallRun���I��点��</summary>
    /// <returns>�i�s�����ɕǂ����邩�ǂ���</returns>
    public bool CheckForwardWall()
    {
        bool hit = Physics.Raycast(transform.position, _wallForward, out var raycastHit, _layLongOfCheckForwardWallOfEndWallRun, _wallLayer);
        return hit;
    }

    /// <summary>WallRun���s������֐�
    /// 1:�O�ς�p���āA�i�s���������߂�
    /// 2:�J�������X������
    /// 3:���x��������֐����Ă�</summary>
    public void WallRuning()
    {
        //�E�̕ǂɓ����葱���Ă�����
        if (CheckWallRight())
        {
            _rb.useGravity = false;

            //�@�������
            Vector3 wallNomal = _rightWall.normal;
            //�O�ς��g���A�i�s���������
            _wallForward = Vector3.Cross(wallNomal, transform.up);

            Debug.Log("�O��:"+_wallForward);

            //�v���X�ƃ}�C�i�X�̊O�σx�N�g���Ǝ��g�̌����Ă���������ׂ�B
            //�߂�����i�ޕ����Ƃ���
            if ((transform.forward - _wallForward).magnitude > (transform.forward - -_wallForward).magnitude)
            {
                _wallForward = -_wallForward;
            }
        }
        //�E�̕ǂɓ����葱���Ă�����
        else if (CheckWallLeft())
        {
            _rb.useGravity = false;

            //�@�������
            Vector3 wallNomal = _leftWall.normal;
            //�O�ς��g���A�i�s���������
            _wallForward = Vector3.Cross(wallNomal, transform.up);

            //�v���X�ƃ}�C�i�X�̊O�σx�N�g���Ǝ��g�̌����Ă���������ׂ�B
            //�߂�����i�ޕ����Ƃ���
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
        //�ǂɌ������ĂƂ�ł��Ȃ����
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
        else //�����I�ɂƂ�
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


    /// <summary>�ǂɋ߂����ǂ����𔻒f����</summary>
    public bool CheckWallRight()
    {
        _rightWallHit = Physics.Raycast(transform.position, transform.right, out _rightWall, m_checkWallRayDistace, _wallLayer);
        return _rightWallHit;
    }




}
