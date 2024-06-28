using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    public GameObject crossHair;
    public Inventory inventory;
    private bool isAiming = false;
    private bool isMoving = false;
    private bool isReloading = false;
    private float lastShootTime = 0;

    public VectorValue pos;
    public PlayerHealth playerHealth;
    public WeaponData weaponData;
    public PauseMenu pauseMenu;
    public PauseMenu pause;

    KeyCode[] weaponKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    Vector3 movement;
    Vector3 aim;
    Vector3 direction = Vector3.zero;

    private void Start()
    {
        if (pos == null)
        {
            Debug.LogError("VectorValue 'pos' is not assigned in the inspector.");
            return;
        }
        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned in the inspector.");
            return;
        }
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth is not assigned in the inspector.");
            return;
        }
        if (weaponData == null)
        {
            Debug.LogError("WeaponData is not assigned in the inspector.");
            return;
        }
        if (crossHair == null)
        {
            Debug.LogError("CrossHair is not assigned in the inspector.");
            return;
        }
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned in the inspector.");
            return;
        }
        transform.position = pos.initialValue;
        inventory.InitializeInventory();
        InitializeHealth();
        InitializeInventory();
    }

    private void InitializeHealth()
    {
        GetComponent<Health>().maxHealth = playerHealth.maxHealth;
        GetComponent<Health>().currentHealth = playerHealth.currentHealth;
    }

    private void InitializeInventory()
    {
        inventory.weapons = weaponData.weapons;
        inventory.currentWeaponIndex = weaponData.currentWeaponIndex;
        inventory.InitializeInventory();
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Move();
        Animate();
        pause.Pauses();

        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeys[i]))
            {
                inventory.SwitchWeapon(i);
                break;
            }
        }

        if (inventory.GetCurrentWeaponName() != WeaponName.Empty)
        {
            Aim();
            if (inventory.GetCurrentWeapon().type == WeaponType.Auto)
            {
                if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse0)) && isAiming)
                    Shoot();
            }
            if (inventory.GetCurrentWeapon().type == WeaponType.Semi)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && isAiming)
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
        if (!isAiming && !isReloading)
        {
            isMoving = movement.magnitude > 0;
            if (movement.magnitude > 1) { movement.Normalize(); }
            float moveCurrent = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * 2 : moveSpeed;
            transform.position = transform.position + movement * moveCurrent * Time.deltaTime;
        }
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        animator.SetTrigger($"{inventory.GetCurrentWeaponName()}");

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

            if (Input.GetKey(KeyCode.Mouse1))
            {
                Cursor.visible = false;
                direction = aim - transform.position;
                direction.Normalize();
                crossHair.SetActive(true);
                isAiming = true;
                animator.SetBool("isAiming", true);
            }
            else
            {
                Cursor.visible = true;
                isAiming = false;
                crossHair.SetActive(false);
                animator.SetBool("isAiming", false);
            }
        }
    }

    void Shoot()
    {
        if (Time.time >= (lastShootTime + inventory.GetCurrentWeapon().fireRate))
        {
            if (inventory.GetCurrentWeapon().currentAmmo > 0 && !isReloading)
            {
                Vector2 shootDirection = crossHair.transform.localPosition;
                shootDirection.Normalize();

                GameObject bullet = Instantiate(inventory.GetCurrentWeapon().bulletPrefab, transform.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetWeapon(inventory.GetCurrentWeapon());
                bulletScript.velocity = shootDirection * 10;
                bulletScript.player = gameObject;
                bullet.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
                Destroy(bullet, 2);
                lastShootTime = Time.time;
                inventory.GetCurrentWeapon().currentAmmo--;
                inventory.GetCurrentWeapon().currentAmmo = Mathf.Max(0, inventory.GetCurrentWeapon().currentAmmo);
                inventory.UpdateAmmoUI();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reload", isReloading);
        yield return new WaitForSeconds(inventory.GetCurrentWeapon().reloadTime);
        inventory.GetCurrentWeapon().currentAmmo = inventory.GetCurrentWeapon().maxAmmo;
        isReloading = false;
        animator.SetBool("Reload", isReloading);
        inventory.UpdateAmmoUI();
    }
}