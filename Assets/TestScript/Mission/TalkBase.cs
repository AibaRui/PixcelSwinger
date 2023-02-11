using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public abstract class TalkBase : MonoBehaviour
{
    [SerializeField] string _humanName;

    [Tooltip("�b�����e�����郊�X�g")]
    protected List<string> _nowTalkText = new List<string>();

    public List<string> NowTalkText { set => _nowTalkText = value; get => _nowTalkText; }

    protected bool _isTalkNow = false;

    public bool IsTalkNow { get => _isTalkNow; }

    protected bool _isEnter = false;

    /// <summary>��b�̓r���ŉ��o���������ۂɁA���݉��o�����ǂ�����\��</summary>
    protected bool _isEvent;

    TalkManager _talkManager;

    protected int _talkedNum = 0;

    private void Awake()
    {
        _talkManager = GameObject.FindObjectOfType<TalkManager>();
    }



    protected abstract void TalkInEvent(int talkNum);

    protected abstract void TalkEndEvent();


    public void StartTalk()
    {
        //������b�͈͓��ɁA�Q�l�����ꍇ�ɓ�Ƃ��Ă΂��̂�h�����߂ɉ�b�̃p�l����active���ǂ����m�F
        if (_talkManager.TalkPanel.activeSelf == false)
        {
            //��b�̃p�l����\���B
            _talkManager.TalkPanel.SetActive(true);

            //������B��
            _talkManager.Weapon.SetActive(false);

            _isTalkNow = true;
            StartCoroutine(Talk());
        }
    }


    IEnumerator Talk()
    {
        //�~�b�V�����̎�t��Ԃɂ���āA��b�̓��e��ς���

        _talkManager.NameText.text = _humanName;

        //�b���Ă���e�L�X�g���X�V�B
        for (int i = 0; i < _nowTalkText.Count; i++)
        {
            //�e�L�X�g���X�V
            _talkManager.TalkText.text = _nowTalkText[i];
            //��b���̃C�x���g����������Ă�
            TalkInEvent(i);

            yield return new WaitUntil(() => !_isEvent);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        _talkedNum++;
        TalkEndEvent();



        //��b�̃p�l�������
        CloseMissionPanel();
        _isTalkNow = false;
    }

    public void CloseMissionPanel()
    {
        _talkManager.TalkPanel.SetActive(false);
        _talkManager.Weapon.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _talkManager.ShowPanel.activeSelf == false)
        {
            _talkManager.ShowPanel.SetActive(true);
            _isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _talkManager.ShowPanel.SetActive(false);
            _isEnter = false;
        }
    }


}
