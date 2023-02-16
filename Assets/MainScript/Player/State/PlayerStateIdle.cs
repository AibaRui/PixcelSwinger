using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateIdle : PlayerStateBase
{
    public override void Enter()
    {

    }

    public override void Exit()
    {
        //一定時間経過したら、アシストUIを出す仕組みを閉じる
        _stateMachine.PlayerController.UIShowSystem.CloseUI();
    }
    public override void LateUpdate()
    {

    }
    public override void FixedUpdate()
    {   
        //Swing用の標準システムの関数
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();
    }

    public override void Update()
    {
        _stateMachine.PlayerController.PlayerIdle.Idle();
        _stateMachine.PlayerController.PlayerMoveing.SetDir();

        //走り、歩き、の切り替え
        _stateMachine.PlayerController.PlayerMoveing.SpeedChange();

        //一定時間経過したら、アシストUIを出す仕組み
        _stateMachine.PlayerController.UIShowSystem.ShowUI();

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


        //Swingのモード切替
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        //Swing
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickDown)
        {
            if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Swing)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSwing);
                Debug.Log("Idle=>Swing");
                return;
            } //Swingに移行
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Idle=>Grapple");
                return;
            }//Grappleに移行
        }


        if(_stateMachine.PlayerController.PlayerInput.IsCtrlDown)
        {
            _stateMachine.TransitionTo(_stateMachine.StateSquat);
            Debug.Log("Idle=>Squat");
            return;
        }

        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if ((h != 0 || v != 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateWalk);
            Debug.Log("Idle=>Move");
            return;
        }

        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Idle=>Jump");
        }






    }
}
