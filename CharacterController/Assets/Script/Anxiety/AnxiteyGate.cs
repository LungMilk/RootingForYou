using UnityEngine;

public class AnxiteyGate : MonoBehaviour
{
    public int lockCount = 3;

    public void RemoveLock()
    {
        lockCount--;

        if (lockCount <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
