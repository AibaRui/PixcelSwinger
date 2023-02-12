using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [Header("�o���ǂ̃��C���[")]
    [SerializeField] LayerMask _layerClimbWall;

    [SerializeField] float _boxCastX = 1;
    [SerializeField] float _boxCastY = 1;
    [SerializeField] float _boxCastZ = 1;

    [SerializeField] float _downLong;

    [SerializeField] float _upLayerLong;

    [SerializeField] Vector3 posAdd;
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

    [Header("Ray�̒���")]
    [SerializeField] float m_checkWallRayDistace = 2f;

    [Header("�ǂ̃��C���[")]
    [SerializeField] LayerMask _wallLayer;

    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    bool m_rightWallHit = false;
    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    bool m_leftWallHit = false;
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
        Gizmos.DrawWireCube(transform.position + transform.forward + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ));

        Vector3 start = this.transform.position + transform.up * 4;
        Vector3 end = start + transform.forward * _upLayerLong;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start, end);

        Vector3 start2 = this.transform.position + -transform.up * _downLong;
        Vector3 end2 = start2 + transform.forward * _upLayerLong;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start2, end2);
    }


    /// <summary>�i���`�F�b�N</summary>
    public void CheckClimbWall()
    {
        Vector3 start = this.transform.position + transform.up * 4;
        Vector3 end = start + transform.forward * _upLayerLong;

        Vector3 start2 = this.transform.position + -transform.up * _downLong;
        Vector3 end2 = start2 + transform.forward * _upLayerLong;

        _UpWall = Physics.Linecast(start, end, _layerClimbWall); // ���������C���ɉ������Ԃ����Ă����� true �Ƃ���
        Quaternion r = Camera.main.transform.rotation;

        _fowardWall = Physics.BoxCast(transform.position + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ), transform.forward, out hitFowardWal, r, 1.0f, _layerClimbWall);



        _downWall = Physics.Linecast(start2, end2, _layerClimbWall);

        if (!_UpWall && _fowardWall)
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
