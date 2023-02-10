using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
using Touch = UnityEngine.Touch;

public class PlayerMovementScript : MonoBehaviour {

    public float speed = 10f;

    public float jumpPower;

    public int extraJumps = 1;

    [SerializeField] Animator anim;

    [SerializeField] LayerMask GroundLayer;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Transform feet;

    [SerializeField] ChronaAnimator chrona;

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
    public AudioSource enemyHit;
    public float touchTime = 0;

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
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                mx = Input.GetAxisRaw("Horizontal");
            }
            else if ((Input.GetAxisRaw("Horizontal") == 0) && Input.touchCount == 0)
            {
                mx = 0;
            }



            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            {

                
                
                if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(0).position.y < Screen.height / 2 )
                {
                    mx = 1;
                    
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(0).position.y < Screen.height / 2 )
                {
                    mx = -1;

                }

            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                mx = 0;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.y > Screen.height / 2 || Input.GetButtonDown("Jump"))
            {

                Jump();
               
            }
            
            CheckGrounded();
            if (Mathf.Abs(mx) > 0.05f)
            {
                anim.SetBool("isRunning", true);
                chrona.StartMoving();
            }
            else
            {
                anim.SetBool("isRunning", false);
                chrona.StopMoving();
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
        if (ultBar.current == 100 && !ultSound.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Q) || ultBar.buttonPress == true)
            {
                ultSound.Play();
                music.Stop();

            }

        }
        if (ultBar.current == 0 && !music.isPlaying)
        {
            music.Play();
            ultSound.Stop();

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
            enemyHit.Play();

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
