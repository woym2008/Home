using PTGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTGame.GameFramework.Fsm
{
    internal sealed class FSM<T> : FSMBase, IFSM<T> where T : class
    {
        private readonly T m_Owner;
        private readonly Dictionary<string, FSMState<T>> m_States;

        private FSMState<T> m_CurrentState;

        private float m_CurrentStateTime;
        private bool m_IsDestroyed;

        private bool m_IsUseFixedUpdate;

        public FSM(string name, T owner, params FSMState<T>[] states)
            : base(name)
        {
            if (owner == null)
            {
                throw new Exception("FSM owner is invalid.");
            }

            if (states == null || states.Length < 1)
            {
                throw new Exception("FSM states is invalid.");
            }

            m_Owner = owner;
            m_States = new Dictionary<string, FSMState<T>>();
            //m_Datas = new Dictionary<string, Variable>();

            foreach (FSMState<T> state in states)
            {
                if (state == null)
                {
                    throw new Exception("FSM states is invalid.");
                }

                string stateName = state.GetType().FullName;
                if (m_States.ContainsKey(stateName))
                {
                    throw new Exception(string.Format("FSM '{0}' state '{1}' is already exist.", name, stateName));
                }

                m_States.Add(stateName, state);
                state.OnInit(this);
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = null;
            m_IsDestroyed = false;
        }

        public T Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public override Type OwnerType
        {
            get
            {
                return typeof(T);
            }
        }

        public override int FSMStateCount
        {
            get
            {
                return m_States.Count;
            }
        }

        public override bool IsRunning
        {
            get
            {
                return m_CurrentState != null;
            }
        }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        public override bool IsDestroyed
        {
            get
            {
                return m_IsDestroyed;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        public FSMState<T> CurrentState
        {
            get
            {
                return m_CurrentState;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态名称。
        /// </summary>
        public override string CurrentStateName
        {
            get
            {
                return m_CurrentState != null ? m_CurrentState.GetType().FullName : null;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        public override float CurrentStateTime
        {
            get
            {
                return m_CurrentStateTime;
            }
        }

        /// <summary>
        /// 是否使用固定频率更新的模式，若是则不使用普通update。
        /// </summary>
        public override bool IsUseFixedUpdate
        {
            get
            {
                return m_IsUseFixedUpdate;
            }

            set
            {
                m_IsUseFixedUpdate = value;
            }
        }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TState">要开始的有限状态机状态类型。</typeparam>
        public void Start<TState>() where TState : FSMState<T>
        {
            if (IsRunning)
            {
                throw new Exception("FSM is running, can not start again.");
            }

            FSMState<T> state = GetState<TState>();
            if (state == null)
            {
                throw new Exception(string.Format("FSM '{0}' can not start state '{1}' which is not exist.", Name, typeof(TState).FullName));
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <param name="stateType">要开始的有限状态机状态类型。</param>
        public void Start(Type stateType)
        {
            if (IsRunning)
            {
                throw new Exception("FSM is running, can not start again.");
            }

            if (stateType == null)
            {
                throw new Exception("State type is invalid.");
            }

            if (!typeof(FSMState<T>).IsAssignableFrom(stateType))
            {
                throw new Exception(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            FSMState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new Exception(string.Format("FSM '{0}' can not start state '{1}' which is not exist.", Name, stateType.FullName));
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要检查的有限状态机状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState<TState>() where TState : FSMState<T>
        {
            return m_States.ContainsKey(typeof(TState).FullName);
        }

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的有限状态机状态类型。</param>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState(Type stateType)
        {
            if (stateType == null)
            {
                throw new Exception("State type is invalid.");
            }

            if (!typeof(FSMState<T>).IsAssignableFrom(stateType))
            {
                throw new Exception(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            return m_States.ContainsKey(stateType.FullName);
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        public TState GetState<TState>() where TState : FSMState<T>
        {
            FSMState<T> state = null;
            if (m_States.TryGetValue(typeof(TState).FullName, out state))
            {
                return (TState)state;
            }

            return null;
        }

        public FSMState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new Exception("State type is invalid.");
            }

            if (!typeof(FSMState<T>).IsAssignableFrom(stateType))
            {
                throw new Exception(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            FSMState<T> state = null;
            if (m_States.TryGetValue(stateType.FullName, out state))
            {
                return state;
            }

            return null;
        }

        public FSMState<T>[] GetAllStates()
        {
            int index = 0;
            FSMState<T>[] allStates = new FSMState<T>[m_States.Count];
            foreach (KeyValuePair<string, FSMState<T>> state in m_States)
            {
                allStates[index++] = state.Value;
            }

            return allStates;
        }

        public void FireEvent(object sender, int eventId)
        {
            
            if (m_CurrentState == null)
            {
                throw new Exception("Current state is invalid.");
            }

            m_CurrentState.OnEvent(this, sender, eventId, null);
        }

        public void FireEvent(object sender, int eventId, object userData)
        {
            throw new NotImplementedException();
        }

        
        public bool HasData(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapseSeconds;
            m_CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        internal override void Shutdown()
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.OnExit(this, true);
                m_CurrentState = null;
                m_CurrentStateTime = 0f;
            }

            foreach (KeyValuePair<string, FSMState<T>> state in m_States)
            {
                state.Value.OnDestroy(this);
            }

            m_States.Clear();

            m_IsDestroyed = true;
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
        internal void ChangeState<TState>() where TState : FSMState<T>
        {
            ChangeState(typeof(TState));
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        internal void ChangeState(Type stateType)
        {
            if (m_CurrentState == null)
            {
                throw new Exception("Current state is invalid.");
            }

            FSMState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new Exception(string.Format("FSM '{0}' can not change state to '{1}' which is not exist.", Name, stateType.FullName));
            }

            m_CurrentState.OnExit(this, false);
            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }
    }
}
