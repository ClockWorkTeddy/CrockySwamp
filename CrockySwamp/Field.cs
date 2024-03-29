﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal class Field
    {
        public enum FieldState
        {
            Crock = 2,
            Frog = 1,
            Empty = 0
        }

        public Point Location { get; set; }
        public FieldState State { get; set; }

        public Beast? Beast { get; set; } = null;

        public Field (Point location, FieldState state = FieldState.Empty)
        {
            Location = location;
            State = state;
        }
    }
}
