using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrappleAndSwingSetting : MonoBehaviour
{
    [Header("�A���J�[�𔭎˂���ꏊ")]
    [Tooltip("�A���J�[�𔭎˂���ꏊ")] [SerializeField] private Transform _gunTip;

    [Header("�A���J�[��������ǂ̃��C���[")]
    [Tooltip("�A���J�[��������ǂ̃��C���[")] [SerializeField] private LayerMask _wallLayer;

    [Header("�A���J�[���n�_�̃}�[�J�[")]
    [Tooltip("�A���J�[���n�_�̃}�[�J�[")] [SerializeField] private Transform _predictionPoint;

    [Header("���C���[�̒���")]
    [Tooltip("���C���[�̒���")] [SerializeField] private List<float> _wireLongs = new List<float>();

    [Header("���`��cast�̔��a")]
    [Tooltip("���`��cast�̔��a")] private float predictionSphereCastRadius;


    [Header("HitPoint�̍ŏ���Animation�������b��")]
    [SerializeField] private float _endHitPoitAnimSecond = 0.5f;

    [Header("�A���J�[�̒��n�_������UI")]
    [SerializeField] private GameObject _hitPointer;

    [Header("�A���J�[�̒��n�_������UI")]
    [SerializeField] private RectTransform _hitPointerUI;

    [Header("�A���J�[�̒���������Text")]
    [SerializeField] private Text _nowWireLongText;

    [Header("Swing��Grapple������Text")]
    [SerializeField] private Text _nowSetText;

    [Header("HitPoint�̏����̐ݒ�")]
    [SerializeField] private SwingHitUI _firstSwingHitUISetting;

    [SerializeField] PlayerInput _playerInput;

    [SerializeField] private LineRenderer lr;

    /// <summary>���݂̃��C���[�̒���</summary>
    private float _wireLong = 25;

    /// <summary>���C���[�̒��������ꂽList�̗v�f������</summary>
    private int _wireLongsNum = 0;

    /// <summary>�A���J�[�̎h�������ʒu</summary>
    private Vector3 _swingAndGrapplePoint;

    private float _countTime = 0;

    /// <summary>Hit����Ray�̏��</summary>
    RaycastHit _predictionHit;

    private bool _isEndHitPoinAnim = false;

    private SpringJoint _joint;

    private SwingHitUI _swingHitUISetting;


    private WireLong _wireLongEnum;

    public WireLong WireLongEnum => _wireLongEnum;

    //Enum
    private SwingOrGrapple _swingOrGrappleEnum = SwingOrGrapple.Swing;

    //Enum�̃v���p�e�B
    public SwingOrGrapple SwingOrGrappleEnum { get => _swingOrGrappleEnum; }

    public Transform GunTip { get => _gunTip; }

    public Transform PredictionPoint { get => _predictionPoint; }

    public RaycastHit PredictionHit { get => _predictionHit; }

    public Vector3 SwingAndGrapplePoint { get => _swingAndGrapplePoint; }

    public SpringJoint Joint { set => _joint = value; }

    public SwingHitUI SwingHitUISetting { get => _swingHitUISetting; set => _swingHitUISetting = value; }

    public SwingHitUI FirstSwingHitUISetting => _firstSwingHitUISetting;

    /// <summary>�A���J�[�ݒu����UI�̕\���̎d��</summary>
    public enum SwingHitUI
    {
        //UI��\�����Ȃ�
        NoUI,
        //�ŏ������\��
        First,
        //�����ƕ\��
        All
    }

    /// <summary>Swing��Grapple�̂ǂ���̏�Ԃ�������</summary>
    public enum SwingOrGrapple
    {
        Swing,
        Grapple,
    }

    public enum WireLong
    {
        Short,
        Midiam,
        Long,
    }

    private void Start()
    {
        //Swing/Grapple�̏�Ԃ�����Text��ݒ�
        _nowSetText.text = _swingOrGrappleEnum.ToString();

        //���C���[�̒�����ݒ�
        _wireLong = _wireLongs[0];

    }

    /// <summary>Swing��Grapple�̏�Ԃ����ւ���֐�</summary>
    public void ChangeTypeSwingOrGrapple()
    {
        //�}�E�X�E�N���b�N����������ύX
        //Swing<=>Grapple�@���݂ɕύX����
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
            if (_wireLongsNum == _wireLongs.Count)
            {
                _wireLongsNum = _wireLongs.Count - 1;
            }
            _wireLong = _wireLongs[_wireLongsNum];

            SetEnumWireLong(_wireLongsNum);

            _nowWireLongText.text = _wireLong.ToString("00");
        }
        //����
        else if (_playerInput.IsMouseScrol < 0)
        {
            _wireLongsNum--;
            if (_wireLongsNum < 0)
            {
                _wireLongsNum = 0;
            }
            _wireLong = _wireLongs[_wireLongsNum];
            SetEnumWireLong(_wireLongsNum);
            _nowWireLongText.text = _wireLong.ToString("00");
        }
    }

    public void SetEnumWireLong(int num)
    {
        if (_wireLongsNum == 0)
        {
            _wireLongEnum = WireLong.Short;
        }
        else if (_wireLongsNum == 1)
        {
            _wireLongEnum = WireLong.Midiam;
        }
        else
        {
            _wireLongEnum = WireLong.Long;
        }
    }

    /// <summary>�A���J�[�̎h���ʒu��T���֐�</summary>
    public void CheckForSwingPoints()
    {
        //Join������=�@����Swing���Ȃ�A���J�[���n�_�����܂��Ă���̂ŒT���Ȃ�
        if (_joint != null)
        {
            return;
        }

        //�A���J�[���n�_���������̂��ARay�̓������Ă���|�C���g�Ɉړ�������
        _predictionPoint.position = _predictionHit.point;

        //�~�`��Cast
        RaycastHit spherCastHit;
        RaycastHit raycastHit;

        //�A���J�[���n�_��Ray���΂��ĒT���B
        //Ray�̒����́A���ݐݒ肵�Ă��郏�C���[�̒���
        float maxDistance = _wireLong;

        //���`��Cast
        Physics.SphereCast(transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out spherCastHit, maxDistance, _wallLayer);
        //�������Cast
        Physics.Raycast(transform.position, Camera.main.transform.forward, out raycastHit, maxDistance, _wallLayer);

        //Ray�̓��������ꏊ
        Vector3 realHitPoint;

        //�^������������ꍇ(RayCast��������ꍇ)
        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        //��������Ȃ��ǂ����̏ꍇ(RayCast��������Ȃ����ASphereCast�����������ꍇ)
        else if (spherCastHit.point != Vector3.zero)
        {
            realHitPoint = spherCastHit.point;
        }
        //�ǂ����������Ȃ��ꍇ
        else
        {
            realHitPoint = Vector3.zero;
        }

        //�A���J�[�ݒu�_�̃}�[�J�[�̏ꏊ��ݒ�
        if (realHitPoint != Vector3.zero)
        {
            _predictionPoint.gameObject.SetActive(true);
            _predictionPoint.position = realHitPoint;
        }
        else
        {
            _predictionPoint.gameObject.SetActive(false);
        }

        //ray��������Ȃ�������SpherCast�̏��������
        _predictionHit = raycastHit.point == Vector3.zero ? spherCastHit : raycastHit;
    }

    /// <summary>Swing����[Hit!]��UI��\������d�g��</summary>
    public void ActivePointer()
    {
        if (!_joint)
        {
            return;
        }

        if (_swingHitUISetting == SwingHitUI.NoUI)
        {
            return;
        }
        else if (_swingHitUISetting == SwingHitUI.First)
        {


            if (_countTime <= _endHitPoitAnimSecond)
            {
                PointerView();
                _countTime += Time.deltaTime;
            }
            else
            {
                if (!_isEndHitPoinAnim)
                {
                    _hitPointer.SetActive(false);
                    _isEndHitPoinAnim = true;
                }
            }

        }
        else
        {
            PointerView();
        }
    }

    private void PointerView()
    {
        //�}�[�J�[�̈ʒu���X�N���[����ʂɕϊ����ĕ\������
        var targetWorldPos = PredictionPoint.position;
        var targetScreenPos = Camera.main.WorldToScreenPoint(targetWorldPos);

        _hitPointer.transform.position = targetScreenPos;

        var cameraDir = Camera.main.transform.forward;
        var targetDir = targetWorldPos - Camera.main.transform.position;

        var isFront = Vector3.Dot(targetDir, cameraDir) > 0;
        _hitPointer.SetActive(isFront);
    }


    /// <summary>[Hit!]��UI���\���ɂ���</summary>
    public void AnAtivePointer()
    {
        _isEndHitPoinAnim = false;
        _countTime = 0;
        _hitPointer.SetActive(false);
    }

    /// <summary>Swing���̃��C���[��`��</summary>
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
