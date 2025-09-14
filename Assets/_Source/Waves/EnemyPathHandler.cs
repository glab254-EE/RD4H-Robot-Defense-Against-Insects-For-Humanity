using System.Collections.Generic;
using UnityEngine;

public static class EnemyPathingManager
{
    // cant make it serializable, will hardcode ts pmo.
    private static string pathTag = "Path";
    public static Queue<Transform> GetPath()
    {
        Queue<Transform> output = new();
        GameObject[] PotentialPath = GameObject.FindGameObjectsWithTag(pathTag);
        if (PotentialPath == null || PotentialPath.Length <= 0) return null;
        int desiredCounter = PotentialPath.Length;
        int currentCounter = 0;
        while (currentCounter < desiredCounter)
        {
            foreach (GameObject gameObject in PotentialPath)
            {
                if (gameObject.transform != null&&gameObject.name.ToLower().Equals((currentCounter + 1).ToString()))
                {
                    currentCounter++;
                    output.Enqueue(gameObject.transform);
                }
            }
        } 
        if (output == null || output.Count <= 0) return null;
        return output;
    }
}
