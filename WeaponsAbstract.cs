using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponsAbstract : MonoBehaviour
{
    public bool reloading;
    public int count1;
    public int count2;
    public int count3;
    public float range;
    public float volume;

    public abstract void Shoot();
    public abstract void Reloader();
}