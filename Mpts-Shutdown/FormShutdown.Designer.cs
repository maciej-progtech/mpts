namespace Mpts.Applications.Shutdown
{
  partial class FormShutdown
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      Mpts.Applications.Shutdown.Properties.Settings settings1 = new Mpts.Applications.Shutdown.Properties.Settings();
      this.button_start_stop = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox_SecondsToClose = new System.Windows.Forms.TextBox();
      this.label_information = new System.Windows.Forms.Label();
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.textBox_MinutesToClose = new System.Windows.Forms.TextBox();
      this.textBox_HoursToClose = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.comboBox_action = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // button_start_stop
      // 
      this.button_start_stop.Location = new System.Drawing.Point(9, 75);
      this.button_start_stop.Name = "button_start_stop";
      this.button_start_stop.Size = new System.Drawing.Size(253, 23);
      this.button_start_stop.TabIndex = 0;
      this.button_start_stop.Text = "Start";
      this.button_start_stop.UseVisualStyleBackColor = true;
      this.button_start_stop.Click += new System.EventHandler(this.button_start_stop_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(136, 17);
      this.label1.TabIndex = 1;
      this.label1.Text = "Delay time [h]:[m]:[s]";
      // 
      // textBox_SecondsToClose
      // 
      settings1.DefaultAction = "Hibernate";
      settings1.DefaultDelayHoursToAction = "1";
      settings1.DefaultDelayMinutesToAction = "30";
      settings1.DefaultMinutesSecondsToAction = "0";
      settings1.SettingsKey = "";
      this.textBox_SecondsToClose.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings1, "DefaultMinutesSecondsToAction", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.textBox_SecondsToClose.Location = new System.Drawing.Point(228, 9);
      this.textBox_SecondsToClose.Name = "textBox_SecondsToClose";
      this.textBox_SecondsToClose.Size = new System.Drawing.Size(34, 22);
      this.textBox_SecondsToClose.TabIndex = 2;
      this.textBox_SecondsToClose.Text = settings1.DefaultMinutesSecondsToAction;
      // 
      // label_information
      // 
      this.label_information.AutoSize = true;
      this.label_information.BackColor = System.Drawing.SystemColors.Highlight;
      this.label_information.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.label_information.Location = new System.Drawing.Point(6, 107);
      this.label_information.Name = "label_information";
      this.label_information.Size = new System.Drawing.Size(78, 17);
      this.label_information.TabIndex = 3;
      this.label_information.Text = "Information";
      this.label_information.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // backgroundWorker1
      // 
      this.backgroundWorker1.WorkerSupportsCancellation = true;
      this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
      // 
      // textBox_MinutesToClose
      // 
      this.textBox_MinutesToClose.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings1, "DefaultDelayMinutesToAction", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.textBox_MinutesToClose.Location = new System.Drawing.Point(188, 9);
      this.textBox_MinutesToClose.Name = "textBox_MinutesToClose";
      this.textBox_MinutesToClose.Size = new System.Drawing.Size(34, 22);
      this.textBox_MinutesToClose.TabIndex = 4;
      this.textBox_MinutesToClose.Text = settings1.DefaultDelayMinutesToAction;
      // 
      // textBox_HoursToClose
      // 
      this.textBox_HoursToClose.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings1, "DefaultDelayHoursToAction", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.textBox_HoursToClose.Location = new System.Drawing.Point(148, 9);
      this.textBox_HoursToClose.Name = "textBox_HoursToClose";
      this.textBox_HoursToClose.Size = new System.Drawing.Size(34, 22);
      this.textBox_HoursToClose.TabIndex = 4;
      this.textBox_HoursToClose.Text = settings1.DefaultDelayHoursToAction;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(179, 12);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(12, 17);
      this.label2.TabIndex = 5;
      this.label2.Text = ":";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(219, 12);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(12, 17);
      this.label3.TabIndex = 6;
      this.label3.Text = ":";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(15, 40);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(51, 17);
      this.label4.TabIndex = 7;
      this.label4.Text = "Action:";
      // 
      // comboBox_action
      // 
      this.comboBox_action.Location = new System.Drawing.Point(73, 40);
      this.comboBox_action.Name = "comboBox_action";
      this.comboBox_action.Size = new System.Drawing.Size(189, 24);
      this.comboBox_action.TabIndex = 8;
      this.comboBox_action.SelectedIndexChanged += new System.EventHandler(this.comboBox_action_SelectedIndexChanged);
      // 
      // FormShutdown
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(274, 133);
      this.Controls.Add(this.comboBox_action);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBox_HoursToClose);
      this.Controls.Add(this.textBox_MinutesToClose);
      this.Controls.Add(this.textBox_SecondsToClose);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label_information);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button_start_stop);
      this.Name = "FormShutdown";
      this.Text = "Shutdown";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShutdown_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button_start_stop;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox_SecondsToClose;
    private System.Windows.Forms.Label label_information;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.TextBox textBox_MinutesToClose;
    private System.Windows.Forms.TextBox textBox_HoursToClose;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox comboBox_action;
  }
}

