using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomOutFOV = 60f;
    [SerializeField] float zoomInFOV = 20f;
    [SerializeField] float zoomOutSens = 2f;
    [SerializeField] float zoomInSens = 1f;

    bool isAlreadyZoomed = false;
    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isAlreadyZoomed == false)
            {
                ZoomIn();
            }

            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomIn()
    {
        isAlreadyZoomed = true;
        fpsCamera.fieldOfView = zoomInFOV;
        fpsController.mouseLook.XSensitivity = zoomInSens;
        fpsController.mouseLook.YSensitivity = zoomInSens;
    }
    private void ZoomOut()
    {
        isAlreadyZoomed = false;
        fpsCamera.fieldOfView = zoomOutFOV;
        fpsController.mouseLook.XSensitivity = zoomOutSens;
        fpsController.mouseLook.YSensitivity = zoomOutSens;
    }
}
