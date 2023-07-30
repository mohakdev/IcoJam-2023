using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] CarController carController;
    [SerializeField] Rigidbody2D rbCar;

    void Update()
    {
        float factor = rbCar.velocity.sqrMagnitude / carController.maxVelocity;
        virtualCam.m_Lens.OrthographicSize = Mathf.Lerp(10, 16, factor);
    }
}
