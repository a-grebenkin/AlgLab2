using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgLab2
{
    public partial class Form1 : Form
    {
        RedBlackTree tree;
        public Form1()
        {
            InitializeComponent();
            tree = null;
            treeView1.ShowLines = false;
            treeView1.ShowPlusMinus = false;
        }

        private void RenderTree()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            if (tree != null)
                AddInTree(tree);
            treeView1.EndUpdate();
            treeView1.ExpandAll();
        }

        private void AddInTree(RedBlackTree tree, TreeNode parent = null)
        {
            var node = new TreeNode(tree.Value.ToString());
            node.ForeColor = Color.White;
            if (tree.Color == ColorNode.Black)
                node.BackColor = Color.Black;
            else
                node.BackColor = Color.Red;
            if (tree.Parent == null)
            {
                treeView1.Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node);
            }
            parent = node;
            if (tree.Left != null)
                AddInTree(tree.Left, parent);
            if (tree.Right != null)
                AddInTree(tree.Right, parent);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(textBox1.Text);
            if (tree == null)
                tree = new RedBlackTree(value);
            else
                tree.Add(value);
            RenderTree();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(textBox1.Text);
            if (tree != null)
                if (tree.Remove(value))
                    tree = null; // удалили последний элемент
            RenderTree();
        }
    }
}
