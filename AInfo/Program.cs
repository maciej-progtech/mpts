using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Mpts.Contracts;
using Mpts.Library.Controls;
using Mpts.Library;
using Mpts.Applications.AInfo.Properties;

namespace Mpts.Applications.AInfo
{
  [Export( typeof( IMainProgram ) )]
  public class Program: MainProgram
  {
    public string AssemblyFile { get; set; }
    private FormWithPropertyGrid form;
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
      form = new FormWithPropertyGrid();
      form.Load += new EventHandler( FormAInfo_Load );
      string[] arg = Environment.GetCommandLineArgs();
      if ( arg.Length > 1 )
        AssemblyFile = arg[ 1 ];
      CheckApplicationLoopAndStartTheForm( form );
    }
    public override string Description
    {
      get { return Resources.Description; }
    }

    private void FormAInfo_Load( object sender, EventArgs e )
    {
      if ( string.IsNullOrEmpty( AssemblyFile ) )
      {
        using ( OpenFileDialog openFileDialog = new OpenFileDialog() )
        {
          if ( openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            AssemblyFile = openFileDialog.FileName;
        }
      }
      try
      {
        form.OutputControl.SelectedObject = AssemblyInformation.GetInformation( AssemblyFile );
        form.Text += String.Format( " :{0}", AssemblyFile );
      }
      catch ( Exception ex )
      {
        form.Text += " - Error";
        form.OutputControl.SelectedObject = ex;
      }
    }
  }
}
