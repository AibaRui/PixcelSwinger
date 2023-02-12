using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateMove : PlayerStateBase
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

        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }



    public override void Update()
    {

        if (_stateMachine.PlayerController.EventEriaCheck.IsEnterEventEria)
        {
            if (_stateMachine.PlayerController.PlayerInput.IsJumping)
            {
                _stateMachine.TransitionTo(_stateMachine.StateEventStop);
                Debug.Log("Move=>EventStop");
                return;
            }
        }

        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        //段差登りの壁チェック
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();




        //Swing用の標準システムの関数
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();
        //Swingのモード切替
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        //Swing
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClick)
        {
            if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Swing)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSwing);
                Debug.Log("Move=>Swing");
                return;
            } //Swingに移行
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Move=>Grapple");
                return;
            }//Grappleに移行
        }

        //現在は動き
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;


        //Idle状態へ
        if ((h == 0 && v == 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateIdle);
            Debug.Log("Move=>Idle");
            return;
        }

        //ジャンプ
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            return;
        }


        //上昇
        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
        }
        //下降
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
        }

        //段差登り
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("Move=>ClimbWall");
            return;
        }
    }
}
