using System;

namespace AlgLab2
{
    public enum ColorNode
    {
        Red,
        Black
    }
    class RBTree
    {
        public RBTree Parent
        {
            get; private set;
        }
        public RBTree Left
        {
            get; private set;
        }
        public RBTree Right
        {
            get; private set;
        }
        public int Value
        {
            get; private set;
        }
        public ColorNode Color
        {
            get; private set;
        }

        public RBTree(int value, RBTree parent = null)
        {
            Value = value;
            Parent = parent;
            Color = ColorNode.Red;
        }

        public void Add(int value)
        {
            if (value < Value)
            {
                if (Left == null)
                {
                    Left = new RBTree(value, this);
                }
                else
                {
                    Left.Add(value);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new RBTree(value, this);
                }
                else
                {
                    Right.Add(value);
                }
            }
        }

        private RBTree Search(int value)
        {
            if (value < Value)
                return Left?.Search(value) ?? null;
            if (value > Value)
                return Right?.Search(value) ?? null;
            if (Value == value)
                return this;
            return null;
        }


        private bool Remove(RBTree tree)
        {
            if (tree == null)
                return false;

            //Корень
            if (tree.Parent == null)
            {
                if (tree.Left != null)
                {
                    tree.Value = tree.Left.Value;
                    Remove(tree.Left);
                }
                else if (tree.Right != null)
                {
                    tree.Value = tree.Right.Value;
                    Remove(tree.Right);
                }
                else
                    return true;//TODO: удаление последнего элемента
                return false;
            }
            //Лист
            if (tree.Left == null && tree.Right == null)
            {
                if (tree.Parent.Left == tree)
                    tree.Parent.Left = null;
                else
                    tree.Parent.Right = null;
            }

            //Узел с левым поддеревом
            if (tree.Left != null && tree.Right == null)
            {
                tree.Left.Parent = tree.Parent;
                if (tree.Parent.Left == tree)
                    tree.Parent.Left = tree.Left;
                else
                    tree.Parent.Right = tree.Left;
            }

            ///Узел с правым поддеревом
            if (tree.Left == null && tree.Right != null)
            {
                tree.Right.Parent = tree.Parent;
                if (tree.Parent.Left == tree)
                    tree.Parent.Left = tree.Right;
                else
                    tree.Parent.Right = tree.Right;
            }

            ///Узел с правым и левым поддеревом
            if (tree.Left != null && tree.Right != null)
            {
                RBTree current = tree.Right;
                while (current.Left != null)
                    current = current.Left;

                tree.Value = current.Value;
                Remove(current);
            }

            return false;

        }

        public bool Remove(int value)
        {
            RBTree tree = Search(value);
            return Remove(tree);
        }


        private void Print(RBTree tree)
        {
            if (tree.Left != null)
                Print(tree.Left);
            Console.Write(tree.Value + " ");
            if (tree.Right != null)
                Print(tree.Right);
        }

        public void Print()
        {
            Console.WriteLine();
            Print(this);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
