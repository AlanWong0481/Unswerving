using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkScript : MonoBehaviour
{
    Text uiText;
    public Image charImage; //人物立繪
    public Image charImageTwo;
    public Image talkImage; //對話框背景

    int counter = -1;

    public List<string> textArray;

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && counter < textArray.Count - 1) //右鍵繼續
        {
            counter++;
            textDataBase(counter);
        }

        if (Input.GetMouseButtonDown(0) && counter < textArray.Count - 1) //左鍵繼續
        {
            counter--;
            textDataBase(counter);
        }
    }

    void textDataBase(int counter)
    {
        if (counter < 0 || counter >= textArray.Count)
        {
            return;
        }

        uiText.text = textArray[counter];

        if (counter >= textArray.Count - 1)
        {
            charImage.gameObject.SetActive(false);
            charImageTwo.gameObject.SetActive(false);
            talkImage.gameObject.SetActive(false);
        }
    }
}