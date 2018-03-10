using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animotionEvent : MonoBehaviour {

	public void playerAttack() {

        Chessman enemy = BoardManager.Instance.playerHitChessman;

        enemy.health -= BoardManager.Instance.selectedChessman.damage;
        damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.selectedChessman.damage, 0, enemy.gameObject.transform);

        if (enemy.health <= 0) {
            BoardManager.Instance.inAttack = false;
        }
        enemy.healthChecker();

   

        BoardManager.Instance.OnPlayerFinishAttack();
    }

    public void enemyAttack() {

        Chessman playerChessman = BoardManager.Instance.selectedChessman;

        playerChessman.health -= BoardManager.Instance.playerHitChessman.damage;
        gameView.instance.updateHealthDisplay();
        damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.playerHitChessman.damage,1, playerChessman.gameObject.transform);

        if (playerChessman.health <= 0) {
            //player died
            gameController.instance.OnPlayerSelectedChessmanDied();
        }
        playerChessman.healthChecker();


        BoardManager.Instance.OnEnemyFinishAttack();
    }

}
