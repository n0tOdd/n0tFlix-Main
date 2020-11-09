namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Xml.Dtd.Declaration;

    sealed class DtdElementToken : DtdToken
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public DtdElementToken()
        {
            _type = DtdTokenType.Element;
        }

        #endregion

        #region Properties

        public ElementDeclarationEntry Entry
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public ElementDeclaration ToElement()
        {
            return new ElementDeclaration { Name = Name, Entry = Entry };
        }

        #endregion
    }
}
