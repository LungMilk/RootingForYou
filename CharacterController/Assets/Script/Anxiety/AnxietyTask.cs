using UnityEngine;

public class AnxietyTask : MonoBehaviour
{
    [Tooltip("Please provide a required Key")]
    public AnxietyKey _anxietyKey;
    [Tooltip("Pleae provide a lock target")]
    public AnxietyLock _anxietyLockTarget;
    public bool _isCompleted;

    /// <summary>
    /// Calling <see cref="Complete"/> will try to unlock the <see cref="AnxietyLock"/>
    /// </summary>
    /// <para>Can call multiple times in attempt to unlock, if <see cref="_isCompleted = true"/> it will run regardless and not try to unlock it.</para>
    public void Complete()
    {
        if (_anxietyKey == null)
        { //print("No Key Given for " + name);
          return;}
        if (_anxietyLockTarget == null)
        {
            //print("No Lock target Given for " + name);
            return;
        }
        if (_isCompleted ) { return;}
        _isCompleted = _anxietyLockTarget.TryKey(_anxietyKey);
    }
}
