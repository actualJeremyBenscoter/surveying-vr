using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface IGrabbable
    {
        void Grab(GameObject grabber);
        void Release();
        GameObject gameObject { get; }
        bool Grabbed { get; }
    }
}