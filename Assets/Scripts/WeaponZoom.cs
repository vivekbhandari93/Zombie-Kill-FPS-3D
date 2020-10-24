using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController firstPersonController;

    [SerializeField] float zoomOutFOV = 60f;
    [SerializeField] float zoomInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;

    bool zoomInToggle = false;


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(zoomInToggle == false)
            {
                zoomInToggle = true;
                fpsCamera.fieldOfView = zoomInFOV;
                firstPersonController.mouseLook.XSensitivity = zoomInSensitivity;
                firstPersonController.mouseLook.YSensitivity = zoomInSensitivity;
            }
            else
            {
                zoomInToggle = false;
                fpsCamera.fieldOfView = zoomOutFOV;
                firstPersonController.mouseLook.XSensitivity = zoomOutSensitivity;
                firstPersonController.mouseLook.YSensitivity = zoomOutSensitivity;
            }
        }
    }

}
