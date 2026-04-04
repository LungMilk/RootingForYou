using UnityEngine;

public class AnxietyFog : MonoBehaviour
{
    //public GameObject anxietyFogPrefab;
    //public Transform gardenBoxPosition;
    public ParticleSystem[] anxietyFog;
    //we need a way for them to stay off for when we complete an anxiety task so they don't come back

    public bool stillActive = true;

    public void DisableFog(bool state)
    {
        SetAllParticles(false);
        stillActive = !state;
    }
        
    //private void OnTriggerEnter(Collider collision)
    //{
    //    //Instantiate(anxietyFogPrefab, gardenBoxPosition);
    //    //Debug.Log("working");
    //    SetAllParticles(true);
    //}

    //private void OnTriggerExit(Collider collision)
    //{
    //    //Destroy(anxietyFogPrefab);
    //    //Debug.Log("outOfFog");
    //    SetAllParticles(false);
    //}

    public void SetAllParticles(bool active)
    {
        if (!stillActive)
        {
            return;
        }

        foreach (ParticleSystem ps in anxietyFog)
        {
            var emission = ps.emission;
            emission.enabled = active;
        }
    }
}
