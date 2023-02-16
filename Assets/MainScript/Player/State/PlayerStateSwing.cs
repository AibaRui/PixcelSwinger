using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateSwing : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerSwing.StartSwing();
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.Joint = _stateMachine.PlayerController.Player.GetComponent<SpringJoint>();

        //空中ジャンプの回数調整
        _stateMachine.PlayerController.PlayerJumping.ReSetAirJump();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerSwing.StopSwing();
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.Joint = null;

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerSwing.SwingingMove();

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
        //_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();

        _stateMachine.PlayerController.PlayerSwing.LegAnimation();

        //離したらSwingを中止
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickUp)
        {
            //上昇
            if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                Debug.Log("Swing=>UpAir");
            }
            //下降
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("Swing=>DownAir");
            }


            var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
            var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

            if (_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                if ((h != 0 && v != 0))
                {
                    _stateMachine.TransitionTo(_stateMachine.StateIdle);
                    Debug.Log("Swing=>Idle");
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    Debug.Log("Swing=>Move");
                }
            }
        }



    }


}
