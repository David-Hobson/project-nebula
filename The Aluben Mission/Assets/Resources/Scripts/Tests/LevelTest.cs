using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LevelTest {

    [Test]
    public void CheckLevelActive()
    {
        // Use the Assert class to test conditions.
        var level = new GameObject().AddComponent<Level>();
        Assert.AreEqual(1, level.LevelActive());
    }

    [Test]
    public void CheckLevelNotComplete()
    {
        // Use the Assert class to test conditions.
        var level = new GameObject().AddComponent<Level>();
        Assert.AreEqual(2, level.LevelNotComplete());
    }

    [Test]
    public void CheckLevelComplete()
    {
        // Use the Assert class to test conditions.
        var level = new GameObject().AddComponent<Level>();
        Assert.AreEqual(3, level.LevelComplete());
    }
}
