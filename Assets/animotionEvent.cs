using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animotionEvent : MonoBehaviour {

	public void playerAttack() {
        print("hit");

        Chessman enemy = BoardManager.Instance.playerHitChessman;

        enemy.health -= BoardManager.Instance.selectedChessman.damage;
        enemy.healthChecker();
        //damageDisplay.instance.spawnDamageDisplay(selectedChessman.damage, 1, OverlappingChessman.gameObject.transform);
    }
}
