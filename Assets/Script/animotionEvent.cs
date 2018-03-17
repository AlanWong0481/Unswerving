using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animotionEvent : MonoBehaviour {

	public void playerAttack() {
        print("playerAttack");
        Chessman enemy = BoardManager.Instance.playerHitChessman;

        enemy.health -= BoardManager.Instance.selectedChessman.damage;
        if (gameView.instance.hitEnemyParticle) {
            Instantiate(gameView.instance.hitEnemyParticle, enemy.gameObject.transform.position, Quaternion.identity);
        }
        damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.selectedChessman.damage, 0, enemy.gameObject.transform);

        if (enemy.health <= 0) {
            BoardManager.Instance.inAttack = false;
        }
        enemy.healthChecker();

        BoardManager.Instance.OnPlayerFinishAttack();
    }

    public void skill() {
        print(BoardManager.Instance.selectedChessman.id);
        switch (BoardManager.Instance.selectedChessman.id) {
            case 0:
                attackSkill();
                break;
            case 2:
                healthSkill();
                break;
        }
    }

        
    public void healParticle() {
        if (gameView.instance.skillHealParticle) {
            Instantiate(gameView.instance.skillHealParticle, BoardManager.Instance.selectedChessman.gameObject.transform.position, Quaternion.identity);
        }
    }

    public void attackSkill() {
        Vector2 v2 = new Vector2(BoardManager.Instance.selectedChessman.CurrentX, BoardManager.Instance.selectedChessman.CurrentY);
        List<Vector2> attackV2List = new List<Vector2>();
        attackV2List.Add(new Vector2(v2.x - 1, v2.y - 1));
        attackV2List.Add(new Vector2(v2.x, v2.y - 1));
        attackV2List.Add(new Vector2(v2.x + 1, v2.y - 1));
        attackV2List.Add(new Vector2(v2.x - 1, v2.y));
        attackV2List.Add(new Vector2(v2.x + 1, v2.y));
        attackV2List.Add(new Vector2(v2.x - 1, v2.y + 1));
        attackV2List.Add(new Vector2(v2.x, v2.y + 1));
        attackV2List.Add(new Vector2(v2.x + 1, v2.y + 1));

        foreach (var item in attackV2List) {
            int x = (int)item.x;
            int y = (int)item.y;
            if (!BoardManager.Instance.isInBoardRange(x, y)) {
                continue;
            }
            if (!BoardManager.Instance.Chessmans[ x, y ]) {
                continue;
            }
            if (BoardManager.Instance.Chessmans[ x, y ].group == groupEnum.black) {
                //attackable
                gameController.instance.DamageChassman(BoardManager.Instance.Chessmans[ x, y ]);
            }
        }
    }

    public void healthSkill() {
        Vector2 v2 = new Vector2(BoardManager.Instance.selectedChessman.CurrentX, BoardManager.Instance.selectedChessman.CurrentY);
        List<Vector2> healthV2List = new List<Vector2>();
        healthV2List.Add(new Vector2(v2.x - 1, v2.y - 1));
        healthV2List.Add(new Vector2(v2.x, v2.y - 1));
        healthV2List.Add(new Vector2(v2.x + 1, v2.y - 1));
        healthV2List.Add(new Vector2(v2.x - 1, v2.y));
        healthV2List.Add(new Vector2(v2.x + 1, v2.y));
        healthV2List.Add(new Vector2(v2.x - 1, v2.y + 1));
        healthV2List.Add(new Vector2(v2.x, v2.y + 1));
        healthV2List.Add(new Vector2(v2.x + 1, v2.y + 1));

        foreach (var item in healthV2List) {
            int x = (int)item.x;
            int y = (int)item.y;
            if (!BoardManager.Instance.isInBoardRange(x, y)) {
                continue;
            }
            if (!BoardManager.Instance.Chessmans[ x, y ]) {
                continue;
            }
            if (BoardManager.Instance.Chessmans[ x, y ].group == groupEnum.white) {
                //healthable
                gameController.instance.healthChassman(BoardManager.Instance.Chessmans[ x, y ]);
            }
        }
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
