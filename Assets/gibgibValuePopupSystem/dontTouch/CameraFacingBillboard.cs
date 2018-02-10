using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
    public int defaultDistance;


	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);

        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float fixedScale = distance / defaultDistance;
        ////Debug.Log(distance);
        transform.localScale = new Vector3(fixedScale, fixedScale, fixedScale);
    }
}
