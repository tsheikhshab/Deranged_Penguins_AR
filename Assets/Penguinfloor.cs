using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacePenguinAtGroundLevel : MonoBehaviour
{
    public GameObject penguinPrefab; // Assign your Penguin prefab here
    public float spawnDistance = 1.5f; // Distance in meters in front of the camera

    private bool isPlaced = false; // Prevent multiple placements
    private float groundY; // Reference height for the floor

    void Start()
    {
        // Set ground level to XR Origin's Y position (floor tracking)
        groundY = Camera.main.transform.position.y;
        Debug.Log("Ground Y-Level Initialized: " + groundY);
    }

    void Update()
    {
        if (isPlaced) return; // Only place once

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlacePenguin();
            isPlaced = true;
        }
    }

    void PlacePenguin()
    {
        // Calculate spawn position 1.5 meters in front of the camera
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        spawnPosition.y = groundY; // Align with floor level

        // Instantiate the penguin
        Instantiate(penguinPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Penguin placed at: " + spawnPosition);
    }
}
