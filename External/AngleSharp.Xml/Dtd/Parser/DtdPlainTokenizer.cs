namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The base tokenizer class for Document Type Definitions.
    /// </summary>
    [DebuggerStepThrough]
    class DtdPlainTokenizer : BaseTokenizer
    {
        #region Fields
        
        protected Boolean _external;
        protected readonly IntermediateStream _stream;
        protected readonly DtdContainer _container;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new DTD tokenizer with the given source and container.
        /// </summary>
        /// <param name="container">The container to use.</param>
        /// <param name="src">The source to inspect.</param>
        public DtdPlainTokenizer(DtdContainer container, TextSource src)
            : base(src)
        {
            _container = container;
            _external = true;
            _stream = new IntermediateStream(src);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Scans the DTD in the doctype as specified in the
        /// official XML spec and (in German) here:
        /// http://www.stefanheymann.de/xml/dtdxml.htm
        /// </summary>
        public DtdToken Get()
        {
            var c = _stream.Current;
            var element = GetElement(c);

            if (element != DtdToken.EOF)
                SkipSpaces(c);

            return element;
        }

        #endregion

        #region States

        protected virtual DtdToken GetElement(Char c)
        {
            if (c == Symbols.LessThan && _stream.ContinuesWith("<?xml"))
            {
                _stream.Advance(4);
                return TextDecl(_stream.Next);
            }
            else if (c != Symbols.EndOfFile)
            {
                var s = ScanString(c, Symbols.EndOfFile);
                return new DtdCommentToken { Data = s };
            }

            return DtdToken.EOF;
        }

        #endregion

        #region Text Declaration

        /// <summary>
        /// The text declaration for external DTDs.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>The token.</returns>
        protected DtdToken TextDecl(Char c)
        {
            if (_external)
            {
                var token = new DtdDeclToken();

                if (c.IsSpaceCharacter())
                {
                    c = SkipSpaces(c);

                    if (_stream.ContinuesWith(AttributeNames.Version))
                    {
                        _stream.Advance(6);
                        return TextDeclVersion(_stream.Next, token);
                    }
                    else if (_stream.ContinuesWith(AttributeNames.Encoding))
                    {
                        _stream.Advance(7);
                        return TextDeclEncoding(_stream.Next, token);
                    }
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// Gets the version specified in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclVersion(Char c, DtdDeclToken decl)
        {
            if (c == Symbols.Equality)
            {
                var q = _stream.Next;

                if (q == Symbols.DoubleQuote || q == Symbols.SingleQuote)
                {
                    StringBuffer.Clear();
                    c = _stream.Next;

                    while (c.IsDigit() || c == Symbols.Dot)
                    {
                        StringBuffer.Append(c);
                        c = _stream.Next;
                    }

                    if (c == q)
                    {
                        decl.Version = StringBuffer.ToString();
                        return TextDeclBetween(_stream.Next, decl);
                    }
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// Between the version and the encoding in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclBetween(Char c, DtdDeclToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (_stream.ContinuesWith(AttributeNames.Encoding))
                {
                    _stream.Advance(7);
                    return TextDeclEncoding(_stream.Next, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// Gets the encoding specified in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclEncoding(Char c, DtdDeclToken decl)
        {
            if (c == Symbols.Equality)
            {
                var q = _stream.Next;

                if (q == Symbols.DoubleQuote|| q == Symbols.SingleQuote)
                {
                    StringBuffer.Clear();
                    c = _stream.Next;

                    if (c.IsLetter())
                    {
                        do
                        {
                            StringBuffer.Append(c);
                            c = _stream.Next;
                        }
                        while (c.IsAlphanumericAscii() || c == Symbols.Dot|| c == Symbols.Underscore || c == Symbols.Minus);
                    }

                    if (c == q)
                    {
                        decl.Encoding = StringBuffer.ToString();
                        return TextDeclAfter(_stream.Next, decl);
                    }
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// After the declaration specified in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclAfter(Char c, DtdDeclToken decl)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (c != Symbols.QuestionMark)
                throw new DomException(DomError.InvalidNodeType);

            return TextDeclEnd(_stream.Next, decl);
        }

        /// <summary>
        /// Checks if the text declaration ended correctly.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclEnd(Char c, DtdDeclToken decl)
        {
            if (c != Symbols.GreaterThan)
                throw new DomException(DomError.InvalidNodeType);

            return decl;
        }

        #endregion

        #region Intermediate Stream

        protected sealed class IntermediateStream
        {
            #region Fields

            private readonly Stack<String> _params;
            private readonly Stack<Int32> _pos;
            private readonly Stack<String> _texts;
            private readonly TextSource _base;
            private readonly Int32 _start;
            private Int32 _end;

            #endregion

            #region ctor

            public IntermediateStream(TextSource src)
            {
                _start = src.Index - 1;
                _pos = new Stack<Int32>();
                _params = new Stack<String>();
                _texts = new Stack<String>();
                _base = src;
            }

            #endregion

            #region Properties

            /// <summary>
            /// The content (of the original stream).
            /// </summary>
            public String Content => _base.Text;

            /// <summary>
            /// The next character.
            /// </summary>
            public Char Next
            {
                get
                {
                    if (_texts.Count == 0)
                    {
                        _end = _base.Index;
                        return _base.ReadCharacter();
                    }
                    else
                    {
                        _pos.Push(_pos.Pop() + 1);

                        if (_pos.Peek() == _texts.Peek().Length)
                        {
                            _pos.Pop();
                            _texts.Pop();
                            _params.Pop();
                        }
                    }

                    return Current;
                }
            }

            /// <summary>
            /// The current character.
            /// </summary>
            public Char Current => _texts.Count == 0 ? _base.Text[_base.Index] : _texts.Peek()[_pos.Peek()];

            #endregion

            #region Methods

            /// <summary>
            /// Pushes the the param entity with its text onto the stack.
            /// </summary>
            /// <param name="param">The param entity to use.</param>
            /// <param name="text">The text to insert.</param>
            public void Push(String param, String text)
            {
                if (_params.Contains(param))
                {
                    throw new DomException(DomError.HierarchyRequest);
                }

                Advance();

                if (!String.IsNullOrEmpty(text))
                {
                    _params.Push(param);
                    _pos.Push(0);
                    _texts.Push(text);
                }
            }

            /// <summary>
            /// Advances by one character.
            /// </summary>
            public void Advance()
            {
                if (_texts.Count != 0)
                {
                    _pos.Push(_pos.Pop() + 1);

                    if (_pos.Peek() == _texts.Peek().Length)
                    {
                        _pos.Pop();
                        _texts.Pop();
                        _params.Pop();
                    }
                }
                else
                {
                    Advance();
                }
            }

            /// <summary>
            /// Advances by n characters.
            /// </summary>
            /// <param name="n">The number of characters to skip.</param>
            public void Advance(Int32 n)
            {
                for (int i = 0; i < n; i++)
                    Advance();
            }

            /// <summary>
            /// Checks if the stream continues with the given word.
            /// </summary>
            /// <param name="word">The word to check for.</param>
            /// <returns>True if it continues, otherwise false.</returns>
            public Boolean ContinuesWith(String word)
            {
                if (_texts.Count != 0)
                {
                    var pos = _pos.Peek();
                    var text = _texts.Peek();

                    if (text.Length - pos < word.Length)
                    {
                        return false;
                    }

                    for (var i = 0; i < word.Length; i++)
                    {
                        if (text[i + pos] != word[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return ContinuesWith(word);
            }

            #endregion
        }

        #endregion

        #region References

        protected void PEReference(Char c, Boolean use = true)
        {
            var buffer = StringBuilderPool.Obtain();

            if (c.IsXmlNameStart())
            {
                do
                {
                    buffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                var temp = buffer.ToPool();

                if (c == Symbols.Semicolon)
                {
                    var p = _container.GetParameter(temp);

                    if (p != null)
                    {
                        if (use)
                        {
                            _stream.Push(temp, p.NodeValue);
                            return;
                        }
                        else
                        {
                            throw new DomException(DomError.InvalidAccess);
                        }
                    }
                }
            }

            if (use)
            {
                throw new DomException(DomError.InvalidAccess);
            }

            StringBuffer.Append(Symbols.Percent).Append(buffer.ToString());
        }

        protected String EReference(Char c)
        {
            var buffer = StringBuilderPool.Obtain();

            if (c.IsXmlNameStart())
            {
                do
                {
                    buffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                var temp = buffer.ToPool();

                if (temp.Length > 0 && c == Symbols.Semicolon)
                {
                    var p = _container.GetEntity(temp);

                    if (p != null)
                        return p.NodeValue;
                }
            }
            else if (c == Symbols.Num)
            {
                c = GetNext();
                var hex = c == 'x' || c == 'X';

                if (hex)
                    c = _stream.Next;

                while (hex ? c.IsHex() : c.IsDigit())
                {
                    buffer.Append(c);
                    c = GetNext();
                }

                var temp = buffer.ToPool();

                if (temp.Length > 0 && c == Symbols.Semicolon)
                {
                    var num = hex ? temp.FromHex() : temp.FromDec();

                    if (num.IsValidAsCharRef())
                        return Char.ConvertFromUtf32(num);

                    throw new DomException(DomError.InvalidCharacter);
                }
            }

            throw new DomException(DomError.Syntax);
        }

        #endregion

        #region Helper

        protected String ScanString(Char c, Char end)
        {
            var buffer = StringBuilderPool.Obtain();

            while (c != end)
            {
                if (c == Symbols.EndOfFile)
                {
                    buffer.ToPool();
                    throw new DomException(DomError.Syntax);
                }
                else if (c == Symbols.Percent)
                {
                    PEReference(_stream.Next, _external);
                    c = _stream.Current;
                    continue;
                }
                else if (c == Symbols.LessThan && _stream.ContinuesWith("<![CDATA["))
                {
                    Advance(8);
                    c = _stream.Next;

                    while (true)
                    {
                        if (c == Symbols.EndOfFile)
                            throw new DomException(DomError.Syntax);

                        if (c == Symbols.SquareBracketClose && _stream.ContinuesWith("]]>"))
                        {
                            _stream.Advance(2);
                            break;
                        }

                        buffer.Append(c);
                        c = GetNext();
                    }
                }
                else if (c == Symbols.Ampersand)
                {
                    buffer.Append(EReference(_stream.Next));
                }
                else
                {
                    buffer.Append(c);
                }

                c = _stream.Next;
            }

            return buffer.ToPool();
        }

        protected Char SkipSpaces(Char c)
        {
            do
            {
                c = _stream.Next;
            }
            while (c.IsSpaceCharacter());

            return c;
        }

        #endregion
    }
}
