using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Fsm
{
    public delegate void FsmEventHandler<T>(IFSM<T> fsm, object sender, object userData) where T : class;
}
