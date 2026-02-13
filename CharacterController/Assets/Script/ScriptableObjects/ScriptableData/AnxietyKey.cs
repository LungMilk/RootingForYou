using UnityEngine;

[CreateAssetMenu(fileName = "New AnxietyKey",menuName = "ScriptableObjects/AnxietyKey")]
public class AnxietyKey : ScriptableObject
{
    [Tooltip("Try to keep a record of all the keys in excel or something")]
    public int _keyID;

    [Tooltip("Ideally this will be read for the pop-ups as a descripter for what action should be done")]
    [TextArea]
    public string _displayText;

}
