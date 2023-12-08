using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [Header("Finish UI Var")]
    public GameObject finishUI;
    public GameObject playerUI;
    public GameObject playerCar;

    [Header("Win/Lose Status")]
    public Text status;

    private void Start()
    {
        StartCoroutine(waitforthefinishUI());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(finishZoneTimer());
            gameObject.GetComponent<BoxCollider>().enabled = false;

            status.text = "You Win";
            status.color = Color.green;
            
        }
        else if (other.gameObject.tag == "OpponentCar")
        {
            StartCoroutine(finishZoneTimer());
            gameObject.GetComponent<BoxCollider>().enabled = false;

            status.text = "You Lose";
            status.color = Color.red;
           
        }
    }

    IEnumerator waitforthefinishUI()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(20f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator finishZoneTimer()
    {
        
        finishUI.SetActive(true);
        playerUI.SetActive(false);
        playerCar.SetActive(false);

        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
    }
}
