using UnityEngine;
using System.Collections;

public class UIButtonHandler : MonoBehaviour
{
    public LevelManager levelManager;

    public void OnHintButtonPressed()
    {
        levelManager.UseHint();
    }
}