using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHighlights : MonoBehaviour
{
    public static AttackHighlights Instance { set; get; }

    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        GameObject attack = highlights.Find(g => !g.activeSelf);

        if (attack == null)
        {
            attack = Instantiate(highlightPrefab);
            highlights.Add(attack);
        }
        return attack;
    }

    public void HighlightAllowedAttack
        (bool[,] attack)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (attack[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void Hidehighlights()
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }
}
