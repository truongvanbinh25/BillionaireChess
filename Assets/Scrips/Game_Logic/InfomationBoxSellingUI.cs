using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfomationBoxSellingUI : MonoBehaviour
{
    private GameManager gameManager;

    public Button button;
    public TextMeshProUGUI textName;
    public RawImage boxColor;
    public TextMeshProUGUI textNumberOfHouse;
    public TextMeshProUGUI textSellingBox;
    public TextMeshProUGUI textSellingHouse;
    public Box box;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Click()
    {
        gameManager.sellingUI.SetActive(true);
        //SellingCanvas sellingCanvas = gameManager.sellingUI.GetComponent<SellingCanvas>();
        gameManager.sellingCanvas.infoBoxClick = box;
        gameManager.sellingCanvas.UpdateSelling();
    }

    private void Update()
    {
        textName.text = box.boxName;
        boxColor.color = box.colorBoxM.color;
        textNumberOfHouse.text = "Số nhà: " + (box.houseLevel).ToString();
        textSellingBox.text = "Giá bán đất: " + (box.boxValue / 2).ToString() + "$";
        textSellingHouse.text = "Giá bán nhà: " + box.houseUpgradeMoney.ToString() + "$/1";
    }
}
