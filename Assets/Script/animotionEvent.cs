using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animotionEvent : MonoBehaviour {

	public void playerAttack() {
        print("hit");

        Chessman enemy = BoardManager.Instance.playerHitChessman;

        enemy.health -= BoardManager.Instance.selectedChessman.damage;
        if (enemy.health <= 0) {
            BoardManager.Instance.inAttack = false;
        }
        enemy.healthChecker();

   

        BoardManager.Instance.OnPlayerFinishAttack();
        //damageDisplay.instance.spawnDamageDisplay(selectedChessman.damage, 1, OverlappingChessman.gameObject.transform);
    }

    public void enemyAttack() {
        print("hit");

        Chessman playerChessman = BoardManager.Instance.selectedChessman;

        playerChessman.health -= BoardManager.Instance.selectedChessman.damage;

        if (playerChessman.health <= 0) {
            //player died
            gameController.instance.OnPlayerSelectedChessmanDied();
        }
        playerChessman.healthChecker();


        BoardManager.Instance.OnEnemyFinishAttack();
    }

}
