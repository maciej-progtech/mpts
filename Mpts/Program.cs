using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Forms;
using Mpts.Library;
using System.Reflection;
using System.IO;
using Mpts.Properties;

namespace Mpts
{
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
    [Import]
    InformationForm form;
    public override void Run()
    {
      

      string currentDir = new FileInfo( Assembly.GetExecutingAssembly().Location ).DirectoryName;


      var catalog = new AggregateCatalog();
      //catalog.Catalogs.Add( new AssemblyCatalog( typeof( Program ).Assembly ) );
      catalog.Catalogs.Add( new DirectoryCatalog( currentDir, "*.*") );
      var container = new CompositionContainer( catalog );
      //CompositionBatch batch = new CompositionBatch();
      //batch.AddPart( AttributedModelServices.CreatePart( form ) );
      container.ComposeParts( this );
      CheckApplicationLoopAndStartTheForm( form );
    }
    public override string Description
    {
      get { return Resources.Description; }
    }

  }
}
