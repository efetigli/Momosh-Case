using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalManager : MonoBehaviour
{
    [SerializeField] public LinkedList<GameObject> CriminalLine;

    private void Awake()
    {
        CriminalLine = new LinkedList<GameObject>();
    }

    public void AddCriminalToCriminalLine(GameObject criminal)
    {
        CriminalLine.AddLast(criminal);
    }
}
