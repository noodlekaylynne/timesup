
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
    Coroutine lastRoutine = null;
    public bool isUlt = false;
    public bool buttonPress = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        if (Input.GetKeyDown(KeyCode.Q) || buttonPress == true){
            lastRoutine = StartCoroutine(drainBar());
            buttonPress = false;
            }
        

    }
    public void UltButton()
    {
        buttonPress = true;

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
            {
                isUlt = true;
            }
            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;
                yield return new WaitForSeconds(time / 100);
                if (current > 0)
                {
                    current -= (1);
                }
                
             else if (current == 0)
                {
                    StopCoroutine(lastRoutine);
                    isUlt = false;
                }

            }
        

        }
        }

    }
