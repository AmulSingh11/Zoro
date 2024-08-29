using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public LevelData[] levels; // Array of levels stored in ScriptableObjects
    public GameObject cylinderPrefab; // Reference to the cylinder prefab
    public Transform cylinderParent; // Parent transform that contains child placeholders
    public int currentLevelIndex = 0;

    private float elapsedTime = 0f;
    private bool isLevelActive = false;
    public TextMeshProUGUI Question;
    public GameObject FinishLevel;
    private void Start()
    {
        StartLevel(currentLevelIndex);
    }

    public void StartLevel(int levelIndex)
    {
        if (levelIndex >= levels.Length) return; // Ensure level index is valid

        LevelData levelData = levels[levelIndex];
        SetupCylinders(levelData);
        elapsedTime = 0f;
        isLevelActive = true;
        Question.text = levelData.Question;
    }

    private void SetupCylinders(LevelData levelData)
    {
        ClearCylinders();

        if (cylinderParent.childCount < levelData.cylinderTextures.Length)
        {
            Debug.LogError("Not enough child placeholders in the parent transform.");
            return;
        }

        for (int i = 0; i < levelData.cylinderTextures.Length; i++)
        {
            GameObject newCylinder = Instantiate(cylinderPrefab);
            Transform placeholder = cylinderParent.GetChild(i);
            newCylinder.transform.SetParent(placeholder, false);
            newCylinder.transform.localPosition = Vector3.zero;
            newCylinder.transform.localRotation = Quaternion.Euler(levelData.initialRotations[i]);

            Cylinder cylinder = newCylinder.GetComponent<Cylinder>();
            cylinder.GetComponent<Renderer>().material.mainTexture = levelData.cylinderTextures[i];
            cylinder.SetCorrectRotation(levelData.correctRotations[i]);

            // Assign the group ID from LevelData
            cylinder.groupId = levelData.groupIds[i];

            puzzleManager.AddCylinder(cylinder);
        }
    }


    private void ClearCylinders()
    {
        // Destroy all existing children of the cylinderParent (the cylinders)
        foreach (Transform child in cylinderParent)
        {
            if (child.childCount > 0)
            {
                for (int i = child.childCount - 1; i >= 0; i--)
                {
                    Destroy(child.GetChild(i).gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (isLevelActive)
        {
            elapsedTime += Time.deltaTime;

            if (puzzleManager.CheckAllCylindersAligned())
            {
                isLevelActive = false;
                CompleteLevel();
            }
        }
    }

    private void CompleteLevel()
    {
        ClearCylinders();
        puzzleManager.ClearCylinders();
        FinishLevel.SetActive(true);
        Debug.Log("Level Completed!");
        float score = CalculateScore();
        Debug.Log("Score: " + score);

        // Load next level if available
        
    }

    public void OnNextLevel()
    {
        FinishLevel.SetActive(false);
        currentLevelIndex++;
        if (currentLevelIndex < levels.Length)
        {
            StartLevel(currentLevelIndex);
        }
        else
        {
            currentLevelIndex = 0;
            Debug.Log("All Levels Completed!");
        }
    }

    private float CalculateScore()
    {
        LevelData levelData = levels[currentLevelIndex];
        return Mathf.Max(0, levelData.timeLimit - elapsedTime); // Simple score based on time taken
    }

    public void UseHint()
    {
        // Implement hint logic (e.g., rotate one random cylinder to correct rotation)
        Cylinder randomCylinder = puzzleManager.GetRandomUnalignedCylinder();
        if (randomCylinder != null)
        {
            randomCylinder.RotateToCorrectRotation();
        }
    }
}
