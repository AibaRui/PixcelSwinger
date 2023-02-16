using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateRun : PlayerStateBase
{
    public override void Enter()
    {

    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerMoveing.SetDir();
    }

    public override void LateUpdate()
    {

    }

    public override void FixedUpdate()
    {

        _stateMachine.PlayerController.PlayerMoveing.Move();

        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }



    public override void Update()
    {
        //����A�����A�̐؂�ւ�
        _stateMachine.PlayerController.PlayerMoveing.SpeedChange();

        //�i���o��̕ǃ`�F�b�N
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();

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
                Debug.Log("Run=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Walk=>Grapple");
                return;
            }//Grapple�Ɉڍs
        }

        //���݂͓���
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.GroundCheck.IsGround)
        {

            //Idle��Ԃ�
            if ((h == 0 && v == 0))
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Run=>Idle");
                return;
            }
            else if ((h != 0 || v != 0))
            {
                //Shift���������瑖��
                if (_stateMachine.PlayerController.PlayerInput.IsLeftShiftDown)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    Debug.Log("Run=>Walk");
                    return;
                }
            }
            //ctrl����������X���C�f�B���O
            else if (v > 0)
            {
                if (_stateMachine.PlayerController.PlayerInput.IsCtrlDown)
                {
                    if (_stateMachine.PlayerController.PlayerSquatAndSliding.IsSliding)
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateSliding);
                        Debug.Log("Run=>Sliding");
                        return;
                    }
                    else
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateSquat);
                        Debug.Log("Run=>Squat");
                        return;
                    }
                }
            }
        }





        //�W�����v
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Run=>Jump");
            return;
        }


        //�㏸
        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
            Debug.Log("Run=>UpAir");
        }
        //���~
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
            Debug.Log("Run=>DownAir");
        }

        //�i���o��
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("Run=>ClimbWall");
            return;
        }
    }
}
