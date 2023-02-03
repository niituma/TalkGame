using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSample : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

    }
    private void Update()
    {
        var e = RotateAsync();
        if (e.MoveNext())
        {
            e.MoveNext();
        }
    }
    private IEnumerator RotateAsync()
    {
        Debug.Log("RotateAsync");

        while (true)
        {
            transform.Rotate(0, 1, 0);
            yield return null;
        }
    }
}
