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
        private double _minDistance;
        private Node _closest;
        /// <summary>
        /// 构建KD树
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool BuildKNN(List<double[]> points)
        {
            if (points.Count == 0) return false;
            _dim = points[0].Length;
            _kdRoot = BuildDFS(0, points.Count - 1, points, 0, null);
            _minDistance = double.MaxValue;
            _closest = null;
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
        private Node BuildDFS(int left, int right, List<double[]> points, int curdim, Node parent)
        {
            if (left > right) return null;
            points.Sort(left, right - left + 1, new Compare(curdim));
            int mid = (left + right) / 2;

            Node cur = new Node(points[mid]);
            cur.Parent = parent;
            cur.Dimension = curdim;
            cur.Left = BuildDFS(left, mid - 1, points, (curdim + 1) % _dim, cur);
            cur.Right = BuildDFS(mid + 1, right, points, (curdim + 1) % _dim, cur);

            return cur;
        }

        /// <summary>
        /// 外部调用
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double[] SearchClosest(double[] point)
        {
            _closest = null;
            _minDistance = double.MaxValue;
            SearchDFS(_kdRoot, point);
            return _closest?.Val;
        }
        /// <summary>
        /// 递归查找
        /// </summary>
        /// <param name="n"></param>
        /// <param name="point"></param>
        private void SearchDFS(Node n, double[] point)
        {
            if (n == null) return;
            bool isleft = false;
            double dis = GetDistance(n.Val, point);
            if (dis < _minDistance)
            {
                _minDistance = dis;
                _closest = n;
            }

            if (point[n.Dimension] < n.Val[n.Dimension])
            {
                if (n.Left != null)
                {
                    isleft = true;
                    SearchDFS(n.Left, point);
                }
                else
                    return;
            }
            else if (point[n.Dimension] >= n.Val[n.Dimension])
            {
                if (n.Right != null)
                {
                    isleft = false;
                    SearchDFS(n.Right, point);
                }
                else
                    return;
            }
            if (_closest != null)
            {
                if (_minDistance > Math.Abs(n.Val[n.Dimension] - _closest.Val[n.Dimension]))
                    if (isleft)
                        SearchDFS(n.Right,point);
                    else
                        SearchDFS(n.Left,point);
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
