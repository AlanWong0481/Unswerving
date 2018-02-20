using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gibgibLibrary;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance {set;get;}
    private bool[,] allowedMoves {set; get;}
    private bool[,] allowedAttack {set; get;}
    
    public Chessman[,] Chessmans{set;get;}
    public Chessman selectedChessman;

    public List<Chessman> allChess;
    public List<Chessman> whiteChess;
    public List<Chessman> blackChess;


    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public string status;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion orientation = Quaternion.Euler(0,0,0);

    public GameObject mouseEffectgameobject;

    private void Start()
    {
        Instance = this;
        spawnAllChessmans();
        updateWhiteBlackChessmanData();
    }

    private void Update()
    {
        updateSelection();
        drawChessboard();
        MouseButtonDownAction();
    }

    public GameObject actionMenu;
    public Vector2 saveChessmanVector2;

    void MouseButtonDownAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    openActionMenu();
                    spawnAndResetMouseEffect(selectionX, selectionY);

                } else
                {
                    switch (status)
                    {
                        case "move":
                            moveChessman(selectionX, selectionY);
                            break;

                        case "attack":
                            Debug.Log("hit");
                            Debug.Log(selectedChessman.name);

                            selectedChessman.GetComponentInChildren<Animator>().SetTrigger("onAttack"); //SetTrigger在Animator是指提取Animator當中的變數。
                            attactChessman(selectionX, selectionY);
                            break;
                    }
                }
            }
        }
    } //滑鼠點擊之後執行的事件

    void openActionMenu()
    {
        saveChessmanVector2 = new Vector2(selectionX,selectionY);
        actionMenu.SetActive(true);
    } //開啟行動選單

    void closeActionMenu()
    {
        actionMenu.SetActive(false);
        canvasScript.Static.openAttackButton = false;
        canvasScript.Static.openMoveButton = false;
        status = "";
    } //關閉行動選單

    public void selectChessman(int x, int y, bool isAttack)
    {
        if (Chessmans[x, y] == null)
            return;

        if (Chessmans[x, y].hasActed == true)
        {
            if (isAttack)
            {
                status = "attack";
                allowedAttack = Chessmans[x, y].PossibleAttack();
                selectedChessman = Chessmans[x, y];
                AttackHighlights.Instance.HighlightAllowedAttack(allowedAttack);
            }
        }
        else
        {
            status = "move";
            allowedMoves = Chessmans[x, y].PossibleMove();
            selectedChessman = Chessmans[x, y];
            BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
        }
    } //選擇棋子，條件判斷可否行動

    /// <summary>
    /// 目標chessmans e.g.Chessmans[ 2, 4 ], 你想要的新位置
    /// </summary>
    public void generalMove(Chessman chessmans ,Vector2 newPos)
    {
        int x = (int)newPos.x;
        int y = (int)newPos.y;

        Chessmans[ chessmans.CurrentX, chessmans.CurrentY ] = null; //清空Chessmans array裡的當前位置
        chessmans.transform.position = getTileCenter(x, y); // gameview 改變目標chessmans的坐標(顯示上改變
        chessmans.SetPosition(x, y); //gamemodel 改變目標chessmans的坐標(背景數值上改變
        Chessmans[ x, y ] = chessmans; //安排Chessmans去array裡新的位置
    } //通用移動

    public void moveChessman(int x,int y) //選擇棋子移動
    {
        if (allowedMoves[x,y])
        {
            generalMove(selectedChessman, new Vector2(x, y));
            Chessmans[x, y].hasActed = true;
        }

        BoardHighlights.Instance.Hidehighlights();
        selectedChessman = null;
        spawnAndResetMouseEffect(selectionX, selectionY);

        closeActionMenu();
    }

    public void attactChessman(int x, int y) //攻擊棋子
    {
        if ( (x == selectedChessman.CurrentX && y == selectedChessman.CurrentY) || Chessmans[x, y] == null)
        {
            return;
        }

        if (Chessmans[x, y].isWhite) //禁止傷害隊友判定
        {
            return;
        }

        if (allowedAttack[x, y])
        {
            Debug.Log(x + " "+ y);
            Debug.Log(Chessmans[x,y]);
            Chessmans[x, y].hasAttacked = true;

            if (Chessmans[x,y] != null)
            {
                Chessmans[x, y].health -= selectedChessman.damage;
                Chessmans[x,y].healthChecker();
            }
        }

        damageDisplay.instance.spawnDamageDisplay(selectedChessman.damage, 1, Chessmans[x,y].transform);
        AttackHighlights.Instance.Hidehighlights();
        selectedChessman = null;
    }

    private void updateSelection() //點擊的圖層是否在ChessPlane
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        
       if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 50.0f, LayerMask.GetMask("ChessPlane"))) //場景人物位置
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    public void spawnChessman(int index,int x, int y) //生成棋子
    {
        GameObject go = Instantiate(chessmanPrefabs[index], getTileCenter(x, y), orientation);
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman> ();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    private void spawnAllChessmans() //生成所有棋子
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[5,8];

        switch (Datebase.instance.sceneLevel)
        {
            case 0 :
                spawnChessman(0, 2, 1);

                spawnChessman(1, 2, 4);

                spawnChessman(2, 1, 0);

                spawnChessman(1, 4, 6);
                break;

            case 1:
                spawnChessman(0, 2, 1);

                spawnChessman(1, 2, 4);

                spawnChessman(2, 1, 0);
                break;
        }
    }

    public Vector3 getTileCenter(int x,int y) //捉取格子的中心
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    public void updateWhiteBlackChessmanData() //捉取場地上所有黑白棋子的資料
    {
        allChess = new List<Chessman>();
        whiteChess = new List<Chessman>();
        blackChess = new List<Chessman>();

        foreach (var item in GameObject.FindGameObjectsWithTag("chess"))
        {
            Chessman chessmanItem = item.GetComponent<Chessman>();
            allChess.Add(chessmanItem);
            if (chessmanItem.isWhite)
            {
                whiteChess.Add(chessmanItem);
            }
            else
            {
                blackChess.Add(chessmanItem);
            }
        }
    }

    public void resetWhiteChess()
    {
        Debug.Log("test turn number" + TurnEnd.Instance.allChessTurn);
        if ((TurnEnd.Instance.allChessTurn + 1) % 2 == 0) //取餘數值 括號可分先後次序
        {
            Debug.Log("test2");
            for (int i = 0; i < whiteChess.Count; i++) //所有白方資料框架
            {
                Debug.Log("test3");
                whiteChess[i].hasActed = false;
            }
        }
    } //修改白方已經行動為false

    private void drawChessboard()
    {
        Vector3 widthLine = Vector3.right * 5;
        Vector3 heigthLine = Vector3.forward * 8;

        for(int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 5; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heigthLine);
            }
        }
        
        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1 ) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    } //繪製棋盤

    void spawnAndResetMouseEffect(int X,int Y) {
        destroyOldMouseEffect();
        GameObject newObject = Instantiate(mouseEffectgameobject);
        newObject.transform.position = getTileCenter(X, Y);
    }
    void destroyOldMouseEffect() {
        GameObject olderObject = GameObject.FindGameObjectWithTag("mouseEffect");
        if (olderObject) {
            Destroy(olderObject);
        }
    }
}
