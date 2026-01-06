using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp051.Services.MessageBus
{
    public class WeakReferencActionWithParameter<T> : WeakReferenceAction, IActionParameter
    {
        private Action<T> action;
        public WeakReferencActionWithParameter(object target, Action<T> action)
            : base(target, null)
        {
            this.action = action;
        }
        public void Execute(T parameter)
        {
            if (action != null && Target != null && Target.IsAlive)
                this.action(parameter);
        }
        public Action<T> Action
        {
            get
            {
                return action;
            }
        }
        #region IActionParameter Members
        public void ExecuteWithParameter(object parameter)
        {
            this.Execute((T)parameter);
        }
        #endregion
    }
}
