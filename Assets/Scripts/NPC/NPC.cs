using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible
{
    public string npcName;

    public string[] dialogue;

    public override void Interact()
    {
        DialogueDisplay.Main.StartDialogue(npcName, dialogue);
    }
}
