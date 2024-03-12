using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Assets.Scrip.Object;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public static GameManager instance;
    [Header("Data")]
    public MenuData menuData;
    public SaveGameDataList saveGameData;

    [HideInInspector]public ChessBoard board;
    [Space(20)]

    [Header("Dice")]
    [SerializeField] private GameObject[] dicePrefab;
    [HideInInspector]public int numberOfDice = 2;
    public Transform[] pointInitDice;
    [HideInInspector]public int numberDice1 = 0;
    [HideInInspector]public int numberDice2 = 0;
    public int currentNumber = 0;
    public float fallSpeed = 10f;
    [HideInInspector] public Dice[] dice;
    [Space(20)]

    [Header("Player")]
    public int numberOfPlayer;
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private Box startPoint;
    public GameObject infomationPoint;
    public GameObject infomationPrefab;
    public List<Player> listPlayer;
    public int currentTurn = -1;
    [Space(20)]

    [Header("House")]
    public GameObject housePrefab;
    [Space(20)]

    [Header("UI")]
    private float countDown = 20f;
    private float turnTime = 20f;
    public GameObject buyUI;
    public GameObject upgradeHouseUI;
    public GameObject welcomeUI;
    public GameObject infoCardUI;
    public GameObject companyUI;
    public GameObject jailUI;
    public GameObject taxUI;
    public GameObject sellingUI;
    public GameObject sellingHouseUI;
    public GameObject sellingToPaidUI;
    public GameObject quitUI;
    public GameObject menuSettingUI;
    [Space(20)]

    [Header("Animation")]
    public Animator buyAni;
    public Animator upgradeAni;
    public Animator welcomeAni;
    public Animator exitHandleAni;
    public Animator rollToPaidAni;
    public Animator jailAni;
    public Animator taxAni;
    [Space(20)]

    [Header("Scroll")]
    public GameObject scrollPrefab;
    public List<ScrollAnimation> scrollAni;
    public List<ListBoxSellingUI> listScroll;
    public Transform pointInitScroll;
    [Space(20)]

    [Header("Audio")]
    public AudioSource clickAd;
    [Space(20)]

    [Header("Option")]
    public List<Box> currentBox;
    public bool areWorking = false;
    public bool isDonedTurn = false;
    public int isCoutinueGame = 0;
    public Fortune fortune;
    public Opportunity opportunity;
    [Space(20)]

    [Header("Camera")]
    public Camera cameraPlayer;
    public Vector3 offsetCamera = new Vector3(0, 10, 0);
    public Vector3 offsetCameraDice = new Vector3(0, 25, 0);
    [Space(20)]

    [Header("Company")]
    public int currentNumberCompany = 0;
    public bool is10x = false;
    public GameObject companyGround;
    [Space(20)]

    [Header("Selling")]
    public InfomationBoxSellingUI InfomationBoxSellingPrefab;
    public bool isSelling = false;
    public int whichSellingIsOn = -1;
    public bool isSellingToBuy = false;
    public bool isSellingToPaid = false;
    public SellingCanvas sellingCanvas;
    [Space(20)]

    [Header("Another")]
    private Box jailBox;
    private Box nguyenTriPhuongBox;
    private Box tanKyTanQuyBox;
    [Space(20)]


    [Header("Button Buy In BuyCanvas")]
    public Button sellingToBuyBTN;
    public Button buyBTN;
    [Space(20)]

    [Header("Save")]
    public string materialFolderPath = "Assets/Materials"; // Only use in unity editor, can't use with enviroment game

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        isCoutinueGame = PlayerPrefs.GetInt("isCountinueGame");

        //Board
        board = ChessBoard.instance;

        //Dice
        dice = new Dice[numberOfDice];
        for(int i = 0; i < numberOfDice; i++)
        {
            dice[i] = Instantiate(dicePrefab[i], pointInitDice[i].position, pointInitDice[i].rotation).GetComponent<Dice>();
        }

        if(isCoutinueGame == 0) //No continute game
        {
            //Box
            currentBox = new List<Box>(numberOfPlayer);

            //Player
            numberOfPlayer = PlayerPrefs.GetInt("numberOfPlayer");
            listPlayer = new List<Player>(numberOfPlayer);
            for (int i = 0; i < numberOfPlayer; i++)
            {
                //Player
                Player player = Instantiate(playerPrefab[i], startPoint.playerPosition[i].position, startPoint.transform.rotation).GetComponent<Player>();
                player.namePlayer = menuData.listDataPlayer[i].inputName.text;
                player.playerColor = menuData.listDataPlayer[i].materialPlayer;
                player.money = PlayerPrefs.GetInt("moneyStart");

                //Infomation banner
                player.infomationPlayerUI = Instantiate(infomationPrefab, infomationPoint.transform).GetComponent<InfomationPlayerUI>();
                player.infomationPlayerUI.playerNumber = i;
                Color playerColor = menuData.listDataPlayer[i].materialPlayer.color;
                playerColor.a = 0.4f; //alpha
                player.infomationPlayerUI.background.color = playerColor;
                player.infomationPlayerUI.arrow.enabled = false;

                //Add
                Box a = board.boxList[0];
                listPlayer.Add(player);
                currentBox.Add(a);
            }

            //Animator
            scrollAni = new List<ScrollAnimation>(numberOfPlayer);

            //Init scroll for player
            InstanteScroll(numberOfPlayer);

            listPlayer[0].infomationPlayerUI.arrow.enabled = true;

        }
        else
        {
            //Data
            saveGameData = SaveManager.instance.Load();
            
            //Game
            areWorking = false;
            isDonedTurn = false;
            currentTurn = -1;

            //Player
            numberOfPlayer = saveGameData.playerDataList.Count;

            //Animator
            scrollAni = new List<ScrollAnimation>(numberOfPlayer);

            InstanteScroll(numberOfPlayer);

            listPlayer = new List<Player>(numberOfPlayer);
            for (int i = 0; i < numberOfPlayer; i++)
            {
                Box currentBoxPlayer = board.boxList[saveGameData.playerDataList[i].currentNumberDice];
                //Player
                Player player = Instantiate(playerPrefab[i], currentBoxPlayer.playerPosition[i].transform.position, startPoint.transform.rotation).GetComponent<Player>();
                player.namePlayer = saveGameData.playerDataList[i].namePlayer;
                player.money = saveGameData.playerDataList[i].money;
                player.currentNumberDice = saveGameData.playerDataList[i].currentNumberDice;
                player.asset = saveGameData.playerDataList[i].asset;
                player.numberOfExistingHouse = saveGameData.playerDataList[i].numberOfExistingHouse;
                player.numberOfExistingVilla = saveGameData.playerDataList[i].numberOfExistingVilla;
                player.numberOfExistingBusStation = saveGameData.playerDataList[i].numberOfExistingBusStation;
                player.receiveMoney = saveGameData.playerDataList[i].receiveMoney;
                player.isOnJail = saveGameData.playerDataList[i].isOnJail;
                player.countDownJail = saveGameData.playerDataList[i].countDownJail;

                //Color
                player.playerColor = LoadMaterialByName(saveGameData.playerDataList[i].nameMaterial);

                //Box player has
                foreach (SaveBox boxPosition in saveGameData.playerDataList[i].save_ListOfBoxPlayerHas)
                {
                    //Add box
                    Box box = board.boxList[boxPosition.boxSerialNumber];
                    player.listOfBoxPlayerHas.Add(box);
                    
                    //Khởi tạo ô đất bên trong danh sách selling và gán box mua = box trong list selling, sau đó thêm vào danh sách
                    InfomationBoxSellingUI ibsUI = Instantiate(InfomationBoxSellingPrefab, listScroll[i].transform);
                    ibsUI.box = box;
                    listScroll[i].listSelling.Add(ibsUI);

                    //Who bougth
                    box.isBought = true;
                    box.moneyToBePaid = boxPosition.moneyToBePaid;
                    box.whoBought = player;
                    box.houseLevel = boxPosition.houseLevel;
                    box.UpdateBoxAfterBuy();
                    box.colorPlayer.material = player.playerColor;
                    AddBoxInMapping(player, box);
                    InstanteHouse(box);
                }

                foreach (int boxPosition in saveGameData.playerDataList[i].save_ListBus)
                {
                    AddBus(player, board.boxList[boxPosition]);
                }

                foreach (int boxPosition in saveGameData.playerDataList[i].save_ListCompany)
                {
                    AddCompany(player, board.boxList[boxPosition]);
                }

                //Infomation banner
                player.infomationPlayerUI = Instantiate(infomationPrefab, infomationPoint.transform).GetComponent<InfomationPlayerUI>();
                player.infomationPlayerUI.playerNumber = i;
                Color playerColor = player.playerColor.color;
                playerColor.a = 0.4f;
                player.infomationPlayerUI.background.color = playerColor;
                player.infomationPlayerUI.arrow.enabled = false;
                
                //Add
                listPlayer.Add(player);
                currentBox.Add(currentBoxPlayer);

            }
            SetArrowPlayer();
        }

        //UI
        buyUI.SetActive(false);
        upgradeHouseUI.SetActive(false);
        welcomeUI.SetActive(false);
        infoCardUI.SetActive(false);
        companyUI.SetActive(false);
        jailUI.SetActive(false);
        taxUI.SetActive(false);
        sellingUI.SetActive(false);
        sellingHouseUI.SetActive(false);
        sellingToPaidUI.SetActive(false);
        quitUI.SetActive(false);
        menuSettingUI.SetActive(false);

        //Another
        tanKyTanQuyBox = board.boxList[1];
        jailBox = board.boxList[10];
        nguyenTriPhuongBox = board.boxList[11];
        companyGround.SetActive(false);
        sellingCanvas = sellingUI.GetComponent<SellingCanvas>();
    }

    private void Update()
    {
        //Nếu người chơi chưa nhấn thảy xúc sắc lượt đầu
        if (currentTurn == -1)
            return;

        //Nếu người chơi chưa kết thúc lượt thì sẽ đặt lại giá trị của 2 xúc sắc là 0
        if (!isDonedTurn)
        {
            numberDice1 = 0;
            numberDice2 = 0;
        }
        
        //Nếu người chơi vẫn ở trong lượt
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            if (!dice[0].isMove() && !dice[1].isMove())
            {
                StartTurn();
            }

        }
    }

    public void StartTurn()
    {
        currentNumber += numberDice1 + numberDice2;
        listPlayer[currentTurn].currentNumberDice += currentNumber;

        //Thiết lập lại số xúc xắc hiện tại
        if (listPlayer[currentTurn].currentNumberDice == 40)
        {
            listPlayer[currentTurn].currentNumberDice -= 40;
        }

        //Kiem tra nguoi choi co o tu vao bang the khong
        if (listPlayer[currentTurn].currentNumberDice > 40)
        {
            if(listPlayer[currentTurn].receiveMoney == false)
            {
                listPlayer[currentTurn].money += 200;
                listPlayer[currentTurn].infomationPlayerUI.UpdateText();
            }               
            listPlayer[currentTurn].receiveMoney = false;
            listPlayer[currentTurn].currentNumberDice -= 40;
        }

        //Kiểm tra người chơi có di chuyển đến ô khác không
        if (currentBox[currentTurn] != board.boxList[listPlayer[currentTurn].currentNumberDice])
        {
            currentBox[currentTurn] = board.boxList[listPlayer[currentTurn].currentNumberDice];
            listPlayer[currentTurn].transform.position = currentBox[currentTurn].playerPosition[currentTurn].position;
            //cameraPlayer.transform.position = listPlayer[currentTurn].transform.position + offsetCamera;
            currentBox[currentTurn].propmtParitcle.Play();

            //Kiểm tra người chơi có ở tù hay không
            if (listPlayer[currentTurn].isOnJail)
            {
                areWorking = true;
                isDonedTurn = false;
                if (listPlayer[currentTurn].countDownJail == 0)
                {
                    listPlayer[currentTurn].isOnJail = false;
                    return;
                }
                jailUI.SetActive(true);
                jailAni.SetInteger("state", 1);
            }

            if (currentBox[currentTurn].isTax100dollar)
            {
                listPlayer[currentTurn].money -= currentBox[currentTurn].moneyToBePaid;
                listPlayer[currentTurn].infomationPlayerUI.UpdateText();
            }

            if (currentBox[currentTurn].isTax)
            {
                areWorking = true;
                isDonedTurn = false;
                taxUI.SetActive(true);
                taxAni.SetInteger("state", 1);
            }

            if (currentBox[currentTurn].isJail)
            {
                setEffectParticle(listPlayer[currentTurn]);
                listPlayer[currentTurn].currentNumberDice = 10;
                listPlayer[currentTurn].transform.position = jailBox.playerPosition[currentTurn].position;
                listPlayer[currentTurn].countDownJail = 3;
                listPlayer[currentTurn].isOnJail = true;
            }

            if (currentBox[currentTurn].isTheland)
            {
                AboutMoney(listPlayer[currentTurn].currentNumberDice);
            }

            if(currentBox[currentTurn].isOpportunity || currentBox[currentTurn].isFortune)
            {
                areWorking = true;
                isDonedTurn = false;
                welcomeUI.SetActive(true);
                welcomeAni.SetInteger("state", 1);
            }
        }
        currentNumber = 0;
        numberDice1 = 0;
        numberDice2 = 0;
    }

    public void EndTurn()
    {
        
    }
    public void NextTurn()
    {
        currentBox[currentTurn].propmtParitcle.Stop();
        currentTurn++;
        areWorking = false;
        countDown = turnTime;
        if (currentTurn >= numberOfPlayer)
        {
            currentTurn = 0;
        }

        //Nếu người chơi vào tù thì thiết lập lại currentBox để kiểm tra điều kiện
        if (listPlayer[currentTurn].isOnJail)
        {
            currentBox[currentTurn] = null;
        }
        SetArrowPlayer();
    }
    public void AboutMoney(int curNumber)
    {
        //Mua đất
        if (!board.boxList[curNumber].isBought && board.boxList[curNumber].isTheland)
        {
            //Dang mo canvas
            areWorking = true;
            isDonedTurn = false;
            buyUI.SetActive(true);
            buyAni.SetInteger("state", 1);
            if (listPlayer[currentTurn].money < board.boxList[curNumber].boxValue)
            {
                buyBTN.interactable = false;
            }
            else
            {
                buyBTN.interactable = true;
            }
        }
        else
        {
            //Nếu ô đất đã được mua
            if (board.boxList[curNumber].isTheland)
            {
                //Với người sở hữu
                if (listPlayer[currentTurn] == board.boxList[curNumber].whoBought && !board.boxList[curNumber].isTheBusStation && !board.boxList[curNumber].isTheCompany && board.boxList[curNumber].houseLevel < board.boxList[curNumber].maxHouseLevel)
                {
                    if (listPlayer[currentTurn].money >= board.boxList[curNumber].houseUpgradeMoney)
                    {
                        //Nâng cấp nhà
                        areWorking = true;
                        isDonedTurn = false;
                        upgradeHouseUI.SetActive(true);
                        upgradeAni.SetInteger("state", 1);
                    }
                }
                else
                {
                    //Nếu người chơi hết tiền
                    if (listPlayer[currentTurn].money < board.boxList[curNumber].moneyToBePaid && board.boxList[curNumber].whoBought != listPlayer[currentTurn])
                    {
                        sellingToPaidUI.SetActive(true);
                        isDonedTurn = false;
                        areWorking = true;
                        return;
                    }

                    //Với ô đất công ty
                    if (board.boxList[curNumber].isTheCompany && listPlayer[currentTurn] != board.boxList[curNumber].whoBought)
                    {
                        areWorking = true;
                        isDonedTurn = false;
                        companyUI.SetActive(true);
                        rollToPaidAni.SetInteger("state", 1);
                        return;
                    }

                    //Với người chơi không sở hữu 
                    //Sẽ thực hiện các animotion và các hàm tương ứng
                    listPlayer[currentTurn].infomationPlayerUI.moneyMinusText.gameObject.SetActive(true);
                }
            }
        }
    }

    public void MoneyMinus(Player sender, Box boxIndex, int money)
    {
        //Nguoi bi tru tien
        sender.infomationPlayerUI.moneyMinusText.text = "-" + money + "$";
        sender.money -= money;
        sender.infomationPlayerUI.UpdateText();
    }

    public void MoneyBonus(Player sender, Box boxIndex, int money)
    {
        sender.infomationPlayerUI.moneyMinusText.gameObject.SetActive(false);

        //Nguoi nhan tien
        if (boxIndex.whoBought != null)
        {
            boxIndex.whoBought.infomationPlayerUI.moneyBonusText.text = "+" + money + "$";
            boxIndex.whoBought.money += money;
            boxIndex.whoBought.infomationPlayerUI.UpdateText();
        }
    }

    public void CardHandlingFortune(Player player)
    {
        //Xu ly the
        if (fortune.bonus15Money)
        {
            bonusOrMinusMoney(player, 15);
        }
        else if (fortune.bonus50Money)
        {
            bonusOrMinusMoney(player, 50);
        }
        else if (fortune.bonus150Money)
        {
            bonusOrMinusMoney(player, 150);
        }
        else if (fortune.bonus200Money)
        {
            bonusOrMinusMoney(player, 200);
        }
        else if (fortune.goToJail)
        {
            setEffectParticle(player);
            player.receiveMoney = true;
            player.isOnJail = true;
            player.transform.position = jailBox.playerPosition[currentTurn].position;
            player.currentNumberDice = 10;
            listPlayer[currentTurn].countDownJail = 3;
            isDonedTurn = true;
        }
        else if (fortune.minus50Money)
        {
            bonusOrMinusMoney(player, -50);
        }
        else if (fortune.minus150Money)
        {
            bonusOrMinusMoney(player, -150);
        }
        else if (fortune.repairHouse)
        {
            bonusOrMinusMoney(player, -player.numberOfExistingHouse * 40);
            bonusOrMinusMoney(player, -player.numberOfExistingVilla * 100);
        }
        else if (fortune.theaterOpening)
        {
            bonusOrMinusMoney(player, (numberOfPlayer - 1) * 50 + 50);
            for (int i = 0; i < listPlayer.Count; i++)
            {
                bonusOrMinusMoney(listPlayer[i], -50);
            }
        }
        else if (fortune.goToStart)
        {
            setEffectParticle(player);
            player.transform.position = startPoint.playerPosition[currentTurn].position;
            player.currentNumberDice = 0;
            bonusOrMinusMoney(player, 200);
        }

        //Thêm lại thẻ vào hàng đợi
        board.fortunesQueue.Enqueue(fortune);
        isDonedTurn = true;
        player.infomationPlayerUI.UpdateText();
    }

    public void CardHandlingOpportunity(Player player)
    {
        //Xu ly the
        if (opportunity.bonus15Money)
        {
            bonusOrMinusMoney(player, 15);
        }
        else if (opportunity.bonus50Money)
        {
            bonusOrMinusMoney(player, 50);
        }
        else if (opportunity.bonus150Money)
        {
            bonusOrMinusMoney(player, 150);
        }
        else if (opportunity.goToJail)
        {
            setEffectParticle(player);
            player.isOnJail = true;
            player.receiveMoney = true;
            player.transform.position = jailBox.playerPosition[currentTurn].position;
            player.currentNumberDice = 10;
            listPlayer[currentTurn].countDownJail = 4;
        }
        else if (opportunity.comeToNguyenTriPhuong)
        {
            setEffectParticle(player);
            player.transform.position = nguyenTriPhuongBox.playerPosition[currentTurn].position;
            if (board.boxList[11].whoBought != null)
            {
                bonusOrMinusMoney(player, -board.boxList[11].moneyToBePaid);
                bonusOrMinusMoney(board.boxList[11].whoBought, board.boxList[11].moneyToBePaid);
            }
            else
            {
                areWorking = true;
                isDonedTurn = false;
                buyUI.SetActive(true);
                buyAni.SetInteger("state", 1);
            }
            player.currentNumberDice = 11;
        }
        else if(opportunity.cometoTanKyTanQuy)
        {
            setEffectParticle(player);
            player.transform.position = board.boxList[1].playerPosition[currentTurn].position;
            player.currentNumberDice = 1;
        }
        else if (opportunity.comeToNguyenTatThanh)
        {
            setEffectParticle(player);
            player.transform.position = board.boxList[23].playerPosition[currentTurn].position;
            player.currentNumberDice = 23;
            bonusOrMinusMoney(player, 200);
        }
        else if (opportunity.threeStepsBackward)
        {
            setEffectParticle(player);
            ForwardOrBackWard(player, -3);
        }
        else if(opportunity.threeStepsForward)
        {
            setEffectParticle(player);
            ForwardOrBackWard(player, 3);
        }
        else if(opportunity.repairHouse)
        {
            bonusOrMinusMoney(player, -player.numberOfExistingHouse * 25);
            bonusOrMinusMoney(player, -player.numberOfExistingVilla * 50);
        }
        else if(opportunity.comeToNearestTheBusStation)
        {
            setEffectParticle(player);
            int a = player.currentNumberDice;
            if(a == 7)
            {
                ComeToNearestTheBusStation(player, 5, 10);
            }
            if(a == 22)
            {
                ComeToNearestTheBusStation(player, 25, 10);
            }
            if(a == 36)
            {
                ComeToNearestTheBusStation(player, 35, 10);
            }
        }
        else if(opportunity.comeToNearestTheCompany)
        {
            setEffectParticle(player);
            if (player.currentNumberDice == 7)
            {
                ComeToNearestTheCompany(player, 12);
            }
            else if (player.currentNumberDice == 22)
            {
                ComeToNearestTheCompany(player, 28);
            }
            else
            {
                ComeToNearestTheCompany(player, 28);
            }

        }
        else if (opportunity.goToStart)
        {
            setEffectParticle(player);
            player.transform.position = startPoint.playerPosition[currentTurn].position;
            player.currentNumberDice = 0;
            bonusOrMinusMoney(player, 200);
        }
        else if(opportunity.wasElectedDirector)
        {
            bonusOrMinusMoney(player, -(numberOfPlayer - 1) * 50 - 50);
            for (int i = 0; i < listPlayer.Count; i++)
            {
                bonusOrMinusMoney(listPlayer[i], 50);
            }
        }

        //Thêm lại thẻ vào hàng đợi
        board.opportunitiesQueue.Enqueue(opportunity);
        isDonedTurn = true;
        player.infomationPlayerUI.UpdateText();
    }

    public void ComeToNearestTheBusStation(Player player, int numberBus, int multiply)
    {
        player.transform.position = board.boxList[numberBus].playerPosition[currentTurn].position;
        player.currentNumberDice = numberBus;
        if (board.boxList[numberBus].whoBought != null && board.boxList[numberBus].whoBought != player)
        {
            bonusOrMinusMoney(player, -board.boxList[numberBus].moneyToBePaid * 2);
            bonusOrMinusMoney(board.boxList[numberBus].whoBought, multiply * 2);
        }
        else
        {
            areWorking = true;
            isDonedTurn = false;
            buyUI.SetActive(true);
            buyAni.SetInteger("state", 1);
        }
    }

    public void ComeToNearestTheCompany(Player player, int numberCompany)
    {
        player.transform.position = board.boxList[numberCompany].playerPosition[currentTurn].position;
        player.currentNumberDice = numberCompany;
        if (board.boxList[numberCompany].whoBought != null && board.boxList[numberCompany].whoBought != player)
        {            
            isDonedTurn = false;
            companyUI.SetActive(true);
            rollToPaidAni.SetInteger("state", 1);
            is10x = true;
        }
        else
        {            
            isDonedTurn = false;
            buyUI.SetActive(true);
            buyAni.SetInteger("state", 1);
        }
    }

    public void ForwardOrBackWard(Player player, int numberOfSteps)
    {
        if(player.currentNumberDice + numberOfSteps == 33)
        {
            areWorking = true;
            isDonedTurn = false;
            welcomeUI.SetActive(true);
            welcomeAni.SetInteger("state", 1);
        }
        player.transform.position = board.boxList[player.currentNumberDice + numberOfSteps].playerPosition[currentTurn].position;
        player.currentNumberDice += numberOfSteps;
    }

    public void bonusOrMinusMoney(Player player, int amountOfMoney)
    {
        player.money += amountOfMoney;
    }

    public void setEffectParticle(Player player)
    {
        board.boxList[player.currentNumberDice].propmtParitcle.Stop();
    }

    public void DestroyPlayer()
    {
        listPlayer[currentTurn].infomationPlayerUI.gameObject.SetActive(false);

        listScroll[currentTurn].gameObject.SetActive(false);
        listScroll.RemoveAt(currentTurn);

        scrollAni[currentTurn].gameObject.SetActive(false);
        scrollAni.RemoveAt(currentTurn);

        //Xóa tất cả các box mà người chơi sở hữu
        for (int i = 0; i < listPlayer[currentTurn].listOfBoxPlayerHas.Count; i++)
        {
            listPlayer[currentTurn].listOfBoxPlayerHas[i].UpdateBoxAfterSell();
        }

        //Dịch chuyển scrollGO và scrollAni của người chơi ở sau player lên trên
        for (int i = currentTurn + 1; i < numberOfPlayer; i++)
        {
            listPlayer[i].infomationPlayerUI.playerNumber--;
            scrollAni[i - 1].playerTurn--;
        }

        //Xóa gameobject player
        Destroy(listPlayer[currentTurn].gameObject);
        listPlayer.RemoveAt(currentTurn);
        currentBox.RemoveAt(currentTurn);
        numberOfPlayer = listPlayer.Count;
        NextTurn();
    }

    public void InstanteScroll(int numberOfPlayer)
    {
        for (int i = 0; i < numberOfPlayer; i++)
        {
            //Tạo scroll tương ứng với mỗi người chơi
            ListBoxSellingUI lbs = Instantiate(scrollPrefab, pointInitScroll.transform).GetComponentInChildren<ListBoxSellingUI>();
            listScroll.Add(lbs);

            //Tạo scroll animation
            ScrollAnimation sa = pointInitScroll.GetChild(i).GetComponent<ScrollAnimation>();
            sa.playerTurn = i;
            scrollAni.Add(sa);

            ////PPhiên bản cũ, không phù hợp với việc xóa người chơi, đã cập nhật
            ////Lấy Scroll Animation của mỗi scroll và gán vào scroll animation global, sử dụng getchild để lấy 
            //Animation a = parentTranform.GetChild(i).GetComponent<ScrollAnimation>().scrollAni;
            //parentTranform.GetChild(i).GetComponent<ScrollAnimation>().playerTurn = i;
        }
    }

    public void InstanteHouse(Box box)
    {
        for (int i = 0; i < box.houseLevel; i++)
        {
            //Tạo nhà
            House house = Instantiate(housePrefab, box.housePosition[i].position, transform.rotation).GetComponent<House>();
            house.RoofColor.material = box.colorBoxM;
            box.listHouse.Add(house);
        }
        
    }

    void SetArrowPlayer()
    {
        if (currentTurn == 0)
        {
            listPlayer[0].infomationPlayerUI.arrow.enabled = true;
            listPlayer[numberOfPlayer - 1].infomationPlayerUI.arrow.enabled = false;
        }
        else if (currentTurn == 1)
        {
            listPlayer[1].infomationPlayerUI.arrow.enabled = true;
            listPlayer[0].infomationPlayerUI.arrow.enabled = false;
        }
        else if (currentTurn == 2)
        {
            listPlayer[2].infomationPlayerUI.arrow.enabled = true;
            listPlayer[1].infomationPlayerUI.arrow.enabled = false;
        }
        else if (currentTurn == 3)
        {
            listPlayer[3].infomationPlayerUI.arrow.enabled = true;
            listPlayer[2].infomationPlayerUI.arrow.enabled = false;
        }
        else if (currentTurn == 4)
        {
            listPlayer[4].infomationPlayerUI.arrow.enabled = true;
            listPlayer[3].infomationPlayerUI.arrow.enabled = false;
        }
    }

    public Material LoadMaterialByName(string materialName)
    {
        //Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
        Material material = Resources.Load<Material>("Materials/" + materialName);

        if (material == null)
        {
            Debug.LogError($"Material '{materialName}' not found at path");
        }

        return material;
    }

    public void AddBoxInMapping(Player player, Box box)
    {
        foreach (var entry in player.colorBoxMapping)
        {
            int boxValue = entry.Key;
            List<int> boxIndexes = entry.Value;

            if (box.boxValue >= boxValue && box.boxValue <= boxValue + 20)
            {
                //Thêm ô đất vào mapping
                boxIndexes.Add(box.boxSerialNumber);

                //Kiểm tra xem có đủ ô đất cùng màu chưa
                if (boxValue == 60 || boxValue == 300)
                {
                    if (boxIndexes.Count == 2)
                    {
                        foreach (int boxIndex in boxIndexes)
                        {
                            board.boxList[boxIndex].moneyToBePaid *= 2;
                            board.boxList[boxIndex].UpdateBoxAfterBuy();
                        }
                    }
                }
                else
                {
                    if (boxIndexes.Count == 3)
                    {
                        foreach (int boxIndex in boxIndexes)
                        {
                            board.boxList[boxIndex].moneyToBePaid *= 2;
                            board.boxList[boxIndex].UpdateBoxAfterBuy();
                        }
                    }
                }
            }
        }
    }

    public void AddBus(Player player, Box box)
    {
        player.listBus.Add(box);
        if (player.listBus.Count == 2)
        {
            for (int i = 0; i < player.listBus.Count; i++)
            {
                player.listBus[i].moneyToBePaid *= 2;
                player.listBus[i].UpdateBoxAfterBuy();
            }
        }
        if (player.listBus.Count == 3)
        {
            for (int i = 0; i < player.listBus.Count; i++)
            {
                if (i == player.listBus.Count - 1)
                {
                    player.listBus[i].moneyToBePaid *= 4;
                    break;
                }
                player.listBus[i].moneyToBePaid *= 2;
                player.listBus[i].UpdateBoxAfterBuy();
            }
        }
        if (player.listBus.Count == 4)
        {
            for (int i = 0; i < player.listBus.Count; i++)
            {
                if (i == player.listBus.Count - 1)
                {
                    player.listBus[i].moneyToBePaid *= 8;
                    break;
                }
                player.listBus[i].moneyToBePaid *= 2;
                player.listBus[i].UpdateBoxAfterBuy();
            }
        }
    }

    public void AddCompany(Player player, Box box)
    {
        player.listCompany.Add(box);
        if (player.listCompany.Count == 1)
        {
            player.listCompany[0].moneyToBePaid = 4;
        }
        if (player.listCompany.Count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                player.listCompany[i].moneyToBePaid = 10;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "One":
                {
                    numberDice1 = 6;
                    break;
                }
            case "Two":
                {
                    numberDice1 = 5;
                    break;
                }
            case "Three":
                {
                    numberDice1 = 4;
                    break;
                }
            case "Four":
                {
                    numberDice1 = 3;
                    break;
                }
            case "Five":
                {
                    numberDice1 = 2;
                    break;
                }
            case "Six":
                {
                    numberDice1 = 1;
                    break;
                }

            case "One1":
                {
                    numberDice2 = 6;
                    break;
                }
            case "Two1":
                {
                    numberDice2 = 5;
                    break;
                }
            case "Three1":
                {
                    numberDice2 = 4;
                    break;
                }
            case "Four1":
                {
                    numberDice2 = 3;
                    break;
                }
            case "Five1":
                {
                    numberDice2 = 2;
                    break;
                }
            case "Six1":
                {
                    numberDice2 = 1;
                    break;
                }
            case "Untagged":
                {
                    numberDice1 = 0;
                    numberDice2 = 0;
                    break;
                }
        }

    }

    private void OnApplicationQuit()
    {
        menuData.listDataPlayer.Clear();
    }
}


