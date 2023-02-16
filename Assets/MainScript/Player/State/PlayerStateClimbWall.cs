using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateClimbWall : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerClimingWall.IsClimb = true;
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerClimingWall.Climb();
    }

    public override void Update()
    {
        //段差登りの壁チェック
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();


        if (!_stateMachine.PlayerController.PlayerClimingWall.IsClimb)
        {
            var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
            var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;



            //Idleか移動
            if (_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                if ((h == 0 || v == 0))
                {
                    _stateMachine.TransitionTo(_stateMachine.StateIdle);
                    Debug.Log("Climb=>Idle");
                    return;
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    Debug.Log("Climb=>Move");
                    return;
                }

            }
            //上昇
            if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                Debug.Log("Climb=>UpAir");
                return;
            }

            //下降
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("Climb=>DownAir");
                return;
            }


        }
    }

    public override void LateUpdate()
    {

    }
}
