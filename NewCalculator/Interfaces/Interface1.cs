using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NewCalculator.Interfaces
{
    public interface IGroupValues
    {
        void SetCalculateValue();
        void SetResetToZero(string sender, string rule);
        void SetResetAndKeepToZero();
        void SetChangePlusMinusValue();

    }
}
