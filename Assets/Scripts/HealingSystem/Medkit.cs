using UnityEngine;

public class Medkit : MonoBehaviour
{
    public enum MedkitType { Small = 10, Large = 30}
    [SerializeField] private MedkitType medkitType;
    [SerializeField] private HealthInventory healthInventory;
    
    public void SetHealthInventory(HealthInventory HealthInventory)
    {
        healthInventory = HealthInventory;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (healthInventory != null)
            {
                if (medkitType == MedkitType.Small)
                {
                    healthInventory.AddSmallMedkit(1);
                }
                else if (medkitType == MedkitType.Large)
                {
                    healthInventory.AddLargeMedkit(1);
                }

                Destroy(gameObject);
            }
        }
    }
}
