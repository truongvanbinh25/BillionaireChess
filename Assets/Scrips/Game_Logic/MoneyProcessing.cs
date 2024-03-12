using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyProcessing : MonoBehaviour
{
    //Luồng hoạt động
    //-Khi người chơi vào ô đất đã bị mua bời người chơi khác thì sẽ thực hiện việc trừ tiền và cộng tiền
    //-Các textmeshpro của bonus và minus chỉ cần SetActive của gameobject là true thì sẽ tự động thực hiện animation (vì đã có sẵn animator trong mỗi object)
    //-Đầu tiên sẽ thực hiện việc trừ tiền người vào ô đất(ở đầu của frame thứ nhất của animation). Cuối frame sẽ thực hiện SetActive của Minus là false, sau đó SetActive Bonus
    //của người nhận là true 
    //-Đầu frame bonus sẽ không làm gì cả, cuối frame sẽ setactive thành false

    private GameManager gameManager;
    private int currentTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void MoneyMinus()
    {
        currentTurn = gameManager.currentTurn;
        gameManager.MoneyMinus(gameManager.listPlayer[currentTurn], gameManager.currentBox[currentTurn], gameManager.currentBox[currentTurn].moneyToBePaid);
    }

    public void EndOfFrameMoneyMinus()
    {
        gameManager.currentBox[currentTurn].whoBought.infomationPlayerUI.moneyBonusText.gameObject.SetActive(true);
        gameManager.MoneyBonus(gameManager.listPlayer[currentTurn], gameManager.currentBox[currentTurn], gameManager.currentBox[currentTurn].moneyToBePaid);
    }

    public void EndOfFrameMoneyPlus()
    {
        gameManager.currentBox[currentTurn].whoBought.infomationPlayerUI.moneyBonusText.gameObject.SetActive(false);
    }

    public void EndOfFrameBuyBox()
    {
        gameManager.listPlayer[currentTurn].infomationPlayerUI.buyBoxText.gameObject.SetActive(false);
    }

    public void EndOfFrameUpgradeHouse()
    {
        gameManager.listPlayer[currentTurn].infomationPlayerUI.upgradeText.gameObject.SetActive(false);
    }

    public void EndOfFrameSellingBox()
    {
        gameManager.listPlayer[gameManager.whichSellingIsOn].infomationPlayerUI.sellingBoxText.gameObject.SetActive(false);
    }

    public void EndOfFrameSellingHouse()
    {
        gameManager.listPlayer[gameManager.whichSellingIsOn].infomationPlayerUI.sellingHouseText.gameObject.SetActive(false);
    }
}
