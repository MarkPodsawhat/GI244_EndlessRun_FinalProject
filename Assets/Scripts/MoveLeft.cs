using System;
using System.Collections;
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
    void FixedUpdate()
    {
        if (playerController.isSpeedBoost)
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }

        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound)
        {
            ObstacleObjectPool.GetInstance().ReturnObject(gameObject);
        }   

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Avatar"))
        {
            ObstacleObjectPool.GetInstance().ReturnObject(gameObject);
        }
    }
}
