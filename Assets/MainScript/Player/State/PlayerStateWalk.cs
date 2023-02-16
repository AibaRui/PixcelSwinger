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

        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
    }



    public override void Update()
    {
        //����A�����A�̐؂�ւ�
        _stateMachine.PlayerController.PlayerMoveing.SpeedChange();

        //�C���x���g�����J��
        if (_stateMachine.PlayerController.PlayerInput.IsTabDown)
        {
            _stateMachine.TransitionTo(_stateMachine.StateInventory);
            Debug.Log("Idle=>Inventory");
            return;
        }


        //��b���́A�C�x���g�V�[���ɓ�������
        if (_stateMachine.PlayerController.EventEriaCheck.IsEnterEventEria)
        {
            if (_stateMachine.PlayerController.PlayerInput.IsJumping)
            {
                _stateMachine.TransitionTo(_stateMachine.StateEventStop);
                Debug.Log("Idle=>EventStop");
                return;
            }
        }

        //�i���o��̕ǃ`�F�b�N
        _stateMachine.PlayerController.WallCheck.CheckClimbWall();

        //Swing�p�̕W���V�X�e���̊֐�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckForSwingPoints();
        //Swing�̃��[�h�ؑ�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ChangeTypeSwingOrGrapple();

        //Swing
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickDown)
        {
            if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Swing)
            {
                _stateMachine.TransitionTo(_stateMachine.StateSwing);
                Debug.Log("Walk=>Swing");
                return;
            } //Swing�Ɉڍs
            else if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleEnum == PlayerGrappleAndSwingSetting.SwingOrGrapple.Grapple)
            {
                _stateMachine.TransitionTo(_stateMachine.StateGrapple);
                Debug.Log("Walk=>Grapple");
                return;
            }//Grapple�Ɉڍs
        }

        //���݂͓���
        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        //�n�ʂɂ��Ă���
        if (_stateMachine.PlayerController.GroundCheck.IsGround)
        {

            //Idle��Ԃ�
            if ((h == 0 && v == 0))
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Walk=>Idle");
                return;
            }
            else if (v > 0)
            {
                //Shift���������瑖��
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
                //ctrl���������炵�Ⴊ��
                if (_stateMachine.PlayerController.PlayerInput.IsCtrlDown)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateSquat);
                    Debug.Log("Walk=>Squat");
                    return;
                }
            }
        }



        //�W�����v
        if (_stateMachine.PlayerController.PlayerInput.IsJumping && _stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateJump);
            Debug.Log("Walk=>Jump");
            return;
        }


        //�㏸
        if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateUpAir);
            Debug.Log("Walk=>UpAir");
        }
        //���~
        if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDownAir);
            Debug.Log("Walk=>DownAir");
        }

        //�i���o��
        if (_stateMachine.PlayerController.WallCheck.IsClimb && v > 0)
        {
            _stateMachine.TransitionTo(_stateMachine.StateClimbWall);
            Debug.Log("Walk=>ClimbWall");
            return;
        }
    }
}
