using System;
using Iesi.Collections.Generic;

namespace TreeStructure
{
    public class Node
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Node Parent { get; set; }
        public virtual ISet<Node> Children { get; set; }

        public virtual ISet<Node> Ancestors { get; set; }
        public virtual ISet<Node> Descendants { get; set; }

        public Node()
        {
            Children = new HashedSet<Node>();
            Ancestors = new HashedSet<Node>();
            Descendants = new HashedSet<Node>();
        }

        public virtual void AddChild(Node childNode)
        {
            Children.Add(childNode);
            childNode.Parent = this;
            childNode.Ancestors.AddAll(this.Ancestors);
            childNode.Ancestors.Add(this);
            this.Descendants.Add(childNode);
        }
    }
}