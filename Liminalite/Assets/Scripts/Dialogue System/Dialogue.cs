using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Dialogue",menuName="Dialogue")]
public class Dialogue : ScriptableObject
{
    
    public DialogueData[] dialogues;
}

[System.Serializable]
public class DialogueData
{
    public string name;
    [TextArea(2, 10)]
    public string text;
    public AudioClip voiceDub;
}