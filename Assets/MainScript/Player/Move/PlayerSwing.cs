using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    [Header("Swing���̈ړ��̑���")]
    [SerializeField] float _swingMoveSpeedH = 5;

    [Header("�A���J�[�̎h�������ʒu�Ɉ����񂹂鑬��")]
    [SerializeField] float _swingBurst = 7;

    [Header("�o�l�̋���")]
    [SerializeField] private float _springPower = 4.5f;

    [Header("�_���p-�̋���")]
    [SerializeField] private float _damperPower = 7;

    [Header("�_���p-�̋���")]
    [SerializeField] private float _massScale = 4.5f;


    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;

    // [SerializeField] Animator _legAnim;

    private SpringJoint _joint;
    private Rigidbody _rb;




    //  Vector3 currentGrapplePosition;
    private void Awake()
    {

    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>�X�E�B���O���̓���</summary>
    public void SwingingMove()
    {
        if (_joint != null)
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


            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 dirs = Camera.main.transform.forward;
                _rb.AddForce(dirs.normalized * 10);
                Debug.Log("����");
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _joint.maxDistance = distanceFromPoint * 0.5f;
                _joint.minDistance = distanceFromPoint * 0.1f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst * 2);
            }
            else
            {
                _joint.maxDistance = distanceFromPoint * 0.8f;
                _joint.minDistance = distanceFromPoint * 0.25f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst);
            }

        }
    }

    public void StartSwing()
    {
        RaycastHit predictionHit = _playerController.PlayerSwingAndGrappleSetting.PredictionHit;

        //��Ԃ����牽�����Ȃ�
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        //swing/grapple�̐ݒ�N���X�Ɏ��g��joint��n��
        _joint = gameObject.AddComponent<SpringJoint>();
        _playerController.PlayerSwingAndGrappleSetting.Joint = _joint;

        //�A���J�[�̒��n�_�B
        Vector3 swingPoint = predictionHit.point;

        //Anchor(joint�����Ă���I�u�W�F�N�g�̃��[�J�����W  ////��)�����ɂ��Ă閽�j�̈ʒu)
        //connectedAnchor(�A���J�[�̂��Ă�_�̃��[���h���W�@////��)�A���J�[�̐�B�o���W�[�W�����v�̋��́A�x���Ă���Ƃ���)

        //autoConfigureConnectedAnchor�̓W���C���g����������On�ɂȂ��Ă���AAnchor��connectedAnchor(�A���J�[�̐ڒn�_)������
        //�܂莩���̋���ʒu�ɃA���J�[���h���Ă����łԂ牺�����Ă����ԁB�Ȃ̂ŃI�t�ɂ���

        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = swingPoint;

        //�����ƃA���J�[�̈ʒu�̊�(�W���C���g)�̒����B
        float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

        //�W���C���g�̒�����ύX(*1���ƃA���J�[���w���Ă��������k�܂�Ȃ����߁A�����ɕ����Ȃ�)
        //�����I�ɒZ�����鎖�ň��������鎖�ɂȂ�
        _joint.maxDistance = distanceFromPoint * 0.8f;
        _joint.minDistance = distanceFromPoint * 0.25f;

        //�΂˂̋���(�̂яk�݂̂��₷��)
        _joint.spring = _springPower;

        //(spring�̌����)�o�l���r���[���ƐL�т�̂��J��Ԃ��Ă��瓮���Ȃ��Ȃ�܂ł̎��ԁB�l�������قǒZ���Ȃ�
        _joint.damper = _damperPower;

        //����
        _joint.massScale = _massScale;

        //_tipPos = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;
        //current(�Ӗ�:����)
        //currentGrapplePosition = gunTip.position;
    }


    /// <summary>�X�E�B���O���~</summary>
    public void StopSwing()
    {
        _playerController.PlayerSwingAndGrappleSetting.Joint = null;

        //legAnim.SetBool("Swing", false);
        Destroy(_joint);
    }

    public void LegAnimation()
    {
        // _legAnim.SetFloat("Speed", _rb.velocity.y);
        // _legAnim.SetBool("Swing", true);

        //if (transform.position.x > _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point.x)
        //{
        //    _legAnim.SetBool("IsLeft", true);
        //    _legAnim.SetBool("IsRight", false);
        //}
        //else
        //{
        //    _legAnim.SetBool("IsRight", true);
        //    _legAnim.SetBool("IsLeft", false);
        //}

    }


}
