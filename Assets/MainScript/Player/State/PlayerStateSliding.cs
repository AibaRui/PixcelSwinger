using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateSliding : PlayerStateBase
{
    public override void Enter()
    {
        //スライディングはじめ
        _stateMachine.PlayerController.PlayerSquatAndSliding.StartSliding();
    }

    public override void Exit()
    {
        //スライディング終了時の設定
        _stateMachine.PlayerController.PlayerSquatAndSliding.StopSliding();
    }

    public override void FixedUpdate()
    {
        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();

        _stateMachine.PlayerController.PlayerSquatAndSliding.SlidingMove();
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
                Debug.Log("Sliding=>Swing");
                return;
            } //Swingに移行
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Sliding=>Grapple");
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
        //スライディングが終わったかどうかの判定
        if (_stateMachine.PlayerController.PlayerSquatAndSliding.IsSlidingEnd)
        {
            //しゃがみ状態へ
            if ((h != 0 || v != 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSquat);
                Debug.Log("Sliding=>Squat");
                return;
            }
        }




        //ジャンプ
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Sliding=>Jump");
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
