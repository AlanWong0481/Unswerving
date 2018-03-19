using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameView : SingletonMonoBehavior<gameView> {

    public GameObject hitEnemyParticle;

    public GameObject skillHealParticle;



    public void init()
    {

    }

    public void updateCostDisplay() {
        // Cost Display
        print(gameModel.instance.cur_Cost);
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

    public Vector3 orlCamPos;
    public Quaternion orlCamRotation;

    public void cameraZoomIn() {
        Transform cameraTs = Camera.main.gameObject.transform;

        orlCamPos = cameraTs.position;
        orlCamRotation = cameraTs.rotation;

        Vector3 startVar = new Vector3(cameraTs.position.x, 1.75f, cameraTs.position.z);
        Quaternion startVar2 = getObjectModelLookAtRotation(startVar, BoardManager.Instance.selectedChessman.gameObject.transform.position);
        startVar += (startVar2 * Vector3.forward);

        startLerp(orlCamPos,orlCamRotation,startVar,startVar2);
    }

    public void reductionCamera() {
        Transform cameraTs = Camera.main.gameObject.transform;

        Vector3 nowCamPos = cameraTs.position;
        Quaternion nowCamRotation = cameraTs.rotation;

        startLerp(nowCamPos, nowCamRotation, orlCamPos, orlCamRotation);

    }

    public bool isLerping = false;
    float moveNeedTime = 0.5f;
    public Vector3 startVar;
    public Quaternion startVar2;

    public Vector3 endVar;
    public Quaternion endVar2;
    float lerpTime;

    public void startLerp(Vector3 startV3,Quaternion startRotaion,Vector3 endV3, Quaternion endRotaion) {
        if (isLerping) {
            return;
        }
        isLerping = true;
        startVar = startV3;
        startVar2 = startRotaion;

        endVar = endV3;
        endVar2 = endRotaion;
        lerpTime = 0;
    }

    public void endLerp() {
        isLerping = false;
    }

    public void lerpMove(Transform cameraTs) {
        lerpTime += Time.deltaTime / moveNeedTime;

        cameraTs.position = Vector3.Slerp(startVar,endVar,lerpTime);
        cameraTs.rotation = Quaternion.Slerp(startVar2,endVar2,lerpTime);

        if (lerpTime >= 1) {
            endLerp();
        }
    }

    public Quaternion getObjectModelLookAtRotation(Vector3 objPos, Vector3 lookAtPoint) {
        Vector3 relativePos = lookAtPoint - objPos;
        return Quaternion.LookRotation(relativePos);
    }

    private void Update() {
        if (isLerping) {
            print("fo");
            lerpMove(Camera.main.gameObject.transform);
        }
    }
}
