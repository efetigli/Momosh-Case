using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailStation : MonoBehaviour
{
    [Header("Needed Scripts")]
    [SerializeField] HandcuffsManager HandcuffsManager;
    [SerializeField] CriminalManager CriminalManager;
    [SerializeField] HandcuffStack HandcuffStack;
    [SerializeField] ChainCriminals ChainCriminals;

    [Header("Tracked Points")]
    [SerializeField] Transform JailLinePointTransform;
    [SerializeField] Transform TruckPointTransform;
    [SerializeField] Transform PlayerTransform;

    private LinkedList<GameObject> CriminalJailList; // CriminalJailLine represents criminals send to jail. (Those criminals are independent from player)

    [Header("Jail Property")]
    [SerializeField] float WaitTimeAtJail; // Time will first criminal will spend time in jail.
    [SerializeField] int maxNumberOfCriminalInJail;
    private LinkedList<GameObject> JailChainList; // JailChainList represents in jail line criminal's chains.

    private void Awake()
    {
        // Initialize CriminalJailLine
        CriminalJailList = new LinkedList<GameObject>();

        // Initialize JailChainList
        JailChainList = new LinkedList<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If there is no criminal follows player then return
        if (CriminalManager.CountCriminalList() == 0)
            return;

        // If there is already crimal in jail then return
        if (CriminalJailList.Count != 0)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            // Detect how many criminal will send to jail. 
            int howManyCriminal = maxNumberOfCriminalInJail;
            if (CriminalManager.CountCriminalList() < maxNumberOfCriminalInJail)
                howManyCriminal = CriminalManager.CountCriminalList();

            LinkedList<GameObject> tempJailChainList = new LinkedList<GameObject>();
            for(int i = 0; i < howManyCriminal; i++)
            {
                // Add criminal to CriminalJailList and remove CriminalList.
                CriminalJailList.AddLast(CriminalManager.FirstCriminalList());
                CriminalManager.RemoveFirstCriminalList();

                if(ChainCriminals.ChainList.Count != 0)
                {
                    tempJailChainList.AddLast(ChainCriminals.ChainList.First.Value);
                    ChainCriminals.ChainList.RemoveFirst();
                }
            }
            JailChainList = tempJailChainList;

            // Checking, are there some criminals left following player.
            if (CriminalManager.CountCriminalList() > 0)
            {
                ChainCriminals.RemoveChainBetweenPlayerAndCriminalAndCreate();
                Destroy(JailChainList.Last.Value);
                JailChainList.RemoveLast();
            }
            // Checking, are there no criminals left following player.
            else if (CriminalManager.CountCriminalList() == 0)
            {
                ChainCriminals.RemoveChainBetweenPlayerAndCriminal();
            }

            // If there is at least one criminal left, then head of criminals follows the player.
            if (CriminalManager.CountCriminalList() != 0)
            {
                CriminalManager.FirstCriminalList().GetComponent<FollowTarget>().TargetTransform = PlayerTransform;
            }

            // Start sending criminals from jail to truck.
            StartCoroutine(GetOnTruck());
            CriminalJailList.First.Value.GetComponent<FollowTarget>().TargetTransform = JailLinePointTransform;
            CriminalJailList.First.Value.GetComponent<FollowTarget>().navAgent.stoppingDistance = 0;
        }
    }

    // Sending criminals from jail to truck.
    private IEnumerator GetOnTruck()
    {

        yield return new WaitForSeconds(WaitTimeAtJail);

        // Add hancuff to the player.
        HandcuffsManager.AddHandcuff(1);
        HandcuffStack.AddHandcuffToStack(this.transform.position);

        Destroy(CriminalJailList.First.Value.transform.GetChild(3).gameObject);

        CriminalJailList.First.Value.GetComponent<FollowTarget>().TargetTransform = TruckPointTransform;
        CriminalJailList.RemoveFirst();

        // Destroy chains when entering the truck.
        if(JailChainList.Count != 0)
        {
            Destroy(JailChainList.First.Value);
            JailChainList.RemoveFirst();
        }

        // Be sure that it is not the last criminal. If it is then not enter.
        if (CriminalJailList.Count != 0)
        {
            CriminalJailList.First.Value.GetComponent<FollowTarget>().TargetTransform = JailLinePointTransform;
            CriminalJailList.First.Value.GetComponent<FollowTarget>().navAgent.stoppingDistance = 0;
            StartCoroutine(GetOnTruck());
        }
    }

    private void PrintLinkedList(LinkedList<GameObject> ll)
    {
        string output = "";
        foreach (GameObject ob in ll)
        {
            output += ob.name + " ";
        }
        Debug.Log(output);
    }
}