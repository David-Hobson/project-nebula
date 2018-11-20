using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NPCTests
{

    [Test]
    public void CheckInteractionWithPlayer()
    {
        var character = new GameObject().AddComponent<NPC>();
        character.startDialogue();
        Assert.AreEqual(character.checkStatus(), true);
    }

    [Test]
    public void CheckNPCEnd()
    {
        var character = new GameObject().AddComponent<NPC>();
        character.startDialogue();
        character.endDialogue();
        Assert.AreEqual(character.checkStatus(), false);
    }
}
