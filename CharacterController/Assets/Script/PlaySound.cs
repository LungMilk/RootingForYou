using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public List<SoundEffectSO> soundEffects;

    public void PlaySoundAtIndexS(int index)
    {
        soundEffects[index].Play();
    }

}
