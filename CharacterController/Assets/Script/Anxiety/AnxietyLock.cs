using UnityEngine;
using UnityEngine.Events;
public class AnxietyLock : MonoBehaviour
{
    public UnityEvent Unlocked;

    [Tooltip("Please give the necessary keyObject")]
    public AnxietyKey _requiredKey;
    public bool _locked = true;

    [Tooltip("Speaker is optional, if given it will set the speaker text to that of the given key")]
    public PopUpSpeaker speaker;
    public void Start()
    {
        if (speaker != null)
            speaker.text = _requiredKey._displayText;
    }
    /// <summary>
    /// Give an AnxietyKey to <see cref="AnxietyLock"/>. This will try and call <see cref="Unlocked"/>
    /// </summary>
    /// <param name="key"></param>
    public bool TryKey(AnxietyKey key)
    {
        if (key == _requiredKey)
        {
            _locked = true;
            Unlocked?.Invoke();
        }
        return _locked;
    }
}
