﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KrestNol
{
    class Point
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
