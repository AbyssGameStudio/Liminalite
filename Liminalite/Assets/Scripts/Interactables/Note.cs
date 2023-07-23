using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Note", menuName="Note")]
public class Note : ScriptableObject
{
    public enum NoteType
    {
        Cannon,
        Collectible
    }
    public string targetName;
    public string authorName;
    public string text;
    public NoteType type;
}


