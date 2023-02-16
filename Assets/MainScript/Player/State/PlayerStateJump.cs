using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateJump : PlayerStateBase
{
    public override void Enter()
    {
        if(_stateMachine.PlayerController.GroundCheck.IsGround)
        {
        _stateMachine.PlayerController.PlayerJumping.Jump();
        }
        else
        {
            _stateMachine.PlayerController.PlayerJumping.AirJump();
        }

    }

    public override void Exit()
    {

    }
    public override void LateUpdate()
    {

    }
    public override void FixedUpdate()
    {
        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void Update()
    {

        //Swing�p�̕W���V�X�e���̊֐�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();
        //Swing�̃��[�h�ؑ�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        //Swing
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickDown)
        {
            if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Swing)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSwing);
                Debug.Log("Jump=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Jump=>Grapple");
                return;
            }//Grapple�Ɉڍs
        }

        if (_stateMachine.PlayerController.Rb.velocity.y>0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
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
