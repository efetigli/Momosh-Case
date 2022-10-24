using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandcuffResourceStation : MonoBehaviour
{
    [Header("Needed Scripts")]
    [SerializeField] HandcuffStack HandcuffStack;
    [SerializeField] HandcuffsManager HandcuffsManager;

    [Header("Timer")]
    [SerializeField] public float CountDownTime;
    private float timer;

    private void Awake()
    {
        timer = CountDownTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StayOnHandcuffResource();
        }
    }

    private void StayOnHandcuffResource()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            HandcuffsManager.AddHandcuff(1);
            HandcuffStack.AddHandcuffToStack();
            timer = CountDownTime;
        }
    }
}
