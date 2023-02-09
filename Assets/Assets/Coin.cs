using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Coin : MonoBehaviour
{
    public AudioSource coinSound;

    void Start()
    {
        coinSound = gameObject.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            coinSound.Play();
            transform.position = Vector3.one * 1000;
            CoinCounterScript.coinAmount += 1;
            Destroy(gameObject, coinSound.clip.length);

            
        }
    }
}
