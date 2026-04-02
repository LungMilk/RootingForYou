using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using ScriptableObjects;
public class NPCHeadTracker : MonoBehaviour
{
    public List<Image> npcHeads;
    public Sprite headIcon;

    public SoundEffectSO completeSoundEffect;
    public int amount { get; private set; }
    private void Awake()
    {
        Initalize();
    }
    public void Initalize()
    {
        amount = npcHeads.Count;
        foreach (var sprite in npcHeads)
        {
            sprite.gameObject.SetActive(true);

            //if (headIcon != null)
            //{
            //    sprite.sprite = headIcon;
            //}
        }
    }
    public void RemoveHead()
    {
        print("Remove heads");
        if (amount <= 0)
        {
            print("No heads left");
            return;
        }
        foreach (var sprite in npcHeads)
        {
            if (sprite.gameObject.activeSelf)
            {
                sprite.gameObject.SetActive(false);
                completeSoundEffect.Play();
                amount--;
                return;
            }
        }
    }
}
