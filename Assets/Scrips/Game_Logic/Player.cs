using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public static Vector3 offset = new Vector3(0, 1.5f, -2);
    public string namePlayer = "None";
    public int money = 200;
    public Material playerColor;
    public int currentNumberDice = 0;
    public int asset = 0;
    public int numberOfExistingHouse = 0;
    public int numberOfExistingVilla = 0;
    public int numberOfExistingBusStation = 0;
    public bool receiveMoney = false;
    public bool isOnJail = false;
    public int countDownJail = 0;
    public List<Box> listOfBoxPlayerHas;
    public List<Box> listBus;
    public List<Box> listCompany;

    [Header("Color Box Mapping")]
    public Dictionary<int, List<int>> colorBoxMapping = new Dictionary<int, List<int>>()
                                                        {
                                                            {60, new List<int>()},
                                                            {100, new List<int>()},
                                                            {140, new List<int> ()},
                                                            {180, new List<int> ()},
                                                            {200, new List<int> ()},
                                                            {240, new List<int> ()},
                                                            {300, new List<int> ()},
                                                            {350, new List<int> ()}
                                                        };

    public InfomationPlayerUI infomationPlayerUI;

    //Save
    public List<int> save_ListOfBoxPlayerHas;
    public List<int> save_ListBus;
    public List<int> save_ListCompany;

    public void Start()
    {
        listOfBoxPlayerHas = new List<Box>();
        listBus = new List<Box>();
        listCompany = new List<Box>();
        GetComponent<Renderer>().material = playerColor;
    }
}