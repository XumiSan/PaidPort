using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GarageBuilding : MonoBehaviour
{
    [SerializeField]
    private GameObject GarageCanvas;
    [SerializeField]
    private GameObject GarageScreen;
    [SerializeField]
    private GameObject ServiceScreen;
    [SerializeField]
    private GameObject UpgradeScreen;
    //damage upgrade
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private Button damageButton;
    [SerializeField]
    private Text upgradeCostTextDamage;
    //health upgrade
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private Button healthButton;
    [SerializeField]
    private Text upgradeCostTextHealth;
    //inventory upgrade
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Button inventoryButton;
    [SerializeField]
    private Text upgradeCostTextInventory;
    //fuel upgrade
    [SerializeField]
    private FuelBar fuelBar;
    [SerializeField]
    private Button fuelButton;
    [SerializeField]
    private Text upgradeCostTextFuel;


    [SerializeField]
    private Text FeedbackTextService;

    private bool isGarageScreenActive = false;
    private bool isServiceScreenActive = true;
    private bool isUpgradeScreenActive = false;



    private bool inArea;

    public void Start()
    {
        UpgradeFuel();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GarageCanvas.SetActive(true);
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GarageCanvas.SetActive(false);
            inArea = false;
        }
    }
    private void Update()
    {
        if (inArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                GarageScreen.SetActive(true);
                ServiceScreen.SetActive(true);
                isGarageScreenActive = true;
                
            }
        }
        if (isGarageScreenActive && Input.GetKeyDown(KeyCode.Tab))
        {
            if (isServiceScreenActive)
            {
                ServiceScreen.SetActive(false);
                UpgradeScreen.SetActive(true);
                isServiceScreenActive = false;
                isUpgradeScreenActive = true;
            }
            else if (isUpgradeScreenActive)
            {
                ServiceScreen.SetActive(true);
                UpgradeScreen.SetActive(false);
                isServiceScreenActive = true;
                isUpgradeScreenActive = false;
            }
        }

    }
    public void Exit()
    {
        
        GarageScreen.SetActive(false);
        
    }
    public void ServiceButton()
    {
        if (GameManager.Instance.GetPlayerMoney() >= 500)
        {

            GameManager.Instance.SubtractMoney(500);
            healthBar.ResetHealth();
            StartCoroutine(DisplayLegacyTextService("Health bertambah"));
            Debug.Log("Health bertambah");

        }
        else
        {
            StartCoroutine(DisplayLegacyTextService("Uang tidak cukup"));
            Debug.Log("Uang tidak cukup");
        }
    }
    public void UpdateDamage()
    {
        if (playerMovement.damagePerHit == 10f) // Upgrade dari level 1 ke level 2
        {
            if (GameManager.Instance.GetPlayerMoney() >= 750)
            {
                playerMovement.damagePerHit = 20f;
                GameManager.Instance.SubtractMoney(750);

                if (damageButton != null)
                {
                    damageButton.GetComponentInChildren<Text>().text = "Upgrade To level 3";
                    PlayerPrefs.SetString("DamageButtonUpgradeText", "Upgrade To level 3");
                }
                if (upgradeCostTextDamage != null)
                {
                    upgradeCostTextDamage.text = "Upgrade Cost: 2000Gc";
                    PlayerPrefs.SetString("UpgradeCostText", "Upgrade Cost: 2000Gc");
                }
                PlayerPrefs.SetFloat("DamagePerHit", playerMovement.damagePerHit);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Drill ke lvl 2");
            }
        }
        else if (playerMovement.damagePerHit == 20f) // Upgrade dari level 2 ke level 3
        {
            if (GameManager.Instance.GetPlayerMoney() >= 2000)
            {
                playerMovement.damagePerHit = 30f;
                GameManager.Instance.SubtractMoney(2000);

                if (damageButton != null)
                {
                    damageButton.GetComponentInChildren<Text>().text = "Upgrade To level 4";
                    PlayerPrefs.SetString("DamageButtonUpgradeText", "Upgrade To level 4");
                }
                if (upgradeCostTextDamage != null)
                {
                    upgradeCostTextDamage.text = "Upgrade Cost: 5000Gc";
                    PlayerPrefs.SetString("UpgradeCostText", "Upgrade Cost: 5000Gc");
                }
                PlayerPrefs.SetFloat("DamagePerHit", playerMovement.damagePerHit);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Drill ke level 3");
            }
        }
        else if (playerMovement.damagePerHit == 30f) // Upgrade dari level 3 ke level 4
        {
            if (GameManager.Instance.GetPlayerMoney() >= 5000)
            {
                playerMovement.damagePerHit = 40f;
                GameManager.Instance.SubtractMoney(5000);

                if (damageButton != null)
                {
                    damageButton.GetComponentInChildren<Text>().text = "Max";
                    PlayerPrefs.SetString("DamageButtonUpgradeText", "Max");
                }
                if (upgradeCostTextDamage != null)
                {
                    upgradeCostTextDamage.text = "Max";
                    PlayerPrefs.SetString("UpgradeCostText", "Max");
                }
                PlayerPrefs.SetFloat("DamagePerHit", playerMovement.damagePerHit);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Drill ke level 4");
            }
        }
    }

    public void UpdateHealth()
    {
        if (healthBar.maxHealth == 10) // Upgrade dari level 1 ke level 2
        {
            if (GameManager.Instance.GetPlayerMoney() >= 750)
            {
                healthBar.maxHealth = 20;
                healthBar.ResetHealth();
                healthBar.UpdateHealthBar();
                GameManager.Instance.SubtractMoney(750);
                Debug.Log("Berhasil Upgrade");

                if (healthButton != null)
                {
                    healthButton.GetComponentInChildren<Text>().text = "Upgrade To level 3";
                    PlayerPrefs.SetString("HealthButtonUpgradeText", "Upgrade To level 3");
                }
                if (upgradeCostTextHealth != null)
                {
                    upgradeCostTextHealth.text = "Upgrade Cost: 2000Gc";
                    PlayerPrefs.SetString("UpgradeCostTextHealth", "Upgrade Cost: 2000Gc");
                }

                PlayerPrefs.SetFloat("MaxHealth", healthBar.maxHealth);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Body ke lvl 2");
            }
        }
        else if (healthBar.maxHealth == 20) // Upgrade dari level 2 ke level 3
        {
            if (GameManager.Instance.GetPlayerMoney() >= 2000)
            {
                healthBar.maxHealth = 30;
                healthBar.ResetHealth();
                healthBar.UpdateHealthBar();
                GameManager.Instance.SubtractMoney(2000);

                if (healthButton != null)
                {
                    healthButton.GetComponentInChildren<Text>().text = "Upgrade To level 4";
                    PlayerPrefs.SetString("HealthButtonUpgradeText", "Upgrade To level 4");
                }
                if (upgradeCostTextHealth != null)
                {
                    upgradeCostTextHealth.text = "Upgrade Cost: 5000Gc";
                    PlayerPrefs.SetString("UpgradeCostTextHealth", "Upgrade Cost: 5000Gc");
                }
                PlayerPrefs.SetFloat("MaxHealth", healthBar.maxHealth);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Bodoy ke level 3");
            }
        }
        else if (healthBar.maxHealth == 30) // Upgrade dari level 3 ke level 4
        {
            if (GameManager.Instance.GetPlayerMoney() >= 5000)
            {
                healthBar.maxHealth = 40;
                healthBar.ResetHealth();
                healthBar.UpdateHealthBar();
                GameManager.Instance.SubtractMoney(5000);

                if (healthButton != null)
                {
                    healthButton.GetComponentInChildren<Text>().text = "Max";
                    PlayerPrefs.SetString("HealthButtonUpgradeText", "Max");
                }
                if (upgradeCostTextHealth != null)
                {
                    upgradeCostTextHealth.text = "Max";
                    PlayerPrefs.SetString("UpgradeCostTextHealth", "Max");
                }
                PlayerPrefs.SetFloat("MaxHealth", healthBar.maxHealth);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Drill ke level 4");
            }
        }
    }
    public void UpdateInventory()
    {
        if (gameManager.maxLimit == 10) // Upgrade dari level 1 ke level 2
        {
            if (GameManager.Instance.GetPlayerMoney() >= 750)
            {
                gameManager.maxLimit = 20;
                GameManager.Instance.SubtractMoney(750);

                if (inventoryButton != null)
                {
                    inventoryButton.GetComponentInChildren<Text>().text = "Upgrade To level 3";
                    PlayerPrefs.SetString("InventoryButtonUpgradeText", "Upgrade To level 3");
                }
                if (upgradeCostTextInventory != null)
                {
                    upgradeCostTextInventory.text = "Upgrade Cost: 2000Gc";
                    PlayerPrefs.SetString("UpgradeCostTextInventory", "Upgrade Cost: 2000Gc");
                }
                PlayerPrefs.SetInt("MaxLimit", gameManager.maxLimit);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Inventory ke lvl 2");
            }
        }
        else if (gameManager.maxLimit == 20) // Upgrade dari level 2 ke level 3
        {
            if (GameManager.Instance.GetPlayerMoney() >= 2000)
            {
                gameManager.maxLimit = 30;
                GameManager.Instance.SubtractMoney(2000);

                if (inventoryButton != null)
                {
                    inventoryButton.GetComponentInChildren<Text>().text = "Upgrade To level 4";
                    PlayerPrefs.SetString("InventoryButtonUpgradeText", "Upgrade To level 4");
                }
                if (upgradeCostTextInventory != null)
                {
                    upgradeCostTextInventory.text = "Upgrade Cost: 5000Gc";
                    PlayerPrefs.SetString("UpgradeCostTextInventory", "Upgrade Cost: 5000Gc");
                }
                PlayerPrefs.SetInt("MaxLimit", gameManager.maxLimit);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Inventory ke level 3");
            }
        }
        else if (gameManager.maxLimit == 30) // Upgrade dari level 3 ke level 4
        {
            if (GameManager.Instance.GetPlayerMoney() >= 5000)
            {
                gameManager.maxLimit = 40;
                GameManager.Instance.SubtractMoney(5000);

                if (inventoryButton != null)
                {
                    inventoryButton.GetComponentInChildren<Text>().text = "Max";
                    PlayerPrefs.SetString("InventoryButtonUpgradeText", "Max");
                }
                if (upgradeCostTextInventory != null)
                {
                    upgradeCostTextInventory.text = "Max";
                    PlayerPrefs.SetString("UpgradeCostTextInventory", "Max");
                }
                PlayerPrefs.SetInt("MaxLimit", gameManager.maxLimit);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Inventory ke level 4");
            }
        }
    }
    public void UpdateFuel()
    {
        if (fuelBar.totalFuel == 100) // Upgrade dari level 1 ke level 2
        {
            if (GameManager.Instance.GetPlayerMoney() >= 750)
            {
                fuelBar.totalFuel = 150;
                fuelBar.ResetFuel();
                fuelBar.UpdateFuelBar();
                GameManager.Instance.SubtractMoney(750);

                if (fuelButton != null)
                {
                    fuelButton.GetComponentInChildren<Text>().text = "Upgrade To level 3";
                    PlayerPrefs.SetString("FuelButtonUpgradeText", "Upgrade To level 3");
                }
                if (upgradeCostTextFuel != null)
                {
                    upgradeCostTextFuel.text = "Upgrade Cost: 2000Gc";
                    PlayerPrefs.SetString("UpgradeCostTextFuel", "Upgrade Cost: 2000Gc");
                }
                PlayerPrefs.SetInt("TotalFuel", fuelBar.totalFuel);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Fuel Tank ke lvl 2");
            }
        }
        else if (fuelBar.totalFuel == 150) // Upgrade dari level 2 ke level 3
        {
            if (GameManager.Instance.GetPlayerMoney() >= 2000)
            {
                fuelBar.totalFuel = 200;
                fuelBar.ResetFuel();
                fuelBar.UpdateFuelBar();
                GameManager.Instance.SubtractMoney(2000);

                if (fuelButton != null)
                {
                    fuelButton.GetComponentInChildren<Text>().text = "Upgrade To level 4";
                    PlayerPrefs.SetString("FuelButtonUpgradeText", "Upgrade To level 4");
                }
                if (upgradeCostTextFuel != null)
                {
                    upgradeCostTextFuel.text = "Upgrade Cost: 5000Gc";
                    PlayerPrefs.SetString("UpgradeCostTextFuel", "Upgrade Cost: 5000Gc");
                }
                PlayerPrefs.SetInt("TotalFuel", fuelBar.totalFuel);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Fuel Tank ke level 3");
            }
        }
        else if (fuelBar.totalFuel == 200) // Upgrade dari level 3 ke level 4
        {
            if (GameManager.Instance.GetPlayerMoney() >= 5000)
            {
                fuelBar.totalFuel = 300;
                fuelBar.ResetFuel();
                fuelBar.UpdateFuelBar();
                GameManager.Instance.SubtractMoney(5000);

                if (fuelButton != null)
                {
                    fuelButton.GetComponentInChildren<Text>().text = "Max";
                    PlayerPrefs.SetString("FuelButtonUpgradeText", "Max");
                }
                if (upgradeCostTextFuel != null)
                {
                    upgradeCostTextFuel.text = "Max";
                    PlayerPrefs.SetString("UpgradeCostTextFuel", "Max");
                }
                PlayerPrefs.SetInt("TotalFuel", fuelBar.totalFuel);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Uang tidak cukup untuk upgrade Drill ke level 4");
            }
        }
    }

    void UpgradeDamage()
    {
        if (damageButton != null)
        {
            damageButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("DamageButtonUpgradeText", "Upgrade To level 2");
        }
        if (upgradeCostTextDamage != null)
        {
            upgradeCostTextDamage.text = PlayerPrefs.GetString("UpgradeCostText", "Upgrade Cost: 750Gc");
        }

       
        playerMovement.damagePerHit = PlayerPrefs.GetFloat("DamagePerHit", 10f);
    }

    void UpgradeHealth()
    {
        UpgradeDamage();
        if (healthButton != null)
        {
            healthButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("HealthButtonUpgradeText", "Upgrade To level 2");
        }
        if (upgradeCostTextHealth != null)
        {
            upgradeCostTextHealth.text = PlayerPrefs.GetString("UpgradeCostHealthText", "Upgrade Cost: 750Gc");
        }
        healthBar.maxHealth = PlayerPrefs.GetFloat("MaxHealth", 10);
        healthBar.UpdateHealthBar();
    }
    void UpgradeInventory()
    {
        UpgradeHealth();
        if (inventoryButton != null)
        {
            inventoryButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("InventoryButtonUpgradeText", "Upgrade To level 2");
        }
        if (upgradeCostTextInventory != null)
        {
            upgradeCostTextInventory.text = PlayerPrefs.GetString("UpgradeCostTextInventory", "Upgrade Cost: 750Gc");
        }
        gameManager.maxLimit = PlayerPrefs.GetInt("MaxLimit", 10);
    }
    void UpgradeFuel()
    {
        UpgradeInventory();
        if (fuelButton != null)
        {
            fuelButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("FuelButtonUpgradeText", "Upgrade To level 2");
        }
        if (upgradeCostTextFuel != null)
        {
            upgradeCostTextFuel.text = PlayerPrefs.GetString("UpgradeCostTextFuel", "Upgrade Cost: 750Gc");
        }
        fuelBar.totalFuel = PlayerPrefs.GetInt("TotalFuel", 100);
        fuelBar.UpdateFuelBar();
    }
    private IEnumerator DisplayLegacyTextService(string displayText)
    {
        FeedbackTextService.text = displayText; 
        FeedbackTextService.enabled = true; 

        yield return new WaitForSeconds(2f); 

        FeedbackTextService.enabled = false;
    }
}
