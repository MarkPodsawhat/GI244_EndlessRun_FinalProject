using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;
    public AudioClip coinsSfx;
    public AudioClip healSfx;
    public AudioClip speedStartSfx;
    public AudioClip speedEndSfx;
    public AudioClip deadSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private bool isOnGround = true;

    public bool isSpeedBoost = false; 

    private InputAction sprintAction;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    private int jumpCount = 2;
    private MoveLeft moveLeft;

    private int maxHp = 3;
    private int hp;

    private float endDuration = 0;
    private float speedDuration = 5;

    public GameObject gameOverMenu;
    public GameObject inGameUI;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    private int score = 0;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        moveLeft = GetComponent<MoveLeft>();

        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
        scoreText.text = score.ToString();

        // rb.AddForce(1000 * Vector3.up);
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        finalScoreText.text = score.ToString();
        scoreText.text = score.ToString();
        lifeText.text = hp.ToString();

        if (sprintAction.triggered)
        {
            moveLeft.speed *= 2;
            moveLeft.leftBound *= 2;
        }

        if (jumpAction.triggered && jumpCount != 0 && !gameOver)
        {
            rb.linearVelocity = new Vector3(0,0,0);
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            jumpCount--;
         
            
            isOnGround = false;
            
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSfx);
        }

        if (!isOnGround || gameOver)
        {
            dirtParticle.Stop();
        }
        else
        {
            if (!dirtParticle.isPlaying)
            {
                dirtParticle.Play();
            }
        }

        if (Time.time >= endDuration && isSpeedBoost)
        {
            playerAudio.PlayOneShot(speedEndSfx);
            isSpeedBoost = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameOver)
        {
            switch (collision.gameObject.tag) 
            {
                case "Ground":
                    isOnGround = true;
                    jumpCount = 2;
                    break;

                case "Obstacle":
                    if (!isSpeedBoost)
                    {
                        hp--;
                    }
                    explosionParticle.Play();
                    playerAudio.PlayOneShot(crashSfx);

                    if (hp <= 0)
                    {
                        playerAudio.PlayOneShot(deadSfx);
                        gameOverMenu.SetActive(true);
                        inGameUI.SetActive(false);
                        gameOver = true;
                        Debug.Log("Game Over!");
                        playerAnim.SetBool("Death_b", true);
                        playerAnim.SetInteger("DeathType_int", 1);
                        Debug.Log(score);
                    }
                    break;

                case "Coin":
                    playerAudio.PlayOneShot(coinsSfx);
                    score++;
                    break;

                case "Heal":
                    if (hp < maxHp)
                    {
                        playerAudio.PlayOneShot(healSfx);
                        hp++;
                    }
                    break ;

                case "SpeedBoost":
                    playerAudio.PlayOneShot(speedStartSfx);
                    isSpeedBoost = true;
                    endDuration = Time.time + speedDuration;


                    break;
            }
        }
    }//onCollisionEnter
}