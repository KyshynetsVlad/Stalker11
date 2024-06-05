using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public new WeaponName name;
    public WeaponType type;
    public float damage;
    public GameObject bulletPrefab;
    public float fireRate;
    public int maxAmmo;
    public float reloadTime;
    public Sprite weaponIcon;
    public Sprite ammoWeapon;

    public Weapon GetWeapon()
    {
        return new Weapon(name, type, damage, bulletPrefab, fireRate, maxAmmo, reloadTime);
    }
}