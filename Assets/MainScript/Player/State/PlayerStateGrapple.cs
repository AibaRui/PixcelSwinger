using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateGrapple : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerGrapple.StartGrapple();
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.Joint = _stateMachine.PlayerController.Player.GetComponent<SpringJoint>();

        //銃のアニメーション
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleAinm();

        //ワイヤーを縮める音を流す
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.WireSound();

        //空中ジャンプの回数調整
        _stateMachine.PlayerController.PlayerJumping.ReSetAirJump();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerGrapple.StopGrapple();

        if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckPointIsHit())
        {
            _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.EndGrappleOrSwing();
        }


        //ループしている音(Wireの音を止める)
        _stateMachine.PlayerController.AudioManager.StopLoopPlayerSE();
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerGrapple.GrappleMove();

        //速度調整のスクリプト
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
        //Hit! のUIを出す
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ActivePointer();
    }

    public override void LateUpdate()
    {
        //縄を描く
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.DrawRope();
    }

    public override void Update()
    {

        //離したらSwingを中止
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickUp || !_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckPointIsHit())
        {
            //上昇
            if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                //Grapple終わりの音を鳴らす
                _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingAndGrappleEndSound();

                _stateMachine.TransitionTo(_stateMachine.StateUpAir);

                Debug.Log("Grapple=>UpAir");
                return;
            }
            //下降
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                //Grapple終わりの音を鳴らす
                _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingAndGrappleEndSound();

                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("Grapple=>DownAir");
                return;
            }
        }

        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickUp || !_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckPointIsHit())
        {
            if (_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                if ((h != 0 && v != 0))
                {
                    _stateMachine.TransitionTo(_stateMachine.StateIdle);
                    Debug.Log("Grapple=>Idle");
                    return;
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    Debug.Log("Grapple=>Move");
                }
            }
        }



    }

}

