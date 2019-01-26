using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTGame.GameFramework.Fsm
{
    public interface IFSM<T> where T : class
    {
        /// <summary>
        /// 获取有限状态机名称。
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        T Owner
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        int FSMStateCount
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        bool IsRunning
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        bool IsDestroyed
        {
            get;
        }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        FSMState<T> CurrentState
        {
            get;
        }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        float CurrentStateTime
        {
            get;
        }

        /// <summary>
        /// 是否使用固定频率更新的模式，若是则不使用普通update。
        /// </summary>
        bool IsUseFixedUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TState">要开始的有限状态机状态类型。</typeparam>
        void Start<TState>() where TState : FSMState<T>;

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <param name="stateType">要开始的有限状态机状态类型。</param>
        void Start(Type stateType);

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要检查的有限状态机状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        bool HasState<TState>() where TState : FSMState<T>;

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的有限状态机状态类型。</param>
        /// <returns>是否存在有限状态机状态。</returns>
        bool HasState(Type stateType);

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        TState GetState<TState>() where TState : FSMState<T>;

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的有限状态机状态类型。</param>
        /// <returns>要获取的有限状态机状态。</returns>
        FSMState<T> GetState(Type stateType);

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        FSMState<T>[] GetAllStates();

        /// <summary>
        /// 抛出有限状态机事件。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="eventId">事件编号。</param>
        void FireEvent(object sender, int eventId);

        /// <summary>
        /// 抛出有限状态机事件。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="eventId">事件编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        void FireEvent(object sender, int eventId, object userData);

        /// <summary>
        /// 是否存在有限状态机数据。
        /// </summary>
        /// <param name="name">有限状态机数据名称。</param>
        /// <returns>有限状态机数据是否存在。</returns>
        bool HasData(string name);
    }
}
