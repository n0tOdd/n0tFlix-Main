namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using System;

    sealed class DtdNotationToken : DtdToken
    {
        #region Fields

        private String _publicIdentifier;
        private String _systemIdentifier;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public DtdNotationToken()
        {
            _type = DtdTokenType.Notation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the declaration is an external id (the system identifier
        /// has been set directly or indirectly).
        /// </summary>
        public Boolean IsExternalId => !String.IsNullOrEmpty(_systemIdentifier);

        /// <summary>
        /// Gets if the declaration is an external id (the system identifier
        /// has been set directly or indirectly).
        /// </summary>
        public Boolean IsPublicId => !String.IsNullOrEmpty(_publicIdentifier) && String.IsNullOrEmpty(_systemIdentifier);

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicIdentifier
        {
            get => _publicIdentifier ?? String.Empty;
            set => _publicIdentifier = value;
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        public String SystemIdentifier
        {
            get => _systemIdentifier ?? String.Empty;
            set => _systemIdentifier = value;
        }

        #endregion

        #region Methods

        public Notation ToElement()
        {
            return new Notation(null, Name)
            {
                PublicId = PublicIdentifier,
                SystemId = SystemIdentifier
            };
        }

        #endregion
    }
}
