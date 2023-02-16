using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrappleAndSwingSetting : MonoBehaviour
{
    [Header("�A���J�[�𔭎˂���ꏊ")]
    [Tooltip("�A���J�[�𔭎˂���ꏊ")] [SerializeField] private Transform _gunTip;

    [Header("�A���J�[��������ǂ̃��C���[")]
    [Tooltip("�A���J�[��������ǂ̃��C���[")] [SerializeField] LayerMask _wallLayer;

    [Header("�A���J�[���n�_�̃}�[�J�[")]
    [Tooltip("�A���J�[���n�_�̃}�[�J�[")] [SerializeField] Transform _predictionPoint;

    [Header("���C���[�̒���")]
    [Tooltip("���C���[�̒���")] [SerializeField] private List<float> _wireLongs = new List<float>();

    private float _wireLong = 25;

    private int _wireLongsNum = 0;

    [SerializeField] Text _nowWireLongText;

    [SerializeField] Text _nowSetText;
    //Enum
    private SwingOrGrapple _swingOrGrappleEnum = SwingOrGrapple.Swing;

    //Enum�̃v���p�e�B
    public SwingOrGrapple SwingOrGrappleEnum { get => _swingOrGrappleEnum; }


    public float predictionSphereCastRadius;
    public Transform GunTip { get => _gunTip; }

    public Transform PredictionPoint { get => _predictionPoint; }


    /// <summary>�A���J�[�̒��n�_�}�[�J�[�̍��W </summary>
    RaycastHit _predictionHit;

    public RaycastHit PredictionHit { get => _predictionHit; }

    /// <summary>�A���J�[�̎h�������ʒu</summary>
    private Vector3 _swingAndGrapplePoint;

    public Vector3 SwingAndGrapplePoint { get => _swingAndGrapplePoint; }

    private SpringJoint _joint;

    public SpringJoint Joint { set => _joint = value; }

    //  Vector3 currentGrapplePosition;


    [SerializeField] PlayerInput _playerInput;


    public LineRenderer lr;
    public enum SwingOrGrapple
    {
        Swing,
        Grapple,
    }

    private void Start()
    {
        _nowSetText.text = _swingOrGrappleEnum.ToString();
        lr = GetComponent<LineRenderer>();

        _wireLong = _wireLongs[0];
    }

    public void ChangeTypeSwingOrGrapple()
    {
        if (_playerInput.IsRightMouseClickDown)
        {
            if (_swingOrGrappleEnum == SwingOrGrapple.Swing)
            {
                _swingOrGrappleEnum = SwingOrGrapple.Grapple;
                _nowSetText.text = "Grapple";
            }
            else
            {
                _swingOrGrappleEnum = SwingOrGrapple.Swing;
                _nowSetText.text = "Swing";
            }
        }

        //���
        if (_playerInput.IsMouseScrol > 0)
        {
            _wireLongsNum++;
            if(_wireLongsNum==_wireLongs.Count)
            {
                _wireLongsNum = _wireLongs.Count-1;
            }
            _wireLong = _wireLongs[_wireLongsNum];
            _nowWireLongText.text = _wireLong.ToString("00");
        }
        //����
        else if (_playerInput.IsMouseScrol < 0)
        {
            _wireLongsNum--;
            if (_wireLongsNum <0)
            {
                _wireLongsNum = 0;
            }
            _wireLong = _wireLongs[_wireLongsNum];
            _nowWireLongText.text = _wireLong.ToString("00");
        }



    }

    /// <summary>�A���J�[�̎h���ʒu��T���֐�</summary>
    public void CheckForSwingPoints()
    {
        if (_joint != null)
        {
            return;
        }

        _predictionPoint.position = _predictionHit.point;

        //�~�`��Cast
        RaycastHit spherCastHit;
        RaycastHit raycastHit;

        float maxDistance = 0;

        if (_swingOrGrappleEnum == SwingOrGrapple.Swing)
        {
            maxDistance = _wireLong;
        }
        else
        {
            maxDistance = _wireLong;
        }
        Physics.SphereCast(transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out spherCastHit, maxDistance, _wallLayer);


        Physics.Raycast(transform.position, Camera.main.transform.forward, out raycastHit, maxDistance, _wallLayer);


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
            _predictionPoint.gameObject.SetActive(true);
            _predictionPoint.position = realHitPoint;
        }
        else
        {
            _predictionPoint.gameObject.SetActive(false);
        }


        _predictionHit = raycastHit.point == Vector3.zero ? spherCastHit : raycastHit;
    }


    /// <summary>����`��</summary>
    public void DrawRope()
    {
        if (!_joint)
        {
            return;
        }

        //   currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        //���������ʒu�����߂�
        //0�͐��������J�n�_
        //�P�͐��������I���_
        lr.positionCount = 2;
        lr.SetPosition(0, _gunTip.position);
        lr.SetPosition(1, _predictionHit.point);
    }


}
