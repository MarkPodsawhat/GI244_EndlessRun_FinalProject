using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField] private int instantPoolSize = 3;
    [SerializeField] private GameObject coinPrefabs;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject healPrefabs;
    [SerializeField] private GameObject speedPrefabs;

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
        }

        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            CreateObstacle(i);
        }

        CreateHeal();
        CreatSpeed();
    }

 

    void CreateCoins()
    {
        GameObject coin = Instantiate(coinPrefabs);
        coin.SetActive(false);
        coinPool.Add(coin);
    }

    void CreateObstacle(int index)
    {
        GameObject obs = Instantiate(obstaclePrefabs[index]);

        obs.SetActive(false);
        obstaclePool.Add(obs);
    }

    void CreateHeal()
    {
        GameObject heal = Instantiate(healPrefabs);

        heal.SetActive(false);

        healPrefabs = heal;
    }

    void CreatSpeed()
    {
        GameObject speed = Instantiate(speedPrefabs);

        speed.SetActive(false);
        speedPrefabs = speed;
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
    }

    public GameObject AcquireObstacle()
    {
        

        GameObject obs = obstaclePool[0];
        obstaclePool.RemoveAt(0);

        obs.SetActive(true);
        return obs;
    }

    public GameObject AcquireHeal()
    {
        healPrefabs.SetActive(true);
        return healPrefabs;
    } 

    public GameObject AcquireSpeed()
    {
        speedPrefabs.SetActive(true);                                                                                                                                       
        return speedPrefabs;
    }

    public void ReturnObject(GameObject objectReturn)
    {
        switch (objectReturn.tag) 
        {
            case "Coin" : 
                coinPool.Add(objectReturn);
                objectReturn.SetActive(false);
                break;

            case "Obstacle":
                obstaclePool.Add(objectReturn);
                objectReturn.SetActive(false);            
                break;

            case "Heal":
                objectReturn.SetActive(false);               
                break;

            case "SpeedBoost":
                objectReturn.SetActive(false);            
                break;
        }
    }
}
