using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialAssist : MonoBehaviour
{
    [Header("��ԍŏ��ɃG���A�ɓ������炷�邱��")]
    [SerializeField] private UnityEvent _firstMove;

    [Header("�A�V�X�g�p�l��")]
    [SerializeField] private GameObject _assistPanel;

    private bool _isFirstEnter = false;  


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _assistPanel.SetActive(true);

            if(!_isFirstEnter)
            {
                _firstMove?.Invoke();
                _isFirstEnter = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _assistPanel.SetActive(false);
        }
    }

}
