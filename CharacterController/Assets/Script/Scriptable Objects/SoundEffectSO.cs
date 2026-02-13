using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundEffectSO", menuName = "Scriptable Objects/SoundEffectSO")]
    public class SoundEffectSO : ScriptableObject
    {
        public AudioClip[] clips;
        public Vector2 volume = new Vector2(0.5f, 0.5f);
        public Vector2 pitch = new Vector2(1, 1);

        private AudioClip GetAudioClip()
        {
            var clip = clips[Random.Range(0, clips.Length)];
            return clip;
        }

        public AudioSource Play(AudioSource audioSourceParam = null)
        {
            if (clips.Length == 0)
            {
                Debug.Log("Missing sound clips for {name}");
                return null;
            }

            var source = audioSourceParam;
            if (source == null)
            {
                var _obj = new GameObject("Sound", typeof(AudioSource));
                source = _obj.GetComponent<AudioSource>();
            }

            source.clip = GetAudioClip();
            source.volume = Random.Range(volume.x, volume.y);
            source.pitch = Random.Range(pitch.x, pitch.y);

            source.Play();

            Destroy(source.gameObject, source.clip.length / source.pitch);

            return source;
        }
        public void Stop(AudioSource audioSourceParam)
        {
            Destroy(audioSourceParam.gameObject, audioSourceParam.clip.length / audioSourceParam.pitch);
        }

    }
}
