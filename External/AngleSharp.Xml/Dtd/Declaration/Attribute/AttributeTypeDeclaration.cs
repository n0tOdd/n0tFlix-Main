namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;

    abstract class AttributeTypeDeclaration
    {
        public AttributeDeclarationEntry Parent 
        { 
            get; 
            set; 
        }

        public abstract Boolean Check(Element element);
    }
}
