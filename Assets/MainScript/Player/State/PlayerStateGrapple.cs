using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateGrapple : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerGrapple.StartGrapple();
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.Joint = _stateMachine.PlayerController.Player.GetComponent<SpringJoint>();

        //空中ジャンプの回数調整
        _stateMachine.PlayerController.PlayerJumping.ReSetAirJump();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerGrapple.StopGrapple();
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.Joint = null;
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerGrapple.GrappleMove();

        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void LateUpdate()
    {
        //縄を描く
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.DrawRope();
    }

    public override void Update()
    {
        //スウィングのポイントの表示
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();

        //離したらSwingを中止
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickUp)
        {
            //上昇
            if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                Debug.Log("Grapple=>UpAir");
                return;
            }
            //下降
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("Grapple=>DownAir");
                return;
            }
        }

        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            if ((h != 0 && v != 0))
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Grapple=>Idle");
                return;
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateWalk);
                Debug.Log("Grapple=>Move");
            }
        }

    }

}

