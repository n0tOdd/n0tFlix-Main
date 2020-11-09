namespace AngleSharp.Xml.Dtd.Parser
{
    using System;

    sealed class DtdDeclToken : DtdToken
    {
        #region ctor

        public DtdDeclToken()
        {
            _type = DtdTokenType.TextDecl;
        }

        #endregion

        #region Properties

        public String Version
        {
            get;
            set;
        }

        public String Encoding
        {
            get;
            set;
        }

        #endregion
    }
}
