using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace HaewolWorkshop
{
    public interface IFSMEntity
    {

    }

    public abstract class FSMPlayer<T> : MonoBehaviour where T : IFSMEntity
    {
        private Dictionary<int, FSMState<T>> states;

        private FSMState<T> currentState = null;
        private FSMState<T> previousState = null;
        private FSMState<T> globalState = null;

        protected virtual void Update()
        {
            globalState?.UpdateState();
            currentState?.UpdateState();
        }

        protected virtual void FixedUpdate()
        {
            globalState?.FixedUpdateState();
            currentState?.FixedUpdateState();
        }

        protected void SetUp(ValueType firstState)
        {
            states = new Dictionary<int, FSMState<T>>();
            var stateTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(FSMState<T>) != t && typeof(FSMState<T>).IsAssignableFrom(t));

            foreach (var stateType in stateTypes)
            {
                var attribute = stateType.GetCustomAttribute<FSMStateAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var state = Activator.CreateInstance(stateType, this as IFSMEntity) as FSMState<T>;

                if (!states.TryAdd(attribute.key, state))
                {
                    Debug.LogError($"{typeof(T)} 의 {attribute.key} 키가 중복되었습니다.");
                }
            }

            ChangeState(firstState);
        }

        public void ChangeState(ValueType enumValue)
        {
            if (!states.TryGetValue((int)enumValue, out var state))
            {
                Debug.LogError($"{GetType()} : 사용할 수 없는 상태입니다. {enumValue}");
                return;
            }

            ChangeState(state);
        }

        private void ChangeState(FSMState<T> newState)
        {
            if (newState == null) return;

            if (currentState != null)
            {
                previousState = currentState;

                currentState.ClearState();

                Debug.Log($"{this.GetType()} : {currentState.GetType()} 상태 클리어");
            }

            currentState = newState;
            currentState.InitializeState();



            Debug.Log($"{this.GetType()} : {newState.GetType()} 상태로 전환");
        }

        public void SetGlobalState(FSMState<T> newState)
        {
            globalState?.ClearState();

            globalState = newState;

            globalState?.InitializeState();
        }

        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }
    }
}