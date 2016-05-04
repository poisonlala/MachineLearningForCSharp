using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    class KNN
    {
        private Node _kdRoot;
        private int _dim;

        /// <summary>
        /// 构建KD树
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool BuildKNN(List<double[]> points)
        {
            if (points.Count == 0) return false;
            _dim = points[0].Length;
            _kdRoot = DFS(0, points.Count - 1, points, 0);
            return true;
        }

        /// <summary>
        /// 递归构建
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="points"></param>
        /// <param name="sortindex"></param>
        /// <returns></returns>
        private Node BuildDFS(int left, int right, List<double[]> points, int curdim)
        {
            if (left > right) return null;
            points.Sort(left, right - left + 1, new Compare(curdim));
            int mid = (left + right) / 2;
            Node cur = new Node(points[mid]);
            cur.Left = BuildDFS(left, mid - 1, points, (curdim + 1) % _dim);
            if (cur.Left != null)
                cur.Left.Parent = cur;
            cur.Right = BuildDFS(mid + 1, right, points, (curdim + 1) % _dim);
            if (cur.Right != null)
                cur.Right.Parent = cur;
            return cur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double[] SearchClosest(double[] point)
        {
            return SearchDFS(_kdRoot, point, 0);
        }

        private double[] SearchDFS(Node n, double[] point, int curdim)
        {
            if (n.Left == null && n.Right == null) return n.Val;
            double[] closest = null;
            if (n.Left != null)
                closest = SearchDFS(n.Left, point, (curdim + 1) % _dim);
            if (n.Right != null)
            {
                if (closest == null)
                    closest = SearchDFS(n.Right, point, (curdim + 1) % _dim);
                else
                {
                    var rclosest = SearchDFS(n.Right, point, (curdim + 1) % _dim);
                    if (GetDistance(rclosest, point) < GetDistance(closest, point))
                        closest = rclosest;
                }
            }

        }

        private double GetDistance(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
                result += Math.Sqrt((b[i] - a[i]) * (b[i] - a[i]));
            return result;
        }
    }
}
