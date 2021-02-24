using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace riGO
{
    interface Module
    {
        void start();
        void stop();
        void toggle();
        String getName();
        bool isRunning();
    }
}
