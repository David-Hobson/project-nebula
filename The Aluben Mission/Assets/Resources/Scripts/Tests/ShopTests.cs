using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ShopTests
{

    [Test]
    public void CheckItemPurchaseSuccess()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int[] index = { 0, 0 };
        shop.AddItem(100, 1, index, true);
        Assert.AreEqual(shop.PurchaseItem(0), true);
    }

    [Test]
    public void CheckItemPurchaseFailure()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int[] index = { 0, 0 };
        shop.AddItem(100, 1, index, true);
        Assert.AreEqual(shop.PurchaseItem(0), false);
    }

    [Test]
    public void CheckItemRemovedOnPurchase()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int[] index = { 0, 0 };
        Item item = new Item(100, 1, index, true);
        shop.AddItem(item);
        shop.PurchaseItem(0);
        Assert.AreEqual(shop.items.IndexOf(item), -1);
    }

    [Test]
    public void CheckNebuliteRemovedOnPurchase()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int ammount = shop.nebulite.GetTotal();
        int[] index = { 0, 0 };
        shop.AddItem(100, 1, index, true);
        shop.PurchaseItem(0);
        Assert.AreEqual(shop.nebulite.GetTotal(), ammount - 100);

    }

    [Test]
    public void CheckItemAddedToInventory()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int[] index = { 0, 0 };
        Item item = new Item(100, 1, index, true);
        shop.AddItem(item);
        shop.PurchaseItem(0);
        Assert.AreNotEqual(shop.inventory.items.IndexOf(item), -1);
    }

    [Test]
    public void CheckWeaponAddedToInventory()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int[] index = { 0, 0 };
        Weapon weapon = new Weapon(100, 1, index, true);
        shop.AddWeapon(weapon);
        shop.PurchaseWeapon(0);
        Assert.AreEqual(shop.inventory.weapons[0].aquired, true);
    }

    [Test]
    public void CheckUpgradeApplied()
    {
        var shop = new GameObject().AddComponent<Shop>();
        int[] index = { 0, 0 };
        Weapon weapon = new Weapon(0, 1, index, true);
        shop.AddWeapon(weapon);
        shop.PurchaseWeapon(0);
        shop.PurchaseWeapon(0);
        Assert.AreEqual(shop.inventory.weapons[0].tier, 2);
    }


}
