using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Shell;

namespace Taskbar11
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // IMPORTANT: Project Name Must Be _Your_ Project Name, Otherwise The Assembly Won't Be Able To Load! 
        private static String ProjectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        private static String AssemblyPathPrefix = ProjectName+'.';
        private const String DllExtension = ".dll";
        private const String AssemblyNotFoundWarning = "ERROR: Assembly Not Found!";
        private const String AssemblyLoaded = "Log: Loaded (Previously Unresolved) Embedded Assembly: ";

        [STAThread]
        public static void Main()
        {
            InitializeDependencies();
            StartProgram();
        }

        /// <summary>
        /// First load the neccessary assemblies.
        /// </summary>
        public static void InitializeDependencies()
        {
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// Called after loading the neccessary assemblies.
        /// </summary>
        public static void StartProgram()
        {
            var application = new App();
            application.InitializeComponent();
            application.Run();
        }

        /// <summary>
        /// Called when an assembly was loaded.
        /// </summary>
        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine(AssemblyLoaded+args.LoadedAssembly.FullName);
        }

        /// <summary>
        /// Load the assembly Microsoft.WindowsAPICodePack.Shell.dll which is configured to be embedded in the target executable (Build Action: "Embedded Resource").   
        /// </summary>
        /// <returns>Assembly from Taskbar11.Microsoft.WindowsAPICodePack.Shell.dll</returns>
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            
            int charLocation = args.Name.IndexOf(',');
            if (charLocation > 0)
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(AssemblyPathPrefix+args.Name.Substring(0,charLocation) + DllExtension))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            return null;
        }
    }
}
