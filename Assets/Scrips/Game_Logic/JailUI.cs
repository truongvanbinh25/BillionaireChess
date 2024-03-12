using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JailUI : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Recharge()
    {
        gameManager.jailAni.SetInteger("state", 2);
        gameManager.listPlayer[gameManager.currentTurn].money -= 50;
        gameManager.listPlayer[gameManager.currentTurn].countDownJail = 0;
        gameManager.listPlayer[gameManager.currentTurn].infomationPlayerUI.UpdateText();
        gameManager.listPlayer[gameManager.currentTurn].isOnJail = false;
        gameManager.areWorking = true;
        gameManager.isDonedTurn = true;
    }

    public void NoRecharger()
    {
        gameManager.isDonedTurn = false;
        gameManager.jailAni.SetInteger("state", 2);
        gameManager.listPlayer[gameManager.currentTurn].countDownJail--;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = "Bạn có muốn nạp 50$ để ra khỏi tù không?\nSố lượt chờ: " + gameManager.listPlayer[gameManager.currentTurn].countDownJail + " lượt";
    }
}
