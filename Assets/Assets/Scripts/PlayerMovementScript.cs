using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovementScript : MonoBehaviour {

    public float speed = 10f;

    public float jumpPower;

    public int extraJumps = 1;

    [SerializeField] Animator anim;

    [SerializeField] LayerMask GroundLayer;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Transform feet;

    int jumpCount = 0;

    bool isGrounded;

    float mx;

    float jumpCoolDown;

    public HealthBar healthBar;
    public UltBar ultBar;
    private bool isWin = false;
    public Transform particle;
    public AudioSource ultSound;
    public AudioSource music;

    private void Start()
    {
        particle.GetComponent<ParticleSystem>().enableEmission = false;
    }
    private void Update()
    {
        if (healthBar.current <= 0)
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
        
        if(isWin == false)
        {
            mx = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                Jump();

            }
            CheckGrounded();
            if (Mathf.Abs(mx) > 0.05f)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            if (mx > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (mx < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        if(ultBar.current >= 100)
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                ultSound.Play();
                music.Stop();
            }
            
        }
        

       
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx * speed, rb.velocity.y);
    }



    void Jump()
    {
        if (isGrounded || jumpCount < extraJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            healthBar.current -= 25;
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            healthBar.current -= 25;
            transform.position = new Vector3(0, -9);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            isWin = true;
            mx = 0;
            particle.GetComponent<ParticleSystem>().enableEmission = true;
        }
    }

    void CheckGrounded()
    {

        if (Physics2D.OverlapCircle(feet.position, 0.5f, GroundLayer))
        {
            anim.SetBool("isGrounded", true);
            isGrounded = true;
            
            jumpCount = 0;

            jumpCoolDown = Time.time + 0.2f;

        }
        else if (Time.time < jumpCoolDown)
        {

            isGrounded = true;
            anim.SetBool("isGrounded", false);
        }
        else
        {

            isGrounded = false;
            anim.SetBool("isGrounded", false);

        }

    }





}
