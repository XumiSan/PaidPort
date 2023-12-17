using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    public int maxLimit = 10;
    public int totalItems = 0;
    private int money = 5000;
    [SerializeField]
    private Text FeedbackTextItem;
    [SerializeField]
    private Text FeedbackTextSell;
    [SerializeField]
    private Canvas inventoryCanvas;
    [SerializeField]
    private Text maxInventory;
    [SerializeField]
    private Text bronzeCountText;
    [SerializeField]
    private Text silverCountText;
    [SerializeField] 
    private Text goldCountText;
    [SerializeField] 
    private Text diamondCountText;
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private GameObject GameOverScreen;
    [SerializeField]
    private GameObject GameScreen;
    [SerializeField]
    private GameObject EndScreen;
    [SerializeField]
    private GameObject EndSound;
    private bool isInventoryActive = false;

    public List<int> groundListType;
    public List<int> groundListTypeIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        inventoryCanvas.gameObject.SetActive(false);
        LoadInventory();
        
    }

    private void Start()
    {
        LoadMoney();
        UpdateMoneyText();
        UpdateItemCountText("Bronze");
        UpdateItemCountText("Silver");
        UpdateItemCountText("Gold");
        UpdateItemCountText("Diamond");

        Audioplayer.instance.PlayMusic(0);

        FeedbackTextItem.enabled = false;
    }

    private void Update()
    {
        maxInventory.text = "Max: " + maxLimit.ToString();
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryCanvas();
        }

        if (inventoryCanvas.gameObject.activeSelf && !Input.GetKey(KeyCode.I) && Input.anyKeyDown)
        {
            inventoryCanvas.gameObject.SetActive(false);
        }

        if (EndScreen.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Audioplayer.instance.stopSFX();
            EndSound.SetActive(false);
            SceneManager.LoadScene("Credit");
        }
    }

    public void SaveInventory()
    {
        foreach (var item in inventory)
        {
            string prefsKey = "Inventory_" + item.Key;
            PlayerPrefs.SetInt(prefsKey, item.Value);
        }

        
        //PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        foreach (string itemName in new List<string> { "Bronze", "Silver", "Gold", "Diamond" })
        {
            string prefsKey = "Inventory_" + itemName;
            if (PlayerPrefs.HasKey(prefsKey))
            {
                inventory[itemName] = PlayerPrefs.GetInt(prefsKey);
            }
            else
            {
                inventory[itemName] = 0;
            }
        }
    }

    public void AddItem(string item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            int newTotal = totalItems + amount;
            if (newTotal <= maxLimit) 
            {
                if (inventory[item] + amount >= 0)
                {
                    totalItems = newTotal;
                    inventory[item] += amount;
                    StartCoroutine(DisplayLegacyTextAddItem("+" + amount + " " + item));
                    Debug.Log(item + " added to inventory: " + amount);
                    

                    if (isInventoryActive)
                    {
                        UpdateItemCountText(item);
                    }
                }
                else
                {
                    Debug.Log("Invalid operation: Cannot have negative items in inventory.");
                }
            }
            else
            {
                Debug.Log("Inventory full. Cannot add more items.");
            }
        }
        else
        {
            Debug.Log("Item " + item + " does not exist in inventory.");
        }
    }

    public void SellAllItems()
    {
        int totalValue = 0;
        List<string> itemsToSell = new List<string>(inventory.Keys);
        bool itemsSold = false;

        foreach (var item in itemsToSell)
        {
            int itemCount = inventory[item];
            if (itemCount > 0)
            {
                totalValue += itemCount * GetValueOfItem(item);
                inventory.Remove(item);
                itemsSold = true;
            }
        }

        if (itemsSold)
        {
            money += totalValue;
            UpdateMoneyText();
            StartCoroutine(DisplayLegacyTextSellItem("Terjual dengan harga: " + totalValue + "Gc"));
            Debug.Log("Sold all items for: " + totalValue + " money.");
        }
        else
        {
            Audioplayer.instance.PlaySFX(7);
            StartCoroutine(DisplayLegacyTextSellItem("Tidak ada Material untuk dijual"));
            Debug.Log("Tidak ada Material untuk dijual.");
        }
    }
    private int GetValueOfItem(string item)
    {
        switch (item)
        {
            case "Bronze":
                return 150;
            case "Silver":
                return 300;
            case "Gold":
                return 500;
            case "Diamond":
                return 1000;
            default:
                return 0;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();

        Debug.Log("Pemain memiliki sekarang: " + money + " uang.");
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
        UpdateMoneyText();
        Debug.Log("Pemain memiliki sekarang: " + money + " uang.");
    }

    public void SaveMoney(int amount)
    {
        PlayerPrefs.SetInt("money", amount);
    }

    public void LoadMoney()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            int amount = PlayerPrefs.GetInt("money");
            money = amount;
            UpdateMoneyText();
        }
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = money + "Gc";
    }
    public int GetPlayerMoney()
    {
        return money;
    }

    private void ToggleInventoryCanvas()
    {
        isInventoryActive = !isInventoryActive;
        inventoryCanvas.gameObject.SetActive(isInventoryActive);

        if (isInventoryActive)
        {
            UpdateItemCountText("Bronze");
            UpdateItemCountText("Silver");
            UpdateItemCountText("Gold");
            UpdateItemCountText("Diamond");
        }
    }

    private void UpdateItemCountText(string item)
    {
        switch (item)
        {
            case "Bronze":
                bronzeCountText.text = "Bronze x " + inventory[item];
                break;
            case "Silver":
                silverCountText.text = "Silver x " + inventory[item];
                break;
            case "Gold":
                goldCountText.text = "Gold x " + inventory[item];
                break;
            case "Diamond":
                diamondCountText.text = "Diamond x " + inventory[item];
                break;
            default:
                break;
        }
      
    }
    private IEnumerator DisplayLegacyTextAddItem(string displayText)
    {
        FeedbackTextItem.text = displayText; 
        FeedbackTextItem.enabled = true; 

        yield return new WaitForSeconds(1f); 

        FeedbackTextItem.enabled = false;
    }
    private IEnumerator DisplayLegacyTextSellItem(string displayText)
    {
        FeedbackTextSell.text = displayText; 
        FeedbackTextSell.enabled = true;

        yield return new WaitForSeconds(2f);

        FeedbackTextSell.enabled = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverScreen.SetActive (true);
        GameScreen.SetActive (false);
        Audioplayer.instance.StopMusic();
        Audioplayer.instance.PlaySFX(8);
    }

    public void End()
    {
        Audioplayer.instance.StopMusic();
        //EndSound.SetActive(true);
        
        EndScreen.SetActive (true);
        GameScreen.SetActive(false);
    }
}


