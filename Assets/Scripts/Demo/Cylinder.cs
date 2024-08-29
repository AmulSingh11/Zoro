using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public int groupId = -1; // Group ID, -1 means no group
    public float rotationSpeed = 90f; // Rotation speed in degrees per second

    private bool isRotating = false; // Tracks whether rotation is active
    private Vector3 currentRotationDirection;
    private float currentRotationSpeed;

    private Quaternion correctRotation;
    public void SetCorrectRotation(Vector3 correctRotation)
    {
        this.correctRotation = Quaternion.Euler(correctRotation);
    }

    private void Update()
    {
        // Apply rotation continuously while isRotating is true
        if (isRotating)
        {
            transform.Rotate(currentRotationDirection * currentRotationSpeed * Time.deltaTime);
        }
    }

    public void StartRotation(Vector3 rotationDirection, float customRotationSpeed)
    {
        currentRotationDirection = rotationDirection;
        currentRotationSpeed = customRotationSpeed;
        isRotating = true; // Start rotating
    }

    public void StopRotation()
    {
        isRotating = false; // Stop rotating
    }

    public bool IsAligned()
    {
        // Check if the current rotation is close to the correct rotation
        return Quaternion.Angle(transform.rotation,correctRotation) < 10f; 
    }

    public void RotateToCorrectRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.up * 0); // Align to correct rotation
    }
}


