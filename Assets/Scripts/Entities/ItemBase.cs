using Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private string containerName;

    protected float weight;

    protected float volume;

    public float Volume { get => volume; set => volume = value; }
    public float Weight { get => weight; set => weight = value; }
    public string Name { get => containerName; set => containerName = value; }
}
