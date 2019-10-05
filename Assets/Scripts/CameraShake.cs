using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
  public  IEnumerator Shake (float l_Duration, float l_Magnitude, float l_MaxDistance)
    {
        Vector3 l_OriginalPos = transform.localPosition;
        float l_TimePass = 0;

        while(l_TimePass<l_Duration)
        {
            float x = Random.Range(Mathf.Clamp(-l_MaxDistance + (l_TimePass/l_Duration),-l_MaxDistance,0), Mathf.Clamp(l_MaxDistance - (l_TimePass/l_Duration),0,l_MaxDistance)) * l_Magnitude;
            float y = Random.Range(Mathf.Clamp(-l_MaxDistance + (l_TimePass / l_Duration), -l_MaxDistance, 0), Mathf.Clamp(l_MaxDistance - (l_TimePass / l_Duration), 0, l_MaxDistance)) * l_Magnitude;

            transform.localPosition = new Vector3(x, y, l_OriginalPos.z);

            l_TimePass += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = l_OriginalPos;
    }
}
