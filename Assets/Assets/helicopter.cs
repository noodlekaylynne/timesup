using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helicopter : MonoBehaviour
{

    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private float originalSpeed;
    private float time;



    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < - 200)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            {
                StartCoroutine(PowerUpTimer());
                print("Slow Down!");
            }


        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator PowerUpTimer()
    {
        originalSpeed = speed;
        speed /= 2;

        yield return new WaitForSeconds(time);
        speed = originalSpeed;
    }
}
