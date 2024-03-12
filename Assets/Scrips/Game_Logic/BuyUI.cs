using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void Buy()
    {
        Box box = gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice];
        Player player = gameManager.listPlayer[gameManager.currentTurn];

        //Am thanh
        gameManager.clickAd.Play();

        // kích hoạt animation
        player.infomationPlayerUI.buyBoxText.gameObject.SetActive(true);
        player.infomationPlayerUI.buyBoxText.text = "-" + box.boxValue + "$";

        //Khởi tạo ô đất bên trong danh sách selling và gán box mua = box trong list selling, sau đó thêm vào danh sách
        InfomationBoxSellingUI ibsUI = Instantiate(gameManager.InfomationBoxSellingPrefab, gameManager.listScroll[gameManager.currentTurn].transform);
        ibsUI.box = box;
        gameManager.listScroll[gameManager.currentTurn].listSelling.Add(ibsUI);

        gameManager.listScroll[gameManager.currentTurn].numberOfBox++;
        player.listOfBoxPlayerHas.Add(box);
        gameManager.buyAni.SetInteger("state", 2);

        //Tính lại tiền phải trả khi sở hữu 2 bến xe trở lên
        if (box.isTheBusStation)
        {
            gameManager.AddBus(player, box);
        }
        //Cong ty
        else if (box.isTheCompany)
        {
            gameManager.AddCompany(player, box);
        }
        //O dat
        else
        {
            gameManager.AddBoxInMapping(player, box);
        }
        box.isBought = true;
        player.money -= box.boxValue;
        player.asset += box.boxValue;
        player.infomationPlayerUI.UpdateText();
        box.whoBought = player;
        box.UpdateBoxAfterBuy();
    }

    public void NoBuy()
    {
        gameManager.clickAd.Play();
        gameManager.buyAni.SetInteger("state", 2);
        gameManager.isDonedTurn = true;
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
        textMeshProUGUI.text = "Bạn có muốn ô " + gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].boxName + " với giá $" + gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].boxValue;
    }
}
