using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{

    [SerializeField] float _swingMoveSpeedH = 5;
    [SerializeField] float _swingMoveSpeedV = 7;
    [SerializeField] float _swinwgSpeedLimit = 15;
    public LineRenderer lr;

    //  Vector3 currentGrapplePosition;

    SpringJoint joint;
    Rigidbody _rb;

    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;
    private void Awake()
    {

    }
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    //void Update()
    //{
    //    if (_waapon.activeSelf)
    //    {

    //    }
    //    else
    //    {
    //        StopSwing();
    //        _control._isWapon = false;
    //    }

    //}



    //private void LateUpdate()
    //{
    //    if (_waapon.activeSelf)
    //    {
    //        DrawRope();
    //    }
    //    else
    //    {
    //        StopSwing();
    //    }
    //}

    /// <summary>�X�E�B���O���̓���</summary>
    public void GrappleMove()
    {
        if (joint != null)
        {
            Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;
            Vector3 swingPoint = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;

            dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
            dir.y = 0;

            if (dir == Vector3.zero)
            {
                // �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ����邾��
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
                //_airVelo = Vector3.zero;
                //_animKatana.SetBool("Move", false);
            }
            else
            {

                _rb.AddForce(dir * _swingMoveSpeedH);
            }


            Vector3 directionToPoint = swingPoint - transform.position;
            _rb.AddForce(directionToPoint.normalized * _swingMoveSpeedV);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
    }



    public void StartGrapple()
    {
        RaycastHit predictionHit = _playerController.PlayerSwingAndGrappleSetting.PredictionHit;

        //��Ԃ����牽�����Ȃ�
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }


        //�A���J�[�̒��n�_�B
        Vector3 swingPoint = predictionHit.point;

        joint = gameObject.AddComponent<SpringJoint>();

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




        //current(�Ӗ�:����)
        // currentGrapplePosition = gunTip.position;
    }


    /// <summary>�X�E�B���O���~</summary>
    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
