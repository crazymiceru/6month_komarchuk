using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class ListControllers : ILateExecute, IInitialization, IExecute
    {
        private event Action _init = delegate { };
        private event Action<float> _execute = delegate { };
        private event Action _lateExecute = delegate { };
        private bool isInitHasPassed = false;
        public int countAddListControllers = 0;

        public void Execute(float deltaTime)
        {
            _execute(deltaTime);
        }

        public void Initialization()
        {
            isInitHasPassed = true;
            _init();
        }

        public void LateExecute()
        {
            _lateExecute();
        }

        internal void Add(IController controller, string name = "")
        {
            countAddListControllers++;
            if (controller is IInitialization init)
            {
                _init += init.Initialization;
                if (isInitHasPassed) init.Initialization();
            }
            if (controller is IExecute execute)
            {
                _execute += execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute += lateExecute.LateExecute;
            }
        }

        internal void Delete(IController controller)
        {
            countAddListControllers--;
            if (controller is IInitialization init)
            {
                _init -= init.Initialization;
            }
            if (controller is IExecute execute)
            {
                _execute -= execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute -= lateExecute.LateExecute;
            }
        }
    }
}
