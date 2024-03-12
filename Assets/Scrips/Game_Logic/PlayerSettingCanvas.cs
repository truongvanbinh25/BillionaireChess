using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingCanvas : MonoBehaviour
{
    private SceneController sceneController;

    public TextMeshProUGUI title;
    public TMP_InputField inputName;
    public Material materialPlayer;
    public Toggle[] toggleColor;
    public Material[] listMaterials;

    public void Start()
    {
        sceneController = SceneController.instante;
    }

    public void NextSettingPlayer()
    {
        sceneController.NextCanvasInSettingCanvas2();
    }

    public void BackSettingPlayer()
    {
        sceneController.BackSettingInSettingCanvas2();
    }

    private void Update()
    {
        if (toggleColor[0].isOn)
        {
            materialPlayer = listMaterials[0];
        }
        else if (toggleColor[1].isOn)
        {
            materialPlayer = listMaterials[1];
        }
        else if (toggleColor[2].isOn)
        {
            materialPlayer = listMaterials[2];
        }
        else
        {
            materialPlayer = listMaterials[3];
        }
    }

}
