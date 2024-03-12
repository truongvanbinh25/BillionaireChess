using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxUI : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI text;
    public Button pay10pt;
    public Button pay200dollar;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Pay10pt()
    {
        gameManager.clickAd.Play();
        gameManager.listPlayer[gameManager.currentTurn].money -= (int)(gameManager.listPlayer[gameManager.currentTurn].money * 0.1f);
        gameManager.taxAni.SetInteger("state", 2);
        gameManager.listPlayer[gameManager.currentTurn].infomationPlayerUI.UpdateText();
    }
    public void Pay200()
    {
        gameManager.clickAd.Play();
        gameManager.listPlayer[gameManager.currentTurn].money -= 200;
        gameManager.taxAni.SetInteger("state", 2);
        gameManager.listPlayer[gameManager.currentTurn].infomationPlayerUI.UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.listPlayer[gameManager.currentTurn].money < 200)
        {
            pay200dollar.interactable = false;
        }
        else
        {
            pay200dollar.interactable = true;
        }
        text.text = "Bạn đã vào ô Thuế\n10% số tiền hiện có hoặc 200$";
    }
}
