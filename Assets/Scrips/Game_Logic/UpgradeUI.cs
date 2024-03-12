using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void Upgrade()
    {
        //Am thanh
        gameManager.clickAd.Play();
        
        Box box = gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice];
        Player player = gameManager.listPlayer[gameManager.currentTurn];

        //Kich hoat animation
        player.infomationPlayerUI.upgradeText.gameObject.SetActive(true);
        player.infomationPlayerUI.upgradeText.text = "-" + box.houseUpgradeMoney + "$";

        //Tạo nhà
        House house = Instantiate(gameManager.housePrefab, gameManager.board.boxList[player.currentNumberDice].housePosition[gameManager.board.boxList[player.currentNumberDice].houseLevel].position, transform.rotation).GetComponent<House>();
        house.RoofColor.material = box.colorBoxM;
        box.listHouse.Add(house);

        box.houseLevel++;
        player.numberOfExistingHouse++;
        if(box.houseLevel == box.maxHouseLevel)
        {
            player.numberOfExistingHouse -= 4;
            player.numberOfExistingVilla++;
        }

        //Tính lại tiền phải trả
        box.moneyToBePaid = (int)(box.moneyToBePaid * 2.6f);

        //Trừ tiền
        player.money -= box.houseUpgradeMoney;
        player.asset += box.houseUpgradeMoney;
        player.infomationPlayerUI.UpdateText();

        //Cập nhật text
        gameManager.upgradeAni.SetInteger("state", 2);
        box.UpdateBoxAfterBuy();
    }

    public void NoUpgrade()
    {
        gameManager.clickAd.Play();
        gameManager.upgradeAni.SetInteger("state", 2);
    }

    public void SellingToBuy()
    {
        gameManager.clickAd.Play();
        gameManager.whichSellingIsOn = gameManager.currentTurn;
        gameManager.isSellingToBuy = true;
        gameManager.buyUI.SetActive(false);
        gameManager.scrollAni[gameManager.currentTurn].scrollAni.SetInteger("state", 1);
    }

    private void Update()
    {
        textMeshProUGUI.text = "Bạn có muốn xây nhà cấp " + (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].houseLevel + 1) +  " ô " + gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].boxName + " với giá $" + gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].houseUpgradeMoney;
    }
}
