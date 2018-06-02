using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface IModel
    {
        int GetId { get; set; }
        void callSomething();
    }
}
