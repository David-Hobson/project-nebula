using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class WeaponTests
{

    [Test]
    public void CheckWeaponCollected()
    {
        var gun = new GameObject().AddComponent<Weapon>();
        gun.AquireWeapon();
        Assert.AreEqual(gun.aquired, true);
    }

    [Test]
    public void CheckWeaponUpgraded()
    {
        var gun = new GameObject().AddComponent<Weapon>();
        gun.UpgradeWeapon();
        Assert.AreEqual(gun.tier, 1);
    }
}
