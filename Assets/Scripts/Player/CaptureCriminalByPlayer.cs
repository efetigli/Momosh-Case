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
        if (other.gameObject.CompareTag("Criminal") && !CriminalManager.CriminalLine.Contains(other.gameObject) && other.gameObject.GetComponent<FollowTarget>().captured == false)
        {
            if (HandcuffsManager.GetNumberOfHandcuffs() > 0)
            {
                CaptureCriminal(other.gameObject);
            }
            else
                Debug.Log("Out of Handcuffs");
        }
        else if(CriminalManager.CriminalLine.Contains(other.gameObject))
            Debug.Log("Already captured this criminal");
    }

    private void CaptureCriminal(GameObject ob)
    {
        if (CriminalManager.CriminalLine.Count == 0)
            ob.GetComponent<FollowTarget>().TargetTransform = this.transform;
        else
            ob.GetComponent<FollowTarget>().TargetTransform = CriminalManager.CriminalLine.Last.Value.transform;

        CriminalManager.AddCriminalToCriminalLine(ob);
        HandcuffsManager.RemoveHandcuff(1);
        HandcuffStack.RemoveHandcuffToStack();
        ob.GetComponent<FollowTarget>().captured = true;

    }

}
