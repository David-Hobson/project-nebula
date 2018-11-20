using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class InventoryTests
{

    [Test]
    public void CheckItemCollectedToInventory()
    {
        var inventory = new GameObject().AddComponent<Inventory>();
        inventory.AddItem(new Item());
        Assert.AreEqual(inventory.items.Count, 1);
    }

    [Test]
    public void CheckItemUsedFromInventory()
    {
        var inventory = new GameObject().AddComponent<Inventory>();
        inventory.AddItem(new Item());
        inventory.UseItem(0);
        Assert.AreEqual(inventory.items.Count, 0);
    }

    [Test]
    public void CheckInventoryDisplayed()
    {
        var inventory = new GameObject().AddComponent<Inventory>();
        inventory.AddItem(new Item());
        inventory.UseItem(0);
        Assert.AreEqual(inventory.Display, 1);
    }

    [Test]
    public void CheckItemDisplayedInInventoryUI()
    {
        var inventory = new GameObject().AddComponent<Inventory>();
        inventory.AddItem(new Item());
        inventory.UseItem(0);
        Assert.AreEqual(inventory.ShowInUI(), 1);
    }

}
