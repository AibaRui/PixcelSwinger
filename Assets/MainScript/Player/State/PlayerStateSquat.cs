using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateSquat : PlayerStateBase
{
    public override void Enter()
    {
        //しゃがみ  始めた時の調整
        _stateMachine.PlayerController.PlayerSquatAndSliding.StartSquat();
    }

    public override void Exit()
    {
        //しゃがみを終えた時の調整
        _stateMachine.PlayerController.PlayerSquatAndSliding.StopSquat();
    }

    public override void FixedUpdate()
    {
        //プレイヤーのしゃがみ動き
        _stateMachine.PlayerController.PlayerSquatAndSliding.SquatMove();

        //プレイヤーの向き調整
        _stateMachine.PlayerController.PlayerSquatAndSliding.SetDir();

        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
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
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.PlayerInput.IsCtrlUp)
        {
            //Idle状態へ
            if ((h == 0 && v == 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Squat=>Idle");
                return;
            }
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
    }

}
