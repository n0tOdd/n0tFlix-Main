namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;

    abstract class AttributeValueDeclaration
    {
        public AttributeDeclarationEntry Parent 
        { 
            get; 
            set; 
        }

        public abstract Boolean Apply(Element element);
    }
}
