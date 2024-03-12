using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfomationPlayerUI : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Layout")]
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI money;
    public TextMeshProUGUI asset;
    public RawImage background;
    public Image arrow;
    //Khi khởi tạo danh sách người chơi thì playerNumber sẽ được đánh số tương ứng với vị trị người chơi trong mảng
    public int playerNumber;
    public Button coinButton;


    [Header("Text About Of Money Animation")]
    public TextMeshProUGUI moneyBonusText;
    public TextMeshProUGUI moneyMinusText;
    public TextMeshProUGUI buyBoxText;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI sellingBoxText;
    public TextMeshProUGUI sellingHouseText;

    private void Start()
    {
        gameManager = GameManager.instance;
        namePlayer.text = gameManager.listPlayer[playerNumber].namePlayer.ToString();
        money.text = "Tiền: $" + gameManager.listPlayer[playerNumber].money.ToString();
        asset.text = "Tài sản: $" + gameManager.listPlayer[playerNumber].asset.ToString();
    }

    public void UpdateText()
    {
        namePlayer.text = gameManager.listPlayer[playerNumber].namePlayer.ToString();
        money.text = "Tiền: $" + gameManager.listPlayer[playerNumber].money.ToString();
        asset.text = "Tài sản: $" + gameManager.listPlayer[playerNumber].asset.ToString();
    }

    public void ClickCoin()
    {
        //Dong tat ca cac scrollAni neu trang thai cua scroll hien tai la mo
        gameManager.whichSellingIsOn = playerNumber;
        if (gameManager.scrollAni[playerNumber].scrollAni.GetInteger("state") == 1)
        {
            //Neu selling Ui dang mo thi dong
            if (gameManager.sellingUI.activeSelf == true)
            {
                gameManager.sellingUI.SetActive(false);
                gameManager.buyUI.SetActive(true);
                gameManager.buyAni.SetInteger("state", 1);
            }
            //Dong scroll ani
            gameManager.scrollAni[playerNumber].scrollAni.SetInteger("state", 2);
            gameManager.isSelling = false;
            return;
        }

        //Dong scroll ani cua tat ca nguoi choi
        for (int i = 0; i < gameManager.numberOfPlayer; i++)
        {
            gameManager.scrollAni[i].scrollAni.SetInteger("state", 2);
        }
        //Mo scoll ani cua nguoi choi
        gameManager.scrollAni[playerNumber].scrollAni.SetInteger("state", 1);
        gameManager.isSelling = true;
    }


}
