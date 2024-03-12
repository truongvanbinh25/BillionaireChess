using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellingHouseCanvas : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI numberHouseText;
    public Box infoBoxClick;
    public Button plusBTN;
    public Button minusBTN;
    public Button confirmBTN;
    public Button cancelBTN;
    public int houseTemp = 0;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Plus()
    {
        int numberOfHouseCurrent = infoBoxClick.houseLevel;
        houseTemp++;
        if(houseTemp > numberOfHouseCurrent)
        {
            houseTemp = numberOfHouseCurrent;
        }
        numberHouseText.text = houseTemp.ToString();
    }
    public void Minus()
    {
        int numberOfHouseCurrent = infoBoxClick.houseLevel;
        houseTemp--;
        if (houseTemp < 0)
        {
            houseTemp = 0;
        }
        numberHouseText.text = houseTemp.ToString();
    }

    public void Selling()
    {
        //Kich hoat animation
        gameManager.listPlayer[gameManager.whichSellingIsOn].infomationPlayerUI.sellingHouseText.gameObject.SetActive(true);
        gameManager.listPlayer[gameManager.whichSellingIsOn].infomationPlayerUI.sellingHouseText.text = "+" + infoBoxClick.houseUpgradeMoney * houseTemp + "$";

        gameManager.listPlayer[gameManager.whichSellingIsOn].asset -= infoBoxClick.houseUpgradeMoney * houseTemp;
        infoBoxClick.whoBought.money += infoBoxClick.houseUpgradeMoney * houseTemp;
        infoBoxClick.houseLevel -= houseTemp;

        for (int i = 0; i < houseTemp; i++)
        {
            Destroy(infoBoxClick.listHouse[infoBoxClick.listHouse.Count - 1].gameObject);
            infoBoxClick.listHouse.RemoveAt(infoBoxClick.listHouse.Count - 1);
        }

        infoBoxClick.whoBought.infomationPlayerUI.UpdateText();
        Exit();
    }

    public void Exit()
    {
        gameManager.isDonedTurn = true;
        houseTemp = 0;
        numberHouseText.text = "0";
        this.gameObject.SetActive(false);
        gameManager.sellingCanvas.UpdateSelling();
    }

}
