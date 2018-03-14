using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preWin : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "chess" && other.gameObject.GetComponent<Chessman>().group == groupEnum.white) {
            //open
            gameController.instance.winBox.SetActive(true);
        }
    }
}
