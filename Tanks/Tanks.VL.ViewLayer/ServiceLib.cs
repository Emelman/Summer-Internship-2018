using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer
{
    class ServiceLib
    {
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        public static DialogResult WarningMessage(string sentence1, string sentence2, MessageBoxButtons specificBox = MessageBoxButtons.YesNo)
        {
            var res = MessageBox.Show(
                sentence1,
                sentence2,
                specificBox,
                MessageBoxIcon.Warning);
            return res;
        }
        public static int SwitchOppositeDirection(CoreModel model)
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    return (int)EnumDirections.Direction.DOWN;
                case (int)EnumDirections.Direction.DOWN:
                    return (int)EnumDirections.Direction.UP;
                case (int)EnumDirections.Direction.LEFT:
                    return (int)EnumDirections.Direction.RIGHT;
                case (int)EnumDirections.Direction.RIGHT:
                    return (int)EnumDirections.Direction.LEFT;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        public static int SwitchNextDirection(CoreModel model)
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    return (int)EnumDirections.Direction.RIGHT;
                case (int)EnumDirections.Direction.DOWN:
                    return (int)EnumDirections.Direction.LEFT;
                case (int)EnumDirections.Direction.LEFT:
                    return (int)EnumDirections.Direction.UP;
                case (int)EnumDirections.Direction.RIGHT:
                    return (int)EnumDirections.Direction.DOWN;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }
    }
}
