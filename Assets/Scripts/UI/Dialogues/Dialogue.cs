using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Sprite of the NPC associated with the dialogue
    public Sprite npcSprite;
    
    // Name of the NPC speaking
    public string name;

    // Sentences spoken by the NPC
    [TextArea(3, 10)]
    public string[] sentences;
}
