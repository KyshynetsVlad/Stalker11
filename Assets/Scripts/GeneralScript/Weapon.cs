using UnityEngine;

public enum WeaponName
{
    Empty,
    TOZ,
    AK
}

public enum WeaponType
{
    Semi,
    Auto
}

[System.Serializable]
public class Weapon
{
    public WeaponName name;
    public WeaponType type;
    public float damage;
    public GameObject bulletPrefab;
    public float fireRate; 
    public int maxAmmo;
    public float reloadTime;
    public int currentAmmo;
    public Sprite weaponIcon;
    public Sprite ammoWeapon;

    public Weapon(WeaponName name, WeaponType type, float damage, GameObject bulletPrefab, float fireRate, int maxAmmo, float reloadTime)
    {
        this.name = name;
        this.type = type;
        this.damage = damage;
        this.bulletPrefab = bulletPrefab;
        this.fireRate = fireRate;
        this.maxAmmo = maxAmmo;
        this.reloadTime = reloadTime;
        this.currentAmmo = maxAmmo;
    }
}