using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateWallRun : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerWallRunning.WallRunStartSet();
        //�󒆃W�����v�̉񐔒���
        _stateMachine.PlayerController.PlayerJumping.ReSetAirJump();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerWallRunning.EndWallRun();
    }

    public override void LateUpdate()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerWallRunning.WallRuning();
        // _stateMachine.PlayerController.PlayerWallRunning.DoWallRun();

        //���x�����̃X�N���v�g
       // _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void Update()
    {
        //Swing�p�̕W���V�X�e���̊֐�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();
        //Swing�̃��[�h�ؑ�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        if (_stateMachine.PlayerController.PlayerInput.IsJumping)
        {
            _stateMachine.PlayerController.PlayerWallRunning.WallRunJump();
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
            
            return;
        }



        //�ǂɓ�����Ȃ��Ȃ����ꍇ
        if (!_stateMachine.PlayerController.WallCheck.CheckWallLeft() && !_stateMachine.PlayerController.WallCheck.CheckWallRight())
        {
            //�㏸
            if (_stateMachine.PlayerController.Rb.velocity.y >= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.PlayerController.PlayerWallRunning.WallRunJumpAuto();
                _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                Debug.Log("WallRun=>UpAir");
                return;
            }

            //���~
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.PlayerController.PlayerWallRunning.WallRunJumpAuto();
                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("WallRun=>DownAir");
                return;
            }

        }

    }
}
