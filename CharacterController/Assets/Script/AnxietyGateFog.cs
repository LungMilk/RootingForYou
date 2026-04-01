using UnityEngine;

public class AnxietyGateFog : MonoBehaviour
{
    public ParticleSystem anxietyFog;

    public void SetAnxietyFog(bool state)
    {
        if (anxietyFog == null) return;
        var emission = anxietyFog.emission;
        emission.enabled = state;
    }

    public void DisableAnxietyFog()
    {
        SetAnxietyFog(false);
    }

    public void EnableAnxietyFog()
    {
        SetAnxietyFog(true);
    }
}