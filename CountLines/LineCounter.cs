using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mpts.Library.Helpers;
using System.IO;
using Mpts.Applications.CountLines.Properties;

namespace Mpts.Applications.CountLines
{
  class LineCounter
  {
    StringBuilder errors = new StringBuilder();
    int LinesCount = 0;
    public LineCounter( string DirectoryPath )
    {
      StringBuilder sb_output = new StringBuilder();
      using ( FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog() )
      {
        if ( string.IsNullOrEmpty( DirectoryPath ) )
        {
          if ( folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            DirectoryPath = folderBrowserDialog.SelectedPath;
        }
      }
      sb_output.Append( "Directory to be analysed: " );
      sb_output.AppendLine( DirectoryPath );
      sb_output.AppendLine();
      try
      {
        FileIterator.Iterate(
          DirectoryPath,
          new string[] { ".cs" },
          new FileIterator.FileIteratorOperation( LoadFileAndCountLines ) );
      }
      catch ( Exception ex )
      {
        errors.AppendLine( string.Format( Resources.Error, ex.Message ) );
      }
      string errors_string = errors.ToString();
      if ( !string.IsNullOrEmpty( errors_string ) )
      {
        sb_output.AppendLine( "Errors:" );
        sb_output.AppendLine( errors_string );
      }
      sb_output.AppendLine( "Result:" );
      sb_output.AppendLine( string.Format(Resources.number_of_lines, LinesCount));
      Result=sb_output.ToString();
    }
    public string Result { get; private set; }

    private static int CountLines( FileInfo f )
    {
      // Read in every line in the file.
      using ( StreamReader reader = new StreamReader( f.FullName ) )
      {
        int counter = 0;
        string line;
        while ( ( line = reader.ReadLine() ) != null )
        {
          counter++;
        }
        return counter;
      }
    }
    private void LoadFileAndCountLines( FileInfo f )
    {
      try
      {
        LinesCount += CountLines( f );
      }
      catch ( Exception ex )
      {
        errors.AppendLine(string.Format(Resources.ErrorInFile,f.FullName, ex.Message));
      }
    }
  }
}
