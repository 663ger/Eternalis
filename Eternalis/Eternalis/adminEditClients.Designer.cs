namespace Eternalis
{
    partial class adminEditClients
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminEditClients));
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.materialButton5 = new MaterialSkin.Controls.MaterialButton();
            this.materialTextBox7 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox6 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox5 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox3 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox2 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialFloatingActionButton2 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialFloatingActionButton3 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.SuspendLayout();
            // 
            // materialLabel2
            // 
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(312, 167);
            this.materialLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(212, 20);
            this.materialLabel2.TabIndex = 71;
            this.materialLabel2.Text = "Дата рождения:";
            // 
            // guna2DateTimePicker1
            // 
            this.guna2DateTimePicker1.Animated = true;
            this.guna2DateTimePicker1.BackColor = System.Drawing.Color.YellowGreen;
            this.guna2DateTimePicker1.Checked = true;
            this.guna2DateTimePicker1.FillColor = System.Drawing.Color.WhiteSmoke;
            this.guna2DateTimePicker1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.guna2DateTimePicker1.Location = new System.Drawing.Point(312, 192);
            this.guna2DateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2DateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            this.guna2DateTimePicker1.Size = new System.Drawing.Size(218, 23);
            this.guna2DateTimePicker1.TabIndex = 70;
            this.guna2DateTimePicker1.Value = new System.DateTime(2023, 11, 27, 0, 0, 0, 0);
            // 
            // materialButton5
            // 
            this.materialButton5.AutoSize = false;
            this.materialButton5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialButton5.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton5.Depth = 0;
            this.materialButton5.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.materialButton5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.materialButton5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.materialButton5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.materialButton5.HighEmphasis = true;
            this.materialButton5.Icon = null;
            this.materialButton5.Location = new System.Drawing.Point(312, 342);
            this.materialButton5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.materialButton5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton5.Name = "materialButton5";
            this.materialButton5.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton5.Size = new System.Drawing.Size(218, 48);
            this.materialButton5.TabIndex = 69;
            this.materialButton5.Text = "Сохранить";
            this.materialButton5.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton5.UseAccentColor = false;
            this.materialButton5.UseVisualStyleBackColor = false;
            this.materialButton5.Click += new System.EventHandler(this.materialButton5_Click);
            // 
            // materialTextBox7
            // 
            this.materialTextBox7.AnimateReadOnly = false;
            this.materialTextBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox7.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox7.Depth = 0;
            this.materialTextBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox7.HideSelection = true;
            this.materialTextBox7.Hint = "email";
            this.materialTextBox7.LeadingIcon = null;
            this.materialTextBox7.Location = new System.Drawing.Point(72, 342);
            this.materialTextBox7.Margin = new System.Windows.Forms.Padding(2);
            this.materialTextBox7.MaxLength = 32767;
            this.materialTextBox7.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox7.Name = "materialTextBox7";
            this.materialTextBox7.PasswordChar = '\0';
            this.materialTextBox7.PrefixSuffixText = null;
            this.materialTextBox7.ReadOnly = false;
            this.materialTextBox7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox7.SelectedText = "";
            this.materialTextBox7.SelectionLength = 0;
            this.materialTextBox7.SelectionStart = 0;
            this.materialTextBox7.ShortcutsEnabled = true;
            this.materialTextBox7.Size = new System.Drawing.Size(218, 48);
            this.materialTextBox7.TabIndex = 68;
            this.materialTextBox7.TabStop = false;
            this.materialTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox7.TrailingIcon = null;
            this.materialTextBox7.UseSystemPasswordChar = false;
            this.materialTextBox7.Leave += new System.EventHandler(this.materialTextBox7_Leave);
            // 
            // materialTextBox6
            // 
            this.materialTextBox6.AnimateReadOnly = false;
            this.materialTextBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox6.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox6.Depth = 0;
            this.materialTextBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox6.HideSelection = true;
            this.materialTextBox6.Hint = "Номер телефона";
            this.materialTextBox6.LeadingIcon = null;
            this.materialTextBox6.Location = new System.Drawing.Point(312, 248);
            this.materialTextBox6.Margin = new System.Windows.Forms.Padding(2);
            this.materialTextBox6.MaxLength = 32767;
            this.materialTextBox6.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox6.Name = "materialTextBox6";
            this.materialTextBox6.PasswordChar = '\0';
            this.materialTextBox6.PrefixSuffixText = null;
            this.materialTextBox6.ReadOnly = false;
            this.materialTextBox6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox6.SelectedText = "";
            this.materialTextBox6.SelectionLength = 0;
            this.materialTextBox6.SelectionStart = 0;
            this.materialTextBox6.ShortcutsEnabled = true;
            this.materialTextBox6.Size = new System.Drawing.Size(218, 48);
            this.materialTextBox6.TabIndex = 67;
            this.materialTextBox6.TabStop = false;
            this.materialTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox6.TrailingIcon = null;
            this.materialTextBox6.UseSystemPasswordChar = false;
            this.materialTextBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.materialTextBox6_KeyPress);
            this.materialTextBox6.TextChanged += new System.EventHandler(this.materialTextBox6_TextChanged);
            // 
            // materialTextBox5
            // 
            this.materialTextBox5.AnimateReadOnly = false;
            this.materialTextBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox5.Depth = 0;
            this.materialTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox5.HideSelection = true;
            this.materialTextBox5.Hint = "Адрес";
            this.materialTextBox5.LeadingIcon = null;
            this.materialTextBox5.Location = new System.Drawing.Point(72, 248);
            this.materialTextBox5.Margin = new System.Windows.Forms.Padding(2);
            this.materialTextBox5.MaxLength = 32767;
            this.materialTextBox5.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox5.Name = "materialTextBox5";
            this.materialTextBox5.PasswordChar = '\0';
            this.materialTextBox5.PrefixSuffixText = null;
            this.materialTextBox5.ReadOnly = false;
            this.materialTextBox5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox5.SelectedText = "";
            this.materialTextBox5.SelectionLength = 0;
            this.materialTextBox5.SelectionStart = 0;
            this.materialTextBox5.ShortcutsEnabled = true;
            this.materialTextBox5.Size = new System.Drawing.Size(218, 48);
            this.materialTextBox5.TabIndex = 66;
            this.materialTextBox5.TabStop = false;
            this.materialTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox5.TrailingIcon = null;
            this.materialTextBox5.UseSystemPasswordChar = false;
            // 
            // materialTextBox3
            // 
            this.materialTextBox3.AnimateReadOnly = false;
            this.materialTextBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox3.Depth = 0;
            this.materialTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox3.HideSelection = true;
            this.materialTextBox3.Hint = "Отчество";
            this.materialTextBox3.LeadingIcon = null;
            this.materialTextBox3.Location = new System.Drawing.Point(72, 167);
            this.materialTextBox3.Margin = new System.Windows.Forms.Padding(2);
            this.materialTextBox3.MaxLength = 32767;
            this.materialTextBox3.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox3.Name = "materialTextBox3";
            this.materialTextBox3.PasswordChar = '\0';
            this.materialTextBox3.PrefixSuffixText = null;
            this.materialTextBox3.ReadOnly = false;
            this.materialTextBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox3.SelectedText = "";
            this.materialTextBox3.SelectionLength = 0;
            this.materialTextBox3.SelectionStart = 0;
            this.materialTextBox3.ShortcutsEnabled = true;
            this.materialTextBox3.Size = new System.Drawing.Size(218, 48);
            this.materialTextBox3.TabIndex = 65;
            this.materialTextBox3.TabStop = false;
            this.materialTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox3.TrailingIcon = null;
            this.materialTextBox3.UseSystemPasswordChar = false;
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.AnimateReadOnly = false;
            this.materialTextBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox2.Depth = 0;
            this.materialTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox2.HideSelection = true;
            this.materialTextBox2.Hint = "Фамилия";
            this.materialTextBox2.LeadingIcon = null;
            this.materialTextBox2.Location = new System.Drawing.Point(312, 86);
            this.materialTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.materialTextBox2.MaxLength = 32767;
            this.materialTextBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.PasswordChar = '\0';
            this.materialTextBox2.PrefixSuffixText = null;
            this.materialTextBox2.ReadOnly = false;
            this.materialTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox2.SelectedText = "";
            this.materialTextBox2.SelectionLength = 0;
            this.materialTextBox2.SelectionStart = 0;
            this.materialTextBox2.ShortcutsEnabled = true;
            this.materialTextBox2.Size = new System.Drawing.Size(218, 48);
            this.materialTextBox2.TabIndex = 64;
            this.materialTextBox2.TabStop = false;
            this.materialTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox2.TrailingIcon = null;
            this.materialTextBox2.UseSystemPasswordChar = false;
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.AnimateReadOnly = false;
            this.materialTextBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox1.HideSelection = true;
            this.materialTextBox1.Hint = "Имя";
            this.materialTextBox1.LeadingIcon = null;
            this.materialTextBox1.Location = new System.Drawing.Point(72, 86);
            this.materialTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.materialTextBox1.MaxLength = 32767;
            this.materialTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.PasswordChar = '\0';
            this.materialTextBox1.PrefixSuffixText = null;
            this.materialTextBox1.ReadOnly = false;
            this.materialTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox1.SelectedText = "";
            this.materialTextBox1.SelectionLength = 0;
            this.materialTextBox1.SelectionStart = 0;
            this.materialTextBox1.ShortcutsEnabled = true;
            this.materialTextBox1.Size = new System.Drawing.Size(218, 48);
            this.materialTextBox1.TabIndex = 63;
            this.materialTextBox1.TabStop = false;
            this.materialTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox1.TrailingIcon = null;
            this.materialTextBox1.UseSystemPasswordChar = false;
            // 
            // materialLabel1
            // 
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.materialLabel1.Location = new System.Drawing.Point(72, 34);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(432, 30);
            this.materialLabel1.TabIndex = 62;
            this.materialLabel1.Text = "Редактирование клиента:";
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Icon = ((System.Drawing.Image)(resources.GetObject("materialFloatingActionButton1.Icon")));
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(687, 11);
            this.materialFloatingActionButton1.Mini = true;
            this.materialFloatingActionButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton1.TabIndex = 73;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // materialFloatingActionButton2
            // 
            this.materialFloatingActionButton2.Depth = 0;
            this.materialFloatingActionButton2.Icon = ((System.Drawing.Image)(resources.GetObject("materialFloatingActionButton2.Icon")));
            this.materialFloatingActionButton2.Location = new System.Drawing.Point(632, 11);
            this.materialFloatingActionButton2.Mini = true;
            this.materialFloatingActionButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton2.Name = "materialFloatingActionButton2";
            this.materialFloatingActionButton2.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton2.TabIndex = 72;
            this.materialFloatingActionButton2.Text = "materialFloatingActionButton2";
            this.materialFloatingActionButton2.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton2.Click += new System.EventHandler(this.materialFloatingActionButton2_Click);
            // 
            // materialFloatingActionButton3
            // 
            this.materialFloatingActionButton3.Depth = 0;
            this.materialFloatingActionButton3.Icon = ((System.Drawing.Image)(resources.GetObject("materialFloatingActionButton3.Icon")));
            this.materialFloatingActionButton3.Location = new System.Drawing.Point(577, 11);
            this.materialFloatingActionButton3.Mini = true;
            this.materialFloatingActionButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton3.Name = "materialFloatingActionButton3";
            this.materialFloatingActionButton3.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton3.TabIndex = 74;
            this.materialFloatingActionButton3.Text = "materialFloatingActionButton3";
            this.materialFloatingActionButton3.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton3.Click += new System.EventHandler(this.materialFloatingActionButton3_Click);
            // 
            // adminEditClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(736, 449);
            this.Controls.Add(this.materialFloatingActionButton3);
            this.Controls.Add(this.materialFloatingActionButton1);
            this.Controls.Add(this.materialFloatingActionButton2);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.guna2DateTimePicker1);
            this.Controls.Add(this.materialButton5);
            this.Controls.Add(this.materialTextBox7);
            this.Controls.Add(this.materialTextBox6);
            this.Controls.Add(this.materialTextBox5);
            this.Controls.Add(this.materialTextBox3);
            this.Controls.Add(this.materialTextBox2);
            this.Controls.Add(this.materialTextBox1);
            this.Controls.Add(this.materialLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "adminEditClients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "adminEditClients";
            this.Load += new System.EventHandler(this.adminEditClients_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private MaterialSkin.Controls.MaterialButton materialButton5;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox7;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox6;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox5;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox3;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox2;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton2;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton3;
    }
}