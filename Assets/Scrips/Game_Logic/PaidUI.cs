using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidUI : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Sell()
    {
        gameManager.clickAd.Play();
        gameManager.whichSellingIsOn = gameManager.currentTurn;
        gameManager.isSellingToPaid = true;
        gameManager.sellingToPaidUI.SetActive(false);
        gameManager.scrollAni[gameManager.currentTurn].scrollAni.SetInteger("state", 1);
    }

    public void Surrender()
    {
        gameManager.sellingToPaidUI.SetActive(false);
        gameManager.DestroyPlayer();
    }

    public void Paid()
    {
        Player player = gameManager.listPlayer[gameManager.currentTurn];
        Box box = gameManager.board.boxList[player.currentNumberDice];
        if (player.money >= box.moneyToBePaid)
        {
            player.money -= box.moneyToBePaid;
            box.whoBought.money += box.moneyToBePaid;
            player.infomationPlayerUI.UpdateText();

            gameManager.isSellingToPaid = false;
            gameManager.sellingToPaidUI.SetActive(false);
        }
    }
}
