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


        if(_stateMachine.PlayerController.EventEriaCheck.IsEnterEventEria)
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
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClick)
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


        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if ((h != 0 || v != 0) && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateMove);
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
