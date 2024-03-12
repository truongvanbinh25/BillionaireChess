using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyGround : MonoBehaviour
{
    private GameManager gameManager;
    public int numberDice1 = 0;
    public int numberDice2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.dice[0].isMove() && !gameManager.dice[1].isMove() && gameManager.currentNumberCompany == 0)
        {
            gameManager.currentNumberCompany += numberDice1 + numberDice2;
            if(gameManager.is10x)
            {
                //Trừ tiền và công tiền
                gameManager.listPlayer[gameManager.currentTurn].money -= 10 * gameManager.currentNumberCompany;
                gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].whoBought.money += 10 * gameManager.currentNumberCompany;
                gameManager.is10x = false;
            }
            else
            {
                //Trừ tiền và cộng tiền
                gameManager.listPlayer[gameManager.currentTurn].money -= gameManager.currentNumberCompany * gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].moneyToBePaid;
                gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].whoBought.money += gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].moneyToBePaid * gameManager.currentNumberCompany;
            }
            gameManager.isDonedTurn = true;
            gameManager.currentNumberCompany = 0;
            gameManager.listPlayer[gameManager.currentTurn].infomationPlayerUI.UpdateText();
            gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].whoBought.infomationPlayerUI.UpdateText();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "One":
                {
                    numberDice1 = 6;
                    break;
                }
            case "Two":
                {
                    numberDice1 = 5;
                    break;
                }
            case "Three":
                {
                    numberDice1 = 4;
                    break;
                }
            case "Four":
                {
                    numberDice1 = 3;
                    break;
                }
            case "Five":
                {
                    numberDice1 = 2;
                    break;
                }
            case "Six":
                {
                    numberDice1 = 1;
                    break;
                }

            case "One1":
                {
                    numberDice2 = 6;
                    break;
                }
            case "Two1":
                {
                    numberDice2 = 5;
                    break;
                }
            case "Three1":
                {
                    numberDice2 = 4;
                    break;
                }
            case "Four1":
                {
                    numberDice2 = 3;
                    break;
                }
            case "Five1":
                {
                    numberDice2 = 2;
                    break;
                }
            case "Six1":
                {
                    numberDice2 = 1;
                    break;
                }
            case "Untagged":
                {
                    numberDice1 = 0;
                    numberDice2 = 0;
                    break;
                }
        }

    }
}
