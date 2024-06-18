using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public List<Weapon> weapons;
    public int currentWeaponIndex;

    public void Reset()
    {
        weapons = new List<Weapon>();
        currentWeaponIndex = 0;
    }
}