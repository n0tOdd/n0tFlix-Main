namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using System;

    sealed class DtdCommentToken : DtdToken
    {
        #region Fields

        private String _data;

        #endregion

        #region ctor

        public DtdCommentToken()
        {
            _type = DtdTokenType.Comment;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the supplied data.
        /// </summary>
        public String Data
        {
            get => _data;
            set => _data = value;
        }

        #endregion

        #region Methods

        public IComment ToElement(Document document)
        {
            return document.CreateComment(_data);
        }

        #endregion
    }
}
