using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite npcSprite;
    
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
