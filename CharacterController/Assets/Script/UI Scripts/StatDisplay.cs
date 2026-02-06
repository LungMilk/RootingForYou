using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{

    public int maxStat;
    public int currentStat;
    public int previewStat;

    public GameObject statSection;
    public List<GameObject> statSections;

    public Color barColor;
    public Color previewColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0;  i < maxStat; i++)
        {
            var currentStat = Instantiate(statSection);
            currentStat.transform.SetParent(this.gameObject.transform);
            statSections.Add(currentStat);
        }
    }

    private void Update()
    {
        //UpdateBarUI();
    }

    public void UpdateBarUI()
    {
        for(int i = 0; i < maxStat; i++)
        {
            statSections[i].GetComponent<Image>().color = Color.white;
        }

        for(int i = 0; i < currentStat; i++)
        {
            statSections[i].GetComponent<Image>().color = barColor;
        }

        for(int i = currentStat; i < currentStat + previewStat; i++)
        {
            statSections[i].GetComponent<Image>().color = barColor + previewColor;
        }
    }
}
