using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCriminals : MonoBehaviour
{
    [SerializeField] private GameObject Chain;
    [SerializeField] private CriminalManager CriminalManager;

    public LinkedList<GameObject> ChainList;

    private void Awake()
    {
        ChainList = new LinkedList<GameObject>();
    }

    public void RemoveChainBetweenPlayerAndCriminalAndCreate()
    {
        Destroy(this.gameObject.transform.GetChild(this.transform.childCount - 1).gameObject);
        CreateChainBetweenPlayerAndCriminal(this.gameObject, CriminalManager.FirstCriminalList());
    }

    public void RemoveChainBetweenPlayerAndCriminal()
    {
        Destroy(this.gameObject.transform.GetChild(this.transform.childCount - 1).gameObject);
    }

    public void CreateChainBetweenPlayerAndCriminal(GameObject player, GameObject newCriminal)
    {
        GameObject temp = Instantiate(Chain) as GameObject;

        temp.transform.position = player.transform.position;
        temp.transform.SetParent(player.transform);
        temp.transform.GetChild(temp.transform.childCount - 1).position = newCriminal.transform.position;

        temp.transform.GetChild(0).GetComponent<HingeJoint>().connectedBody = player.transform.GetChild(0).GetComponent<Rigidbody>();
        temp.transform.GetChild(temp.transform.childCount - 1).GetComponent<HingeJoint>().connectedBody =
            newCriminal.GetComponent<Rigidbody>();
    }

    public void CreateChainBetweenCriminalAndCriminal(GameObject tail, GameObject newCriminal)
    {
        GameObject temp = Instantiate(Chain) as GameObject;

        temp.transform.position = tail.transform.position;
        temp.transform.GetChild(temp.transform.childCount - 1).position = newCriminal.transform.position;

        temp.transform.GetChild(0).GetComponent<HingeJoint>().connectedBody = tail.GetComponent<Rigidbody>();
        temp.transform.GetChild(temp.transform.childCount - 1).GetComponent<HingeJoint>().connectedBody =
            newCriminal.GetComponent<Rigidbody>();

        ChainList.AddLast(temp.gameObject);
    }

    private void PrintLinkedList(LinkedList<GameObject> ll)
    {
        string output = "";
        foreach (GameObject ob in ll)
        {
            output += ob.name + ", ";
        }
        Debug.Log(output);
    }
}
