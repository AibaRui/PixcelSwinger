using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayControler : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;

    [SerializeField] private MusuicManager _musuicManager;

    [SerializeField] private AudioSource _aud;

    private AudioClip _audioClip;

    public AudioClip AudioClip{ get => _audioClip; set => _audioClip = value; }

    private bool _isPause = false;



    public void MusicControl()
    {

        if (_playerInput.IsMusicChange)
        {

        }
        else if (_playerInput.IsMusicStartAndStop)
        {

        }


    }

    /// <summary>Music‚ğ’†~‚·‚é</summary>
    public void StopMusic()
    {

    }

    /// <summary>Music‚ğˆê’â~‚·‚é</summary>
    public void PauseMusic()
    {

    }

    //Music‚ğÄ¶‚·‚é
    public void ResumeMusic()
    {
        

    }

    /// <summary>‹È‚ğˆê’â~EÄ¶‚·‚é</summary>
    public void PauseOrUnPauseMusic()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            _aud.Pause();
        }
        else
        {
            _aud.UnPause();
        }
    }
}
