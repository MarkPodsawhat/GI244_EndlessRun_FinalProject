using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField] private int instantPoolSize = 3;
    [SerializeField] private GameObject coinPrefabs;
    [SerializeField] private GameObject[] obstaclePrefabs = new GameObject[3];

    [SerializeField] private List<GameObject> coinPool = new();
    [SerializeField] private List<GameObject> obstaclePool = new();

    private static ObstacleObjectPool instance;



    

    public static ObstacleObjectPool GetInstance()
    {
        return instance;
    }



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
        

    void Start()
    {
        for (int i = 0; i < instantPoolSize; i++)
        {
            CreateCoins();
            Debug.Log("Creat Coin");
        }

        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            CreateObstacle(i);
        }
    }

 

    void CreateCoins()
    {
        GameObject coin = Instantiate(coinPrefabs);
        coin.SetActive(false);
        coinPool.Add(coin);
    }

    
    public GameObject AcquireCoin()
    {
        if (coinPool.Count == 0)
        {
            CreateCoins();
        }

        GameObject coin = coinPool[0];
        coinPool.RemoveAt(0);

        coin.SetActive(true);
        return coin;

        //foreach (GameObject coin in coinPool)
        //{
        //    if (coin.activeSelf == false)
        //    {
        //        coin.SetActive(true);
        //    }
        //}
    }

    public void ReturnObject(GameObject gameObject)
    {
        if (gameObject.gameObject.CompareTag("Coin"))
        {
            coinPool.Add(gameObject);
            gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Obstacle"))
        {

        }
    }

    void CreateObstacle(int index)
    {
        GameObject obs = Instantiate(obstaclePrefabs[index]);

        obs.SetActive(false);
        obstaclePool.Add(obs);
    }

}
