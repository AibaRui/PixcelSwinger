using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionGetItem : MonoBehaviour
{
    [Header("アイテムの名前")]
    [SerializeField] private ItemDataInformation _itemData;

    [SerializeField]
    private MissionManager _missionManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _missionManager.NowMainMission.NowMissionDetail.ClearMissionTask();
          //  gameObject.SetActive(false);
        }
    }

}
