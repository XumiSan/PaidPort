using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private TextMeshProUGUI debtText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private BackgroundColor bgColorChanger;
    [SerializeField]
    private Color brownColor = new Color(0.6f, 0.3f, 0.1f, 1f);
    [SerializeField]
    private Color darkColor = new Color(0.6f, 0.3f, 0.1f, 1f);

    private int currentHour = 0;
    private int currentMinute = 0;
    private int currentDay = 1;
    private string[] dayNames;
    private string[] dailyDebts;
    private float updateInterval = 0.5f;
    private float timeSinceLastUpdate = 0f;

    [SerializeField]
    private Text FeedbackTextDay;

    //ChangeDayControl
    [SerializeField]
    private SpriteRenderer groundBg;
    [SerializeField]
    private SpriteRenderer skyBg;
    [SerializeField]
    private SpriteRenderer layer3;
    [SerializeField]
    private Sprite dayGround;
    [SerializeField]
    private Sprite daySky;
    [SerializeField]
    private Sprite cloudlayer3;
    [SerializeField]
    private Sprite nightGround;
    [SerializeField]
    private Sprite nightSky;
    [SerializeField]
    private Sprite starlayer3;


    [SerializeField]
    private GameObject NighLight;

    //Fuel Building
    [SerializeField]
    private SpriteRenderer buildingFuelSpriteRenderer;
    [SerializeField]
    private Sprite fueldaySprite;
    [SerializeField]
    private Sprite fuelnightSprite;
    [SerializeField]
    private GameObject fuelGlow;

    //Garage Building
    [SerializeField]
    private SpriteRenderer buildingGarageSpriteRenderer;
    [SerializeField]
    private Sprite garagedaySprite;
    [SerializeField]
    private Sprite garagenightSprite;
    [SerializeField]
    private GameObject garageGlow;

    //Cargo Building
    [SerializeField]
    private SpriteRenderer buildingCargoSpriteRenderer;
    [SerializeField]
    private Sprite cargodaySprite;
    [SerializeField]
    private Sprite cargonightSprite;
    [SerializeField]
    private GameObject cargoGlow;

    void Start()
    {

        dayNames = new string[]
        {
            "Senin",
            "Selasa",
            "Rabu",
            "Kamis",
            "Jumat",
            "Sabtu",
            "Minggu"
        };

        dailyDebts = new string[]
        {
            "500",
            "750",
            "1000",
            "1300",
            "1600",
            "1850",
            "2500"
        };

        UpdateDayAndDebtText();
        LoadSavedTime();
    }

    void Update()
    {

        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            
            UpdateTime();
            timeSinceLastUpdate = 0f;
        }
    }

   public void SaveTime()
    {
        PlayerPrefs.SetInt("SavedHour", currentHour);
        PlayerPrefs.SetInt("SavedMinute", currentMinute);
        PlayerPrefs.SetInt("SavedDay", currentDay);
        PlayerPrefs.Save();
    }
    void UpdateTime()
    {

        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
            {
                currentHour = 0;
                currentDay++;


                if (currentDay > 3)
                {
                    currentDay = 1;
                    Time.timeScale = 0;
                    GameManager.Instance.End();
                    Debug.Log("Siklus hari telah selesai.");
                    enabled = false;
                    return;
                }


                HandleDayChange(currentDay);
            }
        }


        timeText.text = currentHour.ToString("00") + ":" + currentMinute.ToString("00");
        if (currentHour == 23 && currentMinute == 59)
        {
            SubtractDebtFromPlayer();
        }
        if ((currentHour >= 18 && currentHour <= 23) || (currentHour >= 0 && currentHour <= 6))
        {
            // Kode untuk mode malam
            groundBg.sprite = nightGround;
            skyBg.sprite = nightSky;
            layer3.sprite = starlayer3;
            NighLight.SetActive(true);

            bgColorChanger.ChangeBackgroundColor(darkColor);

            //fuel building
            buildingFuelSpriteRenderer.sprite = fuelnightSprite;
            fuelGlow.SetActive(true);
            //garage building
            buildingGarageSpriteRenderer.sprite = garagenightSprite;
            garageGlow.SetActive(true);
            //cargo building
            buildingCargoSpriteRenderer.sprite = cargonightSprite;
            cargoGlow.SetActive(true);
        }
        else
        {
            // Kode untuk mode siang
            groundBg.sprite = dayGround;
            skyBg.sprite = daySky;
            layer3.sprite = cloudlayer3;
            NighLight.SetActive(false);

            bgColorChanger.ChangeBackgroundColor(brownColor);

            //fuel building
            buildingFuelSpriteRenderer.sprite = fueldaySprite;
            fuelGlow.SetActive(false);
            //garage building
            buildingGarageSpriteRenderer.sprite = garagedaySprite;
            garageGlow.SetActive(false);
            //cargo building
            buildingCargoSpriteRenderer.sprite = cargodaySprite;
            cargoGlow.SetActive(false);
        }
    }

    void HandleDayChange(int day)
    {

        Debug.Log("Hari berubah menjadi: " + dayNames[day - 1]);




        UpdateDayAndDebtText();
    }

    void UpdateDayAndDebtText()
    {

        dayText.text = "Hari: " + dayNames[currentDay - 1];
        debtText.text = "Hutang: " + dailyDebts[currentDay - 1] + "Gc";
    }
    void SubtractDebtFromPlayer()
    {
        string debtString = dailyDebts[currentDay - 1];
        int currentDebt = 0;

        if (int.TryParse(debtString, out currentDebt))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager != null)
            {
                int playerMoney = gameManager.GetPlayerMoney();

                if (playerMoney >= currentDebt)
                {

                    gameManager.SubtractMoney(currentDebt);
                    StartCoroutine(DisplayLegacyTextDay("Berhasil Membayar Hutang"));
                    Debug.Log("Pengurangan hutang sebesar " + currentDebt + "Gc dari pemain pada jam 23:59.");
                }
                else
                {
                    StartCoroutine(DisplayLegacyTextDay("Kamu gagal Membayar Hutang T_T"));
                    GameManager.Instance.GameOver();
                    Debug.Log("Uang tidak cukup untuk membayar hutang hari ini.");
                    enabled = false;
                    return;
                }
            }
        }
    }
    private IEnumerator DisplayLegacyTextDay(string displayText)
    {
        FeedbackTextDay.text = displayText;
        FeedbackTextDay.enabled = true;

        yield return new WaitForSeconds(2f);

        FeedbackTextDay.enabled = false;
    }

    void LoadSavedTime()
    {
        if (PlayerPrefs.HasKey("SavedHour") && PlayerPrefs.HasKey("SavedMinute") && PlayerPrefs.HasKey("SavedDay"))
        {
            currentHour = PlayerPrefs.GetInt("SavedHour");
            currentMinute = PlayerPrefs.GetInt("SavedMinute");
            currentDay = PlayerPrefs.GetInt("SavedDay");
        }
    }
}

