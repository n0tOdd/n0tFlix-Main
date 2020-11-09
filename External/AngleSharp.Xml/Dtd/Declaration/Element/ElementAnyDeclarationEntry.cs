namespace AngleSharp.Xml.Dtd.Declaration
{
    using System;

    sealed class ElementAnyDeclarationEntry : ElementDeclarationEntry
    {
        public ElementAnyDeclarationEntry()
        {
            _type = ElementContentType.Any;
        }

        public override Boolean Check(NodeInspector inspector)
        {
            inspector.Index = inspector.Length;
            return true;
        }
    }
}
