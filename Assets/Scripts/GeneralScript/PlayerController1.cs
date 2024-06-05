using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    public float moveSpeed = 1;
    public Animator animator;
    public GameObject crossHair;
    public List<Weapon> weapons;
    public List<Transform> inventorySlots;
    public List<Image> slotBorders;
    public TextMeshProUGUI currentAmmoText; 
    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI nameWeapon;
    public GameObject ammoHUD;
    public Image ammoWeapon;
    private Weapon currentWeapon;
    private int currentWeaponIndex = 0;
    private bool isAiming = false;
    private bool isMoving = false;
    private bool isReloading = false;
    private float lastShootTime = 0;
    private WeaponName currentNameWeapon = WeaponName.Empty;

    KeyCode[] weaponKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };


    Vector3 movement;
    Vector3 aim;
    Vector3 direction = Vector3.zero;

    private void Start()
    {
        if (weapons.Count > 0)
        {
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        }
        UpdateInventoryUI();
        
    }

    private void Update()
    {



        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Move();
        Animate();
        UpdateAmmoHUDVisibility();

        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeys[i]))
            {
                SwitchWeapon(i);
                break; 
            }
        }

        if (currentNameWeapon != WeaponName.Empty)
        {
            Aim();
            if (currentWeapon.type == WeaponType.Auto) 
            {
                if((Input.GetKey(KeyCode.Mouse0)||Input.GetKeyDown(KeyCode.Mouse0))&&isAiming)
                    Shoot();
            }
            if (currentWeapon.type == WeaponType.Semi)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0)&&isAiming)
                    Shoot();
            }
            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(Reload());
                return;
            }
        }
    }

    void Move()
    {
        if (!isAiming&&!isReloading)
        {
            isMoving = movement.magnitude > 0;
            if (movement.magnitude > 1) { movement.Normalize(); }
            moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
            transform.position = transform.position + movement * moveSpeed * Time.deltaTime;
        }
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        animator.SetTrigger($"{currentNameWeapon}");

        if (isAiming)
        {
            animator.SetFloat("FaceX", direction.x);
            animator.SetFloat("FaceY", direction.y);
            animator.SetFloat("FaceXAim", direction.x);
            animator.SetFloat("FaceYAim", direction.y);
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("FaceX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("FaceY", Input.GetAxisRaw("Vertical"));
            }
        }
    }

    void Aim()
    {
        if (!isMoving)
        {
            aim = Input.mousePosition;
            aim = Camera.main.ScreenToWorldPoint(aim);
            aim.z = 0;
           crossHair.transform.position = aim;
            crossHair.transform.position = aim;

            if (Input.GetKey(KeyCode.Mouse1))
            {
                direction = aim - transform.position;
                direction.Normalize();
                crossHair.SetActive(true);
                isAiming = true;
                animator.SetBool("isAiming", true);
            }
            else
            {
                isAiming = false;
                crossHair.SetActive(false);
                animator.SetBool("isAiming", false);
            }
        }
    }
    

    void Shoot()
    {
        
        if (Time.time >= (lastShootTime + currentWeapon.fireRate))
        {
            if (currentWeapon.currentAmmo > 0 && !isReloading) 
            {
                Vector2 shootDirection = crossHair.transform.localPosition;
                shootDirection.Normalize();

                GameObject bullet = Instantiate(currentWeapon.bulletPrefab, transform.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetWeapon(currentWeapon);
                bulletScript.velocity = shootDirection * 10;
                bulletScript.player = gameObject;
                bullet.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
                Destroy(bullet, 2);
                lastShootTime = Time.time;
                currentWeapon.currentAmmo--;
                currentWeapon.currentAmmo = Mathf.Max(0, currentWeapon.currentAmmo);
                UpdateAmmoUI();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reload", isReloading);
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        isReloading = false;
        animator.SetBool("Reload", isReloading);
        UpdateAmmoUI();
    }

    void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = Mathf.Clamp(index, 0, weapons.Count - 1);
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.currentAmmo = currentWeapon.maxAmmo;
            currentNameWeapon = currentWeapon.name;
            UpdateInventoryUI();
            UpdateAmmoUI();
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

    void UpdateInventoryUI()
    {
        if (inventorySlots.Count > 0)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                Transform slot = inventorySlots[i];

                if (slot != null)
                {
                    Image weaponIcon = slot.GetComponent<Image>();

                    if (weaponIcon != null)
                    {
                        if (i < weapons.Count)
                        {
                            weaponIcon.sprite = weapons[i].weaponIcon;
                            weaponIcon.gameObject.SetActive(true);
                        }
                        else
                        {
                            weaponIcon.gameObject.SetActive(false);
                        }
                    }
                }
                if (slotBorders != null && i < slotBorders.Count)
                {
                    Image border = slotBorders[i];
                    if (border != null)
                    {
                        border.gameObject.SetActive(i == currentWeaponIndex);
                    }
                }
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (currentAmmoText != null && maxAmmoText != null && nameWeapon != null && currentWeapon.bulletPrefab != null)
        {
            currentAmmoText.text = currentWeapon.currentAmmo.ToString();
            maxAmmoText.text = currentWeapon.maxAmmo.ToString();
            nameWeapon.text = currentNameWeapon.ToString();
            ammoWeapon.sprite = currentWeapon.ammoWeapon;
            
        }
    }
    void UpdateAmmoHUDVisibility()
    {
        bool isWeaponActive = currentNameWeapon != WeaponName.Empty;
        if (ammoHUD != null)
            ammoHUD.gameObject.SetActive(isWeaponActive);
    }
}
