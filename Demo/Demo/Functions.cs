using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Functions
    {
        public void DoWork()
        {
            var f = new Func<double,double>(x => x+1);
            var g = (Delegate) f;

            var list = new List<Delegate>();
            list.Add(f);
            list.Add(g);
            //list.Add(method);
            list.Add(new Func<double,double>(method));
            //list.Add((Delegate)method);
            list.Add((Func<double,double>)method);

        }

        static double method(double x)
        {
            return x + 1;
        }
    }
}
