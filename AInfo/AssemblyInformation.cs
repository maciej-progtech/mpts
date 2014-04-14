using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Mpts.Applications.AInfo
{
  class AssemblyInformation
  {
    public class ModuleInformation
    {
      [TypeConverterAttribute( typeof( ExpandableObjectConverter ) )]
      public Module Module { get; private set; }
      [TypeConverterAttribute( typeof( ExpandableObjectConverter ) )]
      public PortableExecutableKinds PortableExecutableKind { get; private set; }
      [TypeConverterAttribute( typeof( ExpandableObjectConverter ) )]
      public ImageFileMachine ImageFileMachine { get; private set; }
      public override string ToString()
      {
        return string.Format(
          "Module: {0}, PortableExecutableKind: {1}, ImageFileMachine: {2}",
          Module,
          PortableExecutableKind,
          ImageFileMachine );
      }
      public ModuleInformation( Module m )
      {
        this.Module = m;
        PortableExecutableKinds peKind;
        ImageFileMachine machine;
        this.Module.GetPEKind( out peKind, out machine );
        this.PortableExecutableKind = peKind;
        this.ImageFileMachine = machine;
      }
    }
    [TypeConverterAttribute( typeof( ExpandableObjectConverter ) )]
    public Assembly Assembly { get; private set; }
    [TypeConverterAttribute( typeof( ExpandableObjectConverter ) )]
    public AssemblyName AssemblyName { get; private set; }
    public AssemblyName[] ReferencedAssemblies { get; private set; }
    public Exception Error { get; private set; }
    public List<ModuleInformation> Modules { get; private set; }
    private AssemblyInformation( string FileName )
    {
      this.AssemblyName = AssemblyName.GetAssemblyName( FileName );
      try
      {
        this.Assembly = System.Reflection.Assembly.LoadFile( FileName );
        this.ReferencedAssemblies = this.Assembly.GetReferencedAssemblies();
        Module[] modules = this.Assembly.GetModules();
        Modules = new List<ModuleInformation>();
        for ( int i = 0; i < modules.Length; i++ )
        {
          Modules.Add( new ModuleInformation( modules[ i ] ) );
        }
      }
      catch ( Exception ex )
      {
        this.Assembly = null;
        this.ReferencedAssemblies = null;
        this.Modules = null;
        this.Error = ex;
      }
    }
    public static AssemblyInformation GetInformation( string FileName )
    {
      return new AssemblyInformation( FileName );
    }
  }
}
