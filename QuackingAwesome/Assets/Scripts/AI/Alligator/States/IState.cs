using UnityEngine;

namespace AI.Alligator.States
{
    public abstract class IState : MonoBehaviour
    {
        protected GameObject ai;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Execute() { }


        public IState(GameObject ai)
        {
            this.ai = ai;
        }
    }
}
