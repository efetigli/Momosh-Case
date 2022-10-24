using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandcuffStack : MonoBehaviour
{
    [SerializeField] GameObject Handcuff;
    [SerializeField] Transform HandcuffStackTransform;
    [SerializeField] HandcuffsManager HandcuffsManager;

    [SerializeField] float distanceBetweenTwoHandcuffs;
    [SerializeField] private Vector3 NewHandcuffPosition;

    [SerializeField] private LinkedList<GameObject> HandcuffList;

    private void Awake()
    {
        HandcuffList = new LinkedList<GameObject>();

        for(int i = 0; i < HandcuffsManager.GetNumberOfHandcuffs(); i++)
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
