using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Box : MonoBehaviour
{
    [Header("Kind Of Box")]   
    public bool isTheland = true;
    public bool isTheBusStation = false;
    public bool isTheCompany = false;
    public bool isOpportunity = false;
    public bool isFortune = false;
    public bool isTax = false;
    public bool isTax100dollar = false;
    public bool isJail = false;
    public bool isNothing = false;

    [Header("Infomation")]
    public int boxSerialNumber = 0;
    public string boxName;
    public int boxValue;
    public int moneyToBePaid;
    public int houseUpgradeMoney;
    public int houseLevel = 0;
    public int maxHouseLevel = 5;
    public bool isBought;
    public Player whoBought;
    public float exchangeRate = 0.08f;
    public Material colorBoxM;
    public GameObject colorBoxGO;

    //UI
    [Header("UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI boxValueText;
    public TextMeshProUGUI moneyToBePaidText;
    public TextMeshProUGUI houseUpgradeMoneyText;

    [Header("Player")]
    public Transform playerPositionParent;
    [HideInInspector]public Transform[] playerPosition;
    public Renderer colorPlayer;

    [Header("House")]
    public List<House> listHouse;
    public Transform housePositionParent;
    [HideInInspector]public Transform[] housePosition;
    public Material materialInvisible;

    [Header("Effect")]
    public ParticleSystem propmtParitcle;
    private void Awake()
    {
        playerPosition = InstancePosition(playerPositionParent);
        if(isTheBusStation || isTheCompany || isFortune || isOpportunity || isNothing || isTax || isJail || isTax100dollar)
        {
            return;
        }
        housePosition = InstancePosition(housePositionParent);
    }

    private void Start()
    {
        listHouse = new List<House>();
        propmtParitcle.Stop();
        if (isTax)
        {
            moneyToBePaidText.enabled = true;
            moneyToBePaidText.text = "-" + moneyToBePaid.ToString() + "$";
        }

        //Nếu không phải là ô đất thì không cần các UI
        if (!isTheland)
            return;

        //Thiết lập các thuộc tính 
        nameText.text = boxName;
        boxValueText.text = "Giá: $" + boxValue.ToString();

        moneyToBePaidText.enabled = false;
        houseUpgradeMoneyText.enabled = false;

        colorBoxGO.GetComponent<Renderer>().material = colorBoxM;

        //Vì bến xe và công ty có cách tính tiền khác nên sẽ không sử dụng cách tính này
        if(!isTheBusStation && !isTheCompany)
        {
            moneyToBePaid = (int)(boxValue * exchangeRate);
        }
    }

    public void UpdateBoxAfterBuy()
    {
        boxValueText.enabled = false;
        if (whoBought != null)
        {
            colorPlayer.material = whoBought.GetComponent<Renderer>().material;
        }
        else
        {
            colorPlayer.material = materialInvisible;
        }

        if (!isTheCompany && !isTheBusStation)
        {
            houseUpgradeMoneyText.enabled = true;
        }
        moneyToBePaidText.enabled = true;
        moneyToBePaidText.text = "Tiền Thuế: " + moneyToBePaid.ToString() + "$";
        houseUpgradeMoneyText.text = "Xây Nhà: " + houseUpgradeMoney.ToString() + "$";
    }

    public void UpdateBoxAfterSell()
    {
        isBought = false;

        //Bus
        if (isTheBusStation)
        {
            whoBought.listBus.Remove(this);
            switch (whoBought.listBus.Count)
            {
                case 1:
                    {
                        whoBought.listBus[0].moneyToBePaid = 25;
                        break;
                    }
                case 2:
                    {
                        whoBought.listBus[0].moneyToBePaid = 50;
                        whoBought.listBus[1].moneyToBePaid = 50;
                        break;
                    }
                case 3:
                    {
                        whoBought.listBus[0].moneyToBePaid = 100;
                        whoBought.listBus[1].moneyToBePaid = 100;
                        whoBought.listBus[2].moneyToBePaid = 100;
                        break;
                    }
            }
        }
        //Company
        else if (isTheCompany)
        {
            whoBought.listCompany.Remove(this);
            switch (whoBought.listCompany.Count)
            {
                case 1:
                    {
                        whoBought.listCompany[0].moneyToBePaid = 4;
                        break;
                    }
            }
        }
        //Box
        else
        {
            foreach (var entry in whoBought.colorBoxMapping)
            {
                int boxValue = entry.Key;
                List<int> boxIndexes = entry.Value;

                if (this.boxValue >= boxValue && this.boxValue <= boxValue + 20)
                {
                    if(this.boxValue == 60 || this.boxValue >= 350)
                    {
                        if(boxIndexes.Count == 2)
                        {
                            boxIndexes.Remove(boxSerialNumber);
                            GameManager.instance.board.boxList[boxIndexes[0]].moneyToBePaid /= 2;
                            GameManager.instance.board.boxList[boxIndexes[0]].UpdateBoxAfterBuy();
                        }
                    }
                   else
                   {
                        if (boxIndexes.Count == 3)
                        {
                            boxIndexes.Remove(boxSerialNumber);
                            foreach (int item in boxIndexes)
                            {
                                GameManager.instance.board.boxList[item].moneyToBePaid /= 2;
                                GameManager.instance.board.boxList[item].UpdateBoxAfterBuy();
                            }
                        }
                   }
                }
            }
        }
        whoBought = null;
        moneyToBePaidText.enabled = false;
        houseUpgradeMoneyText.enabled = false;
        boxValueText.enabled = true;
        colorPlayer.material = materialInvisible;
    }

    //Khoi tao dia chi cua nguoi choi
    public Transform[] InstancePosition(Transform parent)
    {
        Transform[] listPosition = new Transform[parent.transform.childCount];
        for (int i = 0; i < listPosition.Length; i++)
        {
            listPosition[i] = parent.transform.GetChild(i);
        }
        return listPosition;
    }
}


//private void Update()
//{
//    if (isTax)
//    {
//        moneyToBePaidText.text = "-" + moneyToBePaid.ToString() + "$";
//    }

//    if (!isTheland)
//        return;

//    nameText.text = boxName;

//    if (!isBought)
//    {
//        boxValueText.text = "Giá: $" + boxValue.ToString();
//    }
//    else
//    {
//        boxValueText.enabled = false;
//        if (whoBought != null)
//        {
//            colorPlayer.material = whoBought.GetComponent<Renderer>().material;
//        }
//        else
//        {
//            colorPlayer.material = materialInvisible;
//        }

//        if (!isTheCompany && !isTheBusStation)
//        {
//            houseUpgradeMoneyText.enabled = true;
//        }
//        moneyToBePaidText.enabled = true;
//    }

//    moneyToBePaidText.text = "Tiền Thuế: " + moneyToBePaid.ToString() + "$";
//    houseUpgradeMoneyText.text = "Xây Nhà: " + houseUpgradeMoney.ToString() + "$";
//}
