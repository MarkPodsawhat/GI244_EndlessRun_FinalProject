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

    public float startDelay = 2;
    public float repeatRate = 2;

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

        InvokeRepeating(nameof(SpawnCoin), startDelay, repeatRate);
        InvokeRepeating(nameof(SpawnObstacle), 3, 3);
    }

    private void Update()
    {
        if (playercontroller.gameOver == true)
        {
            CancelInvoke();
        }
    }




    
    void SpawnObstacle()
    {
        var obs = ObstacleObjectPool.GetInstance().AcquireObstacle();
        obs.transform.SetLocalPositionAndRotation(spawnPos, Quaternion.identity);
    }

    public void SpawnCoin()
    {
        int coinRnd = Random.Range(0, SpawnPos.Length);

        var coins = ObstacleObjectPool.GetInstance().AcquireCoin();
        coins.transform.SetLocalPositionAndRotation(SpawnPos[coinRnd].transform.position, Quaternion.Euler(90,0,0));
    }

    void SpawnSpeedBoost()
    {

    }
}
