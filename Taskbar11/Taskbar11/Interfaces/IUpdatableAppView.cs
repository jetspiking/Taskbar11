using System.Windows;

namespace Taskbar11.Interfaces
{
    public interface IUpdatableAppView
    {
        void Update();
        FrameworkElement GetView();
    }
}
