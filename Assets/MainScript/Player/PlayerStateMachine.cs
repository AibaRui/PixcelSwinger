using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStateMachine : StateMachine
{

    #region State
    [SerializeField]
    private PlayerStateIdle _stateIdle = default;
    [SerializeField]
    private PlayerStateWalk _stateWalk = default;
    [SerializeField]
    private PlayerStateRun _stateRun = default;
    [SerializeField]
    private PlayerStateJump _stateJump = default;
    [SerializeField]
    private PlayerStateUpAir _stateUpAir = default;
    [SerializeField]
    private PlayerStateDownAir _stateDownAir = default;
    [SerializeField]
    private PlayerStateWallRun _stateWallRun = default;
    [SerializeField]
    private PlayerStateSwing _stateSwing = default;
    [SerializeField]
    private PlayerStateGrapple _stateGrapple = default;
    [SerializeField]
    private PlayerStateOpenInventory _stateOpenInventory = default;
    [SerializeField]
    private PlayerStateEventStop _stateEventStop;
    [SerializeField]
    private PlayerStateClimbWall _stateClimbWall = default;
    [SerializeField]
    private PlayerStateSquat _stateSquat = default;
    [SerializeField]
    private PlayerStateSliding _stateSliding = default;

    private PlayerController _playerController = null;

    public PlayerStateEventStop StateEventStop => _stateEventStop;
    public PlayerStateIdle StateIdle => _stateIdle;
    public PlayerStateWalk StateWalk => _stateWalk;
    public PlayerStateRun StateRun => _stateRun;
    public PlayerStateJump StateJump => _stateJump;
    public PlayerStateUpAir StateUpAir => _stateUpAir;

    public PlayerStateDownAir StateDownAir => _stateDownAir;

    public PlayerStateWallRun StateWallRun => _stateWallRun;

    public PlayerStateSwing StateSwing => _stateSwing;

    public PlayerStateGrapple StateGrapple => _stateGrapple;

    public PlayerStateClimbWall StateClimbWall => _stateClimbWall;

    public PlayerStateSliding StateSliding => _stateSliding;

    public PlayerStateSquat StateSquat => _stateSquat;
    public PlayerStateOpenInventory StateInventory => _stateOpenInventory;
    public PlayerController PlayerController => _playerController;
    #endregion
    [SerializeField]
    private GroundCheck _groundCheck;
    public GroundCheck GroundCheck => _groundCheck;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
        Initialize(_stateIdle);
    }

    protected override void StateInit()
    {
        _stateIdle.Init(this);
        _stateWalk.Init(this);
        _stateJump.Init(this);
        _stateUpAir.Init(this);
        _stateDownAir.Init(this);
        _stateWallRun.Init(this);
        _stateSwing.Init(this);
        _stateGrapple.Init(this);
        _stateClimbWall.Init(this);
        _stateEventStop.Init(this);
        _stateOpenInventory.Init(this);
        _stateSquat.Init(this);
        _stateSliding.Init(this);
        _stateRun.Init(this);
    }

}
