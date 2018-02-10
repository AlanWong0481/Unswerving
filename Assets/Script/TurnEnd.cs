using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    public static TurnEnd Instance;
    public Transform canvasTransform;
    public GameObject TurnEndPrefab;
    public GameObject youTurnPrefab;

    public int allChessTurn = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void resetWhiteAction()
    {
        BoardManager.Instance.resetWhiteChess();
    } //恢復白方行動

    public void turnRecord()
    {
        allChessTurn++;
    } //記錄每一個回合

    public void EndTurn()
    {
        Instantiate(TurnEndPrefab, canvasTransform);
        GameObject spawnObject = Instantiate(TurnEndPrefab, new Vector3(0, 253, 0), Quaternion.identity) as GameObject;
    } //結束回合，播放turnEndPrefab動畫

    public void youTurn()
    {
       Instantiate(youTurnPrefab, canvasTransform);
       GameObject spawnObjectTwo = Instantiate(youTurnPrefab, new Vector3(0, 253, 0), Quaternion.identity) as GameObject;
    } //我方回合，播放youTurnPrefab動畫

    public void blackChessAi()
    {
        //4,6
        switch (allChessTurn)
        {
            case 2:
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[2, 4], new Vector2(2, 3));
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[4, 6], new Vector2(3, 6));
                allChessTurn ++;
                Debug.Log("action");
                break;

            case 4:
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[2, 3], new Vector2(2, 2));
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[3, 6], new Vector2(2, 6));
                allChessTurn++;
                Debug.Log("action2");
                break;

            case 6:
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[2, 2], new Vector2(1, 2));
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[2, 6], new Vector2(2, 5));
                allChessTurn++;
                Debug.Log("action3");
                break;

            case 8:
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[1, 2], new Vector2(1, 3));
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[2, 5], new Vector2(2, 4));
                allChessTurn++;
                Debug.Log("action4");
                break;

            case 10:
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[1, 3], new Vector2(1, 4));
                BoardManager.Instance.generalMove(BoardManager.Instance.Chessmans[2, 4], new Vector2(2, 5));
                allChessTurn++;
                Debug.Log("action4");
                break;
        }
    } //黑棋AI

    public void aiLevel()
    {
        switch (Datebase.instance.sceneLevel)
        {
            case 0:
                blackChessAi();
            break;
        }
    } //AI等級
}
