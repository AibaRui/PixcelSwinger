using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    private PlayerStateMachine _stateMachine = default;

    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private PlayerIdle _playerIdle;

    [SerializeField]
    private PlayerMoveing _playerMove;

    [SerializeField]
    private PlayerJumping _playerJumping;

    [SerializeField]
    private PlayerWallRunning _playerWallRun;

    [SerializeField]
    private PlayerSwing _playerSwing;

    [SerializeField]
    private PlayerGrapple _playerGrapple;

    [SerializeField]
    private PlayerClimingWall _playerClimbWall;

    [SerializeField]
    private GroundCheck _groundCheck;

    [SerializeField]
    private WallCheck _wallCheck;

    [SerializeField]
    private PlayerGrappleAndSwingSetting _playerSwingAndGrappleSetting;

    [SerializeField]
    private PlayerVelocityLimitControl _playerVelocityLimitControl;

    [SerializeField]
    private MissionManager _missionManager;

    public PlayerInput PlayerInput => _playerInput;
    public PlayerStateMachine StateMachine => _stateMachine;

    public PlayerIdle PlayerIdle => _playerIdle;

    public PlayerMoveing PlayerMoveing => _playerMove;

    public PlayerJumping PlayerJumping => _playerJumping;

    public GroundCheck GroundCheck => _groundCheck;

    public PlayerWallRunning PlayerWallRunning => _playerWallRun;

    public PlayerSwing PlayerSwing => _playerSwing;


    public PlayerGrapple PlayerGrapple => _playerGrapple;

    public PlayerClimingWall PlayerClimingWall => _playerClimbWall;

    public PlayerGrappleAndSwingSetting PlayerSwingAndGrappleSetting { get => _playerSwingAndGrappleSetting; }

    public PlayerVelocityLimitControl PlayerVelocityLimitControl => _playerVelocityLimitControl;

    public MissionManager MissionManager { get => _missionManager; }

    public WallCheck WallCheck => _wallCheck;

    private Rigidbody _rb;
    public Rigidbody Rb => _rb;

    private GameObject _thisPlayer;

    public GameObject Player { get => _thisPlayer; }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _stateMachine.Init(this);
        _thisPlayer = this.gameObject;
    }
    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void LateUpdate()
    {
        _stateMachine.LateUpdate();
    }

}
