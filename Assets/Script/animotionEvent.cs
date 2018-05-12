using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animotionEvent : MonoBehaviour {

	public void playerAttack() {
        print("playerAttack");
        Chessman enemy = BoardManager.Instance.playerHitChessman;
        if (gameView.instance.hitEnemyParticle) {
            Instantiate(gameView.instance.hitEnemyParticle, enemy.gameObject.transform.position, Quaternion.identity);
        }
        gameController.instance.DamageChassman(enemy, BoardManager.Instance.selectedChessman.damage);

        if (enemy.health <= 0) {
            BoardManager.Instance.inAttack = false;
        }



        /*
         if (gameView.instance.hitEnemyParticle) {
            Instantiate(gameView.instance.hitEnemyParticle, target.gameObject.transform.position, Quaternion.identity);
        }
         */
        //enemy.GetComponentInChildren<animotionEvent>().hit();

        //damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.selectedChessman.damage, 0, enemy.gameObject.transform);

        BoardManager.Instance.OnPlayerFinishAttack();
    } //玩家打人

    public void dragonSkill() {

        foreach (var item in BoardManager.Instance.whiteChess) {
            if (!item) {
                continue;
            }
            Vector2 v2 = BoardManager.Instance.getChessmanV2(item);
            bool hideInPillar = false;
            foreach (var pillarItem in BoardManager.Instance.pillarChessV2) {
                if (!(v2.x != pillarItem.x || v2.y+1 != pillarItem.y)) {
                    hideInPillar = true;
                    break;
                }
            }
            if (!hideInPillar && item.health > 0) {
                bool isSelectedChessman = false;
                if (BoardManager.Instance.selectedChessman == item) {
                    isSelectedChessman = true;
                }
                item.health -= BoardManager.Instance.dragon.skillDamage;
                damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.dragon.skillDamage, 1, item.gameObject.transform);

                item.healthChecker();
                if (isSelectedChessman) {
                    gameController.instance.thisRoundsPlayerTakeDamage = BoardManager.Instance.dragon.skillDamage;
                    if (item.health <= 0 ) {
                        gameController.instance.OnPlayerSelectedChessmanDied();
                    }
                }

            }
        }
    }

    public void dragonSkillFinish() {
        BoardManager.Instance.inAttack = false;
    }

    public void skill() {
        switch (BoardManager.Instance.selectedChessman.id) {
            case 0:
                attackSkill();
                break;
            case 2:
                healthSkill();
                break;
            case 7:
                defUpSkill();
                break;
            case 8:
                arrowSkill();
                break;
        }
        //gameView.instance.reductionCamera();
    } //技能，根據數據庫ID置入
    
    public void healParticle() {
        if (gameView.instance.skillHealParticle) {
            Instantiate(gameView.instance.skillHealParticle, BoardManager.Instance.selectedChessman.gameObject.transform.position, Quaternion.identity);
        }
    } //補血特效

    public void arrowSkill()
    {
        Vector2 v2 = new Vector2(BoardManager.Instance.selectedChessman.CurrentX, BoardManager.Instance.selectedChessman.CurrentY);
        List<Vector2> arrowV2List = new List<Vector2>();
        arrowV2List.Add(new Vector2(v2.x, v2.y + 1));
        arrowV2List.Add(new Vector2(v2.x, v2.y + 2));
        arrowV2List.Add(new Vector2(v2.x, v2.y + 3));
        arrowV2List.Add(new Vector2(v2.x, v2.y + 4));
        arrowV2List.Add(new Vector2(v2.x, v2.y + 5));
        arrowV2List.Add(new Vector2(v2.x, v2.y + 6));
        arrowV2List.Add(new Vector2(v2.x, v2.y + 7));

        if (gameView.instance.skillArrowParticle)
        {
            Instantiate(gameView.instance.skillArrowParticle, BoardManager.Instance.selectedChessman.gameObject.transform.position, Quaternion.identity);

        }
        foreach (var item in arrowV2List)
        {
            int x = (int)item.x;
            int y = (int)item.y;
            if (!BoardManager.Instance.isInBoardRange(x, y))
            {
                continue;
            }
            if (!BoardManager.Instance.Chessmans[x, y])
            {
                continue;
            }
            if (BoardManager.Instance.Chessmans[x, y].group == groupEnum.black)
            {
                //attackable
                gameController.instance.DamageChassman(BoardManager.Instance.Chessmans[x, y], BoardManager.Instance.selectedChessman.skillDamage);

            }
        }
    } //弓箭技能攻擊範圍

    public void defUpSkill() {
        Vector2 v2 = new Vector2(BoardManager.Instance.selectedChessman.CurrentX, BoardManager.Instance.selectedChessman.CurrentY);
        List<Vector2> defV2List = new List<Vector2>();
        defV2List.Add(new Vector2(v2.x - 1, v2.y - 1));
        defV2List.Add(new Vector2(v2.x, v2.y - 1));
        defV2List.Add(new Vector2(v2.x + 1, v2.y - 1));
        defV2List.Add(new Vector2(v2.x - 1, v2.y));
        defV2List.Add(new Vector2(v2.x + 1, v2.y));
        defV2List.Add(new Vector2(v2.x - 1, v2.y + 1));
        defV2List.Add(new Vector2(v2.x, v2.y + 1));
        defV2List.Add(new Vector2(v2.x + 1, v2.y + 1));
        if (gameView.instance.skillDefParticle)
        {
            Instantiate(gameView.instance.skillDefParticle, BoardManager.Instance.selectedChessman.gameObject.transform.position, Quaternion.identity);

        }
        foreach (var item in defV2List)
        {
            int x = (int)item.x;
            int y = (int)item.y;
            if (!BoardManager.Instance.isInBoardRange(x, y)) {
                continue;
            }
            if (!BoardManager.Instance.Chessmans[ x, y ]) {
                continue;
            }
            if (BoardManager.Instance.Chessmans[ x, y ].group == groupEnum.white) {
                BoardManager.Instance.Chessmans[ x, y ].def += 10;
            }
        }


    } //防禦技能範圍

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
        if (gameView.instance.skillAttackParticle) {
            Instantiate(gameView.instance.skillAttackParticle, BoardManager.Instance.selectedChessman.gameObject.transform.position, Quaternion.identity);
        }

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
                gameController.instance.DamageChassman(BoardManager.Instance.Chessmans[ x, y ], BoardManager.Instance.selectedChessman.skillDamage);
                
            }
        }
    } //男主技能攻擊範圍

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
    } //補血技能範圍

    public void hit() {
        Chessman thisChessman = transform.parent.parent.gameObject.GetComponent<Chessman>();
        //GetComponent<Animator>().SetTrigger("onGetHit");
        if (thisChessman.group == groupEnum.white) {
            Chessman playerChessman = BoardManager.Instance.playerHitChessman;
            playerChessman.gameObject.GetComponentInChildren<Animator>().SetTrigger("onGetHit");
        } else {
            Chessman playerChessman = BoardManager.Instance.selectedChessman;
            playerChessman.gameObject.GetComponentInChildren<Animator>().SetTrigger("onGetHit");
        }
    } //被打

    public void enemyAttack() {
        print("dosadjlk");
        Chessman playerChessman = BoardManager.Instance.selectedChessman;
        //playerChessman.GetComponentInChildren<animotionEvent>().hit();
        int takeDamage = 0;

        if (playerChessman.def > 0) {
            if (playerChessman.def- BoardManager.Instance.playerHitChessman.damage >= 0) {
                playerChessman.def -= BoardManager.Instance.playerHitChessman.damage;
                takeDamage = 0;
            } else {
                takeDamage = BoardManager.Instance.playerHitChessman.damage - playerChessman.def;
                print(takeDamage);
                int abs = Mathf.Abs( playerChessman.def -= BoardManager.Instance.playerHitChessman.damage);
                playerChessman.def = 0;
            }
        } else {
            takeDamage = BoardManager.Instance.playerHitChessman.damage;
        }

        playerChessman.health -= takeDamage;
        damageDisplay.instance.spawnDamageDisplay(takeDamage, 1, playerChessman.gameObject.transform);

        gameController.instance.thisRoundsPlayerTakeDamage = BoardManager.Instance.playerHitChessman.damage;

        gameView.instance.updateHealthDisplay();
        if (playerChessman.health <= 0) {
            //player died
            gameController.instance.OnPlayerSelectedChessmanDied();
        }
        
        playerChessman.healthChecker();

        audioEffectScript.instance.attackEnemy();

        BoardManager.Instance.OnEnemyFinishAttack();
    } //敵人攻擊

}
