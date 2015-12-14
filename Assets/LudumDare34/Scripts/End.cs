using UnityEngine;
using System.Collections;

public class End : MonoBehaviour
{
	public delegate void EndDelegate();
	public static event EndDelegate EndEvent;

	public static void TriggerEndGame() {
		EndEvent();
    }
}
