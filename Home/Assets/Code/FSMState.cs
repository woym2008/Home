﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTGame.GameFramework.Fsm
{
    public abstract class FSMState<T> where T : class
    {
        private readonly Dictionary<int, FsmEventHandler<T>> m_EventHandlers;

        public FSMState()
        {
            m_EventHandlers = new Dictionary<int, FsmEventHandler<T>>();
        }

        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnInit(IFSM<T> fsm)
        {

        }

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnEnter(IFSM<T> fsm)
        {

        }

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal virtual void OnUpdate(IFSM<T> fsm, float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 有限状态机状态离开时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
        protected internal virtual void OnExit(IFSM<T> fsm, bool isShutdown)
        {

        }

        /// <summary>
        /// 有限状态机状态销毁时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnDestroy(IFSM<T> fsm)
        {
            m_EventHandlers.Clear();
        }

        /// <summary>
        /// 订阅有限状态机事件。
        /// </summary>
        /// <param name="eventId">事件编号。</param>
        /// <param name="eventHandler">有限状态机事件响应函数。</param>
        protected void SubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                //throw new GameFrameworkException("Event handler is invalid.");
            }

            if (!m_EventHandlers.ContainsKey(eventId))
            {
                m_EventHandlers[eventId] = eventHandler;
            }
            else
            {
                m_EventHandlers[eventId] += eventHandler;
            }
        }

        /// <summary>
        /// 取消订阅有限状态机事件。
        /// </summary>
        /// <param name="eventId">事件编号。</param>
        /// <param name="eventHandler">有限状态机事件响应函数。</param>
        protected void UnsubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                //throw new GameFrameworkException("Event handler is invalid.");
            }

            if (m_EventHandlers.ContainsKey(eventId))
            {
                m_EventHandlers[eventId] -= eventHandler;
            }
        }


        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
        /// <param name="fsm">有限状态机引用。</param>
        protected void ChangeState<TState>(IFSM<T> fsm) where TState : FSMState<T>
        {
            FSM<T> fsmImplement = (FSM<T>)fsm;
            if (fsmImplement == null)
            {
                //throw new GameFrameworkException("FSM is invalid.");
            }

            fsmImplement.ChangeState<TState>();
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        protected void ChangeState(IFSM<T> fsm, Type stateType)
        {
            FSM<T> fsmImplement = (FSM<T>)fsm;
            if (fsmImplement == null)
            {
                //throw new GameFrameworkException("FSM is invalid.");
            }

            if (stateType == null)
            {
                //throw new GameFrameworkException("State type is invalid.");
            }

            if (!typeof(FSMState<T>).IsAssignableFrom(stateType))
            {
                //throw new GameFrameworkException(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            fsmImplement.ChangeState(stateType);
        }

        /// <summary>
        /// 响应有限状态机事件时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="sender">事件源。</param>
        /// <param name="eventId">事件编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        internal void OnEvent(IFSM<T> fsm, object sender, int eventId, object userData)
        {
            FsmEventHandler<T> eventHandlers = null;
            if (m_EventHandlers.TryGetValue(eventId, out eventHandlers))
            {
                if (eventHandlers != null)
                {
                    eventHandlers(fsm, sender, userData);
                }
            }
        }
    }
}
