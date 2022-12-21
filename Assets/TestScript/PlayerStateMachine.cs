using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateMachine : StateMachine
{
    //#region State
    //[SerializeField]
    //private PlayerStateIdle _stateIdle = default;
    //[SerializeField]
    //private PlayerStateMove _stateMove = default;
    //[SerializeField]
    //private PlayerStateJump _stateJump = default;

    //private PlayerController _playerController = null;

    //public PlayerStateIdle StateIdle => _stateIdle;
    //public PlayerStateMove StateMove => _stateMove;
    //public PlayerStateJump StateJump => _stateJump;
    //public PlayerController PlayerController => _playerController;

    protected override void StateInit()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        // var groundChecker = GetComponent<GroundedChecker>();
        //_stateIdle.Init(this);
        //_stateMove.Init(this);
        //_stateJump.Init(this);
    }

    void Start()
    {
       // Initialize(_stateIdle);
    }
}
