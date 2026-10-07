using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.lab2
{
    public class SphereSegment
    {
        public double R;

        private double h;

        public SphereSegment()
        {
            R = 0;
            h = 0;
        }
        public SphereSegment(double R)
        {
            this.R = R;
            h = 0;
        }
        public SphereSegment(double R, double h)
        {
            this.R = R;
            this.h = h;
        }
        public double GetVolume()
        {
            return Math.PI * h * h * (R - h / 3.0);
        }
        public double GetBaseRadius()
        {
            return Math.Sqrt(h * (2 * R - h));
        }
        public double GetCurvedSurfaceArea()
        {
            return 2 * Math.PI * R * h;
        }
    }
}