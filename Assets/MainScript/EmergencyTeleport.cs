using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyTeleport : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Transform _teleportplayer;

    //�v���C���[�̑��삪�����Ȃ��Ȃ����Ƃ��ɌĂԁA�ً}�p�̔閧�V�X�e��
    void Update()
    {
        if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.P))
        {
            _player.position = _teleportplayer.position;
        }
    }
}
