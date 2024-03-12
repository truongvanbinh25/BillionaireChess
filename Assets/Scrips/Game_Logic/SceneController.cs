using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;

public class SceneController : MonoBehaviour
{
    public static SceneController instante;

    private SaveManager saveManager;

    [Header("Data")]
    public MenuData menuData;

    [Header("Button")]
    public Button playButton;
    public Button playCountiueBTN;
    public Button nextButton;
    public Button backMenuBTN;
    public Button startGameBTN;

    [Header("Canvas")]
    public GameObject settingNumberPlayerCanvas;
    public GameObject playerSettingCanvasPrefab;
    public PlayerSettingCanvas[] listPlayerSetting;

    [Header("Opttion")]
    private int numberOfPlayer = 0;
    public int currentSettingCanvas = 0;
    public GameObject pointInitPlayerSetting;
    public TextMeshProUGUI numberOfPlayerText;
    public Toggle[] toggleMoney;
    public bool[] hasChosen;

    private void Awake()
    {
        instante = this;
    }

    private void Start()
    {
        saveManager = SaveManager.instance;
        SaveGameDataList listPlayer = saveManager.Load();
        if (listPlayer.playerDataList.Count > 0)
        {
            playCountiueBTN.interactable = true;
        }
        else
        {
            playCountiueBTN.interactable = false;
        }
    }

    private void Update()
    {
        if (numberOfPlayer == 0)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }
        if (startGameBTN != null)
        {
            if (currentSettingCanvas < numberOfPlayer - 1)
            {
                startGameBTN.interactable = false;
            }
            else
            {
                startGameBTN.interactable = true;
            }
        }

    }

    public void NextToSettingNumberPlayer()
    {
        playButton.gameObject.SetActive(false);
        playCountiueBTN.gameObject.SetActive(false);
        settingNumberPlayerCanvas.SetActive(true);
    }

    public void BackToSettingNumberPlayer()
    {
        settingNumberPlayerCanvas.SetActive(true);
        startGameBTN.gameObject.SetActive(false);
        backMenuBTN.gameObject.SetActive(false);
        for (int i = 0; i < numberOfPlayer; i++)
        {
            Destroy(listPlayerSetting[i].gameObject);
        }
    }

    public void NextToPlayerSetting()
    {
        settingNumberPlayerCanvas.SetActive(false);
        hasChosen = new bool[numberOfPlayer];
        listPlayerSetting = new PlayerSettingCanvas[numberOfPlayer];
        for (int i = 0; i < numberOfPlayer; i++)
        {
            listPlayerSetting[i] = Instantiate(playerSettingCanvasPrefab, pointInitPlayerSetting.transform).GetComponent<PlayerSettingCanvas>();
            listPlayerSetting[i].title.text = "Người chơi " + (i + 1);
        }
        startGameBTN.gameObject.SetActive(true);
        backMenuBTN.gameObject.SetActive(true);
        listPlayerSetting[0].gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        settingNumberPlayerCanvas.SetActive(false);
        playButton.gameObject.SetActive(true);
        playCountiueBTN.gameObject.SetActive(true);
    }

    public void NextCanvasInSettingCanvas2()
    {
        listPlayerSetting[currentSettingCanvas].gameObject.SetActive(false);
        currentSettingCanvas++;
        if(currentSettingCanvas >= numberOfPlayer)
        {
            currentSettingCanvas = numberOfPlayer - 1;
        }
        listPlayerSetting[currentSettingCanvas].gameObject.SetActive(true);
    }
    public void BackSettingInSettingCanvas2()
    {
        listPlayerSetting[currentSettingCanvas].gameObject.SetActive(false);
        currentSettingCanvas--;
        if (currentSettingCanvas <= 0)
        {
            currentSettingCanvas = 0;
        }
        listPlayerSetting[currentSettingCanvas].gameObject.SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("isCountinueGame", 0);
        PlayerPrefs.SetInt("numberOfPlayer", numberOfPlayer);
        for (int i = 0; i < numberOfPlayer; i++)
        {
            menuData.listDataPlayer.Add(listPlayerSetting[i]);
        }
        if (toggleMoney[0].isOn)
        {
            PlayerPrefs.SetInt("moneyStart", 200);
        }
        else if (toggleMoney[1].isOn)
        {
            PlayerPrefs.SetInt("moneyStart", 500);
        }
        else
        {
            PlayerPrefs.SetInt("moneyStart", 1000);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CountinueGame()
    {
        PlayerPrefs.SetInt("isCountinueGame", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Plus Minus of canvas choose number of player
    public void PlusPlayer()
    {
        numberOfPlayer++;
        if (numberOfPlayer > 4)
        {
            numberOfPlayer = 4;
        }
        numberOfPlayerText.text = numberOfPlayer.ToString();
    }
    public void MinusPlayer()
    {
        numberOfPlayer--;
        if(numberOfPlayer < 0) 
        {
            numberOfPlayer = 0;
        }
        numberOfPlayerText.text = numberOfPlayer.ToString();
    }
}
