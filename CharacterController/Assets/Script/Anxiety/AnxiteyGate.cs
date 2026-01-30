using UnityEngine;

public class AnxiteyGate : MonoBehaviour, IActionCall
{
    public int lockCount = 3;
    public void CallAction()
    {
        lockCount--;

        if (lockCount <= 0)
        {
            End();
        }
    }

    public void End()
    {
        this.gameObject.SetActive(false);
    }
}
