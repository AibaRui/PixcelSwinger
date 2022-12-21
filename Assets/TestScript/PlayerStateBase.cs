using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : IState
{
    protected PlayerStateMachine _stateMachine = null;

    public void Init(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}