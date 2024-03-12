using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompanyPayUI : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI rollText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Player player = gameManager.listPlayer[gameManager.currentTurn];
        if(gameManager.is10x)
        {
            rollText.text = "Bạn đã vào ô Công ty \nTiền thuế = Số xúc sắc * " + 10;
        }
        else
        {
            rollText.text = "Bạn đã vào ô Công ty \nTiền thuế = Số xúc sắc * " + gameManager.board.boxList[player.currentNumberDice].moneyToBePaid;
        }
    }
        
}
