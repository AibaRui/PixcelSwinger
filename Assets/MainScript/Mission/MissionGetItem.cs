using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionGetItem : MonoBehaviour
{
    [SerializeField] int _missionNumber;

    [SerializeField] int _missionDetailNumber;

    [Header("ミッションの詳細")]
    [SerializeField] string _missionDetail;

    [Header("インベントリに表記する次のミッション内容")]
    [SerializeField] private string _missionLongDetail;

    [Header("アイテムの名前")]
    [SerializeField] private ItemDataInformation _itemData;



    [SerializeField]
    private UnityEvent _endEvent;

    private MissionManager _missionManager;
    private ItemManager _itemManager;

    private void Start()
    {
        _itemManager = GameObject.FindObjectOfType<ItemManager>();
        _missionManager = GameObject.FindObjectOfType<MissionManager>();
        Debug.Log($"開始:{_missionDetail} : {_missionNumber}_{_missionDetailNumber}目のタスク");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _itemManager.AddItem(_itemData.NameId);
            _missionManager.NowMainMission.CheckMission();
            _endEvent?.Invoke();
            _missionManager.SettingMissionText(_missionDetail, _missionLongDetail);
            Debug.Log($"終了:{_missionDetail} : {_missionNumber}_{_missionDetailNumber}目のタスク");
            Destroy(gameObject);
        }
    }

}
