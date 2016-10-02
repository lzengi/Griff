//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Griff.Definitions.Fast_Maps
//{
//    sealed class FastMap
//    {
//        FastMapNode root;

//        internal void Add(FastMapNode node)
//        {
//            if (root == null)
//            {
//                root = node;
//            }
//            else
//            {
//                bool done = false;
//                FastMapNode current = root;
//                while (!done)
//                {
//                    if (node.Key >= current.Key)
//                    {
//                        FastMapNode prev = current;
//                        current = current.Right;
//                        if (current == null)
//                        {
//                            current = node;
//                            prev.Right = current;
//                            done = true;
//                        }
//                    }
//                    else
//                    {
//                        FastMapNode prev = current;
//                        current = current.Left;
//                        if (current == null)
//                        {
//                            current = node;
//                            prev.Left = current;
//                            done = true;
//                        }
//                    }
//                }
//            }
//        }

//        internal FastMapNode this[ulong key]
//        {
//            get
//            {
//                FastMapNode current = root;
//                do
//                {
//                    if (key == current.Key)
//                    {
//                        return current;
//                    }
//                    else if (key > current.Key)
//                    {
//                        current = current.Right;
//                    }
//                    else
//                    {
//                        current = current.Left;
//                    }
//                }
//                while (current != null);
//                return null;
//            }
//        }
//    }
//}
