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

    private Rigidbody rb;
    private InputAction jumpAction;
    private bool isOnGround = true;

    private InputAction sprintAction;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    private int jumpCount = 2;
    private MoveLeft moveLeft;

    private int hp = 3;

    [SerializeField] private TextMeshProUGUI scoreText;
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
        scoreText.text = score.ToString();
        lifeText.text = hp.ToString();

        if (sprintAction.triggered)
        {
            moveLeft.speed *= 2;
            moveLeft.leftBound *= 2;
        }

        if (jumpAction.triggered && isOnGround && !gameOver)
        {
            //rb.linearVelocity.y = 0;
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            jumpCount--;
            if (jumpCount <= 0 && !gameOver)
            {
                isOnGround = false;
            }
            
            
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            jumpCount = 2;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            hp--;
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSfx);

            if (hp <= 0)
            {
                
                gameOver = true;
                Debug.Log("Game Over!");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                dirtParticle.Stop();
            }
        }
        else if (collision.gameObject.CompareTag("Coin") && !gameOver)
        {
            score += 1;
        }
    }
}