using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateDownAir : PlayerStateBase
{
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }
    public override void LateUpdate()
    {

    }
    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerMoveing.MoveAir();
        _stateMachine.PlayerController.PlayerMoveing.SetDir();

        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();


    }
    public override void Update()
    {
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

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
                Debug.Log("DownAir=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("DownAir=>Grapple");
                return;
            }//Grapple�Ɉڍs
        }





        //�i���o��̕ǃ`�F�b�N
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();

        //�i���o��
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("DownAir=>ClimbWall");
            return;
        }


        //�ǂɋ߂��B�����Ă���B�@����WallRun
        if ((_stateMachine.PlayerController.WallCheck.CheckWallLeft() || _stateMachine.PlayerController.WallCheck.CheckWallRight())
            && (_stateMachine.PlayerController.Rb.velocity.x != 0 || _stateMachine.PlayerController.Rb.velocity.z != 0))
        {
            if (_stateMachine.PlayerController.PlayerWallRunning.IsWallRunCoolTime)
            {
                _stateMachine.TransitionTo(_stateMachine.StateWallRun);
                Debug.Log("DownAir=>WallRun");
                return;
            }
        }

        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.PlayerJumping.IsCanJump)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("DownAir=>JumpAir");
            return;
        }



        if ((h == 0 || v == 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateIdle);
            _stateMachine.PlayerController.PlayerJumping.JumpEndSound();
            return;
        }

        if ((h != 0 || v != 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateWalk);
            _stateMachine.PlayerController.PlayerJumping.JumpEndSound();
            return;
        }


    }
}
