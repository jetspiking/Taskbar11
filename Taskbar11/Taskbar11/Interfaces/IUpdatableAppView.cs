using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Taskbar11.Interfaces
{
    public interface IUpdatableAppView
    {
        void Update();
        FrameworkElement GetView();
    }
}
