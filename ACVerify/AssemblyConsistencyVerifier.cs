using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using Mpts.Applications.ACVerify.Properties;
using Mpts.Library.Helpers;
using Mpts.Library.Controls;

namespace Mpts.Applications.ACVerify
{
  class AssemblyConsistencyVerifier
  {
    StringBuilder sb_Errors = new StringBuilder();
    Dictionary<string, Dictionary<string, AssemblyName>> RealAssembliesDictionary
      = new Dictionary<string, Dictionary<string, AssemblyName>>();
    Dictionary<string, Dictionary<string, AssemblyName>> RealAndReferencedAssembliesDictionary
      = new Dictionary<string, Dictionary<string, AssemblyName>>();
    Dictionary<string, List<AssemblyName>> ReferencesDictionary
      = new Dictionary<string, List<AssemblyName>>();
    private void LoadAsseblyAndAnalyseIt( FileInfo f )
    {
      //creation of new domain:
      AppDomainSetup appDSInfo = new AppDomainSetup();
      appDSInfo.ApplicationBase = f.DirectoryName;
      appDSInfo.ApplicationName = f.Name;
      appDSInfo.DynamicBase = f.DirectoryName;
      Evidence evidence = new Evidence( AppDomain.CurrentDomain.Evidence );

      AppDomain myDomain = AppDomain.CreateDomain( string.Format( "MyNewDomain{0}", f.Name ), evidence, appDSInfo );
      // assembly:
      Assembly assembly = null;
      AssemblyName asname = null;
      try
      {
        asname = AssemblyName.GetAssemblyName( f.FullName );
        CheckAndAddToDictionary( asname, RealAssembliesDictionary );
        CheckAndAddToDictionary( asname, RealAndReferencedAssembliesDictionary );
        if ( Settings.Default.LoadAssembliesIntoDifferentDomain )
          assembly = myDomain.Load( asname );
        else
          assembly = Assembly.Load( asname );
      }
      catch ( Exception ex )
      {
        sb_Errors.AppendLine( string.Format( "Cannot process {0} (reason: {1})", f.FullName, ex.Message ) );
        assembly = null;
      }
      if ( asname != null && assembly != null )
      {
        foreach ( var referenced in assembly.GetReferencedAssemblies() )
        {
          CheckAndAddToDictionary( referenced, RealAndReferencedAssembliesDictionary );
          if ( !ReferencesDictionary.ContainsKey( referenced.FullName ) )
          {
            ReferencesDictionary.Add( referenced.FullName, new List<AssemblyName>() );
          }
          ReferencesDictionary[ referenced.FullName ].Add( asname );
        }
      }
      AppDomain.Unload( myDomain );
    }

    private static void CheckAndAddToDictionary( AssemblyName an, Dictionary<string, Dictionary<string, AssemblyName>> CurrentAssembliesDictionary )
    {
      if ( !CurrentAssembliesDictionary.ContainsKey( an.Name ) )
      {
        CurrentAssembliesDictionary.Add( an.Name, new Dictionary<string, AssemblyName>() );
      }
      bool skip = false;
      if ( Settings.Default.SuppressAssembliesThatConainsOnlyResourcesForParticularCulture )
      {
        foreach ( var element in CurrentAssembliesDictionary[ an.Name ] )
        {
          if ( an.Name == element.Value.Name && an.Version.Equals( element.Value.Version ) )
          {
            skip = true;
            break;
          }
        }
      }
      if ( !CurrentAssembliesDictionary[ an.Name ].ContainsKey( an.FullName ) && !skip )
        CurrentAssembliesDictionary[ an.Name ].Add( an.FullName, an );
    }
    #region static
    static string[] CommonAssemblies = null;
    static AssemblyConsistencyVerifier()
    {
      if ( Settings.Default.SuppressCommonAssembliesInconsistancy )
        CommonAssemblies = Settings.Default.CommonAssembliesSeparatedBySemicolon.Split( ';' );
    }
    #endregion static
    public string Result { get; private set; }
    public AssemblyConsistencyVerifier( string DirectoryPath )
    {
      StringBuilder sb_output = new StringBuilder();
      using ( AdvancedFolderBrowserDialog folderBrowserDialog = new AdvancedFolderBrowserDialog() )
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
          new string[] { ".dll", ".exe" },
          new FileIterator.FileIteratorOperation( LoadAsseblyAndAnalyseIt ) );
        string errors = sb_Errors.ToString();
        if ( !string.IsNullOrEmpty( errors ) )
        {
          sb_output.AppendLine( "Errors during processing:" );
          sb_output.AppendLine( errors );
        }
        sb_output.AppendLine( "Results (real assemblies):" );
        foreach ( KeyValuePair<string, Dictionary<string, AssemblyName>> kvp in RealAssembliesDictionary )
        {
          if ( kvp.Value.Count > 1 )
          {
            sb_output.AppendLine( "Inconsistent assemblies:" );
            foreach ( var kvp_element in kvp.Value )
            {
              sb_output.AppendLine( kvp_element.Value.FullName + " - " + kvp_element.Value.CodeBase );
            }
            sb_output.AppendLine();
          }
        }
        sb_output.AppendLine();
        sb_output.AppendLine( "Results (references):" );
        foreach ( KeyValuePair<string, Dictionary<string, AssemblyName>> kvp in RealAndReferencedAssembliesDictionary )
        {
          if ( kvp.Value.Count > 1 )
          {
            #region kvp.Value.Count > 1
            if ( CommonAssemblies != null && CommonAssemblies.Contains( kvp.Key ) )
              continue;
            sb_output.AppendLine( "Inconsistent references:" );
            foreach ( var kvp_element in kvp.Value )
            {
              sb_output.AppendLine( " - " + kvp_element.Value.FullName + " - " + kvp_element.Value.CodeBase + "referenced by:" );
              if ( ReferencesDictionary.ContainsKey( kvp_element.Value.FullName ) )
              {
                foreach ( var element in ReferencesDictionary[ kvp_element.Value.FullName ] )
                {
                  sb_output.AppendLine( "    -- " + element.FullName + " - " + element.CodeBase );
                }
              }
              else
                sb_output.AppendLine( "    -- not referenced!" );
            }
            sb_output.AppendLine();
            #endregion //kvp.Value.Count > 1
          }

        }
        sb_output.AppendLine();
        sb_output.AppendLine( "Missing Assemblies" );
        foreach ( var kvp_reference in ReferencesDictionary )
        {
          bool referenceFound = false;
          foreach ( var kvp_assemblyList in RealAssembliesDictionary )
          {
            foreach ( var kvp_assemblyName in kvp_assemblyList.Value )
            {
              if ( kvp_assemblyName.Value.FullName == kvp_reference.Key )
              {
                referenceFound = true;
                break;
              }
              if ( referenceFound )
                break;
            }
          }
          if ( !referenceFound )
          {
            Assembly assembly = null;
            try
            {
              assembly = System.Reflection.Assembly.Load( kvp_reference.Key );
            }
            catch ( Exception ex )
            {
              sb_output.AppendLine( string.Format( "- Cannot load assembly {0} (reason: {1}) referenced by:", kvp_reference.Key, ex.Message ) );
              foreach ( var element in ReferencesDictionary[ kvp_reference.Key ] )
              {
                sb_output.AppendLine( "    -- " + element.FullName + " - " + element.CodeBase );
              }
              assembly = null;
            }
          }
        }
        sb_output.AppendLine( "End of Report" );
      }
      catch ( Exception ex )
      {
        MessageBox.Show( ex.Message );
      }
      Result = sb_output.ToString();
    }
  }
}
