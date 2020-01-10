namespace TNCNotify
{
    partial class SignInControl
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.usernameLabel = new MetroFramework.Controls.MetroTextBox();
            this.passwordLabel = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(148, 54);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(68, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Username";
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(148, 102);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(64, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "Password";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.usernameLabel.CustomButton.Image = null;
            this.usernameLabel.CustomButton.Location = new System.Drawing.Point(112, 1);
            this.usernameLabel.CustomButton.Name = "";
            this.usernameLabel.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.usernameLabel.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.usernameLabel.CustomButton.TabIndex = 1;
            this.usernameLabel.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.usernameLabel.CustomButton.UseSelectable = true;
            this.usernameLabel.CustomButton.Visible = false;
            this.usernameLabel.Lines = new string[0];
            this.usernameLabel.Location = new System.Drawing.Point(148, 76);
            this.usernameLabel.MaxLength = 32767;
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.PasswordChar = '\0';
            this.usernameLabel.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.usernameLabel.SelectedText = "";
            this.usernameLabel.SelectionLength = 0;
            this.usernameLabel.SelectionStart = 0;
            this.usernameLabel.ShortcutsEnabled = true;
            this.usernameLabel.Size = new System.Drawing.Size(134, 23);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.UseSelectable = true;
            this.usernameLabel.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.usernameLabel.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // passwordLabel
            // 
            this.passwordLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.passwordLabel.CustomButton.Image = null;
            this.passwordLabel.CustomButton.Location = new System.Drawing.Point(112, 1);
            this.passwordLabel.CustomButton.Name = "";
            this.passwordLabel.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.passwordLabel.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.passwordLabel.CustomButton.TabIndex = 1;
            this.passwordLabel.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.passwordLabel.CustomButton.UseSelectable = true;
            this.passwordLabel.CustomButton.Visible = false;
            this.passwordLabel.Lines = new string[0];
            this.passwordLabel.Location = new System.Drawing.Point(148, 124);
            this.passwordLabel.MaxLength = 32767;
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.PasswordChar = '\0';
            this.passwordLabel.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.passwordLabel.SelectedText = "";
            this.passwordLabel.SelectionLength = 0;
            this.passwordLabel.SelectionStart = 0;
            this.passwordLabel.ShortcutsEnabled = true;
            this.passwordLabel.Size = new System.Drawing.Size(134, 23);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.UseSelectable = true;
            this.passwordLabel.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.passwordLabel.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(148, 14);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(48, 19);
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Sign in";
            // 
            // metroButton1
            // 
            this.metroButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroButton1.Location = new System.Drawing.Point(148, 162);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(134, 31);
            this.metroButton1.TabIndex = 5;
            this.metroButton1.Text = "Submit";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // SignInControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "SignInControl";
            this.Size = new System.Drawing.Size(428, 211);
            this.Load += new System.EventHandler(this.SignInControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox usernameLabel;
        private MetroFramework.Controls.MetroTextBox passwordLabel;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}
