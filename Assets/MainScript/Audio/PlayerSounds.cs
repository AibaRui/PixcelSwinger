using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Œø‰Ê‰¹—p‚ÌAudioSource")]
    [SerializeField]
    private AudioSource _playerSEaudioSource;

    [Header("‘«‰¹")]
    [SerializeField] private AudioClip _walkSound;

    [Header("‘–‚è‚Ì‘«‰¹")]
    [SerializeField] private AudioClip _runSound;

    [Header("ƒWƒƒƒ“ƒv‚ÌŒø‰Ê‰¹")]
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
