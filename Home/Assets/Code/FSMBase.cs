using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTGame.GameFramework.Fsm
{
    public abstract class FSMBase
    {
        private readonly string m_Name;

        public FSMBase()
            : this(null)
        {

        }

        /// <summary>
        /// 初始化有限状态机基类的新实例。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        public FSMBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        /// <summary>
        /// 获取有限状态机名称。
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// 获取有限状态机持有者类型。
        /// </summary>
        public abstract Type OwnerType
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        public abstract bool IsRunning
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        public abstract int FSMStateCount
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        public abstract bool IsDestroyed
        {
            get;
        }

        /// <summary>
        /// 获取当前有限状态机状态名称。
        /// </summary>
        public abstract string CurrentStateName
        {
            get;
        }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        public abstract float CurrentStateTime
        {
            get;
        }

        /// <summary>
        /// 是否使用固定频率更新的模式，若是则不使用普通update。
        /// </summary>
        public abstract bool IsUseFixedUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">当前已流逝时间，以秒为单位。</param>
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        internal abstract void Shutdown();
    }
}
