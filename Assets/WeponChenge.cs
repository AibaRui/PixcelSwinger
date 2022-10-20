using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeponChenge : MonoBehaviour
{
    [SerializeField] GameObject[] _weapons = new GameObject[1];

    [SerializeField] Text _text;


    int num = 1;
    void Start()
    {
        _weapons[num-1].SetActive(true);
        _text.text = _weapons[num-1].name;
    }

    // Update is called once per frame
    void Update()
    {
        Chenge();
    }

    void Chenge()
    {
        float wh = Input.GetAxis("Mouse ScrollWheel");
        if (wh != 0)
        {
            if (wh > 0)
            {
                _weapons[num-1].SetActive(false);
                num -= 1;
                if (num == 0)
                {
                    num = _weapons.Length;
                }
                _weapons[num - 1].SetActive(true);
                Debug.Log(num);
            }
            else if (wh < 0)
            {
                _weapons[num-1].SetActive(false);

                num += 1;
                if (num > _weapons.Length)
                {
                    num = 1;
                }
                Debug.Log(num);

            }
            _weapons[num - 1].SetActive(true);
            _text.text = _weapons[num - 1].name;
        }
    }
}
