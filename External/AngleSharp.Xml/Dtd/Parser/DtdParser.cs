namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The parser for the Document Type Definition.
    /// Can be used internally and externally.
    /// </summary>
    [DebuggerStepThrough]
    sealed class DtdParser
    {
        #region Fields

        private readonly DtdTokenizer _tokenizer;
        private readonly DtdContainer _result;
        private readonly TextSource _src;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Dtd parser for the given (DTD) content.
        /// </summary>
        /// <param name="content">The DTD to parse.</param>
        public DtdParser(String content)
            : this(new DtdContainer(), new TextSource(content))
        {
        }

        /// <summary>
        /// Creates a new Dtd parser that uses the given container
        /// as the result for parsing the given source.
        /// </summary>
        /// <param name="container">The container to use.</param>
        /// <param name="source">The source to parse.</param>
        public DtdParser(DtdContainer container, TextSource source)
        {
            _tokenizer = new DtdTokenizer(container, source);
            _result = container;
            _src = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the DTD is internal.
        /// </summary>
        public Boolean IsInternal
        {
            get => _tokenizer.IsExternal;
            set => _tokenizer.IsExternal = !value;
        }

        /// <summary>
        /// Gets the DtdContainer, i.e. the result of the computation.
        /// </summary>
        public DtdContainer Result => _result;

        #endregion

        #region Methods

        /// <summary>
        /// Parses the set source.
        /// </summary>
        public void Parse()
        {
            var token = _tokenizer.Get();
            Prolog(token);

            while (token.Type != DtdTokenType.EOF)
            {
                token = _tokenizer.Get();
                Consume(token);
            }

            _result.Text = _tokenizer.Content;
        }

        #endregion

        #region Consuming

        /// <summary>
        /// Consume the given prolog token (first token to consume).
        /// </summary>
        /// <param name="token">The token to consume.</param>
        void Prolog(DtdToken token)
        {
            if (_tokenizer.IsExternal && token.Type == DtdTokenType.TextDecl)
            {
                var pi = (DtdDeclToken)token;

                if (!String.IsNullOrEmpty(pi.Encoding))
                {
                    SetEncoding(_src, pi.Encoding);
                }
            }
            else
            {
                Consume(token);
            }
        }

        /// <summary>
        /// Consumes the given token.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        void Consume(DtdToken token)
        {
            switch (token.Type)
            {
                case DtdTokenType.Attribute:
                    _result.AddAttribute(((DtdAttributeToken)token).ToElement());
                    break;

                case DtdTokenType.Element:
                    _result.AddElement(((DtdElementToken)token).ToElement());
                    break;

                case DtdTokenType.Entity:
                    AddEntity((DtdEntityToken)token);
                    break;

                case DtdTokenType.Notation:
                    _result.AddNotation(((DtdNotationToken)token).ToElement());
                    break;

                case DtdTokenType.TextDecl:
                    throw new DomException(DomError.InvalidNodeType);

                case DtdTokenType.Comment:
                case DtdTokenType.ProcessingInstruction:
                    break;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Sets the document's encoding to the given one.
        /// </summary>
        /// <param name="source">The source to change.</param>
        /// <param name="encoding">The encoding to use.</param>
        static void SetEncoding(TextSource source, String encoding)
        {
            if (TextEncoding.IsSupported(encoding))
            {
                var enc = TextEncoding.Resolve(encoding);

                if (enc != null)
                {
                    source.CurrentEncoding = enc;
                }
            }
        }

        /// <summary>
        /// Adds the given entity to the DTD container.
        /// </summary>
        /// <param name="token">The entity token to add.</param>
        void AddEntity(DtdEntityToken token)
        {
            if (token.IsExtern)
            {
                token.Value = GetText(token.SystemIdentifier);
            }

            if (token.IsParameter)
            {
                _result.AddParameter(token.ToElement());
            }
            else
            {
                _result.AddEntity(token.ToElement());
            }
        }

        /// <summary>
        /// Gets the text included in the external source.
        /// </summary>
        /// <param name="url">The URL of the external source.</param>
        /// <returns>The contained string.</returns>
        String GetText(String url)
        {
            //if (Configuration.HasHttpRequester)
            //{
            //    if (!Location.IsAbsolute(url))
            //        url = Location.MakeAbsolute(_result.Url, url);

            //    var http = Configuration.GetHttpRequester();
            //    var response = http.Request(new DefaultHttpRequest { Address = new Uri(url) });
            //    var stream = new SourceManager(response.Content);
            //    var tok = new DtdPlainTokenizer(_result, stream);
            //    var token = tok.Get();

            //    if (token.Type == DtdTokenType.TextDecl)
            //    {
            //        var pi = (DtdDeclToken)token;

            //        if (!String.IsNullOrEmpty(pi.Encoding))
            //        {
            //            SetEncoding(stream, pi.Encoding);
            //        }

            //        token = tok.Get();
            //    }

            //    if (token.Type == DtdTokenType.Comment)
            //    {
            //        return ((DtdCommentToken)token).Data;
            //    }
            //}

            return String.Empty;
        }

        #endregion
    }
}
