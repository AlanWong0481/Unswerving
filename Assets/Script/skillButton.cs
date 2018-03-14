using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillButton : MonoBehaviour {

	public void OnClick() {
        if (gameModel.instance.playerInMovingAni) {
            return;
        }
        gameController.instance.OnPlayerClickSkillButton();
    }
}
