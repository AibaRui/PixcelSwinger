using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateSquat : PlayerStateBase
{
    public override void Enter()
    {
        //���Ⴊ��  �n�߂����̒���
        _stateMachine.PlayerController.PlayerSquatAndSliding.StartSquat();
    }

    public override void Exit()
    {
        //���Ⴊ�݂��I�������̒���
        _stateMachine.PlayerController.PlayerSquatAndSliding.StopSquat();
    }

    public override void FixedUpdate()
    {
        //�v���C���[�̂��Ⴊ�ݓ���
        _stateMachine.PlayerController.PlayerSquatAndSliding.SquatMove();

        //�v���C���[�̌�������
        _stateMachine.PlayerController.PlayerSquatAndSliding.SetDir();

        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void LateUpdate()
    {

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
                Debug.Log("Move=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Move=>Grapple");
                return;
            }//Grapple�Ɉڍs
        }

        //���݂͓���
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.PlayerInput.IsCtrlUp)
        {
            //Idle��Ԃ�
            if ((h == 0 && v == 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Squat=>Idle");
                return;
            }
        }

        //�W�����v
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            return;
        }


        //�㏸
        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
        }
        //���~
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
        }
    }

}
