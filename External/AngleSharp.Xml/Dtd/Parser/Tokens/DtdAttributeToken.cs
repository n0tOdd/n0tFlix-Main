namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Xml.Dtd.Declaration;
    using System.Collections.Generic;

    sealed class DtdAttributeToken : DtdToken
    {
        #region Fields

        private readonly List<AttributeDeclarationEntry> _attributes;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public DtdAttributeToken()
        {
            _attributes = new List<AttributeDeclarationEntry>();
            _type = DtdTokenType.Attribute;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of attributes defined.
        /// </summary>
        public List<AttributeDeclarationEntry> Attributes => _attributes;

        #endregion

        #region Methods

        public AttributeDeclaration ToElement()
        {
            return new AttributeDeclaration(_attributes) { Name = Name };
        }

        #endregion
    }
}
