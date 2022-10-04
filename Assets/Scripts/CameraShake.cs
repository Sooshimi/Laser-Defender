using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;
    
    Vector3 initialPosition; // initial position of camera to reset camera back to after screenshake

    void Start()
    {
        initialPosition = transform.position; // gets current position of camera
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0;

        while (elapsedTime < shakeDuration)
        {
            // moving camera to a random position
            // 'insideUnitCircle' is a Vector2, but 'position' is a Vector3, so we cast 'insideUnitCircle' as a Vector3
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude; 
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition; // resets camera position
    }
}
