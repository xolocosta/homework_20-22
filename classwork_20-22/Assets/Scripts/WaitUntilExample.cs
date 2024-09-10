using UnityEngine;
using System.Collections;

public class WaitUntilExample : MonoBehaviour
{
    public int frame;

    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        Debug.Log("Waiting for princess to be rescued...");
        yield return new WaitUntil(() => frame >= 10);
        Debug.Log("Princess was rescued!");
    }

    void Update()
    {
        if (frame <= 10)
        {
            Debug.Log("Frame: " + frame);
            frame++;
        }
    }
}