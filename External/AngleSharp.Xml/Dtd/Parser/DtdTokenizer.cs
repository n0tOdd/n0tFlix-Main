namespace AngleSharp.Xml.Dtd.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using AngleSharp.Xml.Dtd.Declaration;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The tokenizer class for Document Type Definitions.
    /// </summary>
    [DebuggerStepThrough]
    sealed class DtdTokenizer : DtdPlainTokenizer
    {
        #region Constants

        private const String ENTITY = "ENTITY";
        private const String ELEMENT = "ELEMENT";
        private const String NOTATION = "NOTATION";
        private const String ATTLIST = "ATTLIST";
        private const String EMPTY = "EMPTY";
        private const String ANY = "ANY";
        private const String PCDATA = "#PCDATA";
        private const String NDATA = "NDATA";
        private const String CDATA = "CDATA";
        private const String ID = "ID";
        private const String IDREF = "IDREF";
        private const String IDREFS = "IDREFS";
        private const String ENTITIES = "ENTITIES";
        private const String NMTOKEN = "NMTOKEN";
        private const String NMTOKENS = "NMTOKENS";
        private const String REQUIRED = "#REQUIRED";
        private const String IMPLIED = "#IMPLIED";
        private const String FIXED = "#FIXED";
        private const String PUBLIC = "PUBLIC";
        private const String SYSTEM = "SYSTEM";
        private const String INCLUDE = "INCLUDE";
        private const String IGNORE = "IGNORE";

        #endregion

        #region Fields

        private Char _endChar;
        private Int32 _includes;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new DTD tokenizer with the given source and container.
        /// </summary>
        /// <param name="container">The container to use.</param>
        /// <param name="src">The source to inspect.</param>
        public DtdTokenizer(DtdContainer container, TextSource src)
            : base(container, src)
        {
            _includes = 0;
            IsExternal = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the parsed content.
        /// </summary>
        public String Content => _stream.Content;

        /// <summary>
        /// Gets or sets if the DTD is from an external source.
        /// </summary>
        public Boolean IsExternal
        {
            get => _external;
            set
            {
                _external = value;
                _endChar = _external ? Symbols.EndOfFile : Symbols.SquareBracketClose;
            }
        }

        #endregion

        #region General

        /// <summary>
        /// Gets the next found DTD element by advancing
        /// and applying the rules for DTD.
        /// </summary>
        /// <param name="c">The current character.</param>
        /// <returns>The found declaration.</returns>
        protected override DtdToken GetElement(Char c)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (c == _endChar && _includes == 0)
            {
                return DtdToken.EOF;
            }

            if (c == Symbols.SquareBracketClose)
            {
                if (_includes > 0 && _stream.Next == Symbols.SquareBracketClose && _stream.Next == Symbols.GreaterThan)
                {
                    _includes--;
                    return GetElement(_stream.Next);
                }
            }
            else if (c == Symbols.LessThan)
            {
                c = _stream.Next;

                if (c == Symbols.QuestionMark)
                {
                    return ProcessingStart(_stream.Next);
                }
                else if (c == Symbols.ExclamationMark)
                {
                    _stream.Advance();

                    if (_stream.ContinuesWith(ENTITY))
                    {
                        _stream.Advance(5);
                        c = _stream.Next;

                        if (c.IsSpaceCharacter())
                        {
                            return EntityDeclaration(c);
                        }
                    }
                    else if (_stream.ContinuesWith(ELEMENT))
                    {
                        _stream.Advance(6);
                        c = _stream.Next;

                        if (c.IsSpaceCharacter())
                        {
                            return TypeDeclaration(c);
                        }
                    }
                    else if (_stream.ContinuesWith(ATTLIST))
                    {
                        _stream.Advance(6);
                        c = _stream.Next;

                        if (c.IsSpaceCharacter())
                        {
                            return AttributeDeclaration(c);
                        }
                    }
                    else if (_stream.ContinuesWith(NOTATION))
                    {
                        _stream.Advance(7);
                        c = _stream.Next;

                        if (c.IsSpaceCharacter())
                        {
                            return NotationDeclaration(c);
                        }
                    }
                    else if (_stream.ContinuesWith("--"))
                    {
                        _stream.Advance();
                        return CommentStart(_stream.Next);
                    }
                    else if (_stream.Current == Symbols.SquareBracketOpen && _external)
                    {
                        return Conditional(_stream.Next);
                    }
                }
            }
            else if (c == Symbols.Percent)
            {
                PEReference(_stream.Next);
                return GetElement(_stream.Current);
            }

            throw new DomException(DomError.InvalidCharacter);
        }

        #endregion

        #region Conditional

        /// <summary>
        /// Treats the conditional sects with respect.
        /// http://www.w3.org/TR/REC-xml/#sec-condition-sect
        /// </summary>
        /// <param name="c">The current character.</param>
        /// <returns>The evaluated token.</returns>
        private DtdToken Conditional(Char c)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (_stream.ContinuesWith(INCLUDE))
            {
                _stream.Advance(6);

                do c = _stream.Next;
                while (c.IsSpaceCharacter());

                if (c == Symbols.SquareBracketOpen)
                {
                    _includes++;
                    return GetElement(_stream.Next);
                }
            }
            else if (_stream.ContinuesWith(IGNORE))
            {
                _stream.Advance(5);

                do c = _stream.Next;
                while (c.IsSpaceCharacter());

                if (c == Symbols.SquareBracketOpen)
                {
                    var nesting = 0;
                    var lastThree = new[] { Symbols.Null, Symbols.Null, Symbols.Null };

                    do
                    {
                        c = _stream.Next;

                        if (c == Symbols.EndOfFile)
                        {
                            break;
                        }

                        lastThree[0] = lastThree[1];
                        lastThree[1] = lastThree[2];
                        lastThree[2] = c;

                        if (lastThree[0] == Symbols.LessThan && lastThree[1] == Symbols.ExclamationMark && lastThree[2] == Symbols.SquareBracketOpen)
                        {
                            nesting++;
                        }
                    }
                    while (nesting != 0 || lastThree[0] != Symbols.SquareBracketClose || lastThree[1] != Symbols.SquareBracketClose || lastThree[2] != Symbols.GreaterThan);

                    if (c == Symbols.GreaterThan)
                    {
                        return GetElement(_stream.Next);
                    }
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        #endregion

        #region Processing Instruction

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdToken ProcessingStart(Char c)
        {
            if (c.IsXmlNameStart())
            {
                StringBuffer.Clear();
                StringBuffer.Append(c);
                return ProcessingTarget(_stream.Next, new DtdPIToken());
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        private DtdToken ProcessingTarget(Char c, DtdPIToken pi)
        {
            while (c.IsXmlName())
            {
                StringBuffer.Append(c);
                c = _stream.Next;
            }

            pi.Target = StringBuffer.ToString();
            StringBuffer.Clear();

            if (String.Compare(pi.Target, TagNames.Xml, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return TextDecl(c);
            }

            if (c == Symbols.QuestionMark)
            {
                c = _stream.Next;

                if (c == Symbols.GreaterThan)
                {
                    return pi;
                }
            }
            else if (c.IsSpaceCharacter())
            {
                return ProcessingContent(_stream.Next, pi);
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        private DtdToken ProcessingContent(Char c, DtdPIToken pi)
        {
            while (c != Symbols.EndOfFile)
            {
                if (c == Symbols.QuestionMark)
                {
                    c = GetNext();

                    if (c == Symbols.GreaterThan)
                    {
                        pi.Content = StringBuffer.ToString();
                        return pi;
                    }

                    StringBuffer.Append(Symbols.QuestionMark);
                }
                else
                {
                    StringBuffer.Append(c);
                    c = GetNext();
                }
            }

            throw new DomException(DomError.InvalidCharacter);
        }

        #endregion

        #region Comments

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdToken CommentStart(Char c)
        {
            StringBuffer.Clear();
            return Comment(c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdToken Comment(Char c)
        {
            while (c.IsXmlChar())
            {
                if (c == Symbols.Minus)
                {
                    return CommentDash(_stream.Next);
                }

                StringBuffer.Append(c);
                c = _stream.Next;
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdToken CommentDash(Char c)
        {
            if (c == Symbols.Minus)
            {
                return CommentEnd(_stream.Next);
            }

            return Comment(c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdToken CommentEnd(Char c)
        {
            if (c == Symbols.GreaterThan)
            {
                return new DtdCommentToken() { Data = StringBuffer.ToString() };
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        #endregion

        #region Declaration Name

        private Boolean DeclarationNameBefore(Char c, DtdToken decl)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (c == Symbols.EndOfFile)
            {
                throw new DomException(DomError.InvalidCharacter);
            }

            if (c == Symbols.Percent)
            {
                PEReference(_stream.Next);
                return DeclarationNameBefore(_stream.Current, decl);
            }

            if (c.IsXmlNameStart())
            {
                StringBuffer.Clear();
                StringBuffer.Append(c);
                return DeclarationName(_stream.Next, decl);
            }

            return false;
        }

        private Boolean DeclarationName(Char c, DtdToken decl)
        {
            while (c.IsXmlName())
            {
                StringBuffer.Append(c);
                c = _stream.Next;
            }

            if (c == Symbols.Percent)
            {
                PEReference(_stream.Next);
                return DeclarationName(_stream.Current, decl);
            }

            decl.Name = StringBuffer.ToString();
            StringBuffer.Clear();

            if (c == Symbols.EndOfFile)
            {
                throw new DomException(DomError.InvalidCharacter);
            }

            return c.IsSpaceCharacter();
        }

        #endregion

        #region Entity Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-entity-decl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdToken EntityDeclaration(Char c)
        {
            var decl = new DtdEntityToken();

            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces(c);
            }

            if (c == Symbols.Percent)
            {
                decl.IsParameter = true;

                if (!_stream.Next.IsSpaceCharacter())
                {
                    throw new DomException(DomError.InvalidCharacter);
                }

                c = SkipSpaces(c);
            }

            if (DeclarationNameBefore(c, decl))
            {
                c = SkipSpaces(c);

                if (_stream.ContinuesWith(SYSTEM))
                {
                    decl.IsExtern = true;
                    _stream.Advance(5);
                    return EntityDeclarationBeforeSystem(_stream.Next, decl);
                }
                else if (_stream.ContinuesWith(PUBLIC))
                {
                    decl.IsExtern = true;
                    _stream.Advance(5);
                    return EntityDeclarationBeforePublic(_stream.Next, decl);
                }
                else if (Symbols.DoubleQuote == c || Symbols.SingleQuote == c)
                {
                    StringBuffer.Clear();
                    return EntityDeclarationValue(_stream.Next, c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdToken EntityDeclarationBeforeValue(Char c, DtdEntityToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces(c);

                if (Symbols.DoubleQuote == c || Symbols.SingleQuote == c)
                {
                    return EntityDeclarationValue(_stream.Next, c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdToken EntityDeclarationValue(Char c, Char end, DtdEntityToken decl)
        {

            decl.Value = ScanString(c, end);
            return EntityDeclarationAfter(_stream.Next, decl);
        }

        private DtdToken EntityDeclarationBeforePublic(Char c, DtdEntityToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces(c);
                StringBuffer.Clear();

                if (Symbols.DoubleQuote == c || Symbols.SingleQuote == c)
                {
                    return EntityDeclarationPublic(_stream.Next, c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdToken EntityDeclarationPublic(Char c, Char quote, DtdEntityToken decl)
        {
            while (c != quote)
            {
                if (!c.IsPubidChar())
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                StringBuffer.Append(c);
                c = _stream.Next;
            }

            decl.PublicIdentifier = StringBuffer.ToString();
            return EntityDeclarationBeforeSystem(_stream.Next, decl);
        }

        private DtdToken EntityDeclarationBeforeSystem(Char c, DtdEntityToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces(c);
                StringBuffer.Clear();

                if (Symbols.DoubleQuote == c || Symbols.SingleQuote == c)
                {
                    return EntityDeclarationSystem(_stream.Next, c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdToken EntityDeclarationSystem(Char c, Char quote, DtdEntityToken decl)
        {
            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                StringBuffer.Append(c);
                c = _stream.Next;
            }

            decl.SystemIdentifier = StringBuffer.ToString();
            return EntityDeclarationAfter(_stream.Next, decl);
        }

        private DtdToken EntityDeclarationAfter(Char c, DtdEntityToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces(c);

                if (decl.IsExtern && !decl.IsParameter && String.IsNullOrEmpty(decl.ExternNotation) && _stream.ContinuesWith(NDATA))
                {
                    _stream.Advance(4);
                    c = _stream.Next;

                    while (c.IsSpaceCharacter())
                    {
                        c = _stream.Next;
                    }

                    if (c.IsXmlNameStart())
                    {
                        StringBuffer.Clear();

                        do
                        {
                            StringBuffer.Append(c);
                            c = _stream.Next;
                        }
                        while (c.IsXmlName());

                        decl.ExternNotation = StringBuffer.ToString();
                        return EntityDeclarationAfter(c, decl);
                    }

                    throw new DomException(DomError.InvalidNodeType);
                }
            }

            if (c == Symbols.EndOfFile)
            {
                throw new DomException(DomError.InvalidCharacter);
            }
            else if (c == Symbols.GreaterThan)
            {
                return decl;
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        #endregion

        #region Attribute Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#attdecls.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdAttributeToken AttributeDeclaration(Char c)
        {
            var decl = new DtdAttributeToken();

            if (DeclarationNameBefore(_stream.Next, decl))
            {
                c = SkipSpaces(c);

                while (true)
                {
                    if (c == Symbols.GreaterThan)
                    {
                        return AttributeDeclarationAfter(c, decl);
                    }
                    else if (!c.IsXmlNameStart())
                    {
                        break;
                    }

                    StringBuffer.Clear();
                    decl.Attributes.Add(AttributeDeclarationName(c));
                    c = _stream.Current;

                    if (c.IsSpaceCharacter())
                    {
                        c = SkipSpaces(c);
                    }
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private AttributeDeclarationEntry AttributeDeclarationName(Char c)
        {
            var value = new AttributeDeclarationEntry();

            do
            {
                StringBuffer.Append(c);
                c = _stream.Next;
            }
            while (c.IsXmlName());

            if (!c.IsSpaceCharacter())
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            value.Name = StringBuffer.ToString();
            StringBuffer.Clear();
            return AttributeDeclarationType(_stream.Next, value);
        }

        private AttributeDeclarationEntry AttributeDeclarationType(Char c, AttributeDeclarationEntry value)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (c == Symbols.RoundBracketOpen)
            {
                var type = new AttributeEnumeratedType();
                value.Type = type;
                AttributeDeclarationTypeEnumeration(_stream.Next, type);
            }
            else if (c.IsUppercaseAscii())
            {
                var id = String.Empty;

                while (true)
                {
                    if (c.IsSpaceCharacter())
                    {
                        id = StringBuffer.ToString();
                        StringBuffer.Clear();
                        break;
                    }
                    else if (c == Symbols.GreaterThan)
                    {
                        throw new DomException(DomError.InvalidNodeType);
                    }
                    else if (c == Symbols.Null)
                    {
                        StringBuffer.Append(Symbols.Replacement);
                    }
                    else if (c == Symbols.EndOfFile)
                    {
                        throw new DomException(DomError.InvalidCharacter);
                    }
                    else
                    {
                        StringBuffer.Append(c);
                    }

                    c = _stream.Next;
                }

                switch (id)
                {
                    case CDATA:
                        value.Type = new AttributeStringType();
                        break;

                    case ID:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.ID };
                        break;

                    case IDREF:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.IDREF };
                        break;

                    case IDREFS:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.IDREFS };
                        break;

                    case ENTITY:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.ENTITY };
                        break;

                    case ENTITIES:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.ENTITIES };
                        break;

                    case NMTOKEN:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.NMTOKEN };
                        break;

                    case NMTOKENS:
                        value.Type = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.NMTOKENS };
                        break;

                    case NOTATION:
                        var type = new AttributeEnumeratedType { IsNotation = true };
                        value.Type = type;

                        while (c.IsSpaceCharacter())
                            c = _stream.Next;

                        if (c != Symbols.RoundBracketOpen)
                        {
                            throw new DomException(DomError.InvalidNodeType);
                        }

                        AttributeDeclarationTypeEnumeration(_stream.Next, type);
                        break;

                    default:
                        throw new DomException(DomError.InvalidNodeType);
                }
            }

            return AttributeDeclarationValue(_stream.Next, value);
        }

        private void AttributeDeclarationTypeEnumeration(Char c, AttributeEnumeratedType parent)
        {
            while (true)
            {
                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }

                if (c == Symbols.EndOfFile)
                {
                    throw new DomException(DomError.InvalidCharacter);
                }

                if (!c.IsXmlName())
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                do
                {
                    StringBuffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }

                parent.Names.Add(StringBuffer.ToString());
                StringBuffer.Clear();

                if (c == Symbols.RoundBracketClose)
                {
                    break;
                }
                else if (c == Symbols.Pipe)
                {
                    c = _stream.Next;
                }
                else
                {
                    throw new DomException(DomError.InvalidNodeType);
                }
            }
        }

        private AttributeDeclarationEntry AttributeDeclarationValue(Char c, AttributeDeclarationEntry value)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            var isfixed = false;

            if (c == Symbols.Num)
            {
                do
                {
                    StringBuffer.Append(c);
                    c = _stream.Next;

                    if (c == Symbols.EndOfFile)
                    {
                        throw new DomException(DomError.InvalidCharacter);
                    }
                    else if (c == Symbols.GreaterThan)
                    {
                        break;
                    }
                }
                while (!c.IsSpaceCharacter());

                var tag = StringBuffer.ToString();
                StringBuffer.Clear();

                switch (tag)
                {
                    case REQUIRED:
                        value.Default = new AttributeRequiredValue();
                        return value;
                    case IMPLIED:
                        value.Default = new AttributeImpliedValue();
                        return value;
                    case FIXED:
                        isfixed = true;
                        break;
                }

                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }
            }

            var defvalue = AttributeDeclarationBeforeDefaultValue(c);
            _stream.Advance();

            value.Default = new AttributeCustomValue
            {
                Value = defvalue,
                IsFixed = isfixed
            };
            return value;
        }

        private String AttributeDeclarationBeforeDefaultValue(Char c)
        {
            if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                return ScanString(_stream.Next, c);
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdAttributeToken AttributeDeclarationAfter(Char c, DtdAttributeToken decl)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (c == Symbols.GreaterThan)
            {
                return decl;
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        #endregion

        #region Notation Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#Notations.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdNotationToken NotationDeclaration(Char c)
        {
            if (c.IsSpaceCharacter())
            {
                var decl = new DtdNotationToken();

                if (DeclarationNameBefore(_stream.Next, decl))
                {
                    c = SkipSpaces(c);

                    if (_stream.ContinuesWith(PUBLIC))
                    {
                        _stream.Advance(5);
                        return NotationDeclarationBeforePublic(_stream.Next, decl);
                    }
                    else if (_stream.ContinuesWith(SYSTEM))
                    {
                        _stream.Advance(5);
                        return NotationDeclarationBeforeSystem(_stream.Next, decl);
                    }

                    return NotationDeclarationAfterSystem(c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdNotationToken NotationDeclarationBeforePublic(Char c, DtdNotationToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }

                if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote)
                {
                    return NotationDeclarationPublic(_stream.Next, c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdNotationToken NotationDeclarationPublic(Char c, Char quote, DtdNotationToken decl)
        {
            StringBuffer.Clear();

            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                {
                    throw new DomException(DomError.InvalidNodeType);
                }
                else if (!c.IsPubidChar())
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                StringBuffer.Append(c);
                c = _stream.Next;
            }

            decl.PublicIdentifier = StringBuffer.ToString();
            return NotationDeclarationAfterPublic(_stream.Next, decl);
        }

        private DtdNotationToken NotationDeclarationAfterPublic(Char c, DtdNotationToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces(c);

                if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote)
                {
                    return NotationDeclarationSystem(_stream.Next, c, decl);
                }
            }

            if (c == Symbols.GreaterThan)
            {
                return decl;
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdNotationToken NotationDeclarationBeforeSystem(Char c, DtdNotationToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }

                if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote)
                {
                    return NotationDeclarationSystem(_stream.Next, c, decl);
                }
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdNotationToken NotationDeclarationSystem(Char c, Char quote, DtdNotationToken decl)
        {
            StringBuffer.Clear();

            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                StringBuffer.Append(c);
                c = _stream.Next;
            }

            decl.SystemIdentifier = StringBuffer.ToString();
            return NotationDeclarationAfterSystem(_stream.Next, decl);
        }

        private DtdNotationToken NotationDeclarationAfterSystem(Char c, DtdNotationToken decl)
        {
            if (c.IsSpaceCharacter())
                c = SkipSpaces(c);

            if (c == Symbols.GreaterThan)
                return decl;

            throw new DomException(DomError.InvalidNodeType);
        }

        #endregion

        #region Type Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#elemdecls.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private DtdElementToken TypeDeclaration(Char c)
        {
            var decl = new DtdElementToken();

            if (DeclarationNameBefore(c, decl))
            {
                c = SkipSpaces(c);

                if (c == Symbols.RoundBracketOpen)
                {
                    return TypeDeclarationBeforeContent(_stream.Next, decl);
                }
                else if (_stream.ContinuesWith(ANY))
                {
                    _stream.Advance(2);
                    decl.Entry = ElementDeclarationEntry.Any;
                    return TypeDeclarationAfterContent(_stream.Next, decl);
                }
                else if (_stream.ContinuesWith(EMPTY))
                {
                    _stream.Advance(4);
                    decl.Entry = ElementDeclarationEntry.Empty;
                    return TypeDeclarationAfterContent(_stream.Next, decl);
                }

                return TypeDeclarationAfterContent(c, decl);
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        private DtdElementToken TypeDeclarationBeforeContent(Char c, DtdElementToken decl)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (_stream.ContinuesWith(PCDATA))
            {
                _stream.Advance(6);
                decl.Entry = TypeDeclarationMixed(_stream.Next);
            }
            else
            {
                decl.Entry = TypeDeclarationChildren(c);
            }

            return TypeDeclarationAfterContent(_stream.Current, decl);
        }

        private ElementChildrenDeclarationEntry TypeDeclarationChildren(Char c)
        {
            var entries = new List<ElementQuantifiedDeclarationEntry>();
            var connection = Symbols.Null;

            while (true)
            {
                if (entries.Count > 0)
                {
                    if (c != Symbols.Pipe && c != Symbols.Comma)
                    {
                        throw new DomException(DomError.InvalidNodeType);
                    }

                    if (entries.Count == 1)
                    {
                        connection = c;
                    }
                    else if (connection != c)
                    {
                        throw new DomException(DomError.InvalidNodeType);
                    }

                    c = _stream.Next;
                }

                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (c.IsXmlNameStart())
                {
                    var name = TypeDeclarationName(c);
                    entries.Add(name);
                }
                else if (c == Symbols.RoundBracketOpen)
                {
                    entries.Add(TypeDeclarationChildren(_stream.Next));
                }
                else
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                c = _stream.Current;

                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }

                if (c == Symbols.RoundBracketClose)
                {
                    break;
                }
            }

            c = _stream.Next;

            if (entries.Count == 0)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            if (connection == Symbols.Comma)
            {
                var sequence = new ElementSequenceDeclarationEntry();
                sequence.Sequence.AddRange(entries);
                sequence.Quantifier = TypeDeclarationQuantifier(c);
                return sequence;
            }
            else
            {
                var choice = new ElementChoiceDeclarationEntry();
                choice.Choice.AddRange(entries);
                choice.Quantifier = TypeDeclarationQuantifier(c);
                return choice;
            }
        }

        private ElementNameDeclarationEntry TypeDeclarationName(Char c)
        {
            StringBuffer.Clear();
            StringBuffer.Append(c);

            while ((c = _stream.Next).IsXmlName())
            {
                StringBuffer.Append(c);
            }

            return new ElementNameDeclarationEntry
            {
                Name = StringBuffer.ToString(),
                Quantifier = TypeDeclarationQuantifier(c)
            };
        }

        private ElementQuantifier TypeDeclarationQuantifier(Char c)
        {
            switch (c)
            {
                case Symbols.Asterisk:
                    _stream.Advance();
                    return ElementQuantifier.ZeroOrMore;

                case Symbols.QuestionMark:
                    _stream.Advance();
                    return ElementQuantifier.ZeroOrOne;

                case Symbols.Plus:
                    _stream.Advance();
                    return ElementQuantifier.OneOrMore;

                default:
                    return ElementQuantifier.One;
            }
        }

        private ElementMixedDeclarationEntry TypeDeclarationMixed(Char c)
        {
            var entry = new ElementMixedDeclarationEntry();

            while (true)
            {
                while (c.IsSpaceCharacter())
                {
                    c = _stream.Next;
                }

                if (c == Symbols.RoundBracketClose)
                {
                    c = _stream.Next;

                    if (c == Symbols.Asterisk)
                    {
                        entry.Quantifier = ElementQuantifier.ZeroOrMore;
                        _stream.Advance();
                        return entry;
                    }

                    if (entry.Names.Count == 0)
                    {
                        break;
                    }
                }
                else if (c == Symbols.Pipe)
                {
                    c = _stream.Next;

                    while (c.IsSpaceCharacter())
                    {
                        c = _stream.Next;
                    }

                    StringBuffer.Clear();

                    if (c.IsXmlNameStart())
                    {
                        StringBuffer.Append(c);

                        while ((c = _stream.Next).IsXmlName())
                        {
                            StringBuffer.Append(c);
                        }

                        entry.Names.Add(StringBuffer.ToString());
                        continue;
                    }
                }

                throw new DomException(DomError.InvalidNodeType);
            }

            return entry;
        }

        private DtdElementToken TypeDeclarationAfterContent(Char c, DtdElementToken decl)
        {
            while (c.IsSpaceCharacter())
            {
                c = _stream.Next;
            }

            if (c == Symbols.GreaterThan)
            {
                return decl;
            }

            throw new DomException(DomError.InvalidNodeType);
        }

        #endregion
    }
}
