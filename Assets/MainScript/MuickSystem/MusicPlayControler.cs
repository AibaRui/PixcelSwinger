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

    /// <summary>Music�𒆎~����</summary>
    public void StopMusic()
    {

    }

    /// <summary>Music���ꎞ��~����</summary>
    public void PauseMusic()
    {

    }

    //Music���Đ�����
    public void ResumeMusic()
    {
        

    }

    /// <summary>�Ȃ��ꎞ��~�E�Đ�����</summary>
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
