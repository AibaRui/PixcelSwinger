using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class TalkManager : MonoBehaviour
{
    [Header("Movie���ɔ�\���ɂ���������")]
    [SerializeField] private List<GameObject> _offObjectWithMovie = new List<GameObject>();

    [SerializeField] private GameObject _weapons;

    [Header("��b�̂������Ƃ̃p�l��")]
    [SerializeField] private GameObject _talkPanel;

    [Header("��b����Text")]
    [SerializeField] private Text _talkText;

    [Header("��b�̐l�̖��O��Text")]
    [SerializeField] private Text _nameText;

    [Header("��b�͈͓��ɓ��������ɏo���A�V�X�g�\�L")]
    [SerializeField] private GameObject _asistTolkPanel;

    public GameObject AsistTalkPanel => _asistTolkPanel;

    public GameObject Weapon => _weapons;

    public GameObject TalkPanel => _talkPanel;
    public Text TalkText => _talkText;

    public Text NameText => _nameText;

    private TalkBase _talkBase;

    public TalkBase TalkBase { get => _talkBase; set => _talkBase = value; }


    Vector3 _rotation;

    /// <summary>���[�r�[�̊J�n���ɌĂԁB��ʂ�UI���\���ɂ���</summary>
    public void OffMoiveUIs()
    {
        _offObjectWithMovie.ForEach(i => i.SetActive(false));
        _rotation = _weapons.transform.eulerAngles;
    }

    /// <summary>���[�r�[�̏I�����ɌĂԁB��ʂ�UI��\������</summary>
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
