using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateMissionCheck : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.Rb.velocity = Vector3.zero;
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
       if(!_stateMachine.PlayerController.MissionManager.CheckMission.IsTalkNow)
        {
            _stateMachine.TransitionTo(_stateMachine.StateIdle);
            Debug.Log("Talk=>Idle");
        }

    }


}
