using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<Cylinder> cylinders = new List<Cylinder>();

    public void AddCylinder(Cylinder cylinder)
    {
        cylinders.Add(cylinder);
    }

    public void ClearCylinders()
    {
        cylinders.Clear();
    }

    public bool CheckAllCylindersAligned()
    {
        foreach (Cylinder cylinder in cylinders)
        {
            if (!cylinder.IsAligned())
            {
                return false;
            }
        }

        Debug.Log("All cylinders aligned!");
        return true;
    }

    public List<Cylinder> GetCylindersByGroup(int groupId)
    {
        List<Cylinder> group = new List<Cylinder>();
        foreach (Cylinder cylinder in cylinders)
        {
            if (cylinder.groupId == groupId)
            {
                group.Add(cylinder);
            }
        }
        return group;
    }

    public Cylinder GetRandomUnalignedCylinder()
    {
        List<Cylinder> unalignedCylinders = new List<Cylinder>();
        foreach (Cylinder cylinder in cylinders)
        {
            if (!cylinder.IsAligned())
            {
                unalignedCylinders.Add(cylinder);
            }
        }

        if (unalignedCylinders.Count > 0)
        {
            return unalignedCylinders[Random.Range(0, unalignedCylinders.Count)];
        }

        return null;
    }
}