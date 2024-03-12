using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCanvas : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void ExitBuying()
    {
        gameManager.isDonedTurn = true;
        gameManager.buyUI.SetActive(false);
        gameManager.buyAni.SetInteger("state", 3);
    }

    public void ExitUpgrade()
    {
        gameManager.isDonedTurn = true;
        gameManager.upgradeHouseUI.SetActive(false);
        gameManager.upgradeAni.SetInteger("state", 3);
    }

    public void CardHandlingExit()
    {
        gameManager.isDonedTurn = true;
        gameManager.welcomeAni.SetInteger("state", 3);
        gameManager.fortune = null;
        gameManager.opportunity = null;
    }
    public void RollToPaidExit()
    {
        gameManager.isDonedTurn = true;
        gameManager.companyUI.SetActive(false);
        gameManager.rollToPaidAni.SetInteger("state", 3);
    }
    public void JailRecharge()
    {
        gameManager.isDonedTurn = true;
        gameManager.jailUI.SetActive(false);
        gameManager.jailAni.SetInteger("state", 3);
    }
    public void ExitPayTax()
    {
        gameManager.isDonedTurn = true;
        gameManager.taxUI.SetActive(false);
        gameManager.taxAni.SetInteger("state", 3);
    }

    public void ExitSelling()
    {
        gameManager.isDonedTurn = true;
        gameManager.sellingUI.SetActive(false);
    }

    public void ExitSellingToBuy()
    {
        gameManager.buyUI.SetActive(true);
    }
}
