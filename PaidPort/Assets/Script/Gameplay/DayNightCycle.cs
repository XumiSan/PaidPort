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
    private int currentHour = 0;
    private int currentMinute = 0;
    private int currentDay = 1;
    private string[] dayNames;
    private string[] dailyDebts;
    private float updateInterval = -5f;
    private float timeSinceLastUpdate = 0f;
    [SerializeField]
    private Text FeedbackTextDay;
    [SerializeField]
    private GameObject Day;
    [SerializeField]
    private GameObject Night;
    [SerializeField]
    private GameObject NighLight;
    [SerializeField]
    private GameObject FuelDay;
    [SerializeField]
    private GameObject FuelNight;

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


                if (currentDay > 7)
                {
                    currentDay = 1;
                    Time.timeScale = 0;
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
        timeText.text = currentHour.ToString("00") + ":" + currentMinute.ToString("00");
        if (currentHour == 7 && currentMinute == 00)
        {
            Night.SetActive(false);
            NighLight.SetActive(false);
            Day.SetActive(true);
            
            FuelNight.SetActive(false);
            FuelDay.SetActive(true);
        }
        timeText.text = currentHour.ToString("00") + ":" + currentMinute.ToString("00");
        if (currentHour == 18 && currentMinute == 00)
        {
            Day.SetActive(false);
            NighLight.SetActive(true);
            Night.SetActive(true);
            
            FuelDay.SetActive(false);
            FuelNight.SetActive(true);
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
                    StartCoroutine(DisplayLegacyTextDay("Berhasil Membayar Hutang" ));
                    Debug.Log("Pengurangan hutang sebesar " + currentDebt + "Gc dari pemain pada jam 23:59.");
                }
                else
                {
                    StartCoroutine(DisplayLegacyTextDay("Kamu gagal Membayar Hutang T_T" ));
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
}


