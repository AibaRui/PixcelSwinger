using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;
    private float shotInterval;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {

            shotInterval += 1;

            if (shotInterval % 5 == 0 && shotCount > 0)
            {
                shotCount -= 1;

                GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

              Vector3  dir = Camera.main.transform.TransformDirection(Vector3.forward);

                bulletRb.AddForce(dir * shotSpeed);

                //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

                Destroy(bullet, 3.0f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;

        }

    }
}
