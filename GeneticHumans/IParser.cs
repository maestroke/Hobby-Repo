﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticHumans {
    public interface IParser {
        void Parse(InputRoot input, World world);
    }
}
