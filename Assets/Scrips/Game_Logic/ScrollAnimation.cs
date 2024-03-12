using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimation : MonoBehaviour
{
    private GameManager gameManager;

    public Animator scrollAni;
     
    public Button exitBTN;

    public int playerTurn;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        scrollAni.SetInteger("state", 0);
    }

    public void EndOfFrameSelling()
    {
        gameManager.scrollAni[playerTurn].scrollAni.SetInteger("state", 3);
    }

    public void ExitSellingBTN()
    {
        //Đang mở scroll để bán đất
        if(gameManager.isSellingToBuy == true && gameManager.areWorking == true)
        {
            gameManager.isSellingToBuy = false;
            gameManager.buyUI.SetActive(true);
            gameManager.buyAni.SetInteger("state", 1);
            gameManager.scrollAni[gameManager.currentTurn].scrollAni.SetInteger("state", 2);
            gameManager.isSelling = false;
            if (gameManager.listPlayer[gameManager.currentTurn].money >= gameManager.currentBox[gameManager.currentTurn].boxValue)
            {
                gameManager.buyBTN.interactable = true;
            }
            if (gameManager.sellingUI.activeSelf == true)
            {
                gameManager.sellingUI.SetActive(false);
            }
            return;
        }

        //Đang mở scroll để bán trả nợ
        if (gameManager.isSellingToPaid == true)
        {
            gameManager.sellingToPaidUI.SetActive(true);
            gameManager.scrollAni[playerTurn].scrollAni.SetInteger("state", 2);
            return;
        }
        gameManager.scrollAni[playerTurn].scrollAni.SetInteger("state", 2);
        gameManager.isSelling = false;
        if (gameManager.sellingUI.activeSelf == true)
        {
            gameManager.sellingUI.SetActive(false);
        }
    }

}
