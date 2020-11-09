namespace AngleSharp.Xml.Dtd.Parser
{
    sealed class DtdEndOfFileToken : DtdToken
    {
        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        public DtdEndOfFileToken()
        {
            _type = DtdTokenType.EOF;
        }
    }
}
