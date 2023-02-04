using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerWallRunning : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    [Header("�Ǒ���̑���")]
    [SerializeField] float m_runSpeed = 8;

    [Header("Ray�̒���")]
    [SerializeField] float m_checkWallRayDistace = 2f;

    [Header("�ǂ̃��C���[")]
    [SerializeField] LayerMask _wallLayer;

    [Header("�J�������X�����鑬�x")]
    [SerializeField] float _changeCameraSpeed = 5;

    [Header("�W�����v�̃p���[")]
    [SerializeField] float _jumpPower = 7;

    [Header("�W�����v�̃p���[")]
    [SerializeField] float _wallRunCoolTime = 0.5f;

    [SerializeField] Vector2 _addJumpDir;

    [Header("�N�[���^�C��")]
    [SerializeField] float _coolTime = 0.3f;

    private bool _isCanWallRun = false;

    public bool IsCanWallRun { get => _isCanWallRun; }

    private bool _isCoolTime = true;

    public bool IsCoolTime;

    public bool IsWallRunCoolTime { get => _isCoolTime; }

    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    bool m_rightWallHit = false;
    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    bool m_leftWallHit = false;

    bool m_fowardWallHit = false;

    RaycastHit m_rightWall;
    RaycastHit m_leftWall;
    RaycastHit m_fowardWall;

    bool _startHitRight;

    /// <summary>��RUN�̐i�ޕ���</summary>
    Vector3 wallForward;

    bool _isChangeCamera = false;

    Vector3 jumpFowrd;


    [SerializeField] Animator _anim;
    [SerializeField] AudioSource _aud;
    Rigidbody _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

     IEnumerator WallRunCoolTime()
    {
        yield return new WaitForSeconds(_wallRunCoolTime);
        _isCoolTime = true;
    }

    public void WallRunStartSet()
    {
        _startHitRight = CheckWallRight();
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

    private void ChangeDutch()
    {
        if (_startHitRight)
        {
            _camera.m_Lens.Dutch += Time.deltaTime * _changeCameraSpeed;

            if (_camera.m_Lens.Dutch >= 20)
            {
                _isChangeCamera = true;
                _camera.m_Lens.Dutch = 20;
            }
        }
        else
        {
            _camera.m_Lens.Dutch -= Time.deltaTime * _changeCameraSpeed;

            if (_camera.m_Lens.Dutch <= -20)
            {
                _isChangeCamera = true;
                _camera.m_Lens.Dutch = -20;
            }
        }
    }

    public void DoWallRun()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(wallForward * m_runSpeed);  


        Debug.Log("WallRunning");
    }



    public void WallRuning()
    {
        if (CheckWallRight())
        {
            _rb.useGravity = false;
            Vector3 wallNomal = m_rightWall.normal;
            wallForward = Vector3.Cross(wallNomal, transform.up);

            //�O�σx�N�g�����}�C�i�X�������ꍇ�A������ς���B
            if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude)
            {
                wallForward = -wallForward;
            }
        }
        else if (CheckWallLeft())
        {
            _rb.useGravity = false;
            Vector3 wallNomal = m_leftWall.normal;
            wallForward = Vector3.Cross(wallNomal, transform.up);

            if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude)
            {
                wallForward = -wallForward;
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
            Vector3 dir = wallForward + -transform.right + transform.up;
            _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
        }
        else
        {
            Vector3 dir = wallForward + transform.right + transform.up;
            _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
        }
    }

    public void WallRunJump()
    {
        //�ǂɌ������ĂƂ�ł��Ȃ����
        if (!CheckfowardWall())
        {
            if (_startHitRight)
            {
                Vector3 dir = Camera.main.transform.forward + transform.up * _addJumpDir.y + (-transform.right*_addJumpDir.x);
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
                Vector3 dir = wallForward + -transform.right * _addJumpDir.x + transform.up * _addJumpDir.y;
                _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
            }
            else
            {
                Vector3 dir = wallForward + transform.right * _addJumpDir.x + transform.up * _addJumpDir.y;
                _rb.AddForce(dir.normalized * _jumpPower, ForceMode.Impulse);
            }
        }
    }


    public void EndWallRun()
    {
        _camera.m_Lens.Dutch = 0;
        _rb.useGravity = true;
        _anim.SetBool("IsWallRun", false);
        _isChangeCamera = false;
        _isCoolTime = false;
        StartCoroutine(WallRunCoolTime());
    }



    bool CheckfowardWall()
    {
        m_fowardWallHit = Physics.Raycast(transform.position, Camera.main.transform.forward, out m_fowardWall, 1, _wallLayer);
        return m_fowardWallHit;
    }

    public bool CheckWallLeft()
    {
        m_leftWallHit = Physics.Raycast(transform.position, -transform.right, out m_leftWall, m_checkWallRayDistace, _wallLayer);
        return m_leftWallHit;
    }


    /// <summary>�ǂɋ߂����ǂ����𔻒f����</summary>
    public bool CheckWallRight()
    {
        m_rightWallHit = Physics.Raycast(transform.position, transform.right, out m_rightWall, m_checkWallRayDistace, _wallLayer);
        return m_rightWallHit;
    }




}
