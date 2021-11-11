using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattaryPickup : MonoBehaviour
{
    [SerializeField] float angleRestore;
    [SerializeField] float intensityRestore;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<FlashLight>().RestoreAngle(angleRestore);
            FindObjectOfType<FlashLight>().RestoreIntensity(intensityRestore);
            Destroy(gameObject);
        }
    }

}
