using System;

namespace AlgLab2
{
    public enum ColorNode
    {
        Red,
        Black
    }

    internal class RedBlackTree
    {
        public RedBlackTree Parent { get; private set; }
        public RedBlackTree Left { get; private set; }
        public RedBlackTree Right { get; private set; }
        public int Value { get; private set; }

        public ColorNode Color { get; }

        public RedBlackTree(int value, RedBlackTree parent = null)
        {
            Value = value;
            Parent = parent;
            Color = ColorNode.Red;
        }

        public void Add(int value)
        {
            if (value < Value)
                if (Left == null)
                    Left = new RedBlackTree(value, this);
                else
                    Left.Add(value);
            else
                if (Right == null)
                    Right = new RedBlackTree(value, this);
                else
                    Right.Add(value);
        }

        private RedBlackTree Search(int value)
        {
            if (value < Value) 
                return Left?.Search(value);
            if (value > Value) 
                return Right?.Search(value);

            return Value == value ? this : null;
        }

        private static bool Remove(RedBlackTree tree)
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
                    return true; //TODO: удаление последнего элемента

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
            if (tree.Left == null || tree.Right == null) 
                return false;
            var current = tree.Right;

            while (current.Left != null) 
                current = current.Left;

            tree.Value = current.Value;
            Remove(current);

            return false;
        }

        public bool Remove(int value)
        {
            var tree = Search(value);
            return Remove(tree);
        }

        private void Print(RedBlackTree tree)
        {
            if (tree.Left != null) 
                Print(tree.Left);
            Console.Write(tree.Value + " ");
            if (tree.Right != null) 
                Print(tree.Right);
        }
    }
}