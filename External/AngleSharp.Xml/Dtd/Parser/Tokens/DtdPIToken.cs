namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using System;

    sealed class DtdPIToken : DtdToken
    {
        #region Fields

        private String _target;
        private String _content;

        #endregion

        #region ctor

        public DtdPIToken()
        {
            _type = DtdTokenType.ProcessingInstruction;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the target data.
        /// </summary>
        public String Target
        {
            get => _target;
            set => _target = value;
        }

        /// <summary>
        /// Gets or sets the content data.
        /// </summary>
        public String Content
        {
            get => _content;
            set => _content = value;
        }

        #endregion

        #region Methods

        public IProcessingInstruction ToElement(Document document)
        {
            return document.CreateProcessingInstruction(_target, _content);
        }

        #endregion
    }
}
