using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;

    public float leftBound = -15;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {


        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
            //ObstacleObjectPool.GetInstance().ReturnCoin(gameObject);
            Debug.Log(transform.position.x);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Avatar"))
        {
            if (this.gameObject.CompareTag("Coin"))
            {
                ObstacleObjectPool.GetInstance().ReturnObject(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
