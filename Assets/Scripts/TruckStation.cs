using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Criminal"))
        {
            Destroy(other.gameObject);
        }
    }
}
