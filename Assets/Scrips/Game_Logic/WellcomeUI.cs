using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WellcomeUI : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI wellcomeText;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    
    //Button Yes
    public void DrawCard()
    {
        gameManager.areWorking = true;

        if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isFortune)
        {
            //Lấy thẻ khí vận ra
            gameManager.fortune = gameManager.board.fortunesQueue.Dequeue();
        }
        if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isOpportunity)
        {
            //Lấy thẻ cơ hội ra
            gameManager.opportunity = gameManager.board.opportunitiesQueue.Dequeue();
        }
        gameObject.SetActive(false);
        gameManager.infoCardUI.SetActive(true);
    }

    private void Update()
    {
		Debug.Log("Cin chao");
        if (gameManager.board.boxList[gameManager.listPlayer[gameManager.currentTurn].currentNumberDice].isFortune)
        {
            wellcomeText.text = "Chúc mừng bạn đã vào ô Khí vận";
        }
        else
        {
            wellcomeText.text = "Chúc mừng bạn đã vào ô cơ hội";
        }
    }
}
