using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGetItem : MonoBehaviour
{
    [Header("í«â¡Ç∑ÇÈèeÇÃéØï ID")]
    [SerializeField] private string _gunName;
    private GunSkinManager _gunSkinManager;

    private void OnEnable()
    {
        _gunSkinManager = GameObject.FindObjectOfType<GunSkinManager>();
        if(_gunSkinManager.GetGunSkinSaveData.Contains(_gunName))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _gunSkinManager.AddGunSkin(_gunName);
            Destroy(gameObject);
        }
    }
}
