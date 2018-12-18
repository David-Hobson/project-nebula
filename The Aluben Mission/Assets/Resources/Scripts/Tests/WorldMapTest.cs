using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class WorldMapTest {

    [Test]
    public void CheckWorldMapActive() {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        bool expected = false;

        var actual = worldmap.mapActive();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckUnlockedLevels()
    {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        int expected = 0;

        var actual = worldmap.unlockedLevel();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckUnlockedWorldSelction()
    {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        int expected = 0;

        var actual = worldmap.unlockedWorld();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckLockedWorldSelection()
    {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        int expected = 6;

        var actual = worldmap.lockedWorld();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckLockedLevelSelection()
    {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        int expected = 6;

        var actual = worldmap.lockedLevel();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckWorldMapCorrection()
    {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        bool expected = false;

        var actual = worldmap.mapCorrection();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckWorldMapUpdate()
    {
        var worldmap = new GameObject().AddComponent<WorldMap>();
        bool expected = false;

        var actual = worldmap.worldMapUpdate();

        Assert.AreEqual(expected, actual);
    }
}
