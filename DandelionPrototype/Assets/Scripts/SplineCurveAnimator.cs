using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineCurveAnimator : MonoBehaviour
{
    public SplineContainer spline;
    public float animationSpeed = 1.0f;
    public Vector3 animationMagnitude;

    [SerializeField] BezierKnot[] controlPoints;
    private float time = 0f;

    private Vector3 localPos;

    public float waveLength = 1.0f; // Default value, adjust as needed

    void Start()
    {
        // Get control points from the spline
        controlPoints = spline.Spline.ToArray();

        localPos = this.gameObject.transform.localPosition;
    }

    /*
    var knot0 = MySpline.Spline.ToArray()[0];

    knot0.Position = MySpline.transform.InverseTransformPoint(KnotTarget.position);

knot0.Rotation = Quaternion.Inverse(MySpline.transform.rotation)* KnotTarget.rotation;

    MySpline.Spline.SetKnot(0, knot0);

} */

    void Update()
    {
        this.transform.localPosition = localPos;
        // Animate control points
        for (int i = 1; i < controlPoints.Length; i++)
        {
            //Vector3 newPosition = controlPoints[i].Position;
            BezierKnot currKnot = controlPoints[i];        //get the knot
            //Transform currnot
            currKnot.Position.y = Mathf.Sin(Time.time * animationSpeed + i) * animationMagnitude.y;
            //currKnot.Position.x = controlPoints[i].Position.x;
            currKnot.Position.x = Mathf.Sin(Time.time * animationSpeed + i) * animationMagnitude.x;
            //currKnot.Position.z = controlPoints[i].Position.z;
            //currKnot.Position.z = Mathf.Sin(Time.time * animationSpeed + i) * animationMagnitude.z;


            //Vector3 currKnot.Position = controlPoints[i].Position;
            //currentPosition.y = Mathf.Sin(Time.time * animationSpeed + i) * animationMagnitude;
            //controlPoints[i].Position = currentPosition;
            spline.Spline.SetKnot(i, currKnot);
        }
    }
}