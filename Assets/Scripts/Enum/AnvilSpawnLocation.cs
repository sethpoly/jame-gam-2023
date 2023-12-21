using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnvilSpawnLocation
{

    [AnvilSpawnLocationAttribute(x: -5f, y: 2.5f)]
    Start,
    [AnvilSpawnLocationAttribute(x: 0f, y: 2.5f)]
    Middle,
    [AnvilSpawnLocationAttribute(x: 5f, y: 2.5f)]
    End,
}


[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class AnvilSpawnLocationAttribute : Attribute
{
    public float x { get; }
    public float y { get; }

    public AnvilSpawnLocationAttribute(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
