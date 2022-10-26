using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalManager : MonoBehaviour
{
    public LinkedList<GameObject> CriminalList { get; private set; } // CriminalList represents criminals, which follows player, captured by player.

    private void Awake()
    {
        // Initialize CriminalList
        CriminalList = new LinkedList<GameObject>();
    }

    public void AddLastCriminalList(GameObject criminal)
    {
        CriminalList.AddLast(criminal);
    }

    public void RemoveLastCriminalList()
    {
        CriminalList.RemoveLast();
    }

    public void RemoveFirstCriminalList()
    {
        CriminalList.RemoveFirst();
    }

    public GameObject LastCriminalList()
    {
        return CriminalList.Last.Value;
    }

    public GameObject FirstCriminalList()
    {
        return CriminalList.First.Value;
    }

    public int CountCriminalList()
    {
        return CriminalList.Count;
    }

    public bool ContainsCriminalList(GameObject ob)
    {
        return CriminalList.Contains(ob);
    }
}
