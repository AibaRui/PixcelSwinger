using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateUpAir : PlayerStateBase
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
        _stateMachine.PlayerController.PlayerMoveing.LegAnimation();

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
                Debug.Log("UpAir=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("UpAir=>Grapple");
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
        }

        //�ǂɋ߂��B�����Ă���B�@����WallRun
        if ((_stateMachine.PlayerController.WallCheck.CheckWallLeft() || _stateMachine.PlayerController.WallCheck.CheckWallRight())
            && (_stateMachine.PlayerController.Rb.velocity.x != 0 || _stateMachine.PlayerController.Rb.velocity.z != 0))
        {
            if (_stateMachine.PlayerController.PlayerWallRunning.IsWallRunCoolTime)
            {
                _stateMachine.TransitionTo(_stateMachine.StateWallRun);
                Debug.Log("UpAir=>WallRun");
                return;
            }
        }

        //�W�����v
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.PlayerJumping.IsCanJump)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("UpAir=>JumpAir");
            return;
        }

        //���~
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
        }

        //Idle���ړ�
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

    }
}
