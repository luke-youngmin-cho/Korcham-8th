using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interactions
{
    public interface IInteractable
    {
        void Interaction(Interactor interactor);
    }
}