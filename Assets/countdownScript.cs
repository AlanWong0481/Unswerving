using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class countdownScript : SingletonMonoBehavior<countdownScript> {
    public Image countDownImageCom;
    public Sprite[] Sprite;

	// Use this for initialization
	void Start () {
        countDownImageCom = GetComponent<Image>();
	}
	
    public void updateCountDownImage() {
        countDownImageCom.sprite = Sprite[ BoardManager.Instance.dragon.countDown - BoardManager.Instance.dragon.curCountDown ];
    }
}
