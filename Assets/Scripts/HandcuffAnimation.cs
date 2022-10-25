using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandcuffAnimation : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPosition;
    Vector3 EndPosition;
    bool IsStartMoving;

    AnimationCurve curve;
    float time;
    float curveAngle;

    GameObject HandcuffStack;
    GameObject WhichCriminal;

    void Start()
    {
        this.transform.position = StartPosition;

        HandcuffStack = GameObject.Find("/Player/Body/Handcuff Stack");

        #region Set Curve
        Keyframe[] ks = new Keyframe[3];

        ks[0] = new Keyframe(0, 0);
        ks[0].inTangent = curveAngle;    // -5 units on the y axis for 1 unit on the x axis.

        ks[1] = new Keyframe(0.5f, curveAngle);
        ks[1].inTangent = 0f;    // straight

        ks[2] = new Keyframe(1, 0);
        ks[2].outTangent = curveAngle;    // +5 units on the y axis for 1 unit on the x axis.

        curve = new AnimationCurve(ks);
        #endregion
    }

    void Update()
    {
        PreparationParaboleMovement();
        ParaboleMovement();
        FinishParaboleMovement();
    }

    private void PreparationParaboleMovement()
    {
        if (IsStartMoving)
            return;

        WhichCriminal = HandcuffStack.GetComponent<HandcuffStack>().CapturedCriminal;
        // Reset captured criminal report in HandcuffStack.
        HandcuffStack.GetComponent<HandcuffStack>().CapturedCriminal = null;
        IsStartMoving = true;
    }

    private void ParaboleMovement()
    {
        time += Time.deltaTime;
        EndPosition = HandcuffStack.GetComponent<HandcuffStack>().HandcuffsGlobalPostion(WhichCriminal);
        Vector3 pos = Vector3.Lerp(StartPosition, EndPosition, time);
        pos.y += curve.Evaluate(time);
        transform.position = pos;
    }

    private void FinishParaboleMovement()
    {
        if (transform.position != EndPosition)
            return;

        // Reset values
        time = 0;
        IsStartMoving = false;
        WhichCriminal = null;
        this.GetComponent<HandcuffAnimation>().enabled = false;
    }
}
