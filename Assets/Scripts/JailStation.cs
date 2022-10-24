using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailStation : MonoBehaviour
{
    [SerializeField] HandcuffsManager HandcuffsManager;
    [SerializeField] CriminalManager CriminalManager;
    [SerializeField] HandcuffStack HandcuffStack;

    [SerializeField] Transform JailLinePointTransform;
    [SerializeField] Transform TruckPointTransform;

    public LinkedList<GameObject> CriminalJailLine;

    [SerializeField] float WaitTimeAtJail;

    private void Awake()
    {
        CriminalJailLine = new LinkedList<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CriminalManager.CriminalLine.Count == 0)
            return;

        if (CriminalJailLine.Count != 0)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            int howManyCriminal = 3;
            if (CriminalManager.CriminalLine.Count < 3)
                howManyCriminal = CriminalManager.CriminalLine.Count;

            for(int i = 0; i < howManyCriminal; i++)
            {
                CriminalJailLine.AddFirst(CriminalManager.LastCriminalAtCriminalLine());

                CriminalManager.CriminalLine.RemoveLast();
                //HandcuffsManager.AddHandcuff(1);
                //HandcuffStack.AddHandcuffToStack();
            }

            StartCoroutine(GetOnTruck());
            CriminalJailLine.First.Value.GetComponent<FollowTarget>().TargetTransform = JailLinePointTransform;
            CriminalJailLine.First.Value.GetComponent<FollowTarget>().navAgent.stoppingDistance = 0;
        }
    }

    private IEnumerator GetOnTruck()
    {

        yield return new WaitForSeconds(WaitTimeAtJail);

        HandcuffsManager.AddHandcuff(1);
        HandcuffStack.AddHandcuffToStack();

        CriminalJailLine.First.Value.GetComponent<FollowTarget>().TargetTransform = TruckPointTransform;
        CriminalJailLine.RemoveFirst();

        if (CriminalJailLine.Count != 0)
        {
            CriminalJailLine.First.Value.GetComponent<FollowTarget>().TargetTransform = JailLinePointTransform;
            CriminalJailLine.First.Value.GetComponent<FollowTarget>().navAgent.stoppingDistance = 0;
            StartCoroutine(GetOnTruck());
        }
    }
}
