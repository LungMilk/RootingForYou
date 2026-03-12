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
    }
    public void StopSoundEffect()
    {
        //this is dirty but works
        if (playingEffect != null && playingSource != null)
        {
            playingEffect.Stop(playingSource);
            Destroy(playingSource.gameObject);
            playingEffect = null;
            playingSource = null;
        }
    }
}
