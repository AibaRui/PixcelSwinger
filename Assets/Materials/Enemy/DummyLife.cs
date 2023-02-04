using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLife : MonoBehaviour
{
    [SerializeField] float _limitTime = 1;
    float _countTime = 0;

    AudioSource _aud;
    void Start()
    {
        GameObject player= GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = player.transform.position - transform.position;
        transform.forward = dir;


        if (_aud)
        {
            _aud = GetComponent<AudioSource>();
            _aud.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {


        _countTime += Time.deltaTime;
        if (_limitTime <= _countTime)
        {
            Destroy(gameObject);
        }
    }
}
