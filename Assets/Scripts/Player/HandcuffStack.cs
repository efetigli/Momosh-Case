using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandcuffStack : MonoBehaviour
{
    [Header("Handcuff Property")]
    [SerializeField] GameObject Handcuff; // Handcuff prefab
    [SerializeField] Transform HandcuffStackTransform; // Handcuff stacking position
    [SerializeField] HandcuffsManager HandcuffsManager;
    private LinkedList<GameObject> HandcuffList;

    [Header("Handcuff Stack Position Property")]
    [SerializeField] private float distanceBetweenTwoHandcuffs; // Stacking vertical distance between two handcuffs
    private Vector3 NewHandcuffStackPosition; // Next handcuff's position in handcuff stack
    public Vector3 HandcuffGlobalPosition; // Next handcuff's position in world space.

    public Vector3 NewHandcuffCriminalPosition; // Next handcuff's position in world space.
    public GameObject CapturedCriminal;

    public bool flag;
    private void Awake()
    {
        // Initialize HandcuffList 
        HandcuffList = new LinkedList<GameObject>();

        // Create initial Handcuffs in handcuff stack
        for (int i = 0; i < HandcuffsManager.GetNumberOfHandcuffs(); i++)
        {
            InitialAddHandcuffToStack(HandcuffsManager.GetNumberOfHandcuffs() - i);
        }
    }

    private void Update()
    {
        if(!flag)
            HandcuffGlobalPosition = transform.TransformPoint(NewHandcuffStackPosition);
        else if (flag)

            HandcuffGlobalPosition = CapturedCriminal.transform.GetChild(2).transform.position;
    }

    // Just use when initializing HandcuffList
    public void InitialAddHandcuffToStack(int howMuchHandcuffLeft)
    {
        GameObject temp = Instantiate(Handcuff) as GameObject;
        temp.transform.parent = this.gameObject.transform;
        temp.transform.localPosition = NewHandcuffStackPosition;
        temp.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);

        HandcuffList.AddLast(temp);

        if(howMuchHandcuffLeft > 1)
            NewHandcuffStackPosition += new Vector3(0f, distanceBetweenTwoHandcuffs, 0f);
    }

    public void RemoveHandcuffToStack(GameObject criminal)
    {
        flag = true;
        
        GameObject temp = HandcuffList.Last.Value;
        CapturedCriminal = criminal;
        HandcuffList.RemoveLast();

        temp.GetComponent<HandcuffAnimation>().StartPosition = 
            transform.TransformPoint(NewHandcuffStackPosition - new Vector3(0f, distanceBetweenTwoHandcuffs, 0f));
        temp.GetComponent<HandcuffAnimation>().enabled = true;

        temp.transform.parent = criminal.transform;
        temp.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);

        NewHandcuffStackPosition -= new Vector3(0f, distanceBetweenTwoHandcuffs, 0f);
    }

    public void AddHandcuffToStack(Vector3 spawnPoint)
    {
        flag = false;

        GameObject temp = Instantiate(Handcuff) as GameObject;

        temp.GetComponent<HandcuffAnimation>().StartPosition = spawnPoint;
        temp.GetComponent<HandcuffAnimation>().enabled = true;

        temp.transform.parent = this.gameObject.transform;
        temp.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);

        HandcuffList.AddLast(temp);

        NewHandcuffStackPosition += new Vector3(0f, distanceBetweenTwoHandcuffs, 0f);
    }
}
