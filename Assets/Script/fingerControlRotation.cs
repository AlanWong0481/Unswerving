using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerControlRotation : SingletonMonoBehavior<fingerControlRotation>
{
    public GameObject cameraGameObject;
    public Transform miniCameraTransform;
    float onPressYAngle = 0.0f;
    float closestAngle = 0.0f;
   public  bool onpress = false;
    bool startLerpMovement = false;
    float startTime = 0.0f;
    Vector2 testOne;
    Vector2 testTwo;

    public float fingerMoveSpeed ;

    float onPressFloat;

	// Update is called once per frame
	void Update () {
        LerpMove();
        if (miniCameraTransform) {
            miniCameraTransform.rotation = Quaternion.Euler(0,0, transform.rotation.eulerAngles.y); ;
        }
        /*
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width * 0.8)
        { // - 向上  + 向下
        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            startLerpMovement = false;
            testOne = new Vector2(Input.mousePosition.x, 0);
            testTwo = new Vector2(0, Input.mousePosition.y);
            onPressFloat = mouseDistance(testOne, testTwo);
            onPressYAngle = transform.rotation.eulerAngles.y;
            onpress = true;
        }

        if (Input.GetMouseButtonUp(0) ) {
            closestAngle = calibration(transform.rotation.eulerAngles.y);
            startLerpMovement = true;
            startTime = Time.time;
            onpress = false;
        }
        if (onpress) {
            float fixedMouseDistance = (onPressFloat - mouseDistance(testOne, testTwo));
            float allowMouseDelay = 25;
            if ( fixedMouseDistance >= allowMouseDelay) //滑鼠delay判定
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, onPressYAngle + (fixedMouseDistance - allowMouseDelay) * fingerMoveSpeed, transform.rotation.eulerAngles.z);
            }
            else if (fixedMouseDistance <= -allowMouseDelay)
            {

                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, onPressYAngle + (fixedMouseDistance + allowMouseDelay) * fingerMoveSpeed, transform.rotation.eulerAngles.z);
            }

            //transform.rotation = Quaternion.Euler(0, 0,  onPressYAngle + (360 * holdHeightPrecent));
        }
	}

    float mouseDistance(Vector2 axisX, Vector2 axisY)
    {
       return (Vector2.Distance(axisX, Input.mousePosition) + Vector2.Distance(axisY, Input.mousePosition)) / 2;
    }
    
    int calibration(float number) {
        int[] array = { 360,180,0 };
        float[] DistanceArray = new float[array.Length];

        for (int i = 0; i < array.Length; i++) {
            DistanceArray[i] = Mathf.Abs(number - array[i]);
        }

        int smallestNumberIndex = 0;

        for (int i = 0; i < array.Length; i++) {
            if (i != smallestNumberIndex && DistanceArray[smallestNumberIndex] >DistanceArray[i] ) {
                smallestNumberIndex = i ;
            }
        }

        return array[smallestNumberIndex];
    }
    void LerpMove() {
        if (startLerpMovement) {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.Lerp(transform.rotation.eulerAngles.y, closestAngle, (Time.time - startTime) * 1.75f), transform.rotation.eulerAngles.z);
            if (Mathf.Abs(transform.rotation.eulerAngles.y - closestAngle) <= 5.0f) {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, closestAngle, transform.rotation.eulerAngles.z);
                startLerpMovement = false;
            }

        }
    }


}
