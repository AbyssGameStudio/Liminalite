using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string name;
    [TextArea(2, 10)]
    public string text;
    public AudioClip voiceDub;
}
