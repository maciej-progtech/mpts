using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Mpts.Contracts;

namespace Mpts
{
  [Export]
  public partial class InformationForm: Form
  {
    [ImportMany]
    IEnumerable<IMainProgram> Programs { get; set; }
    public InformationForm()
    {
      InitializeComponent();
    }

    private void InformationForm_Load( object sender, System.EventArgs e )
    {
      foreach ( var element in Programs )
      { 
        ToolStripMenuItem newitem = new ToolStripMenuItem( element.Name );
        newitem.Tag = element;
        newitem.Click += new System.EventHandler( menu_programs_Click );
        this.programsToolStripMenuItem.DropDownItems.Add( newitem );
      }
    }

    void menu_programs_Click( object sender, System.EventArgs e )
    {
      ToolStripMenuItem newitem = sender as ToolStripMenuItem;
      if ( newitem != null )
      {
        IMainProgram program = newitem.Tag as IMainProgram;
        if ( program != null )
          program.Run();
      }
    }
  }
}
