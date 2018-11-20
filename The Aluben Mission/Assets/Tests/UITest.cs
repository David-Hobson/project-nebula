using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class UITest {

    [Test]
    public void CheckStartGame()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(1, ui.StartGame());
    }

    [Test]
    public void CheckReturnToTitle()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(2, ui.ReturnToTitle());
    }

    [Test]
    public void CheckExitGame()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(3, ui.ExitGame());
    }

    [Test]
    public void CheckPlayerPause()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(4, ui.PlayerPause());
    }

    [Test]
    public void CheckExitMenu()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(5, ui.ExitMenu());
    }

    [Test]
    public void CheckExitLevel()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(6, ui.ExitLevel());
    }

    [Test]
    public void CheckHpGauge()
    {
        // Use the Assert class to test conditions.
        var ui = new GameObject().AddComponent<UI>();
        Assert.AreEqual(7, ui.HpGauge());
    }
}
