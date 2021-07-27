using System.Collections;
using System.Collections.Generic;
using Services.Run.Core.Entities;

namespace Services.Run.Application
{
    public class ByOrderExtension : IComparer<Point>
    {
        public int Compare(Point x, Point y)
            => (new CaseInsensitiveComparer()).Compare(x.Order, y.Order);
    }
}