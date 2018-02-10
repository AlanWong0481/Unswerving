using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour { //controller係負責比指命比其他程式做野。例如初始化則可以由它進行。

	void Start ()
    {
        TurnEnd.Instance.youTurn();
	}
}
