using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitForSecond : MonoBehaviour
{
    public static waitForSecond Instance { set; get; }

    private void Awake ()
    {
        Instance = this;
    }

    public void delay()
    {
        StartCoroutine(startDelayedAnimation());
    }

    IEnumerator startDelayedAnimation()
    {
        Debug.Log("timeWaitForSecondStarted");

        TurnEnd.Instance.EndTurn();
        yield return new WaitForSeconds(2);
        TurnEnd.Instance.youTurn();

        Debug.Log("timeWaitForSecondEnded");
    } //延遲2秒
}
