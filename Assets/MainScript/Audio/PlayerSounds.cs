using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("���ʉ��p��AudioSource")]
    [SerializeField]
    private AudioSource _playerSEaudioSource;

    [Header("����")]
    [SerializeField] private AudioClip _walkSound;

    [Header("����̑���")]
    [SerializeField] private AudioClip _runSound;

    [Header("�W�����v�̌��ʉ�")]
    [SerializeField] private AudioClip _jumpSound;


    public void WalkSound()
    {
        _playerSEaudioSource.PlayOneShot(_walkSound);
    }

    public void RunSound()
    {
        _playerSEaudioSource.PlayOneShot(_runSound);
    }

    public void JumpSound()
    {
        _playerSEaudioSource.PlayOneShot(_jumpSound);
    }
}
