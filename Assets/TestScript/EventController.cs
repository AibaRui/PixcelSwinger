using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [Tooltip("���݁A��b/Movie���̃C�x���g�����ǂ�����\��")]
    private bool _isEventNow = false;

    public bool IsEventNow => _isEventNow; 


    public void ChangeEventSituationTrue()
    {
        _isEventNow = true;
        Debug.Log("�C�x���g���ɂȂ�܂���");
    }

    public void ChangeEventSituationFalse()
    {
        _isEventNow = false;
        Debug.Log("�C�x���g���I���܂���");
    }

}
