using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ItemTests
{

    [Test]
    public void CheckItemCollected()
    {
        var item = new GameObject().AddComponent<Item>();
        item.PickupItem(1);
        Assert.AreEqual(item.GetOwner(), 1);
    }

    [Test]
    public void CheckItemUsed()
    {
        var item = new GameObject().AddComponent<Item>();
        item.UseItem();
        Assert.AreEqual(item.GetActive(), true);
    }

    [Test]
    public void CheckItemDeleted()
    {
        var item = new GameObject().AddComponent<Item>();
        item.DeleteItem();
        Assert.AreEqual(item.GetOwner(), null);
    }

    [UnityTest]
    public IEnumerator CheckItemLifetime()
    {
        var item = new GameObject().AddComponent<Item>();
        item.UseItem();
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(item.GetDuration(), 0);
    }
}