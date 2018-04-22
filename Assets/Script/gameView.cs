using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameView : SingletonMonoBehavior<gameView> {

    public GameObject hitEnemyParticle;

    public GameObject skillHealParticle;

    public GameObject skillAttackParticle;

    public GameObject skillDefParticle;

    public GameObject skillArrowParticle;

     public chessmanLerpMove chessmanLerpMove;

    public void init() {
        updateCostDisplay();
    }

    public void updateCostDisplay() {
        // Cost Display
        canvasScript.instance.costText.text = gameModel.instance.cur_Cost.ToString();
    }
    public void updateActonDisplay() {
        canvasScript.instance.updateBarInformation();
        int val = gameModel.instance.getCurrentChessmanActionVal();
        print(gameModel.instance.getCurrentChessmaName() + "的移動值還有" + val + "步");
    }

    public void updateHealthDisplay() {
        canvasScript.instance.updateBarInformation();
        int val = gameModel.instance.getCurrentChessmanHealth();
        print(gameModel.instance.getCurrentChessmaName() + "的血量值還有" + val + "");
    }

    public void showupPlayerSelectWhatChessman() {
        int healthval = gameModel.instance.getCurrentChessmanHealth();
        int attackval = gameModel.instance.getCurrentChessmanDamage();
        int Actionval = gameModel.instance.getCurrentChessmanActionVal();

        canvasScript.instance.updateBarInformation();
        print("你選擇了" + gameModel.instance.getCurrentChessmaName() + "他的血量值有" + healthval + " 他的攻擊值有" + attackval + "  他的移動值有" + Actionval);
    }

    Vector3 orlCamPos;
    Quaternion orlCamRotation;

    public void cameraZoomIn() {
        Transform cameraTs = Camera.main.gameObject.transform;

        orlCamPos = cameraTs.position;
        orlCamRotation = cameraTs.rotation;

        Vector3 startVar = new Vector3(cameraTs.position.x, 1.75f, cameraTs.position.z);
        Quaternion startVar2 = getObjectModelLookAtRotation(startVar, BoardManager.Instance.selectedChessman.gameObject.transform.position);
        startVar += (startVar2 * Vector3.forward);
        lerpMoveTest.startLerp(orlCamPos,startVar,curve, CameraMoveNeedTime);
        quaternionTest.startLerp(orlCamRotation, startVar2, curve, CameraMoveNeedTime,null, reductionCamera);
    }

    public void reductionCamera() {
        Transform cameraTs = Camera.main.gameObject.transform;

        Vector3 nowCamPos = cameraTs.position;
        Quaternion nowCamRotation = cameraTs.rotation;

        lerpMoveTest.startLerp(nowCamPos, orlCamPos, curve, CameraMoveNeedTime);
        quaternionTest.startLerp(nowCamRotation, orlCamRotation, curve, CameraMoveNeedTime,null,null);
    }

    public float CameraMoveNeedTime = 1f;
    public AnimationCurve curve;

    Vector3 startVar;
    Quaternion startVar2;

    Vector3 endVar;
    Quaternion endVar2;

    public Quaternion getObjectModelLookAtRotation(Vector3 objPos, Vector3 lookAtPoint) {
        Vector3 relativePos = lookAtPoint - objPos;
        return Quaternion.LookRotation(relativePos);
    }

    public vector3Lerp lerpMoveTest = new vector3Lerp();
    public QuaternionLerp quaternionTest = new QuaternionLerp();

    private void Update() {
        if (lerpMoveTest.isLerping) {
            Transform cameraTs = Camera.main.gameObject.transform;
            cameraTs.position = lerpMoveTest.update();
            cameraTs.rotation = quaternionTest.update();
        }
    }
}
public class chessmanLerpMove : vector3Lerp {
    int X;
    int Y;
    public chessmanLerpMove(int x, int y) {
        X = x;
        Y = y;
    }

    public override void startExtraCode() {
        BoardManager.Instance.Chessmans[ X, Y ].gameObject.GetComponentInChildren<Animator>().SetBool("onWalk", isLerping);
        gameModel.instance.playerInMovingAni = isLerping;
    }
    public override void endExtraCode() {
        BoardManager.Instance.Chessmans[ X, Y ].gameObject.GetComponentInChildren<Animator>().SetBool("onWalk", isLerping);
        gameModel.instance.playerInMovingAni = isLerping;
    }
}