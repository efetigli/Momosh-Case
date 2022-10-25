using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandcuffAnimation : MonoBehaviour
{
    public Vector3 EndPosition;
    public Vector3 StartPosition;
    public bool IsStartMoving;

    public AnimationCurve curve;
    float time;
    public float curveAngle;

    private GameObject HandcuffStack;

    void Start()
    {
        this.transform.position = StartPosition;

        HandcuffStack = GameObject.Find("/Player/Body/Handcuff Stack");

        Keyframe[] ks = new Keyframe[3];

        ks[0] = new Keyframe(0, 0);
        ks[0].inTangent = curveAngle;    // -5 units on the y axis for 1 unit on the x axis.

        ks[1] = new Keyframe(0.5f, curveAngle);
        ks[1].inTangent = 0f;    // straight

        ks[2] = new Keyframe(1, 0);
        ks[2].outTangent = curveAngle;    // +5 units on the y axis for 1 unit on the x axis.

        curve = new AnimationCurve(ks);
    }

    void Update()
    {
        ParaboleMovement();
    }

    private void ParaboleMovement()
    {
        time += Time.deltaTime;
        EndPosition = HandcuffStack.GetComponent<HandcuffStack>().HandcuffGlobalPosition;
        Vector3 pos = Vector3.Lerp(StartPosition, EndPosition, time);
        pos.y += curve.Evaluate(time);
        transform.position = pos;

        if (transform.position == EndPosition)
        {
            time = 0;
            this.GetComponent<HandcuffAnimation>().enabled = false;
        }
    }
}
