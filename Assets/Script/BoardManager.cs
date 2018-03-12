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
        selectedChessman = whiteChess[ 0 ];
        gameView.instance.showupPlayerSelectWhatChessman();

    }

    private void Update()
    {
        updateSelection();
        drawChessboard();
        MouseButtonDownAction();
    }

    public void uiMovement(dir dir) {
        if (inAttack) {
            return;
        }
        if (!selectedChessman) {
            return;
        }

        if (selectedChessman.curActionVal <= 0) {
            gameController.instance.OnPlayerSelectedChessmanRunOutActionVal();
            return;
        }

        int x = selectedChessman.CurrentX;
        int y = selectedChessman.CurrentY;
        switch (dir) {
            case dir.up:
                y += 1;
                selectedChessman.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                break;
            case dir.down:
                y -= 1;
                selectedChessman.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case dir.left:
                x -= 1;
                selectedChessman.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case dir.right:
                x += 1;
                selectedChessman.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }

        if (x < 7 && x >= 0 && y < 10 && y >= 0 ) {
            Chessman OverlappingChessman = checkOverlapping(new Vector2(x, y));

            if (OverlappingChessman) {
                if (OverlappingChessman.group == groupEnum.black) {
                    playerChessHitEnemy(OverlappingChessman);
                    return;
                }
                if (OverlappingChessman.group == groupEnum.item) {
                    playerChessHititem(OverlappingChessman);
                }
            }
            
            selectedChessman.curActionVal--;
            gameView.instance.updateActonDisplay();
            generalMove(selectedChessman, new Vector2(x, y));

        }

    } //限制角色移動於場地上的範圍和角色轉向

    public Chessman playerHitChessman;
    public bool inAttack = false;

    public void playerChessHitEnemy(Chessman OverlappingChessman) {
            inAttack = true;
            playerHitChessman = OverlappingChessman;
            selectedChessman.GetComponentInChildren<Animator>().SetTrigger("onAttack"); //SetTrigger在Animator是指提取Animator當中的變數。

            selectedChessman.curActionVal--;
            gameView.instance.updateActonDisplay();
            return;
    } //角色攻擊敵人

    public void playerChessHititem(Chessman OverlappingChessman) {
        playerHitChessman = OverlappingChessman;
        bottle playerHitBottle = OverlappingChessman.gameObject.GetComponent<bottle>();
        playerHitBottle.itemEvent();
        Destroy(OverlappingChessman.gameObject);

        return;
    } //

    public Chessman checkOverlapping(Vector2 v2) {
        foreach (var item in allChess) {
            if (v2 .x== item.CurrentX && v2 .y== item.CurrentY) {
                return item;
            }
        }
        return null;
    } //檢查角色是否重疊

    public void OnPlayerFinishAttack()
    {
        if (selectedChessman.CurrentX == playerHitChessman.CurrentX + 1 && selectedChessman.CurrentY == playerHitChessman.CurrentY) {
            playerHitChessman.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        } else if (selectedChessman.CurrentX == playerHitChessman.CurrentX - 1 && selectedChessman.CurrentY == playerHitChessman.CurrentY) {
            playerHitChessman.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        } else if (selectedChessman.CurrentX == playerHitChessman.CurrentX && selectedChessman.CurrentY == playerHitChessman.CurrentY + 1) {
            playerHitChessman.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (selectedChessman.CurrentX == playerHitChessman.CurrentX && selectedChessman.CurrentY == playerHitChessman.CurrentY - 1) {
            playerHitChessman.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        playerHitChessman.GetComponentInChildren<Animator>().SetTrigger("onAttack");
    } //當角色完成攻擊

    public void OnEnemyFinishAttack() {
        inAttack = false;
    } //當敵人完成攻擊

    public GameObject actionMenu;
    public Vector2 saveChessmanVector2;

    void MouseButtonDownAction()
    {
        if (inAttack) {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            print(new Vector2(selectionX, selectionY));

            if (selectionX >= 0 && selectionY >= 0) {
                selectedChessman = Chessmans[ selectionX , selectionY ];
                if (selectedChessman.group != groupEnum.white) {
                    //選擇對象不是白方
                    return;
                }
                gameView.instance.showupPlayerSelectWhatChessman();
            }
        }
    } //滑鼠點擊

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

    public void moveChessman(int x,int y) //棋子移動
    {
        if (allowedMoves[x,y])
        {
            generalMove(selectedChessman, new Vector2(x, y));
        }

        BoardHighlights.Instance.Hidehighlights();
        selectedChessman = null;
        spawnAndResetMouseEffect(selectionX, selectionY);
    }

    public void attactChessman(int x, int y) //攻擊棋子
    {
        if ( (x == selectedChessman.CurrentX && y == selectedChessman.CurrentY) || Chessmans[x, y] == null)
        {
            return;
        }

        if (Chessmans[x, y].group == groupEnum.white ) //禁止傷害隊友判定
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
        Chessmans[ x, y ].init();
    }

    private void spawnAllChessmans() //生成所有棋子
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[7,10];

        switch (Datebase.instance.sceneLevel)
        {
            case 0 :
                spawnChessman(0, 3, 1);

                spawnChessman(1, 2, 4);

                spawnChessman(2, 2, 0);

                spawnChessman(1, 4, 6);

                spawnChessman(3, 5, 3);

                Debug.Log("test one");
                break;

            case 1:
                spawnChessman(0, 2, 1);

                spawnChessman(1, 2, 4);

                Debug.Log("test two");
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
            if (chessmanItem.group == groupEnum.white)
            {
                whiteChess.Add(chessmanItem);
            }
            else if(chessmanItem.group == groupEnum.black)
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
        Vector3 widthLine = Vector3.right * 7;
        Vector3 heigthLine = Vector3.forward * 10;

        for(int i = 0; i <= 10; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 7; j++)
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
