using UnityEngine;

namespace AI.Alligator.States
{
    public abstract class IState : MonoBehaviour
    {
        public abstract void ExitState();
        public abstract void Execute();
    }
}
