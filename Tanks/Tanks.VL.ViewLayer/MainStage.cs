using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.VL.ViewLayer
{
    public partial class MainStage : Form
    {
        public MainStage(string[] args)
        {
            InitializeComponent();
            this.Size = new Size(int.Parse(args[0]), int.Parse(args[1]));
        }
    }
}
