using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public GameObject[] obstaclePrefab;
    //public GameObject coinPrefabs;
    //public GameObject SpeedBoostPrefabs;

    private int obstacleIndex;
    public Vector3 spawnPos = new(25, 0, 0);
    public GameObject[] SpawnPos;


    private PlayerController playercontroller;



    private void Awake()
    {
        playercontroller = GetComponent<PlayerController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Instantiate(obstaclePrefab, new Vector3(25, 0, 0), obstaclePrefab.transform.rotation);

        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();

        //InvokeRepeating(nameof(SpawnCoin), 2.5f, 2.7f);
        //InvokeRepeating(nameof(SpawnObstacle), 2, 2.1f);
        StartCoroutine(CoinRoutine());
        StartCoroutine(ObstacleRoutine());
        InvokeRepeating(nameof(SpawnHeal), 15, 20.3f);
        InvokeRepeating(nameof(SpawnSpeed), 10, 32.6f);
        

    }


    private void Update()
    {
        if (playercontroller.gameOver == true)
        {
            CancelInvoke();
            StopAllCoroutines();
            
        }
    }

    
    IEnumerator CoinRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        
        while (true)
        {
            SpawnCoin();
            if (playercontroller.isSpeedBoost)
            {
                yield return new WaitForSeconds(1.35f);
            }
            else
            {
                yield return new WaitForSeconds(2.7f);
            }
        }
    }

    IEnumerator ObstacleRoutine()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            SpawnObstacle();
            if (playercontroller.isSpeedBoost)
            {
                yield return new WaitForSeconds(1.05f);
            }
            else
            {
                yield return new WaitForSeconds(2.1f);
            }
        }
    }

    void SpawnObstacle()
    {
        var obs = ObstacleObjectPool.GetInstance().AcquireObstacle();
        obs.transform.SetLocalPositionAndRotation(spawnPos, Quaternion.identity);
    }

    public void SpawnCoin()
    {
        int rnd = Random.Range(0, SpawnPos.Length);

        var coins = ObstacleObjectPool.GetInstance().AcquireCoin();
        coins.transform.SetLocalPositionAndRotation(SpawnPos[rnd].transform.position, Quaternion.Euler(90,0,0));
    }

    void SpawnHeal()
    {
        int rnd = Random.Range(0, SpawnPos.Length);

        var heal = ObstacleObjectPool.GetInstance().AcquireHeal();
        heal.transform.SetLocalPositionAndRotation(SpawnPos[rnd].transform.position, Quaternion.Euler(-90,0,0));
    }

    void SpawnSpeed()
    {
        int rnd = Random.Range(0, SpawnPos.Length);

        var heal = ObstacleObjectPool.GetInstance().AcquireSpeed();
        heal.transform.SetLocalPositionAndRotation(SpawnPos[rnd].transform.position, Quaternion.Euler(0, 0, 0));
    }
}
