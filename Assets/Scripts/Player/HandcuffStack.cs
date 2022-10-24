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
    private Vector3 NewHandcuffPosition; // Next handcuff's position in handcuff stack


    private void Awake()
    {
        // Initialize HandcuffList 
        HandcuffList = new LinkedList<GameObject>();

        // Create initial Handcuffs in handcuff stack
        for (int i = 0; i < HandcuffsManager.GetNumberOfHandcuffs(); i++)
        {
            InitialAddHandcuffToStack();
        }
    }

    private GameObject CreateHandcuff()
    {
        GameObject created = Instantiate(Handcuff) as GameObject;
        created.transform.parent = this.gameObject.transform;
        created.transform.localPosition = NewHandcuffPosition;
        created.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);

        return created;
    }

    // Just use when initializing HandcuffList
    public void InitialAddHandcuffToStack()
    {
        GameObject temp = CreateHandcuff();

        HandcuffList.AddLast(temp);

        NewHandcuffPosition += new Vector3(0f, distanceBetweenTwoHandcuffs, 0f);
    }

    public void RemoveHandcuffToStack()
    {
        GameObject temp = HandcuffList.Last.Value;

        HandcuffList.RemoveLast();

        Destroy(temp);

        NewHandcuffPosition -= new Vector3(0f, distanceBetweenTwoHandcuffs, 0f);
    }

    public void AddHandcuffToStack()
    {
        GameObject temp = CreateHandcuff();

        HandcuffList.AddLast(temp);

        NewHandcuffPosition += new Vector3(0f, distanceBetweenTwoHandcuffs, 0f);
    }


}
