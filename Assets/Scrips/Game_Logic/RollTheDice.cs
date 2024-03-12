using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollTheDice : MonoBehaviour
{
    private int rotationX;
    private int rotationZ;
    private Vector3 diceRotation;
    [SerializeField] private Button buttonRoll;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonRollToPaid;
    private GameManager gameManager;
    private Rigidbody[] diceRB;
    // Start is called before the first frame update
    void Start()
    {
        if(buttonNext != null)
        {
            buttonNext.interactable = false;
        }
        
        gameManager = GameManager.instance;
        diceRB = new Rigidbody[2];
        for (int i = 0; i < gameManager.numberOfDice; i++)
        {
            diceRB[i] = gameManager.dice[i].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonRoll == null)
        {
            return;
        }

        //Nếu 2 viên xúc sắc đã dừng và đã thực hiện các thao tác thì cho phép qua lượt
        if (!gameManager.dice[0].isMove() && !gameManager.dice[1].isMove() && gameManager.isDonedTurn)
        {
            buttonNext.interactable = true;
        }

        //Nếu 2 viên xúc sắc đã dừng
        if (!gameManager.dice[0].isMove() && !gameManager.dice[1].isMove() && !gameManager.areWorking)
        {
            buttonRoll.interactable = true;
        }
        else
        {
            buttonRoll.interactable = false;
        }
    }

    public void Roll()
    {
        if(gameManager.currentTurn == -1)
        {
            if(gameManager.isCoutinueGame == 0)
            {
                gameManager.currentTurn = 0;
            }
            else
            {
                gameManager.currentTurn = gameManager.saveGameData.currentTurn;
            }
        }
        RollDice();

        gameManager.areWorking = true;
        gameManager.isDonedTurn = true;
    }

    public void RollToPaid()
    {
        RollDice();
        gameManager.rollToPaidAni.SetInteger("state", 2);
        //gameManager.cameraPlayer.transform.position = gameManager.gameObject.transform.position + gameManager.offsetCameraDice;
        gameManager.gameObject.SetActive(false);
        gameManager.companyGround.SetActive(true);
    }

    public void RollDice()
    {
        for (int i = 0; i < gameManager.numberOfDice; i++)
        {
            rotationX = Random.Range(1, 360);
            rotationZ = Random.Range(1, 360);
            diceRotation = new Vector3(rotationX, 0, rotationZ);
            gameManager.dice[i].transform.position = gameManager.pointInitDice[i].position;
            gameManager.dice[i].transform.rotation = Quaternion.Euler(diceRotation);

            diceRB[i].velocity = Vector3.down * gameManager.fallSpeed;
        }
    }

    public void NextTurn()
    {
        gameManager.isDonedTurn = false;
        buttonNext.interactable = false;
        gameManager.gameObject.SetActive(true);
        FindObjectOfType<GameManager>().NextTurn();
    }

}
