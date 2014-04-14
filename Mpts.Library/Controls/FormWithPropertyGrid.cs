using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mpts.Library.Controls
{
  public partial class FormWithPropertyGrid: CommonForm
  {
    public FormWithPropertyGrid()
    {
      InitializeComponent();
    }
    public PropertyGrid OutputControl { get { return this.propertyGrid; } }
  }
}
