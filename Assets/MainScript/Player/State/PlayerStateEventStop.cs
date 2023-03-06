using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateEventStop : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.TalkManager.DoTalk();
        _stateMachine.PlayerController.AudioManager.StartTalk();

        //話している間は、アシストパネルを出す計算時間に含めない
        _stateMachine.PlayerController.IsMove = false;
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.IsMove = true;
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
