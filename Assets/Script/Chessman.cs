using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum groupEnum {
    white,
    black,
    other,
    item
}

public class Chessman : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public int maxHealth;
    [HideInInspector]
    public int health;
    public int damage;
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public groupEnum group;
    public bool hasActed;
    public bool hasAttacked;

    public int ActionVal = 20;
    public int curActionVal = 0;

    public void init() {
        resetActionVal();
    }

    public void resetActionVal() {
        curActionVal = ActionVal;
    }

    public void resetHealthVal() {
        health = maxHealth;
    }

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[7, 10];
    }

    public bool moveLimit(Vector2 v2) //移動限制
    {
        //Debug.Log(v2);
        if ((v2.x > 4 || v2.x < 0) || (v2.y > 7 || v2.y < 0))
        {
            return false;
        }
        return true;
    }

    public virtual bool[,] PossibleAttack()
    {
        return new bool[5, 8];
    }

    public bool AttackLimit(Vector2 v3)
    {
        //mouse
        //Debug.Log(v3);
        if ((v3.x > 4 || v3.x < 0) || (v3.y > 7 || v3.y < 0))
        {
            return false;
        }
        return true;
    } //攻擊的極限距離

    public void healthChecker() //血量檢查
    {
        if (health <= 0)
        {
            //Destroy(gameObject);
            GetComponentInChildren<Animator>().SetTrigger("onLose");
            BoardManager.Instance.Chessmans[CurrentX, CurrentY] = null;
            Destroy(this);
        }
    }

    public bool isMoving = false;
    float moveNeedTime = 0.5f;
    public Vector3 startV3;
    public Vector3 endV3;
    float lerpTime;

    public void startAutoMovement(Vector3 toWhere) {
        isMoving = true;
        GetComponentInChildren<Animator>().SetBool("onWalk",isMoving);
        gameModel.instance.playerInMovingAni = isMoving;
        startV3 = gameObject.transform.position;
        endV3 = toWhere;
        lerpTime = 0;
    }

    public void endAutoMovement() {
        isMoving = false;
        GetComponentInChildren<Animator>().SetBool("onWalk", isMoving);
        gameModel.instance.playerInMovingAni = isMoving;
    }

    public void lerpMove() {
        lerpTime += Time.deltaTime / moveNeedTime;
        gameObject.transform.position = Vector3.Lerp(startV3,endV3,lerpTime);
        if (lerpTime >= 1) {
            endAutoMovement();
        }
    }

    private void Update() {
        if (isMoving) {
            lerpMove();
        }
    }
}
