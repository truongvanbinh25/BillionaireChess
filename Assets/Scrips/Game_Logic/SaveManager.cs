using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Rendering;
using TMPro;

public class SaveBox
{
    public int boxSerialNumber = -1;
    public int houseLevel = 0;
    public int moneyToBePaid = 0;
}

[Serializable]
public class PlayerDataToSave
{
    public string namePlayer = "None";
    public int money = 200;
    public int currentNumberDice = 0;
    public string nameMaterial = "";
    public int asset = 0;
    public int numberOfExistingHouse = 0;
    public int numberOfExistingVilla = 0;
    public int numberOfExistingBusStation = 0;
    public bool receiveMoney = false;
    public bool isOnJail = false;
    public int countDownJail = 0;

    //Save
    public List<SaveBox> save_ListOfBoxPlayerHas = new List<SaveBox>();
    public List<int> save_ListBus = new List<int>();
    public List<int> save_ListCompany = new List<int>();

    public PlayerDataToSave() { }

    public PlayerDataToSave(Player player)
    {
        namePlayer = player.namePlayer;
        money = player.money;
        currentNumberDice += player.currentNumberDice;
        asset = player.asset;
        numberOfExistingHouse = player.numberOfExistingHouse;
        numberOfExistingVilla = player.numberOfExistingVilla;
        numberOfExistingBusStation = player.numberOfExistingBusStation;
        receiveMoney = player.receiveMoney;
        isOnJail = player.isOnJail;
        countDownJail = player.countDownJail;

        //Material
        nameMaterial = player.playerColor.name;

        //List Of Box Playe rHas
        for (int i = 0; i < player.listOfBoxPlayerHas.Count; i++)
        {
            SaveBox sb = new SaveBox();
            sb.boxSerialNumber = player.listOfBoxPlayerHas[i].boxSerialNumber;
            sb.houseLevel = player.listOfBoxPlayerHas[i].houseLevel;
            sb.moneyToBePaid = player.listOfBoxPlayerHas[i].moneyToBePaid;

            save_ListOfBoxPlayerHas.Add(sb);
        }

        //List Bus
        for (int i = 0; i < player.listBus.Count; i++)
        {
            save_ListBus.Add(player.listBus[i].boxSerialNumber);
        }

        //List Company
        for (int i = 0; i < player.listCompany.Count; i++)
        {
            save_ListCompany.Add(player.listCompany[i].boxSerialNumber);
        }
    }
}

[XmlRoot("PlayerDataList")]
public class SaveGameDataList
{
    [XmlElement("Players")]
    public List<PlayerDataToSave> playerDataList = new List<PlayerDataToSave>();

    public int currentTurn = 0;
    public bool areWorking = false;
    public bool isDonedTurn = false;
    public int isCoutinueGame = 0;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private GameManager gameManager;
    private string xmlPath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        xmlPath = Path.Combine(Application.persistentDataPath, "data_player_save.xml");
        //Check file
        if (!File.Exists(xmlPath))
        {
            //If don't exist
            SaveDefaultData();
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    
    public void SaveDefaultData()
    {
        try
        {
            SaveGameDataList defaultSaveDataList = new SaveGameDataList();

            //Chuyển đổi đối tượng thành dữ liệu XML
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameDataList));

            //Ghi dữ liệu
            using (StreamWriter streamWriter = new StreamWriter(xmlPath))
            {
                serializer.Serialize(streamWriter, defaultSaveDataList);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void Save()
    {
        try
        {
            SaveGameDataList saveDataList = new SaveGameDataList();

            for (int i = 0; i < gameManager.listPlayer.Count; i++)
            {
                PlayerDataToSave playerDataToSave = new PlayerDataToSave(gameManager.listPlayer[i]);
                saveDataList.playerDataList.Add(playerDataToSave);
            }

            saveDataList.currentTurn = gameManager.currentTurn;
            saveDataList.areWorking = gameManager.areWorking;
            saveDataList.isDonedTurn = gameManager.isDonedTurn;

            //Chuyển đổi đối tượng thành dữ liệu XML
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameDataList));

            //Ghi du lieu
            using (StreamWriter streamWriter = new StreamWriter(xmlPath))
            {
                serializer.Serialize(streamWriter, saveDataList);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

    }

    public SaveGameDataList Load()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameDataList));
            SaveGameDataList listPlayer = new SaveGameDataList();

            using (StreamReader streamReader = new StreamReader(xmlPath))
            {
                listPlayer = (SaveGameDataList)serializer.Deserialize(streamReader);
            }

            return listPlayer;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return null;
        }
    }
}
