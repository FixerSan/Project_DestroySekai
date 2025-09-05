using JetBrains.Annotations;
using NUnit.Framework;
using System;
using UnityEngine;

public class Util 
{
    public static T GetOrAddComponent<T>(GameObject _obj) where T : UnityEngine.Component
    {
        T componenet = _obj.GetComponent<T>(); ;
        if(componenet == null) componenet = _obj.AddComponent<T>();

        return componenet;
    }

    public static T GetOrAddComponent<T>(Transform _trans) where T : UnityEngine.Component
    {
        T component = _trans.GetComponent<T>();
        if(component == null) _trans.gameObject.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(GameObject _obj, string _name = null, bool _recursive = false) where T : UnityEngine.Object
    {
        if (_obj == null) return null;

        if (!_recursive)
        {
            for (int i = 0; i < _obj.transform.childCount; i++)
            {
                Transform trans = _obj.transform.GetChild(i);
                if (string.IsNullOrEmpty(_name) || trans.name == _name)
                {
                    T comp = trans.GetComponent<T>();
                    if(comp != null) return comp;
                }
            }
        }

        else
        {
            foreach (T comp in _obj.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(_name) || comp.name == _name)
                    return comp;
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject _obj, string _name, bool _recursive)
    {
       return FindChild<Transform>(_obj, _name, _recursive)?.gameObject;
    }

    public static T ParseEnum<T>(string _value)
    {
        return (T)Enum.Parse(typeof(T), _value, true);
    }
}
