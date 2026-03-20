using Unity.Cinemachine;
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


    [Header("Camera Settings")]
    public CinemachineCamera cam;
    [Tooltip("How long we will look at the anxiety lock")]
    public float duration = 5f;
    public void Start()
    {
        _locked = true;
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
            if (speaker.textBubble != null)
            {
                speaker.DismissBubble();
            }
            if (_locked)
            {
                HeadTrackerUI.instance.RemoveNPCHead();
            }
            _locked = false;
            Unlocked?.Invoke();
        }
        return !_locked;
    }

    public void LookAtMe()
    {
        if (cam == null)
        {
            return;
        }
        CameraManager.Instance.SwitchCamera(cam, duration);
    }

    public void DestroyLock()
    {
        Unlocked?.Invoke();
        speaker.DismissBubble();
    }
}
