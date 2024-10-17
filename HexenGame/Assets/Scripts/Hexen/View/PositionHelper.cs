using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionHelper
{
    public static HexGridPos WorldToGrid(Vector3 worldPos)
    {
        int Q = Mathf.RoundToInt((Mathf.Sqrt(3f) / 3f * worldPos.x) - (1f / 3f * worldPos.z));
        int R = Mathf.RoundToInt((2f / 3f * worldPos.z));
        int S = -Q - R;

        return new HexGridPos(Q, R, S);
    }

    public static Vector3 GridToWorld(HexGridPos gridPos)
    {
        float x = (Mathf.Sqrt(3f) * gridPos.Q + Mathf.Sqrt(3f) / 2f * gridPos.R);
        float z = 3f / 2f * gridPos.R;
        float y = 0.1f;

        return new Vector3(x, y, z);
    }
}
