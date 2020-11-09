namespace AngleSharp.Xml.Dtd.Parser
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The abstract base class of any DTD token.
    /// </summary>
    [DebuggerStepThrough]
    abstract class DtdToken
    {
        #region Fields

        private static DtdEndOfFileToken eof;
        protected String _name;
        protected DtdTokenType _type;

        #endregion

        #region Factory

        /// <summary>
        /// Gets the end of file token.
        /// </summary>
        public static DtdEndOfFileToken EOF => eof ?? (eof = new DtdEndOfFileToken());

        #endregion

        #region Properties

        public DtdTokenType Type => _type;

        public String Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion
    }
}
