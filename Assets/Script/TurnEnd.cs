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
}
