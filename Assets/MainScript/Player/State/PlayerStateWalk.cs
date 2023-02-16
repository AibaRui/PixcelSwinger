using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateWalk : PlayerStateBase
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

        //インベントリを開く
        if (_stateMachine.PlayerController.PlayerInput.IsTabDown)
        {
            _stateMachine.TransitionTo(_stateMachine.StateInventory);
            Debug.Log("Idle=>Inventory");
            return;
        }


        //会話等の、イベントシーンに入った時
        if (_stateMachine.PlayerController.EventEriaCheck.IsEnterEventEria)
        {
            if (_stateMachine.PlayerController.PlayerInput.IsJumping)
            {
                _stateMachine.TransitionTo(_stateMachine.StateEventStop);
                Debug.Log("Idle=>EventStop");
                return;
            }
        }

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
                Debug.Log("Walk=>Swing");
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

        //地面についている
        if (_stateMachine.PlayerController.GroundCheck.IsGround)
        {

            //Idle状態へ
            if ((h == 0 && v == 0))
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Walk=>Idle");
                return;
            }
            else if (v > 0)
            {
                //Shiftをおしたら走る
                if (_stateMachine.PlayerController.PlayerInput.IsLeftShiftDown)
                {
                    if (_stateMachine.PlayerController.PlayerInput.IsLeftShiftDown)
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateRun);
                        Debug.Log("Walk=>Run");
                        return;
                    }
                }
            }
            else
            {
                //ctrlをおしたらしゃがむ
                if (_stateMachine.PlayerController.PlayerInput.IsCtrlDown)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateSquat);
                    Debug.Log("Walk=>Squat");
                    return;
                }
            }
        }



        //ジャンプ
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Walk=>Jump");
            return;
        }


        //上昇
        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
            Debug.Log("Walk=>UpAir");
        }
        //下降
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
            Debug.Log("Walk=>DownAir");
        }

        //段差登り
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("Walk=>ClimbWall");
            return;
        }
    }
}
