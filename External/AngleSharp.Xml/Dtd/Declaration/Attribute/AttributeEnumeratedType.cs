namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    sealed class AttributeEnumeratedType : AttributeTypeDeclaration
    {
        #region Fields

        private readonly List<String> _names;

        #endregion

        #region ctor

        public AttributeEnumeratedType()
        {
            _names = new List<String>();
        }

        #endregion

        #region Properties

        public Boolean IsNotation
        {
            get;
            set;
        }

        public List<String> Names => _names;

        #endregion

        #region Methods

        public override Boolean Check(Element element)
        {
            if (element.HasAttribute(Parent.Name))
            {
                var value = element.GetAttribute(Parent.Name);

                if (!Names.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
