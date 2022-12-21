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

    /// <summary>キーによる方向</summary>
    public Vector3 InputVector => inputVector;

    public bool IsJumping { get => isJumping; set => isJumping = value; }

    private Vector3 inputVector;
    private bool isJumping;


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

        isJumping = Input.GetKeyDown(jump);
    }

    private void Update()
    {
        HandleInput();
    }
}
