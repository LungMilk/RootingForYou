using UnityEngine;

public class AnxietyFog : MonoBehaviour
{
    //public GameObject anxietyFogPrefab;
    //public Transform gardenBoxPosition;
    public ParticleSystem[] anxietyFog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Instantiate(anxietyFogPrefab, gardenBoxPosition);
        //Debug.Log("working");
        SetAllParticles(true);
    }

    private void OnTriggerExit(Collider collision)
    {
        //Destroy(anxietyFogPrefab);
        //Debug.Log("outOfFog");
        SetAllParticles(false);
    }

    private void SetAllParticles(bool active)
    {
        foreach (ParticleSystem ps in anxietyFog)
        {
            var emission = ps.emission;
            emission.enabled = active;
        }
    }
}
