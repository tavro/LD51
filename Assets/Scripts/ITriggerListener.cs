using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerListener
{
	void TriggerEnter(GameObject obj);
	void TriggerStay(GameObject obj);
	void TriggerExit(GameObject obj);
}
