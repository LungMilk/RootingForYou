using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public List<SoundEffectSO> soundEffects;
    private AudioSource playingSource;
    private SoundEffectSO playingEffect;
    public void PlaySoundAtIndexS(int index)
    {
        playingSource = soundEffects[index].Play();
        playingEffect = soundEffects[index];
        //Invoke("DestroySoundEffect",100f);
    }

    /// <summary>
    /// Stops whatever sound effect is playing from this objects audio source
    /// </summary>
    public void StopSoundEffect()
    {
        //this is dirty but works
        Debug.Log("stopping");
        if (playingEffect != null && playingSource != null)
        {
            playingEffect.Stop(playingSource);
            Destroy(playingSource.gameObject);
            playingEffect = null;
            playingSource = null;
        }
    }
    /// <summary>
    /// Destroys the sound effects game object and stops the sound effect from playing
    /// </summary>
    public void DestroySoundEffect()
    {
        StopSoundEffect();
        Destroy(this);
    }
    /// <summary>
    /// set the game object it is attached to to different state with given parameter, IF there is a sound effect on the object playing we turn it off
    /// </summary>
    /// <param name="state"></param>
    public void SetActiveState(bool state)
    {
        if (playingEffect != null && playingSource != null)
        {
            StopSoundEffect();

        }
        this.gameObject.SetActive(state);
    }
}
