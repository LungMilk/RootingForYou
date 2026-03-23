using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public enum PlayerMood
{
    happy, anxious,
}
public class HeadTrackerUI : MonoBehaviour
{
    //manage player head element
    //call the NPC tracker ot changeits state

    public static HeadTrackerUI instance;
    public NPCHeadTracker npcTracker;
    [SerializeField] private int npcs;

    public PlayerMood playerMood = PlayerMood.happy;
    public Image playerFace;
    [Header("Sprites")]
    public Sprite goodPCFace;
    public Sprite badPCFace;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        SetPlayerMood(PlayerMood.happy);
        npcs = npcTracker.amount;
    }
    public void SetPlayerMood(PlayerMood mood)
    {
        //print("setting player mood to" + mood);
        playerMood = mood;

        if (playerMood == PlayerMood.happy)
        {
            playerFace.sprite = goodPCFace;
        }else if(playerMood == PlayerMood.anxious)
        {
            playerFace.sprite = badPCFace;
        }
    }

    public void SetPlayerMoodForSeconds(float duration, PlayerMood mood)
    {
        SetPlayerMood(mood);
        Invoke("SwitchPlayerMood", duration);
    }
    [ContextMenu("Switch player mood")]
    public void SwitchPlayerMood()
    {
        if (playerMood == PlayerMood.happy) { SetPlayerMood(PlayerMood.anxious); }
        else if (playerMood == PlayerMood.anxious) { SetPlayerMood(PlayerMood.happy); }
    }
    [ContextMenu("Remove NPC Head")]
    public void RemoveNPCHead()
    {
        npcTracker.RemoveHead();
        npcs = npcTracker.amount;
    }
}
