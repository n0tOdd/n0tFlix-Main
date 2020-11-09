namespace AngleSharp.Xml.Dtd.Declaration
{
    using System;

    sealed class ElementEmptyDeclarationEntry : ElementDeclarationEntry
    {
        public ElementEmptyDeclarationEntry()
        {
            _type = ElementContentType.Empty;
        }

        public override Boolean Check(NodeInspector inspector)
        {
            return inspector.Length == 0;
        }
    }
}
