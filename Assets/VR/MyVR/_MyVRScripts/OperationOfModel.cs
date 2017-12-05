using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationOfModel{

    public static void SetCenter(Transform trans)
    {
        Transform parent = trans;
        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;

        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= parent.GetComponentsInChildren<Transform>().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;

        foreach (Transform t in parent)
        {
            t.position = t.position - bounds.center;
        }
        parent.transform.position = bounds.center + parent.position;
    }

    public static void SetScale(Transform trans, float parentSize)
    {
        Renderer[] renders = trans.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }
        float sizeMax = bounds.size.x;
        sizeMax = sizeMax < bounds.size.y ? bounds.size.y : sizeMax;
        sizeMax *= trans.localScale.x;
        trans.localScale *= parentSize / sizeMax;
    }

    public static Bounds GetAABB(Transform trans)
    {
        Transform parent = trans;

        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= parent.GetComponentsInChildren<Transform>().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }
        return bounds;
    }

}
