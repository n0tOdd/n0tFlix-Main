namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using System;

    sealed class DtdEntityToken : DtdToken
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public DtdEntityToken()
        {
            _type = DtdTokenType.Entity;
        }

        #endregion

        #region Properties

        public Boolean IsParameter 
        { 
            get; 
            set;
        }

        public String PublicIdentifier 
        { 
            get; 
            set; 
        }

        public String SystemIdentifier
        {
            get;
            set;
        }

        public Boolean IsPublic => PublicIdentifier != null;

        public Boolean IsExtern
        {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }

        public String ExternNotation
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public Entity ToElement()
        {
            return new Entity(null, Name)
            {
                NotationName = null,
                NodeValue = Value
            };
        }

        #endregion
    }
}
