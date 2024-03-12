using UnityEngine;

public class QuitCanvas : MonoBehaviour
{
    private GameManager gameManager;
    private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        saveManager = SaveManager.instance;
    }

    public void Save()
    {   
        saveManager.Save();
        Quit();
    }   
    
    public void NoSave()
    {
        Quit();
    }

    public void Quit()
    {
        gameManager.menuData.listDataPlayer.Clear();
        Application.Quit();
    }
}
