using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
   public enum VFXType
    {
        JUMP,
        VFX_2
    }

    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXType(VFXType vfxType, Vector3 position)
    {
        foreach(var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab);
                item.transform.position = position;
                break;
            }
        }
    }
}

[Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}