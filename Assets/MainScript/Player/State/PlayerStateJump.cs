using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateJump : PlayerStateBase
{
    public override void Enter()
    {
        if (_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.PlayerController.PlayerJumping.Jump();
            _stateMachine.PlayerController.PlayerJumping.JumpSound();
        }
        else
        {
            _stateMachine.PlayerController.PlayerJumping.AirJump();
            _stateMachine.PlayerController.PlayerJumping.JumpSound();
        }

        //ループの音を消す
        _stateMachine.PlayerController.AudioManager.StopLoopPlayerSE();
    }

    public override void Exit()
    {

    }
    public override void LateUpdate()
    {

    }
    public override void FixedUpdate()
    {
        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void Update()
    {
        //Swingのモード切替
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        //Swing/Grappeのワイヤーの刺せる場所を探す
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();

        //Swing
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickDown && _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.IsHit)
        {
            if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Swing)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSwing);
                Debug.Log("Jump=>Swing");
                return;
            } //Swingに移行
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Jump=>Grapple");
                return;
            }//Grappleに移行
        }

        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
        }

        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
        }


        if (_stateMachine.PlayerController.PlayerInput.IsJumping)
            _stateMachine.TransitionTo(_stateMachine.StateJump);
    }
}
