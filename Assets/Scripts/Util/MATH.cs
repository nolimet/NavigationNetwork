using UnityEngine;
using System.Collections;

public class MATH {

    public static float speedToAngle(Vector2 v2,float scale)
    {
        float output = 0f;
        v2 = v2 / scale;
        output = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        Debug.Log(output);
        return output;
    }
}
