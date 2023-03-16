using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyTeleport : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Transform _teleportplayer;

    //プレイヤーの操作が聞かなくなったときに呼ぶ、緊急用の秘密システム
    void Update()
    {
        if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.P))
        {
            _player.position = _teleportplayer.position;
        }
    }
}
