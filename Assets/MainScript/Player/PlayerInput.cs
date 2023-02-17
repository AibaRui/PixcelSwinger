using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("キー配置")]
    [SerializeField] private KeyCode forward = KeyCode.W;
    [SerializeField] private KeyCode back = KeyCode.S;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.Space;

    [Header("曲の再生/一時停止")]
    [SerializeField] private KeyCode _muiscStartAndStop = KeyCode.Q;
    [Header("曲の再生キー")]
    [SerializeField] private KeyCode _muiscChenge = KeyCode.E;


    /// <summary>キーによる方向</summary>
    private Vector3 inputVector;
    public Vector3 InputVector => inputVector;

    [Tooltip("曲の再生/一時停止")]
    private bool _isMusicStartAndStop;

    public bool IsMusicStartAndStop => _isMusicStartAndStop;

    [Tooltip("曲の変更")]
    private bool _isMusicChange;

    public bool IsMusicChange => _isMusicChange;
 



    [Tooltip("スペースを押す")]
    private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

    [Tooltip("左クリック_押す")]
    private bool _isLeftMouseClickDown = false;
    public bool IsLeftMouseClickDown { get => _isLeftMouseClickDown; }

    [Tooltip("左クリック_離す")]
    private bool _isLeftMouseClickUp = false;
    public bool IsLeftMouseClickUp { get => _isLeftMouseClickUp; }

    [Tooltip("右クリック_押す")]
    private bool _isRightMouseClickDown = false;
    public bool IsRightMouseClickDown { get => _isRightMouseClickDown; }

    [Tooltip("左Ctrl_押す")]
    private bool _isCtrlDown;
    public bool IsCtrlDown { get => _isCtrlDown; }

    [Tooltip("左Ctrl_離す")]
    private bool _isCtrlUp;
    public bool IsCtrlUp { get => _isCtrlUp; }

    private float _isMouseScrol = 0;

    public float IsMouseScrol => _isMouseScrol;



    [Tooltip("Tab_押す")]
    private bool _isTabDown;
    public bool IsTabDown => _isTabDown;

    [Tooltip("左Shift_押す")]
    private bool _isLeftShiftDown = false;
    public bool IsLeftShiftDown => _isLeftShiftDown;

    private bool _isLeftShift = false;
    public bool IsLeftShift => _isLeftShift;

    [Tooltip("左Shift_離す")]
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

        //マウスの左クリック
        _isLeftMouseClickDown = Input.GetMouseButtonDown(0);
        _isLeftMouseClickUp = Input.GetMouseButtonUp(0);


        //マウス右クリック
        _isRightMouseClickDown = Input.GetMouseButtonDown(1);


        //Ctrlを押したか
        _isCtrlDown = Input.GetKeyDown(KeyCode.LeftControl);
        //Ctrlを離したか
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


        //曲の再生/一時停止
        _isMusicStartAndStop = Input.GetKeyDown(_muiscStartAndStop);

        //曲の変更
        _isMusicChange = Input.GetKeyDown(_muiscChenge);
    }

    private void Update()
    {
        HandleInput();
    }
}
