namespace AngleSharp.Xml.Dtd.Declaration
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    sealed class NodeInspector
    {
        private readonly List<INode> nodes;

        public NodeInspector(IElement element)
        {
            nodes = new List<INode>();

            foreach (var child in element.ChildNodes)
            {
                if ((child is IText && !String.IsNullOrEmpty(((IText)child).Text)) || child is IElement)
                {
                    nodes.Add(child);
                }
            }
        }

        public List<INode> Children => nodes;

        public INode Current => Children[Index];

        public Int32 Length => Children.Count;

        public Int32 Index
        {
            get;
            set;
        }

        public Boolean IsCompleted => Children.Count == Index;
    }
}
