using UnityEngine;
using TMPro;
using NUnit.Framework;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

[System.Serializable]
public struct AttributeReferences {
    public PlantAttribute Attribute;
    public string extraPositiveTag;
    public string positiveTag;
    public string neutralTag;
    public string negativeTag;
}

[CreateAssetMenu(fileName = "new ResourceRequest.asset", menuName = "ScriptableObjects/AttributeSpriteReferences")]
public class AttributeSpriteReferences : ScriptableObject {
    public List<AttributeReferences> library;
    public string CreateTag(string text)
    {
        return $"<sprite name=\"{text}\">";
    }
}
public class AttributeSpriteAssigner : MonoBehaviour
{
    public TMP_SpriteAsset spriteAsset;
    public PopUpSpeaker speaker;
    private string outputText;
    public AttributeSpriteReferences spriteLibrary;
    public GardenBox gBox;
    private void Start()
    {
        //probably work on a way to set or ask for different bubbles
    }

    public string RecieveAttribute(PlantAttribute attribute, int value)
    {
        //given the attribute we nee dto assign it to the many different glyphs
        var foundRef = spriteLibrary.library.Find(x => x.Attribute == attribute);

        if (value == 0f)
        {
            //return spriteLibrary.CreateTag(foundRef.neutralTag);
            return null;
        }
        else if (value > 0f)
        {
            return spriteLibrary.CreateTag(foundRef.positiveTag);
        } else if (value >= 5f)
        {
            return spriteLibrary.CreateTag(foundRef.extraPositiveTag);
        } else if (value < 0f)
        {
            return spriteLibrary.CreateTag(foundRef.negativeTag);
        }

        return null;
    }
    public string AttributeDictToTagString(Dictionary<PlantAttribute,int> attribute)
    {
        string finalString = "";
        foreach (var entry in attribute)
        {
            finalString += " " + RecieveAttribute(entry.Key, entry.Value);
        }
        return finalString;
    }
    [ContextMenu("Testing")]
    public void DisplayAttributeBubble()
    {
        outputText = AttributeDictToTagString(gBox.GetAttributeTotals());
        print(outputText);

        speaker.text = outputText;
        speaker.CreateBubble();
    }
}
