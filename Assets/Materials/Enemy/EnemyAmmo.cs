using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmo : MonoBehaviour
{
    [SerializeField] float _ammoSpeed = 2;
    [SerializeField] float _limitTime = 10;
    float _countTime = 0;

    AudioSource _aud;

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 velo = player.transform.position - transform.position;
        _rb.velocity = velo * _ammoSpeed;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KatanaAttack")
        {
            _rb.velocity = -_rb.velocity * 2;
            _countTime -= 5;
            gameObject.tag = "KatanaAttack";
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "KatanaAttack")
            {
                Destroy(gameObject);
            }
        }
    }

}
