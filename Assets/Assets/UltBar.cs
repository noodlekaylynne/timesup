
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UltBar : MonoBehaviour
{
    public int minimum;
    public int maximum;
    public int current;
    public Image mask;
    public float time = 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(drainBar());
        }
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    private IEnumerator drainBar()
    {
        float timeElapsed = 0;
        if (current >= 100) {
            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;
                yield return new WaitForSeconds(time / 4);
                current -= (25);

            }
        }
       
    }

}