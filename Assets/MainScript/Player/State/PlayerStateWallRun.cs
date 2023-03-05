using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateWallRun : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerWallRunning.WallRunStartSet();
        //空中ジャンプの回数調整
        _stateMachine.PlayerController.PlayerJumping.ReSetAirJump();
        _stateMachine.PlayerController.PlayerWallRunning.WallRunAudio();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerWallRunning.EndWallRun();

        _stateMachine.PlayerController.AudioManager.StopLoopPlayerSE();
    }

    public override void LateUpdate()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerWallRunning.WallRuning();
        // _stateMachine.PlayerController.PlayerWallRunning.DoWallRun();

        //速度調整のスクリプト
        // _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void Update()
    {
        //Swing用の標準システムの関数
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();
        //Swingのモード切替
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        if (_stateMachine.PlayerController.PlayerInput.IsJumping || _stateMachine.PlayerController.PlayerWallRunning.CheckForwardWall())
        {
            _stateMachine.PlayerController.PlayerWallRunning.WallRunJump();
            _stateMachine.PlayerController.PlayerJumping.JumpSound();
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);

            return;
        }


        //壁に当たらなくなった場合
        if (!_stateMachine.PlayerController.WallCheck.CheckWallLeft() && !_stateMachine.PlayerController.WallCheck.CheckWallRight())
        {
            //上昇
            if (_stateMachine.PlayerController.Rb.velocity.y >= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.PlayerController.PlayerWallRunning.WallRunJumpAuto();
                _stateMachine.PlayerController.PlayerJumping.JumpSound();
                _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                Debug.Log("WallRun=>UpAir");
                return;
            }

            //下降
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.PlayerController.PlayerWallRunning.WallRunJumpAuto();
                _stateMachine.PlayerController.PlayerJumping.JumpSound();
                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("WallRun=>DownAir");
                return;
            }

        }

    }
}
