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

    //[Tooltip("Pop up speaker text will be overriden by the Anxiety key text off by default")]
    //public bool overrideText;
    public void Start()
    {
        _locked = true;
        //if (speaker != null)
        //    speaker.text = _requiredKey._displayText;
    }
    /// <summary>
    /// Give an AnxietyKey to <see cref="AnxietyLock"/>. This will try and call <see cref="Unlocked"/>
    /// </summary>
    /// <param name="key"></param>
    public bool TryKey(AnxietyKey key)
    {
        if (key == _requiredKey)
        {
            if (speaker != null && speaker.textBubble != null)
            {
                speaker.DismissBubble();
            }

            _locked = false;

            HeadTrackerUI.instance.RemoveNPCHead();
            Unlocked?.Invoke();
            return true;
        }

        return false;
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
        //Unlocked?.Invoke();
        if (speaker != null)
        {
            speaker.DismissBubble();
        }

        this.gameObject.SetActive(false);
    }
}
