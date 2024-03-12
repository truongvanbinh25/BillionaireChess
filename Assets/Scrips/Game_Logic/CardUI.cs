using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI infoCardText;
    public TextMeshProUGUI nameCardText;
    
    private void Start()
    {
        gameManager = GameManager.instance;
    }

    //Button Close
    public void Close()
    {
        gameManager.welcomeAni.SetInteger("state", 2);

        if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isFortune)
        {
            //Xử lý thẻ và thêm thẻ khí vận vào lại hàng đợi
            gameManager.CardHandlingFortune(gameManager.listPlayer[gameManager.currentTurn]);
        }
        if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isOpportunity)
        {
            //Xử lý thẻ và thêm thẻ cơ hội vào lại hàng đợi
            gameManager.CardHandlingOpportunity(gameManager.listPlayer[gameManager.currentTurn]);
        }

        gameObject.SetActive(false);

    }

    private void Update()
    {
        if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isFortune)
        {
            nameCardText.text = "Khí Vận";
            infoCardText.text = gameManager.fortune.descriptionFortune.ToString();
        }
        else if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isOpportunity)
        {
            nameCardText.text = "Cơ Hội";
            infoCardText.text = gameManager.opportunity.descriptionOpportunity.ToString();
        }
    }
}
