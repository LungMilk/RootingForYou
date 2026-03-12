using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public List<GameObject> seedButtons;

    //public int seedsInInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //foreach (var button in seedButtons)
        //{
        //    button.gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < seedButtons.Count; i++)
        {
            seedButtons[i].SetActive(true);
        }
    }
}
