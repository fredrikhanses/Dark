using System;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    public string name;
    public Sprite image;
    public float textSpeed;
    [TextArea(3, 5)]
    public string[] text;
}
