using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] Slider _sensitivitySlider;

    [SerializeField] private float _firstSet = 0.5f;

    CinemachinePOV _camera;
    [SerializeField] CinemachineVirtualCamera _ca;
    [SerializeField] float _maxSensitivity = 300;
    [SerializeField] float _mixSensitivity = 50;
    void Start()
    {
        _camera = _ca.GetCinemachineComponent<CinemachinePOV>();
        _sensitivitySlider = _sensitivitySlider.GetComponent<Slider>();

        //�X���C�_�[�̍ő�l�̐ݒ�
        _sensitivitySlider.maxValue = _maxSensitivity;

        //�X���C�_�[�̌��ݒl�̐ݒ�
        _sensitivitySlider.minValue = _mixSensitivity;

        _sensitivitySlider.value = _firstSet;
    }

    public void ChangeSensitivity(float value)
    {
        _camera.m_HorizontalAxis.m_MaxSpeed = value;
        _camera.m_VerticalAxis.m_MaxSpeed = value;
    }
}
