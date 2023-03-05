using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class TalkManager : MonoBehaviour
{
    [Header("Movie中に非表示にしたいもの")]
    [SerializeField] private List<GameObject> _offObjectWithMovie = new List<GameObject>();

    [SerializeField] private GameObject _weapons;

    [Header("会話のおおもとのパネル")]
    [SerializeField] private GameObject _talkPanel;

    [Header("会話文のText")]
    [SerializeField] private Text _talkText;

    [Header("会話の人の名前のText")]
    [SerializeField] private Text _nameText;

    [Header("会話範囲内に入った時に出すアシスト表記")]
    [SerializeField] private GameObject _asistTolkPanel;

    [Header("アシストパネルを出すときの音")]
    [SerializeField] private AudioClip _showAssistUIAudio;

    [Header("会話を続けるときの音")]
    [SerializeField] private AudioClip _talkAudio;

    [SerializeField] private AudioManager _audioManager;

   public GameObject AsistTalkPanel => _asistTolkPanel;

    public GameObject Weapon => _weapons;

    public GameObject TalkPanel => _talkPanel;
    public Text TalkText => _talkText;

    public Text NameText => _nameText;

    private TalkBase _talkBase;

    public TalkBase TalkBase { get => _talkBase; set => _talkBase = value; }


    Vector3 _rotation;

    public void TalkToNext()
    {
        _audioManager.PlayeGameUISE(_talkAudio);
    }

    public void OpenAssistUI()
    {
        _asistTolkPanel.SetActive(true);
        _audioManager.PlayeGameUISE(_showAssistUIAudio);
    }

    public void CloseAssistUI()
    {
        _asistTolkPanel.SetActive(false);
    }

    /// <summary>ムービーの開始時に呼ぶ。画面のUIを非表示にする</summary>
    public void OffMoiveUIs()
    {
        _offObjectWithMovie.ForEach(i => i.SetActive(false));
        _rotation = _weapons.transform.eulerAngles;
    }

    /// <summary>ムービーの終了時に呼ぶ。画面のUIを表示する</summary>
    public void OnMovieUIs()
    {
        _offObjectWithMovie.ForEach(i => i.SetActive(true));
        _weapons.transform.eulerAngles = _rotation;
    }


    public void DoTalk()
    {
        _talkBase.StartTalk();
    }

}
