using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    public List<GameObject> bars;
    public int beauty;
    public int calmness;
    public int passion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateBars();
    }

    public void UpdateBars()
    {
        for(int i = 0; i < bars.Count; i++)
        {
            bars[i].GetComponent<StatDisplay>().UpdateBarUI();
        }

        bars[0].GetComponent<StatDisplay>().currentStat = beauty;
        bars[1].GetComponent<StatDisplay>().currentStat = calmness;
        bars[2].GetComponent<StatDisplay>().currentStat = passion;
    }
}
