using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] GameObject _waapon;
    [SerializeField] float _swingMoveSpeedH = 5;
    [SerializeField] float _swingMoveSpeedV = 7;
    [SerializeField] float _swinwgSpeedLimit = 15;
    public LineRenderer lr;


    [Header("�A���J�[�𔭎˂���ꏊ")]
    [Tooltip("�A���J�[�𔭎˂���ꏊ")] [SerializeField] Transform gunTip;

    [Header("�A���J�[��������ǂ̃��C���[")]
    [Tooltip("�A���J�[��������ǂ̃��C���[")] [SerializeField] LayerMask _wallLayer;

    public float predictionSphereCastRadius;


    [Header("�A���J�[���n�_�̃}�[�J�[")]
    [Tooltip("�A���J�[���n�_�̃}�[�J�[")] [SerializeField] Transform predictionPoint;

    [Header("�A���J�[���h����ő�̒���")]
    [Tooltip("�A���J�[���h����ő�̒���")] [SerializeField] float _maxSwingDistance = 25f;


    /// <summary>�A���J�[�̒��n�_�}�[�J�[�̍��W </summary>
    RaycastHit predictionHit;

    /// <summary>�A���J�[�̎h�������ʒu</summary>
    Vector3 swingPoint;

    GameObject _player;



    //  Vector3 currentGrapplePosition;
    PlayerMoveing _playerMove;
    P_Control _control;

    SpringJoint joint;
    Rigidbody m_rb;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        _control = _player.GetComponent<P_Control>();
        lr = _player.gameObject.GetComponent<LineRenderer>();
        _playerMove = _player.gameObject.GetComponent<PlayerMoveing>();
        m_rb = _player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_waapon.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _control._isSwing = true;
                StartSwing();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _control._isSwing = false;
                StopSwing();
            }

            CheckForSwingPoints();

            if (joint != null)
            {
                SwingingMove();
            }
            _control._isWapon = true;
        }
        else
        {
            StopSwing();
            _control._isWapon = false;
        }

    }

    private void LateUpdate()
    {
        if (_waapon.activeSelf)
        {
            DrawRope();
        }
        else
        {
            StopSwing();
        }
    }


    /// <summary>�A���J�[�̎h���ʒu��T���֐�</summary>
    void CheckForSwingPoints()
    {
        if (joint != null)
        {
            return;
        }

        //�~�`��Cast
        RaycastHit spherCastHit;
        Physics.SphereCast(_player.transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out spherCastHit, _maxSwingDistance, _wallLayer);

        RaycastHit raycastHit;
        Physics.Raycast(_player.transform.position, Camera.main.transform.forward, out raycastHit, _maxSwingDistance, _wallLayer);


        Vector3 realHitPoint;

        //�^������������ꍇ
        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        //��������Ȃ��ǂ����̏ꍇ
        else if (spherCastHit.point != Vector3.zero)
        {
            realHitPoint = spherCastHit.point;
        }

        else
        {
            realHitPoint = Vector3.zero;
        }

        //�}�[�J�[�̏ꏊ�̐ݒ�
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }

        else
        {
            predictionPoint.gameObject.SetActive(false);
        }


        predictionHit = raycastHit.point == Vector3.zero ? spherCastHit : raycastHit;
    }






    /// <summary>�X�E�B���O���̓���</summary>
    void SwingingMove()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");


        if (h > 0) m_rb.AddForce(transform.right * _swingMoveSpeedH);

        if (h < 0) m_rb.AddForce(-transform.right * _swingMoveSpeedH);

        if (v > 0) m_rb.AddForce(transform.forward * _swingMoveSpeedV);

        //Space�������Ă�ԃA���J�[���k��ň���������
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            m_rb.AddForce(directionToPoint.normalized * _swingMoveSpeedV);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }

        //���ɉ����Ă�ԃA���J�[���L�тĉ��ɉ������
        if (v < 0)
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + 5;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

    /// <summary>����`��</summary>
    void DrawRope()
    {
        if (!joint)
        {
            return;
        }

        //   currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        //���������ʒu�����߂�
        //0�͐��������J�n�_
        //�P�͐��������I���_
        lr.positionCount = 2;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    void StartSwing()
    {
        //��Ԃ����牽�����Ȃ�
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        //�X�E�B���O��������O���b�v���͂��Ȃ�
        if (GetComponent<Grapple>() != null)
        {
            GetComponent<Grapple>().StopGrapple();
        }
        if (_player.GetComponent<WallRun>() != null)
        {
            _player.GetComponent<WallRun>().StopWallRun();
        }

        //�A���J�[�̒��n�_��ԓ_�̈ʒu�ɂ���
        swingPoint = predictionHit.point;
        joint = _player.gameObject.AddComponent<SpringJoint>();

        //Anchor(joint�����Ă���I�u�W�F�N�g�̃��[�J�����W  ////��)�����ɂ��Ă閽�j�̈ʒu)
        //connectedAnchor(�A���J�[�̂��Ă�_�̃��[���h���W�@////��)�A���J�[�̐�B�o���W�[�W�����v�̋��́A�x���Ă���Ƃ���)

        //autoConfigureConnectedAnchor�̓W���C���g����������On�ɂȂ��Ă���AAnchor��connectedAnchor(�A���J�[�̐ڒn�_)������
        //�܂莩���̋���ʒu�ɃA���J�[���h���Ă����łԂ牺�����Ă����ԁB�Ȃ̂ŃI�t�ɂ���

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;



        float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);


        //�W���C���g�̒�����ύX(*1���ƃA���J�[���w���Ă��������k�܂�Ȃ����߁A�����ɕ����Ȃ�)
        //�����I�ɒZ�����鎖�ň��������鎖�ɂȂ�
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        //�΂˂̋���
        joint.spring = 4.5f;

        //(spring�̌����)�o�l���r���[���ƐL�т�̂��J��Ԃ��Ă��瓮���Ȃ��Ȃ�܂ł̎��ԁB�l�������قǒZ���Ȃ�
        joint.damper = 7f;

        joint.massScale = 4.5f;



        lr.positionCount = 2;


        //current(�Ӗ�:����)
        // currentGrapplePosition = gunTip.position;
    }


    /// <summary>�X�E�B���O���~</summary>
    public void StopSwing()
    {
        _control._isSwing = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
}
