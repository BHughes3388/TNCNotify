namespace TNCNotify
{
    partial class TabControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.startButton = new MetroFramework.Controls.MetroButton();
            this.refreshButton = new MetroFramework.Controls.MetroButton();
            this.onlineMachineListView = new MetroFramework.Controls.MetroListView();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.machineListView = new MetroFramework.Controls.MetroListView();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.usersListView = new MetroFramework.Controls.MetroListView();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.ItemSize = new System.Drawing.Size(200, 34);
            this.metroTabControl1.Location = new System.Drawing.Point(3, 3);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(867, 461);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            this.metroTabControl1.UseStyleColors = true;
            this.metroTabControl1.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.startButton);
            this.metroTabPage1.Controls.Add(this.refreshButton);
            this.metroTabPage1.Controls.Add(this.onlineMachineListView);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(859, 419);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Online";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(757, 381);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseSelectable = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(676, 381);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseSelectable = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // onlineMachineListView
            // 
            this.onlineMachineListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.onlineMachineListView.CheckBoxes = true;
            this.onlineMachineListView.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.onlineMachineListView.FullRowSelect = true;
            this.onlineMachineListView.Location = new System.Drawing.Point(22, 26);
            this.onlineMachineListView.Name = "onlineMachineListView";
            this.onlineMachineListView.OwnerDraw = true;
            this.onlineMachineListView.Size = new System.Drawing.Size(810, 333);
            this.onlineMachineListView.Style = MetroFramework.MetroColorStyle.Blue;
            this.onlineMachineListView.TabIndex = 1;
            this.onlineMachineListView.UseCompatibleStateImageBehavior = false;
            this.onlineMachineListView.UseSelectable = true;
            this.onlineMachineListView.View = System.Windows.Forms.View.Details;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.machineListView);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(859, 419);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Machines";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // machineListView
            // 
            this.machineListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineListView.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.machineListView.FullRowSelect = true;
            this.machineListView.Location = new System.Drawing.Point(22, 26);
            this.machineListView.Name = "machineListView";
            this.machineListView.OwnerDraw = true;
            this.machineListView.Size = new System.Drawing.Size(810, 333);
            this.machineListView.Style = MetroFramework.MetroColorStyle.Blue;
            this.machineListView.TabIndex = 3;
            this.machineListView.UseCompatibleStateImageBehavior = false;
            this.machineListView.UseSelectable = true;
            this.machineListView.View = System.Windows.Forms.View.Details;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.usersListView);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(859, 419);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Users";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // usersListView
            // 
            this.usersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usersListView.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.usersListView.FullRowSelect = true;
            this.usersListView.Location = new System.Drawing.Point(22, 26);
            this.usersListView.Name = "usersListView";
            this.usersListView.OwnerDraw = true;
            this.usersListView.Size = new System.Drawing.Size(810, 333);
            this.usersListView.Style = MetroFramework.MetroColorStyle.Blue;
            this.usersListView.TabIndex = 4;
            this.usersListView.UseCompatibleStateImageBehavior = false;
            this.usersListView.UseSelectable = true;
            this.usersListView.View = System.Windows.Forms.View.Details;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange;
            // 
            // TabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroTabControl1);
            this.Name = "TabControl";
            this.Size = new System.Drawing.Size(873, 467);
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Load += new System.EventHandler(this.TabControl_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroListView onlineMachineListView;
        private MetroFramework.Controls.MetroButton startButton;
        private MetroFramework.Controls.MetroButton refreshButton;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroListView machineListView;
        private MetroFramework.Controls.MetroListView usersListView;
    }
}
