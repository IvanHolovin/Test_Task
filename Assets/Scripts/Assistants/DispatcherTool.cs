using System;

namespace DefaultNamespace.Assistants
{
    public class DispatcherTool<TClass, TValue> where TClass : DispatcherTool<TClass, TValue>, new()
    {
        private static TClass _instance;

        public static TClass Instance
        { 
            get
            {
                if (_instance == null)
                {
                    _instance = new TClass();
                } 
                return _instance;
                }
        }

        private Action<TValue> _onActionHappened;

        public void AddListener(Action<TValue> listener)
        {
            _onActionHappened += listener;
        }

        public void RemoveListener(Action<TValue> listener)
        {
            _onActionHappened -= listener;
        }

        public void ActionHappened(TValue value)
        {
            _onActionHappened?.Invoke(value);
        }
    }
}