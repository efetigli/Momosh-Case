using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandcuffsManager : MonoBehaviour
{
    // "numberOfHandcuffs" shows how many arrest can player make.
    [SerializeField] private int numberOfHandcuffs;

    public void AddHandcuff(int value)
    {
        numberOfHandcuffs += value;
    }

    public void RemoveHandcuff(int value)
    {
        numberOfHandcuffs -= value;
    }

    public int GetNumberOfHandcuffs()
    {
        return numberOfHandcuffs;
    }
}
