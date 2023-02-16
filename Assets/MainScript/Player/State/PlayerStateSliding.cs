using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateSliding : PlayerStateBase
{
    public override void Enter()
    {
        //�X���C�f�B���O�͂���
        _stateMachine.PlayerController.PlayerSquatAndSliding.StartSliding();
    }

    public override void Exit()
    {
        //�X���C�f�B���O�I�����̐ݒ�
        _stateMachine.PlayerController.PlayerSquatAndSliding.StopSliding();
    }

    public override void FixedUpdate()
    {
        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();

        _stateMachine.PlayerController.PlayerSquatAndSliding.SlidingMove();
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
                Debug.Log("Sliding=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Sliding=>Grapple");
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
                Debug.Log("Sliding=>Idle");
                return;
            }

            if((h!=0 || v!=0) && _stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateWalk);
                Debug.Log("Sliding=>Move");
                return;
            }
        }
        //�X���C�f�B���O���I��������ǂ����̔���
        if (_stateMachine.PlayerController.PlayerSquatAndSliding.IsSlidingEnd)
        {
            //���Ⴊ�ݏ�Ԃ�
            if ((h != 0 || v != 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSquat);
                Debug.Log("Sliding=>Squat");
                return;
            }
        }




        //�W�����v
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Sliding=>Jump");
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
