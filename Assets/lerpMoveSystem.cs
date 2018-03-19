using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//這是為unity的lerp系統加以開發的系統，用者要先命名一個class承繼lerpMoveSystem，再把d oPart(ref Vector3 startVar, ref Vector3 endVar, ref float curvedValue) override掉如下例子
//然後把update放入每幀都會更新的程式碼 例：void Update()就可

public class vector3Lerp : lerpMoveSystem<Vector3> {
    public Vector3 var;
    public override Vector3 doPart(ref Vector3 startVar, ref Vector3 endVar, ref float curvedValue) {
        var = Vector3.Lerp(startVar, endVar, curvedValue);
        return var;
    }
    public override void startExtraCode() {
    }
    public override void endExtraCode() {
    }
}

public class QuaternionLerp : lerpMoveSystem<Quaternion> {
    public Quaternion var;
    public override Quaternion doPart(ref Quaternion startVar, ref Quaternion endVar, ref float curvedValue) {
        var = Quaternion.Slerp(startVar, endVar, curvedValue);
        return var;
    }
    public override void startExtraCode() {
    }
    public override void endExtraCode() {
    }
}

public abstract class lerpMoveSystem<T> {
    public bool isLerping = false;
    public float moveNeedTime = 0f;
    AnimationCurve aniCurve;

    T startVar;
    T endVar;

    public float lerpTime;

    public void startLerp(T start, T end, AnimationCurve curve, float moveTime) {
        if (isLerping) {
            return;
        }
        isLerping = true;
        startVar = start;
        endVar = end;
        aniCurve = curve;
        moveNeedTime = moveTime;
        lerpTime = 0;
        startExtraCode();
    }
    public void startLerp(T start, T end, float moveTime) {
        if (isLerping) {
            return;
        }
        isLerping = true;
        startVar = start;
        endVar = end;
        moveNeedTime = moveTime;
        lerpTime = 0;
        startExtraCode();
    }

    void endLerp() {
        isLerping = false;
        endExtraCode();
    }

    public abstract void startExtraCode();
    public abstract void endExtraCode();

    public abstract T doPart(ref T startVar, ref T endVar, ref float curvedValue);

    public T update() {
        if (!isLerping) {
            return default(T);
        }
        T outPutData;

        lerpTime += Time.deltaTime / moveNeedTime;
        float curvedValue = lerpTime;
        if (aniCurve != null) {
            curvedValue = aniCurve.Evaluate(lerpTime);
        }

        //do part
        outPutData = doPart(ref startVar, ref endVar, ref curvedValue);

        if (lerpTime >= 1) {
            endLerp();
        }
        return outPutData;
    }
}