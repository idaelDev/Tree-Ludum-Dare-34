using UnityEngine;
using System.Collections;

public class Pastille : MonoBehaviour {

    public PastilleType type;

    public delegate void PastilleGrab_delegate(PastilleType type);
    public event PastilleGrab_delegate PastilleGrabEvent;

}

public enum PastilleType
{
    FLOWER,
    ANIMAL,
    FRUIT,
    BRANCH
}
