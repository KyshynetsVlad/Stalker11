using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Weapon> weapons;
    public List<InventorySlot> slots;
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI nameWeaponText;
    public GameObject ammoHUD;
    public Image ammoWeapon;

    public int currentWeaponIndex = 0;
    private Weapon currentWeapon;
    private WeaponName currentNameWeapon = WeaponName.Empty;

    private Dictionary<HealthPickupType, int> healthPickups = new Dictionary<HealthPickupType, int>();

    public void InitializeInventory()
    {
        if (weapons.Count > 0)
        {
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        }
        else
        {
            currentWeapon = null;
        }
        UpdateAmmoHUDVisibility();
        UpdateInventoryUI();
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponName GetCurrentWeaponName()
    {
        return currentNameWeapon;
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = Mathf.Clamp(index, 0, weapons.Count - 1);
            currentWeapon = weapons[currentWeaponIndex];
            
            currentNameWeapon = currentWeapon.name;
            UpdateInventoryUI();
            UpdateAmmoUI();
            UpdateAmmoHUDVisibility();
        }
        else
        {
            Debug.LogWarning("Invalid weapon index: " + index);
        }
    }

    public void PickUpWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
        SwitchWeapon(weapons.Count - 1);
    }

    private void UpdateInventoryUI()
    {
        Debug.Log("Updating Inventory UI");
        Debug.Log(slots.Count);
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < weapons.Count)
            {
               Debug.Log("+1");
               slots[i].SetWeapon(weapons[i], i == currentWeaponIndex);
            }
            else
            {
                Debug.Log("+2");
                slots[i].ClearSlot();
            }
        }
    }
    //public void UpdateInventoryUI()
    //{
    //    if (currentWeapon != null)
    //    {
    //        currentAmmoText.text = currentWeapon.currentAmmo.ToString();
    //        maxAmmoText.text = currentWeapon.maxAmmo.ToString();
    //        nameWeaponText.text = currentWeapon.weaponName.ToString();
    //    }

    //    for (int i = 0; i < slots.Count; i++)
    //    {
    //        if (i < weapons.Count)
    //        {
    //            slots[i].SetWeapon(weapons[i]);
    //        }
    //        else
    //        {
    //            slots[i].ClearSlot();
    //        }
    //    }

    //    // Обновление UI аптечек
    //    // Добавьте сюда код для обновления отображения аптечек в инвентаре
    //    // Например, текст или иконки, показывающие количество каждого типа аптечек
    //}

    public void UpdateAmmoUI()
    {
        if (currentAmmoText != null && maxAmmoText != null && nameWeaponText != null && currentWeapon != null && currentWeapon.bulletPrefab != null)
        {
            currentAmmoText.text = currentWeapon.currentAmmo.ToString();
            maxAmmoText.text = currentWeapon.maxAmmo.ToString();
            nameWeaponText.text = currentNameWeapon.ToString();
            ammoWeapon.sprite = currentWeapon.ammoWeapon;
        }
    }

    private void UpdateAmmoHUDVisibility()
    {
        bool isWeaponActive = currentWeapon != null && currentNameWeapon != WeaponName.Empty;
        if (ammoHUD != null)
        {
            ammoHUD.gameObject.SetActive(isWeaponActive);
        }
    }

    public void AddHealthPickup(HealthPickup pickup)
    {
        if (!healthPickups.ContainsKey(pickup.pickupType))
        {
            healthPickups[pickup.pickupType] = 0;
        }
        healthPickups[pickup.pickupType]++;
        UpdateInventoryUI();
    }

    public void UseHealthPickup(HealthPickupType type)
    {
        if (healthPickups.ContainsKey(type) && healthPickups[type] > 0)
        {
            healthPickups[type]--;
            // Лечение игрока в зависимости от типа
            // Например: playerHealth.Heal(healthPickupHealAmount[type]);
            UpdateInventoryUI();
        }
    }

    public int GetHealthPickupCount(HealthPickupType type)
    {
        if (healthPickups.ContainsKey(type))
        {
            return healthPickups[type];
        }
        return 0;
    }

}