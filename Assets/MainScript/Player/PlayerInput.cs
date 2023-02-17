using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("�L�[�z�u")]
    [SerializeField] private KeyCode forward = KeyCode.W;
    [SerializeField] private KeyCode back = KeyCode.S;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.Space;

    [Header("�Ȃ̍Đ�/�ꎞ��~")]
    [SerializeField] private KeyCode _muiscStartAndStop = KeyCode.Q;
    [Header("�Ȃ̍Đ��L�[")]
    [SerializeField] private KeyCode _muiscChenge = KeyCode.E;


    /// <summary>�L�[�ɂ�����</summary>
    private Vector3 inputVector;
    public Vector3 InputVector => inputVector;

    [Tooltip("�Ȃ̍Đ�/�ꎞ��~")]
    private bool _isMusicStartAndStop;

    public bool IsMusicStartAndStop => _isMusicStartAndStop;

    [Tooltip("�Ȃ̕ύX")]
    private bool _isMusicChange;

    public bool IsMusicChange => _isMusicChange;
 



    [Tooltip("�X�y�[�X������")]
    private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

    [Tooltip("���N���b�N_����")]
    private bool _isLeftMouseClickDown = false;
    public bool IsLeftMouseClickDown { get => _isLeftMouseClickDown; }

    [Tooltip("���N���b�N_����")]
    private bool _isLeftMouseClickUp = false;
    public bool IsLeftMouseClickUp { get => _isLeftMouseClickUp; }

    [Tooltip("�E�N���b�N_����")]
    private bool _isRightMouseClickDown = false;
    public bool IsRightMouseClickDown { get => _isRightMouseClickDown; }

    [Tooltip("��Ctrl_����")]
    private bool _isCtrlDown;
    public bool IsCtrlDown { get => _isCtrlDown; }

    [Tooltip("��Ctrl_����")]
    private bool _isCtrlUp;
    public bool IsCtrlUp { get => _isCtrlUp; }

    private float _isMouseScrol = 0;

    public float IsMouseScrol => _isMouseScrol;



    [Tooltip("Tab_����")]
    private bool _isTabDown;
    public bool IsTabDown => _isTabDown;

    [Tooltip("��Shift_����")]
    private bool _isLeftShiftDown = false;
    public bool IsLeftShiftDown => _isLeftShiftDown;

    private bool _isLeftShift = false;
    public bool IsLeftShift => _isLeftShift;

    [Tooltip("��Shift_����")]
    private bool _isLeftShiftUp = false;
    public bool IsLeftShiftUp => _isLeftShiftUp;

    private float _horizontalInput;
    public float HorizontalInput { get => _horizontalInput; }

    private float _verticalInput;

    public float VerticalInput { get => _verticalInput; }

    private float yInput;

    public void HandleInput()
    {
        _horizontalInput = 0;
        _verticalInput = 0;

        if (Input.GetKey(forward))
        {
            _verticalInput++;
            // Debug.Log("+++");
        }

        if (Input.GetKey(back))
        {
            _verticalInput--;
        }

        if (Input.GetKey(left))
        {
            _horizontalInput--;
        }

        if (Input.GetKey(right))
        {
            _horizontalInput++;
        }

        //�}�E�X�̍��N���b�N
        _isLeftMouseClickDown = Input.GetMouseButtonDown(0);
        _isLeftMouseClickUp = Input.GetMouseButtonUp(0);


        //�}�E�X�E�N���b�N
        _isRightMouseClickDown = Input.GetMouseButtonDown(1);


        //Ctrl����������
        _isCtrlDown = Input.GetKeyDown(KeyCode.LeftControl);
        //Ctrl�𗣂�����
        _isCtrlUp = Input.GetKeyUp(KeyCode.LeftControl);

        //Space
        _isJumping = Input.GetKeyDown(jump);

        //Tab
        _isTabDown = Input.GetKeyDown(KeyCode.Tab);

        // Shift
        _isLeftShiftDown = Input.GetKeyDown(KeyCode.LeftShift);
        _isLeftShiftUp = Input.GetKeyUp(KeyCode.LeftShift);
        _isLeftShift = Input.GetKey(KeyCode.LeftShift);

        _isMouseScrol = Input.GetAxis("Mouse ScrollWheel");


        //�Ȃ̍Đ�/�ꎞ��~
        _isMusicStartAndStop = Input.GetKeyDown(_muiscStartAndStop);

        //�Ȃ̕ύX
        _isMusicChange = Input.GetKeyDown(_muiscChenge);
    }

    private void Update()
    {
        HandleInput();
    }
}
