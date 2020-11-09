namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;

    sealed class AttributeStringType : AttributeTypeDeclaration
    {
        public override Boolean Check(Element element)
        {
            return true;
        }
    }
}
