using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [Tooltip("現在、会話/Movie等のイベント中かどうかを表す")]
    private bool _isEventNow = false;

    public bool IsEventNow => _isEventNow; 


    public void ChangeEventSituationTrue()
    {
        _isEventNow = true;
        Debug.Log("イベント中になりました");
    }

    public void ChangeEventSituationFalse()
    {
        _isEventNow = false;
        Debug.Log("イベントが終わりました");
    }

}
