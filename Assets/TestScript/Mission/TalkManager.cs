using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class TalkManager : MonoBehaviour
{
    [SerializeField] private GameObject _weapons;

    [SerializeField] private GameObject _showPanel;

    [SerializeField] private GameObject _talkPanel;

    [SerializeField] private Text _talkText;

    [SerializeField] private Text _nameText;

    public GameObject Weapon => _weapons;

    public GameObject ShowPanel => _showPanel;
    public GameObject TalkPanel => _talkPanel;
    public Text TalkText => _talkText;

    public Text NameText => _nameText;

    private TalkBase _talkBase;

    public TalkBase TalkBase { get => _talkBase; set => _talkBase = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTalk()
    {
        _talkBase.StartTalk();
    }

}
