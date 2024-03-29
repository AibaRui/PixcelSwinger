using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("効果音用のAudioSource")]
    [SerializeField]
    private AudioSource _playerSEaudioSource;

    [Header("足音")]
    [SerializeField] private AudioClip _walkSound;

    [Header("走りの足音")]
    [SerializeField] private AudioClip _runSound;

    [Header("ジャンプの効果音")]
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
