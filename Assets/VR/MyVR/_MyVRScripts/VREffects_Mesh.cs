using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VREffects_Mesh : VREffects {

    public MeshRenderer[] meshRenderers;

    private void Start()
    {
        if (meshRenderers.Length == 0)
        {
            meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        }
    }

    public override void ShowHide(bool b)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].enabled = b;
        }
    }

    public override void ChangeColor(Color color)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            for (int j = 0; j < meshRenderers[i].materials.Length; j++)
            {
                float a = meshRenderers[i].materials[j].GetColor("_Color").a;
                color.a = a;
                meshRenderers[i].materials[j].SetColor("_Color", color);
            }
        }
    }

    public override void Scale(bool add)
    {
        
    }

}
