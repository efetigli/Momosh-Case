using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCriminalByPlayer : MonoBehaviour
{

    private CriminalManager CriminalManager;
    private HandcuffsManager HandcuffsManager;
    [SerializeField] private HandcuffStack HandcuffStack;

    private void Awake()
    {
        CriminalManager = this.GetComponent<CriminalManager>();
        HandcuffsManager = this.GetComponent<HandcuffsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If criminal already follows player.
        //if (CriminalManager.ContainsCriminalList(other.gameObject))
        //    return;


        if (other.gameObject.CompareTag("Criminal"))
        {
            // If criminal already captured return.
            if (other.gameObject.GetComponent<FollowTarget>().captured == true)
                return;

            // Check is there enough handcuff at the stack of the player.
            if (HandcuffsManager.GetNumberOfHandcuffs() > 0)
            {
                CaptureCriminal(other.gameObject);
            }
        }
    }

    private void CaptureCriminal(GameObject ob)
    {
        // If there is no captured criminal, then first captured criminal is going to follow player.
        if (CriminalManager.CountCriminalList() == 0)
            ob.GetComponent<FollowTarget>().TargetTransform = this.transform;
        // If already captured criminal, then first captured criminal is going to follow tail criminal.
        else
            ob.GetComponent<FollowTarget>().TargetTransform = CriminalManager.LastCriminalList().transform;

        CriminalManager.AddLastCriminalList(ob);
        HandcuffsManager.RemoveHandcuff(1);
        HandcuffStack.RemoveHandcuffToStack(ob);
        ob.GetComponent<FollowTarget>().captured = true;
    }

}
