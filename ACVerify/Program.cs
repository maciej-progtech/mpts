using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Forms;
using Mpts.Applications.ACVerify.Properties;
using Mpts.Contracts;
using Mpts.Library;
using Mpts.Library.Controls;

namespace Mpts.Applications.ACVerify
{
  [Export( typeof( IMainProgram ) )]
  public class Program: MainProgram
  {
    FormWithSimpleOutput form;
    string StartDirectory = string.Empty;
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
      form = new FormWithSimpleOutput();
      form.Load += new EventHandler( form_Load );
      string[] arg = Environment.GetCommandLineArgs();
      if ( arg.Length > 1 )
        StartDirectory = arg[ 1 ];
      CheckApplicationLoopAndStartTheForm( form );
    }
    public override string Description
    {
      get { return Resources.Description; }
    }

    void form_Load( object sender, EventArgs e )
    {
      form.Text = String.Format( "{0}: {1}", Assembly.GetExecutingAssembly().GetName().Name, StartDirectory );
      form.OutputControl.Text = new AssemblyConsistencyVerifier( StartDirectory ).Result;
    }

  }
}
