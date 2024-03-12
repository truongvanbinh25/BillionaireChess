using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGameCanvas : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void OpenSetting()
    {
        gameManager.menuSettingUI.SetActive(true);
    }
}
