using UnityEngine;
using System.Collections.Generic;

public static class TransformHelper
{
    // Return list of all children (including subchildren) of a transform
    public static List<Transform> GetAllChildren(this Transform transform)
    {
        var children = new List<Transform>();
        for (int n = 0; n < transform.childCount; n++)
            children.Add(transform.GetChild(n));
        for (int i = 0; i < children.Count; i++)
            for (int j = 0; j < children[i].childCount; j++)
                children.Add(children[i].GetChild(j));

        return children;
    }
}