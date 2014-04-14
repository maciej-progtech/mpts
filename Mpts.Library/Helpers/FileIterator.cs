using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mpts.Library.Helpers
{
  public static class FileIterator
  {
    public delegate void FileIteratorOperation(FileInfo fi);
    public static void Iterate(
      string DirectoryPath,
      string[] AllowedExtensions,
      FileIteratorOperation OperationToDo )
    {
      // 1. get the information of the directory
      DirectoryInfo directoryInfo = new DirectoryInfo( DirectoryPath );
      // 2. get content of the directory and go through each subdirectory
      foreach ( DirectoryInfo d in directoryInfo.GetDirectories() )
      {
        Iterate( d.FullName, AllowedExtensions, OperationToDo );
      }
      //3. loop through each file in the directory and call operation
      foreach ( FileInfo f in directoryInfo.GetFiles() )
      {
        if ( AllowedExtensions.Contains<string>( f.Extension ) )
          OperationToDo( f );
      }
    }

  }
}
