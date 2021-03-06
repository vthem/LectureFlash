using VT.Observer;
using System;
using VT.Unity;

namespace VT.Front
{
    public class FrontObjectModifier<Component, Var> : FrontObjectComponentBinding where Component : UnityEngine.Component
    {
        public FrontObjectModifier(string frontObjectName, string varName, Action<Component, Var> modify) : this(frontObjectName, () =>
        {
            return new FrontObjectModifierComponent<Component, Var>(frontObjectName, varName, modify);
        })
        {
        }

        internal FrontObjectModifier(string frontObjectName, Func<FrontObjectComponent> constructor) : base(frontObjectName, constructor)
        {
        }
    }

    internal class FrontObjectModifierComponent<Component, Var> : FrontObjectComponent where Component : UnityEngine.Component
    {
        private readonly GenericVarObserver<Var> observer;
        private readonly Action<Component, Var> modify;
        private Component component;
        private bool componentSet = false;

        internal FrontObjectModifierComponent(string frontObjectName, string varName, Action<Component, Var> modify) : base(frontObjectName)
        {
            observer = new GenericVarObserver<Var>($"{frontObjectName}:{varName}", varName, VarUpdated);
            this.modify = modify;
        }

        private void VarUpdated(Var newValue)
        {
            if (!componentSet)
            {
                return;
            }
            modify?.Invoke(component, newValue);
        }

        protected override void InnerDispose()
        {
            observer?.Dispose();
        }

        protected override void InnerInitialize()
        {
            if (Object == null)
            {
                FrontSystem.Logger.Error($"Object is null in InnerInitialize of {FrontObjectName}");
                return;
            }
            component = Object?.GetComponent<Component>();
            componentSet = component != null;
            if (!componentSet)
            {
                FrontSystem.Logger.Error($"Failed to get {typeof(Component)} on {FrontObjectName} Path={Object.transform.FullName()} Id={Object.Identifier}");
                return;
            }
            observer.Sync();
        }
    }
}