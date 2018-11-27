using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NebuliteTests
{

    [Test]
    public void CheckAddNebulite()
    {
        var neb = new GameObject().AddComponent<Nebulite>();
        neb.AddNebulite(50);
        Assert.AreEqual(neb.GetLevel(), 50);
    }

    [Test]
    public void CheckRemoveNebulite()
    {
        var neb = new GameObject().AddComponent<Nebulite>();
        neb.RemoveNebulite(50);
        Assert.AreEqual(neb.GetTotal(), -50);
    }

    [Test]
    public void CheckResetNebulite()
    {
        var neb = new GameObject().AddComponent<Nebulite>();
        neb.ResetNebulite();
        Assert.AreEqual(neb.GetLevel(), 0);
    }

    [Test]
    public void CheckUpdateNebulite()
    {
        var neb = new GameObject().AddComponent<Nebulite>();
        neb.AddNebulite(150);
        neb.UpdateNebulite();
        Assert.AreEqual(neb.GetTotal(), 150);
    }

}