using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard instance;
    [Header("Box")]
    public Box[] boxList;

    [Header("Fortune")]
    public Fortune[] fortuneList;
    public Queue<Fortune> fortunesQueue;

    [Header("Opportunity")]
    public Opportunity[] opportunitiesList;
    public Queue<Opportunity> opportunitiesQueue;

    private void Awake()
    {
        instance = this;

        boxList = GetComponentsInChildren<Box>();

        for (int i = 0; i < boxList.Length; i++)
        {
            boxList[i].boxSerialNumber = i;
        }

        fortunesQueue = new Queue<Fortune>();
        opportunitiesQueue = new Queue<Opportunity>();

        RandomList(fortuneList);
        RandomList(opportunitiesList);

        for (int i = 0; i < fortuneList.Length; i++)
        {
            fortunesQueue.Enqueue(fortuneList[i]);
        }

        for (int i = 0; i < opportunitiesList.Length; i++)
        {
            opportunitiesQueue.Enqueue(opportunitiesList[i]);
        }
    }

    public void RandomList<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = Random.Range(i, n);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

}
