using System;


namespace HaewolWorkshop
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FSMStateAttribute : System.Attribute
    {
        public readonly int key;

        public FSMStateAttribute(int key)
        {
            this.key = key;
        }
    }


    public abstract class FSMState<T> where T : IFSMEntity
    {
        protected readonly T ownerEntity;

        public virtual void InitializeState() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void ClearState() { }

        public FSMState(IFSMEntity owner)
        {
            ownerEntity = (T)owner;
        }
    }
}