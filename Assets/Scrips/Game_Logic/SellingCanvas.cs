using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellingCanvas : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector] public Box infoBoxClick;
    public TextMeshProUGUI query;
    public Button sellingAllBTN;
    public Button sellingBoxBTN;
    public Button sellingHouseBTN;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }
    
    public void SellingAll()
    {
        gameManager.clickAd.Play();

        //Selling ALL House
        SellingAllHouse();

        //Selling house
        infoBoxClick.whoBought.money += infoBoxClick.houseLevel * infoBoxClick.houseUpgradeMoney;
        SellingBox();
    }

    public void SellingBox()
    {
        //Nhạc nền
        gameManager.clickAd.Play();

        gameObject.SetActive(false);

        infoBoxClick.whoBought.infomationPlayerUI.sellingBoxText.gameObject.SetActive(true);
        infoBoxClick.whoBought.infomationPlayerUI.sellingBoxText.text = "+" + infoBoxClick.boxValue / 2 + "$";
        infoBoxClick.whoBought.money += infoBoxClick.boxValue / 2;

        //Xóa box khỏi danh sách box của người chơi
        infoBoxClick.whoBought.listOfBoxPlayerHas.Remove(infoBoxClick.whoBought.listOfBoxPlayerHas.Find(x => x == infoBoxClick));
        gameManager.listPlayer[gameManager.whichSellingIsOn].asset -= infoBoxClick.boxValue;

        //Xóa box khỏi danh sách selling và sắp xếp lại
        InfomationBoxSellingUI a = gameManager.listScroll[gameManager.whichSellingIsOn].listSelling.FirstOrDefault(x => x.box == infoBoxClick);
        gameManager.listScroll[gameManager.whichSellingIsOn].listSelling.Remove(a);
        Destroy(a.gameObject);
        gameManager.listScroll[gameManager.whichSellingIsOn].numberOfBox--;

        infoBoxClick.whoBought.infomationPlayerUI.UpdateText();
        infoBoxClick.UpdateBoxAfterSell();
    }

    public void SellingHouse()
    {
        gameManager.clickAd.Play();
        gameManager.sellingHouseUI.SetActive(true);
        gameManager.sellingHouseUI.GetComponent<SellingHouseCanvas>().infoBoxClick = infoBoxClick;
    }

    public void SellingAllHouse()
    {
        gameManager.listPlayer[gameManager.whichSellingIsOn].asset -= infoBoxClick.houseUpgradeMoney * infoBoxClick.houseLevel;
        infoBoxClick.whoBought.money += infoBoxClick.houseUpgradeMoney * infoBoxClick.houseLevel;

        for (int i = 0; i < infoBoxClick.houseLevel; i++)
        {
            Destroy(infoBoxClick.listHouse[infoBoxClick.listHouse.Count - 1].gameObject);
            infoBoxClick.listHouse.RemoveAt(infoBoxClick.listHouse.Count - 1);
        }

        infoBoxClick.houseLevel = 0;
    }

    public void UpdateSelling()
    {
        query.text = "Bạn có muốn bán ô " + infoBoxClick.boxName + " không?";

        //Nếu ô đất không có nhà thì làm mờ button bán nhà
        if (infoBoxClick.houseLevel > 0)
        {
            sellingHouseBTN.interactable = true;
            sellingBoxBTN.interactable = false;
        }
        else
        {
            sellingHouseBTN.interactable = false;
            sellingBoxBTN.interactable = true;
        }
    }
}
