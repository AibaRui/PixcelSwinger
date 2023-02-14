using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEnterZoneBase : MonoBehaviour
{
    private Mission _missionBase;

    public void Init(Mission missionBase)
    {
        _missionBase = missionBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // _missionBase.EnterMission();
            Destroy(gameObject);
        }
    }

}
