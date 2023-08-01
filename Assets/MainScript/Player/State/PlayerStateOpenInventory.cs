using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateOpenInventory : PlayerStateBase
{
    public override void Enter()
    {
        //�v���C���[�̓������~�߂�
        _stateMachine.PlayerController.Rb.velocity = Vector3.zero;

        //�C���x���g�����J���Ă���Ԃ́A�A�V�X�g�p�l�����o���v�Z���ԂɊ܂߂Ȃ�
        _stateMachine.PlayerController.IsMove = false;

        //�C���x���g����UI��Active�ɂ���
        _stateMachine.PlayerController.InventoryManager.InventoryOpen();

        //�C���x���g�����J����
        _stateMachine.PlayerController.AudioManager.OpenInvrntory();
    }

    public override void Exit()
    {
        //�����Ă���
        _stateMachine.PlayerController.IsMove = false;

        //�C���x���g����UI��ActiveFalse�ɂ���
        _stateMachine.PlayerController.InventoryManager.InventoryClose();
    }

    public override void FixedUpdate()
    {

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        if (_stateMachine.PlayerController.PlayerInput.IsTabDown)
        {
            //�Q�[���ݒ�̕ύX���e��K�p������
            _stateMachine.PlayerController.GameSetting.CheckChange();
            //���ʐݒ�̕ύX���e��K�p������
            _stateMachine.PlayerController.AudioSetting.CheckDoSetting();

            if (_stateMachine.PlayerController.GameSetting.IsChangeSetting || _stateMachine.PlayerController.AudioSetting.IsChange)
            {
                Debug.Log("-------�ύX����");
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Inventory=>Idle");
            }
        }

    }


}
