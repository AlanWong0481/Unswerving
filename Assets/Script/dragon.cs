using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : Chessman {
    public int countDown;
    public int curCountDown;

    public void doCountDown() {
        curCountDown++;
        countCountDown();
    }

    public void countCountDown() {
        if (curCountDown >= countDown) {
            action();
            curCountDown = 0;
        }
    }

    public void action() {
        BoardManager.Instance.inAttack = true;
        GetComponentInChildren<Animator>().SetTrigger("onSkill");
    }

}
