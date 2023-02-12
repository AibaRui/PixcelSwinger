using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateEventStop : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.TalkManager.DoTalk();
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        if(!_stateMachine.PlayerController.EventController.IsEventNow)
        {
            _stateMachine.PlayerController.EventEriaCheck.IsEnterEventEria = false;
            _stateMachine.TransitionTo(_stateMachine.StateIdle);
            Debug.Log("Event=>Idle");
            return;
        }
    }
}
