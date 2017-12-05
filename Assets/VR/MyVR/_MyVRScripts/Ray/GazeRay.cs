using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeRay : EmitRay {
    public GazePoint gazePoint;
    protected override void Update()
    {
        if (EmitRayAndGetObj())
        {
            gazePoint.SetGazePointActive(true);
            gazePoint.SetPosition(currentHit.point);
        }
    }
}
