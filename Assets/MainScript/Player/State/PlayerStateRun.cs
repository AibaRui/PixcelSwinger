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

        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }



    public override void Update()
    {
        //走り、歩き、の切り替え
        _stateMachine.PlayerController.PlayerMoveing.SpeedChange();

        //段差登りの壁チェック
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();

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
                Debug.Log("Run=>Swing");
                return;
            } //Swingに移行
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Walk=>Grapple");
                return;
            }//Grappleに移行
        }

        //現在は動き
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.GroundCheck.IsGround)
        {

            //Idle状態へ
            if ((h == 0 && v == 0))
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Run=>Idle");
                return;
            }
            else if ((h != 0 || v != 0))
            {
                //Shiftを押したら走る
                if (_stateMachine.PlayerController.PlayerInput.IsLeftShiftDown)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    Debug.Log("Run=>Walk");
                    return;
                }
            }
            //ctrlを押したらスライディング
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





        //ジャンプ
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Run=>Jump");
            return;
        }


        //上昇
        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
            Debug.Log("Run=>UpAir");
        }
        //下降
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
            Debug.Log("Run=>DownAir");
        }

        //段差登り
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("Run=>ClimbWall");
            return;
        }
    }
}
