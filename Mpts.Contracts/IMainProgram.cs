using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mpts.Contracts
{
  public interface IMainProgram
  {
    void Run();
    string Name { get; }
    string Description { get; }
  }
}
