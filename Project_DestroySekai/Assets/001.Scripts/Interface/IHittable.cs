using JetBrains.Annotations;
using UnityEngine;

public interface IHittable
{
    public abstract void Hit(float _force);   
}
