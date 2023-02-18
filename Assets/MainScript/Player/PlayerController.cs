using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour,ISave
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
    private PlayerSquatAndSliding _playerSquatAndSliding;

    [SerializeField]
    private GroundCheck _groundCheck;

    [SerializeField]
    private WallCheck _wallCheck;

    [SerializeField]
    private PlayerEventEriaCheck _eventEriaCheck;

    [SerializeField]
    private PlayerGrappleAndSwingSetting _playerSwingAndGrappleSetting;

    [SerializeField]
    private PlayerVelocityLimitControl _playerVelocityLimitControl;

    [SerializeField]
    private EventController _eventController;

    [SerializeField]
    private TalkManager _talkManager;

    [SerializeField]
    private InventoryManager _inventoryManager;

    [SerializeField]
    private UIShowSystem _uiShowSystem;

    [SerializeField] private PlayerPositionSaveManager _playerPositionSaveManager;

    public UIShowSystem UIShowSystem => _uiShowSystem;

    public InventoryManager InventoryManager => _inventoryManager;

    public TalkManager TalkManager => _talkManager;

    public EventController EventController => _eventController;

    public PlayerInput PlayerInput => _playerInput;
    public PlayerStateMachine StateMachine => _stateMachine;

    public PlayerIdle PlayerIdle => _playerIdle;

    public PlayerMoveing PlayerMoveing => _playerMove;

    public PlayerJumping PlayerJumping => _playerJumping;

    public PlayerSquatAndSliding PlayerSquatAndSliding => _playerSquatAndSliding;

    public GroundCheck GroundCheck => _groundCheck;

    public PlayerWallRunning PlayerWallRunning => _playerWallRun;

    public PlayerSwing PlayerSwing => _playerSwing;

    public PlayerGrapple PlayerGrapple => _playerGrapple;

    public PlayerClimingWall PlayerClimingWall => _playerClimbWall;

    public PlayerGrappleAndSwingSetting PlayerSwingAndGrappleSetting { get => _playerSwingAndGrappleSetting; }

    public PlayerVelocityLimitControl PlayerVelocityLimitControl => _playerVelocityLimitControl;

    public WallCheck WallCheck => _wallCheck;

    public PlayerEventEriaCheck EventEriaCheck => _eventEriaCheck;

    private Rigidbody _rb;
    public Rigidbody Rb => _rb;

    private GameObject _thisPlayer;

    public GameObject Player { get => _thisPlayer; }

    /// <summary>インターフェイス。データのロードを揃えるための関数</summary>
    public void FistDataLodeOnGameStart()
    {
        _playerPositionSaveManager.SetPlayerPosition();
    }

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
