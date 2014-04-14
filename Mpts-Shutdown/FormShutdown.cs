using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mpts.Applications.Shutdown.Properties;

namespace Mpts.Applications.Shutdown
{
  public partial class FormShutdown: Form
  {

    public FormShutdown()
    {
      InitializeComponent();

      //action list initialisation
      foreach ( var element in Enum.GetValues( typeof( PowerState ) ) )
      {
        this.comboBox_action.Items.Add( element );
        if ( element.ToString() == Settings.Default.DefaultAction )
          this.comboBox_action.SelectedItem = element;
      }
    }

    private void backgroundWorker1_DoWork( object sender, DoWorkEventArgs e )
    {
      try
      {
        int seconds_to_action = Convert.ToInt32( e.Argument );
        PowerState action = PowerState.Hibernate;
        MethodInvoker checkaction = new MethodInvoker( delegate()
        {
          action = (PowerState)this.comboBox_action.SelectedItem;
        } );
        if ( this.InvokeRequired )
          this.BeginInvoke( checkaction );
        
        for ( int i = seconds_to_action; i > 0; i-- )
        {
          MethodInvoker settext = new MethodInvoker( delegate()
          {
            this.label_information.Text = string.Format( Resources.info_seconds_to_action, action, new TimeSpan(0,0, i) );
          } );
          if ( this.InvokeRequired )
            this.BeginInvoke( settext );
          else
            settext();
          System.Threading.Thread.Sleep( 1000 );
          if ( backgroundWorker1.CancellationPending )
            return;
        }
        Application.SetSuspendState( action, true, true );
      }
      finally
      {
        MethodInvoker setbuttontext = new MethodInvoker( delegate()
        {
          button_start_stop.Text = "Start";
          this.label_information.Text = "";
        } );
        if ( this.InvokeRequired )
          this.BeginInvoke( setbuttontext );
        else
          setbuttontext();
      }
    }

    private void button_start_stop_Click( object sender, EventArgs e )
    {
      if ( backgroundWorker1.IsBusy )
      {
        backgroundWorker1.CancelAsync();
      }
      else
      {
        TimeSpan number_of_seconds_to_wait = TimeSpan.MaxValue;
        if ( TimeSpan.TryParse(
          string.Format( "{2}:{1}:{0}", textBox_SecondsToClose.Text, textBox_MinutesToClose.Text, textBox_HoursToClose.Text ),
        out number_of_seconds_to_wait ) )
        {
          backgroundWorker1.RunWorkerAsync( number_of_seconds_to_wait.TotalSeconds );
          button_start_stop.Text = "Stop";
        }
      }
    }

    private void FormShutdown_FormClosing( object sender, FormClosingEventArgs e )
    {
      Mpts.Applications.Shutdown.Properties.Settings.Default.Save();
    }

    private void comboBox_action_SelectedIndexChanged( object sender, EventArgs e )
    {
      Settings.Default.DefaultAction = this.comboBox_action.SelectedItem.ToString();
    }
  }
}
