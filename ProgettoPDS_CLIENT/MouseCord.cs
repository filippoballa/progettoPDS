using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_CLIENT
{
    class MouseCord
    {
        public int X;
        public int Y;

        public MouseCord( int a, int b) {
            this.X = a;
            this.Y = b;
        }

        public int aggiornaCordinate( int a, int b) {
            
            if (a == this.X && b == this.Y)
                return 0;
            else {
                this.X = a;
                this.Y = b;
                return 1;
            }
        }
    }
}
