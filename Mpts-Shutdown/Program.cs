using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Mpts.Contracts;
using Mpts.Library;
using Mpts.Applications.Shutdown.Properties;

namespace Mpts.Applications.Shutdown
{
  [Export( typeof( IMainProgram ) )]
  public class Program: MainProgram
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault( false );
      Program p = new Program();
      p.Run();
    }
    public override void Run()
    {
      CheckApplicationLoopAndStartTheForm( new FormShutdown() );
    }
    public override string Description
    {
      get { return Resources.Description; }
    }
  }
}
