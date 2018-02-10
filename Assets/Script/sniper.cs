using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniper : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[5, 8];
        List<Vector2> v2Array = new List<Vector2>();

        v2Array.Add(new Vector2((int)(transform.position.x - 0.5f), (int)(transform.position.z - 0.5f)));
        v2Array.Add(new Vector2((int)(transform.position.x - 0.5f), (int)(transform.position.z - 0.5f) + 1));
        v2Array.Add(new Vector2((int)(transform.position.x - 0.5f), (int)(transform.position.z - 0.5f) - 1));
        v2Array.Add(new Vector2((int)(transform.position.x - 0.5f) - 1, (int)(transform.position.z - 0.5f)));
        v2Array.Add(new Vector2((int)(transform.position.x - 0.5f) + 1, (int)(transform.position.z - 0.5f)));

        foreach (var item in v2Array)
        {
            if (moveLimit(item))
            {
                r[(int)item.x, (int)item.y] = true;
            }
        }
        return r;
    }

}
