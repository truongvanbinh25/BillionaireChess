using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstantiateScroll : MonoBehaviour
{
    ////Đã được bỏ trực tiếp vào GameManager để phù hợp với việc cập nhật
    //private GameManager gameManager;
    //public GameObject scrollPrefab;
    //[HideInInspector]public List<ListBoxSellingUI> scrollGO = new List<ListBoxSellingUI>();
    //// Start is called before the first frame update
    //public void Start()
    //{   
    //    gameManager = GameManager.instance;
    //    Transform parentTranform = transform;

    //    for (int i = 0; i < gameManager.numberOfPlayer; i++)
    //    {
    //        //Tạo scroll tương ứng với mỗi người chơi
    //        ListBoxSellingUI lbs = Instantiate(scrollPrefab, gameObject.transform).GetComponentInChildren<ListBoxSellingUI>();
    //        scrollGO.Add(lbs);

    //        //Tạo scroll animation
    //        ScrollAnimation sa = parentTranform.GetChild(i).GetComponent<ScrollAnimation>();
    //        sa.playerTurn = i;
    //        gameManager.scrollAni.Add(sa);

    //        ////PPhiên bản cũ, không phù hợp với việc xóa người chơi, đã cập nhật
    //        ////Lấy Scroll Animation của mỗi scroll và gán vào scroll animation global, sử dụng getchild để lấy 
    //        //Animation a = parentTranform.GetChild(i).GetComponent<ScrollAnimation>().scrollAni;
    //        //parentTranform.GetChild(i).GetComponent<ScrollAnimation>().playerTurn = i;
    //    }
    //}
}
