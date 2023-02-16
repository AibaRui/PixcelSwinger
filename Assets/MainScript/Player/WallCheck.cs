using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [Header("�o���ǂ̃��C���[")]
    [SerializeField] LayerMask _layerClimbWall;

    [Header("���ʂ�Ray")]
    [SerializeField] float _boxCastFowardMidleX = 1;
    [SerializeField] float _boxCastFowardMidleY = 1;
    [SerializeField] float _boxCastFowardMidleZ = 1;
    [SerializeField] Vector3 _posAddFowardMidle;

    [Header("���ʏ��Ray�̏�̒���")]
    [SerializeField] float _upWallCheckLayLongY = 4;

    [Header("���ʏ��Ray�̐��ʂ̒���")]
    [SerializeField] float _upWallCheckLayLongZ = 3;

    [Header("���ʉ���Ray�̏�̒���")]
    [SerializeField] float _downWallCheckLayLongY = 4;

    [Header("���ʉ���Ray�̐��ʂ̒���")]
    [SerializeField] float _downWallCheckLayLongZ = 3;


    /// <summary>�O���ɕǂ����邩�ǂ���</summary>
    private bool _fowardWall;

    /// <summary>�o��ǂ̏�ɕǂ����邩�ǂ���</summary>
    private bool _UpWall;

    private bool _downWall;

    public bool FowardWall { get => _fowardWall; }

    public bool UpWall { get => _UpWall; }

    public bool DownWall { get => _downWall; }

    RaycastHit hitFowardWal;

    private bool _isClimb;

    public bool IsClimb { get => _isClimb; }


    //�Ǒ���̐ݒ�

    [Header("����Ray�̒���(WalllRun�p)")]
    [SerializeField] float _checkWallRayDistace = 2f;

    [Header("�ǂ̃��C���[")]
    [SerializeField] LayerMask _wallLayer;

    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    bool _rightWallHit = false;
    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    bool _leftWallHit = false;
    RaycastHit m_rightWall;
    RaycastHit m_leftWall;

    /// <summary>��RUN��</summary>
    bool _isWallRun;
    /// <summary>��RUN���ł��邩�ǂ���</summary>
    bool _okWallRun = true;

    /// <summary>��RUN�̐i�ޕ���</summary>
    Vector3 wallForward;


    Vector3 jumpFowrd;

    void Start()
    {

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward + _posAddFowardMidle, 
            new Vector3(_boxCastFowardMidleX, _boxCastFowardMidleY, _boxCastFowardMidleZ));

        //���ʏ��Ray
        Vector3 startUp1 = this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp1 = startUp1 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp2 = transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp2 = startUp2 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp3 = -transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp3 = startUp3 + transform.forward * _upWallCheckLayLongZ;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startUp1, endUp1);
        Gizmos.DrawLine(startUp2, endUp2);
        Gizmos.DrawLine(startUp3, endUp3);

        //���ʉ���Ray
        Vector3 startDown = this.transform.position + -transform.up * _downWallCheckLayLongY;
        Vector3 endDown = startDown + transform.forward * _downWallCheckLayLongZ;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startDown, endDown);


        //WallRUn�p����Ray
        Gizmos.color = Color.yellow;
        Vector3 rightEnd = transform.position + transform.right * _checkWallRayDistace;
        Vector3 leftEnd = transform.position + -transform.right * _checkWallRayDistace;
        Gizmos.DrawLine(transform.position, rightEnd);
        Gizmos.DrawLine(transform.position, leftEnd);
    }


    /// <summary>�i���`�F�b�N</summary>
    public void CheckClimbWall()
    {
        //���Ray(���ʂ̏�ɕǂ����邩�ǂ���)
        Vector3 startUp1 = this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp1 = startUp1 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp2 = transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp2 = startUp2 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp3 = -transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp3 = startUp3 + transform.forward * _upWallCheckLayLongZ;

        bool upWallMidle = Physics.Linecast(startUp1, endUp1, _layerClimbWall);
        bool upWallRight = Physics.Linecast(startUp2, endUp2, _layerClimbWall);
        bool upWallLeft = Physics.Linecast(startUp3, endUp3, _layerClimbWall);

        //����Ray(���ʂ̑����ɕǂ����邩�ǂ���)
        Vector3 startDown = this.transform.position + -transform.up * _downWallCheckLayLongY;
        Vector3 endDown = startDown + transform.forward * _downWallCheckLayLongZ;

        _downWall = Physics.Linecast(startDown, endDown, _layerClimbWall);

        //���ʂ̕�
        _fowardWall = Physics.BoxCast(transform.position + _posAddFowardMidle, new Vector3(_boxCastFowardMidleX, _boxCastFowardMidleY, _boxCastFowardMidleZ), transform.forward, out hitFowardWal, Quaternion.identity, 1.0f, _layerClimbWall);



        if ((!upWallMidle && !upWallLeft && ! upWallRight) &&( _fowardWall || _downWall))
        {
            _isClimb = true;
        }
        else
        {
            _isClimb = false;
        }
    }




    /// <summary>�ǂɋ߂����ǂ����𔻒f����</summary>
    public bool CheckWallLeft()
    {
        _leftWallHit = Physics.Raycast(transform.position, -transform.right, out m_leftWall, _checkWallRayDistace, _wallLayer);
        return _leftWallHit;
    }


    /// <summary>�ǂɋ߂����ǂ����𔻒f����</summary>
    public bool CheckWallRight()
    {
        _rightWallHit = Physics.Raycast(transform.position, transform.right, out m_rightWall, _checkWallRayDistace, _wallLayer);
        return _rightWallHit;
    }

}
