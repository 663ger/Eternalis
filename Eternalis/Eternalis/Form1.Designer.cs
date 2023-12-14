namespace Eternalis
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox2 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialSwitch1 = new MaterialSkin.Controls.MaterialSwitch();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.AnimateReadOnly = false;
            this.materialTextBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox1.HideSelection = true;
            this.materialTextBox1.Hint = "Введите логин";
            this.materialTextBox1.LeadingIcon = ((System.Drawing.Image)(resources.GetObject("materialTextBox1.LeadingIcon")));
            this.materialTextBox1.Location = new System.Drawing.Point(50, 168);
            this.materialTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.materialTextBox1.Size = new System.Drawing.Size(301, 48);
            this.materialTextBox1.TabIndex = 0;
            this.materialTextBox1.TabStop = false;
            this.materialTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox1.TrailingIcon = null;
            this.materialTextBox1.UseSystemPasswordChar = false;
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.AnimateReadOnly = false;
            this.materialTextBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox2.Depth = 0;
            this.materialTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.materialTextBox2.HideSelection = true;
            this.materialTextBox2.Hint = "Введите пароль";
            this.materialTextBox2.LeadingIcon = ((System.Drawing.Image)(resources.GetObject("materialTextBox2.LeadingIcon")));
            this.materialTextBox2.Location = new System.Drawing.Point(50, 244);
            this.materialTextBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialTextBox2.MaxLength = 32767;
            this.materialTextBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.PasswordChar = '•';
            this.materialTextBox2.PrefixSuffixText = null;
            this.materialTextBox2.ReadOnly = false;
            this.materialTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox2.SelectedText = "";
            this.materialTextBox2.SelectionLength = 0;
            this.materialTextBox2.SelectionStart = 0;
            this.materialTextBox2.ShortcutsEnabled = true;
            this.materialTextBox2.Size = new System.Drawing.Size(301, 48);
            this.materialTextBox2.TabIndex = 1;
            this.materialTextBox2.TabStop = false;
            this.materialTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox2.TrailingIcon = null;
            this.materialTextBox2.UseSystemPasswordChar = false;
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.materialButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.materialButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.materialButton1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(123, 358);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(160, 36);
            this.materialButton1.TabIndex = 2;
            this.materialButton1.Text = "Войти";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = false;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Icon = ((System.Drawing.Image)(resources.GetObject("materialFloatingActionButton1.Icon")));
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(348, 12);
            this.materialFloatingActionButton1.Mini = true;
            this.materialFloatingActionButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton1.TabIndex = 3;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // materialSwitch1
            // 
            this.materialSwitch1.Depth = 0;
            this.materialSwitch1.Location = new System.Drawing.Point(243, 314);
            this.materialSwitch1.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch1.Name = "materialSwitch1";
            this.materialSwitch1.Ripple = true;
            this.materialSwitch1.Size = new System.Drawing.Size(47, 29);
            this.materialSwitch1.TabIndex = 4;
            this.materialSwitch1.UseVisualStyleBackColor = true;
            this.materialSwitch1.CheckedChanged += new System.EventHandler(this.materialSwitch1_CheckedChanged);
            // 
            // materialLabel1
            // 
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.materialLabel1.HighEmphasis = true;
            this.materialLabel1.Location = new System.Drawing.Point(12, 119);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(376, 34);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "Войдите в свою учетную запись";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.materialLabel1.UseAccent = true;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(123, 1);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(160, 115);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 6;
            this.guna2PictureBox1.TabStop = false;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            this.materialLabel2.Location = new System.Drawing.Point(120, 321);
            this.materialLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(121, 17);
            this.materialLabel2.TabIndex = 51;
            this.materialLabel2.Text = "показать/скрыть ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(400, 408);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialSwitch1);
            this.Controls.Add(this.materialFloatingActionButton1);
            this.Controls.Add(this.materialButton1);
            this.Controls.Add(this.materialTextBox2);
            this.Controls.Add(this.materialTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox1;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox2;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
    }
}

