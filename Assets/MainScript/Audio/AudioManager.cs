using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] private AudioSource _bgmAudioSource;

    [Header("SystemSE")]
    [SerializeField] private AudioSource _systemAudioSource;


    [Header("1度のみ鳴らすPlayerのSE")]
    [SerializeField] private AudioSource _playerSEAudioSourceIsOneShot;

    [Header("ループするPlayerのSE")]
    [SerializeField] private AudioSource _playerSEAudioSourceIsLoop;

    [Header("GameのUIのSE")]
    [SerializeField] private AudioSource _gameUISEAudioSource;

    private void Start()
    {

        
    }

    public void PlayeGameUISE(AudioClip audioClip)
    {
        _gameUISEAudioSource.PlayOneShot(audioClip);
    }



    /// <summary>BGMを流す</summary>
    /// <param name="audioClip">流したいBGM</param>
    public void PlayBGM(AudioClip　audioClip)
    {
        //_bgmAudioSource.clip = _clips[audioKind];
        _bgmAudioSource.Play();
    }

    public void PlayerSE(AudioClip audioClip,bool isLoop)
    {
        if(isLoop)
        {
            _playerSEAudioSourceIsLoop.clip = audioClip;
            _playerSEAudioSourceIsLoop.Play();
        }
        else
        {
            _playerSEAudioSourceIsOneShot.PlayOneShot(audioClip);
        }
    }

    public void StopLoopPlayerSE()
    {
        _playerSEAudioSourceIsLoop.Stop();
    }


    public void StartTalk()
    {
        _playerSEAudioSourceIsLoop.Stop();
        _playerSEAudioSourceIsOneShot.Stop();
    }

    public void OpenInvrntory()
    {
        _playerSEAudioSourceIsLoop.Stop();
        _playerSEAudioSourceIsOneShot.Stop();
    }
}
