using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 0.1f;
    [SerializeField] float minLightIntensity;
    [SerializeField] float minAngle = 40f;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }
    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }
    public void RestoreAngle(float restoreAngle)
    {
        myLight.spotAngle += restoreAngle;
    }
    public void RestoreIntensity(float restoreIntensity)
    {
        myLight.intensity += restoreIntensity;
    }
    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minAngle)
        {
            return;
        }
        else
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        if (myLight.intensity <= minLightIntensity)
        {
            return;
        }
        else
        {
            myLight.intensity -= lightDecay * Time.deltaTime;
        }
    }

   
}
