using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface IInteractable
    {
        void BeginInteraction(GameObject go);
        void EndInteraction();
        bool IsInteracting { get; }
        GameObject gameObject { get; }
    }
}