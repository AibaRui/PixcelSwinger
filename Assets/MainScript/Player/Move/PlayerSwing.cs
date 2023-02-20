using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    [Header("Swing���̈ړ��̑���")]
    [SerializeField] float _swingMoveSpeedH = 5;

    [Header("�A���J�[�̎h�������ʒu�Ɉ����񂹂鑬��")]
    [SerializeField] float _swingBurst = 7;

    public LineRenderer lr;

    //  Vector3 currentGrapplePosition;

    SpringJoint joint;

    Rigidbody _rb;


    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;

    [SerializeField] Animator _legAnim;

    private void Awake()
    {

    }
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>�X�E�B���O���̓���</summary>
    public void SwingingMove()
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


            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);


            if(Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 dirs = Camera.main.transform.forward;
                _rb.AddForce(dirs.normalized * 10);
                Debug.Log("����");
            }

            if (Input.GetKey(KeyCode.Space))
            {
                joint.maxDistance = distanceFromPoint * 0.5f;
                joint.minDistance = distanceFromPoint * 0.1f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst * 2);
            }
            else
            {
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst);
            }

        }
    }

    public void StartSwing()
    {
        RaycastHit predictionHit = _playerController.PlayerSwingAndGrappleSetting.PredictionHit;
        //swingJointPoint = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;
        //��Ԃ����牽�����Ȃ�
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        _playerController.PlayerSwingAndGrappleSetting.Joint = joint;

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



        lr.positionCount = 2;

        //_tipPos = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;
        //current(�Ӗ�:����)
        //currentGrapplePosition = gunTip.position;
    }


    /// <summary>�X�E�B���O���~</summary>
    public void StopSwing()
    {
        lr.positionCount = 0;
        _playerController.PlayerSwingAndGrappleSetting.Joint = null;

        _legAnim.SetBool("Swing", false);
        Destroy(joint);
    }

    public void LegAnimation()
    {
        _legAnim.SetFloat("Speed", _rb.velocity.y);

        _legAnim.SetBool("Swing", true);

        if (transform.position.x > _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point.x)
        {
            _legAnim.SetBool("IsLeft", true);
            _legAnim.SetBool("IsRight", false);
        }
        else
        {
            _legAnim.SetBool("IsRight", true);
            _legAnim.SetBool("IsLeft", false);
        }

    }


}
