namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;

    sealed class AttributeImpliedValue : AttributeValueDeclaration
    {
        public override Boolean Apply(Element element)
        {
            return true;
        }
    }
}
