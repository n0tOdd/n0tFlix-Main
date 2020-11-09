namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    sealed class ElementMixedDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        #region Fields

        private readonly List<String> _names;

        #endregion

        #region ctor

        public ElementMixedDeclarationEntry()
        {
            _names = new List<String>();
            _type = ElementContentType.Mixed;
        }

        #endregion

        #region Properties

        public List<String> Names => _names;

        #endregion

        #region Methods

        public override Boolean Check(NodeInspector inspector)
        {
            if (_quantifier == ElementQuantifier.One && inspector.Length > 1)
            {
                return false;
            }
            else
            {
                for (; inspector.Index < inspector.Length; inspector.Index++)
                {
                    var child = inspector.Current;

                    if (child is IElement && !_names.Contains(child.NodeName))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        #endregion
    }
}
