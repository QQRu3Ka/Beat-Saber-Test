using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreak
{
    void Break(Side side, Vector3 point, Vector3 normal, Plane plane, GameObject sword);
    void CheckSide(bool isRightSide, Vector3 pointOfHit, float preHitAngle, GameObject cube, GameObject sword);
}
