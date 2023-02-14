using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionGetItem : MonoBehaviour
{
    [SerializeField] int _missionNumber;

    [SerializeField] int _missionDetailNumber;

    [Header("�~�b�V�����̏ڍ�")]
    [SerializeField] string _missionDetail;

    [Header("�A�C�e���̖��O")]
    [SerializeField] private ItemDataInformation _itemData;

    [SerializeField]
    List<Events> _talkEvents = new List<Events>();

    private Dictionary<int, List<UnityEvent>> _events = new Dictionary<int, List<UnityEvent>>();

    [SerializeField]
    private UnityEvent _endEvent;

    private MissionManager _missionManager;
    private ItemManager _itemManager;

    private void Start()
    {
        _itemManager = GameObject.FindObjectOfType<ItemManager>();
        _missionManager = GameObject.FindObjectOfType<MissionManager>();
        Debug.Log($"�J�n:{_missionDetail} : {_missionNumber}_{_missionDetailNumber}�ڂ̃^�X�N");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _itemManager.AddItem(_itemData.NameId);
            _missionManager.NowMainMission.CheckMission();
            _endEvent?.Invoke();
            Debug.Log($"�I��:{_missionDetail} : {_missionNumber}_{_missionDetailNumber}�ڂ̃^�X�N");
            Destroy(gameObject);
        }
    }

}
