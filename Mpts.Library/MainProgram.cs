using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mpts.Contracts;
using System.Reflection;
using System.Windows.Forms;

namespace Mpts.Library
{
  public abstract class MainProgram: IMainProgram
  {
    #region IMainProgram Members
    public string Name
    {
      get { return Assembly.GetAssembly(this.GetType()).GetName().Name; }
    }
    public abstract void Run();
    public abstract string Description { get; }
    public void CheckApplicationLoopAndStartTheForm( Form f )
    {
      if ( Application.MessageLoop )
        f.Show();
      else
        Application.Run( f );
    }
    #endregion
  }
}
