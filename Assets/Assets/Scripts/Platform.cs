using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    private float originalSpeed;
    public float time;
    public UltBar Ultbar;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
     
        
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Ultbar.current >= 100)
            {
                StartCoroutine(PowerUpTimer());
                print("space");
            }
            

        }
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    private IEnumerator PowerUpTimer()
    {
        originalSpeed = speed;
        speed /= 2;

        yield return new WaitForSeconds(time);
        speed = originalSpeed;
    }
}
