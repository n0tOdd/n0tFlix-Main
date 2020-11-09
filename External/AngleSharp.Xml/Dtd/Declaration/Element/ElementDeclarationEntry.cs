namespace AngleSharp.Xml.Dtd.Declaration
{
    using System;
    using System.Collections.Generic;

    abstract class ElementDeclarationEntry
    {
        #region Fields

        protected ElementContentType _type;

        private static ElementAnyDeclarationEntry _any;
        private static ElementEmptyDeclarationEntry _empty;

        #endregion

        #region Properties

        public ElementContentType Type => _type;

        public static ElementAnyDeclarationEntry Any => _any ?? (_any = new ElementAnyDeclarationEntry());

        public static ElementEmptyDeclarationEntry Empty => _empty ?? (_empty = new ElementEmptyDeclarationEntry());

        #endregion

        #region Methods

        public abstract Boolean Check(NodeInspector inspector);

        #endregion
    }

    abstract class ElementQuantifiedDeclarationEntry : ElementDeclarationEntry
    {
        protected ElementQuantifier _quantifier;

        public ElementQuantifier Quantifier
        {
            get => _quantifier;
            set => _quantifier = value;
        }
    }

    abstract class ElementChildrenDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        protected List<ElementQuantifiedDeclarationEntry> _children;

        public ElementChildrenDeclarationEntry()
        {
            _children = new List<ElementQuantifiedDeclarationEntry>();
            _type = ElementContentType.Children;
        }
    }
}
