namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;

    sealed class AttributeRequiredValue : AttributeValueDeclaration
    {
        public override Boolean Apply(Element element)
        {
            return element.HasAttribute(Parent.Name);
        }
    }
}
