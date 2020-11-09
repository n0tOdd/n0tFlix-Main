namespace AngleSharp.Js
{
    using Jint.Native;
    using Jint.Native.Object;
    using Jint.Runtime.Descriptors;
    using System;

    internal sealed class DomNodeInstance : ObjectInstance
    {
        private readonly EngineInstance _instance;
        private readonly Object _value;
        public override bool Extensible => true;

        public DomNodeInstance(EngineInstance engine, Object value)
            : base(engine.Jint)
        {
            _instance = engine;
            _value = value;

            SetPrototypeOf(engine.GetDomPrototype(value.GetType()));
        }

        public Object Value => _value;

        public String Class => Prototype.ToString();

        public override PropertyDescriptor GetOwnProperty(JsValue propertyName)
        {
            if (Prototype is DomPrototypeInstance prototype && prototype.TryGetFromIndex(_value, propertyName.ToString(), out PropertyDescriptor ou))
            {
                return ou;
            }

            return base.GetOwnProperty(propertyName);
        }
    }
}