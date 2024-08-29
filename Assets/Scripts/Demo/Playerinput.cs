using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public AudioManager audioManager;
    public float rotationSpeed = 90f; // Rotation speed in degrees per second

    private Cylinder activeCylinder = null; // Currently rotating cylinder

    private void Update()
    {
        // Detect mouse input (or tap) for rotation
        if (Input.GetMouseButtonDown(0)) // Left click/tap begins rotation
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Cylinder clickedCylinder = hit.collider.GetComponent<Cylinder>();

                if (clickedCylinder != null)
                {
                    // Start rotating the clicked cylinder or its group
                    StartRotation(clickedCylinder);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) // Release click/tap ends rotation
        {
            StopRotation();
        }
    }

    private void StartRotation(Cylinder clickedCylinder)
    {
        // Rotate the entire group if the cylinder is part of one
        if (clickedCylinder.groupId != -1)
        {
            List<Cylinder> group = puzzleManager.GetCylindersByGroup(clickedCylinder.groupId);
            foreach (Cylinder cylinder in group)
            {
                cylinder.StartRotation(Vector3.up, rotationSpeed); // Start rotating the entire group
            }
        }
        else
        {
            clickedCylinder.StartRotation(Vector3.up, rotationSpeed); // Start rotating the clicked cylinder
        }

        audioManager.PlayRotationSound(); // Play rotation sound

        activeCylinder = clickedCylinder; // Keep track of the active cylinder for stopping
    }

    private void StopRotation()
    {
        if (activeCylinder != null)
        {
            // Stop rotation for the active cylinder and its group if necessary
            if (activeCylinder.groupId != -1)
            {
                List<Cylinder> group = puzzleManager.GetCylindersByGroup(activeCylinder.groupId);
                foreach (Cylinder cylinder in group)
                {
                    cylinder.StopRotation(); // Stop rotating the entire group
                }
            }
            else
            {
                activeCylinder.StopRotation(); // Stop rotating the clicked cylinder
            }

            puzzleManager.CheckAllCylindersAligned(); // Check if all cylinders are aligned after stopping

            activeCylinder = null; // Clear the active cylinder reference
        }
    }
}
