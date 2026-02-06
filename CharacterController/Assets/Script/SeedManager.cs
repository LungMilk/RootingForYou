using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public List<GameObject> seedButtons;

    public int seedsInInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set all seedbuttons inactive
    }

    // Update is called once per frame
    void Update()
    {
        //set the active seed buttons to the number of seeds in inventory 
    }
}
