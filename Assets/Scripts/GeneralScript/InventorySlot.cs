using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image weaponIcon;
    public Image slotBorder;

    public void SetWeapon(Weapon weapon, bool isSelected)
    {
        if (weapon != null && weapon.weaponIcon != null)
        {
            weaponIcon.sprite = weapon.weaponIcon;
            weaponIcon.gameObject.SetActive(true);
        }
        else
        {
            weaponIcon.sprite = null; // Встановлюємо зображення за замовчуванням
            weaponIcon.gameObject.SetActive(true); // Активуємо, навіть якщо встановлено за замовчуванням
        }

        slotBorder.gameObject.SetActive(isSelected);
    }

    public void ClearSlot()
    {
        weaponIcon.gameObject.SetActive(false);
        slotBorder.gameObject.SetActive(false);
    }
}