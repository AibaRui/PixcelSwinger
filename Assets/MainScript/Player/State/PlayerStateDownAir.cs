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

        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();


    }
    public override void Update()
    {
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        //Swing用の標準システムの関数
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();

        //Swingのモード切替
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();
        //Swing
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickDown)
        {
            if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Swing)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSwing);
                Debug.Log("DownAir=>Swing");
                return;
            } //Swingに移行
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("DownAir=>Grapple");
                return;
            }//Grappleに移行
        }





        //段差登りの壁チェック
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();

        //段差登り
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("DownAir=>ClimbWall");
            return;
        }


        //壁に近い。動いている。　時にWallRun
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
