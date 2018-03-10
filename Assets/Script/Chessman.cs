using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public int health;
    public int damage;
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public bool isWhite;
    public bool hasActed;
    public bool hasAttacked;

    public int actionVal = 20;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[5, 8];
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
            Destroy(gameObject);
            BoardManager.Instance.Chessmans[CurrentX, CurrentY] = null;
        }
    }
}
