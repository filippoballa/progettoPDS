using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_CLIENT
{
    class MouseCord
    {
        private int cordX;
        private int cordY;
        private const int DIFF = 5;

        public MouseCord() 
        {
            this.cordX = 0;
            this.cordY = 0;
        }

        public MouseCord(int x, int y)
        {
            this.cordX = x;
            this.cordY = y;
        }

        public int CordX
        {
            get { return this.cordX; }
            set { this.cordX = value; }
        }

        public int CordY
        {
            get { return this.cordY; }
            set { this.cordX = value; }
        }

        public bool aggiornaCord(int x, int y)
        { 
            bool result = false;
            int part1 = Math.Abs(this.cordX - x );
            int part2 = Math.Abs(this.cordY - y );

            if ( part1 > DIFF || part2 > DIFF )
                result = true;

            if (result) {
                this.cordX = x;
                this.cordY = y;
            }

            return result;

        }
    }
}
