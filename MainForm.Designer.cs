namespace Indexador
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxArchivos;
        private System.Windows.Forms.Button btnAgregarArchivos;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnLimpiar; // Declaración del botón "Limpiar"
        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer; // Declaración de mediaPlayer

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            listBoxArchivos = new ListBox();
            btnAgregarArchivos = new Button();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            btnExportar = new Button();
            lblName = new Label();
            lblValue = new Label();
            btnCargar = new Button();
            btnLimpiar = new Button();
            mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)mediaPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // listBoxArchivos
            // 
            listBoxArchivos.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxArchivos.FormattingEnabled = true;
            listBoxArchivos.Location = new Point(22, 149);
            listBoxArchivos.Name = "listBoxArchivos";
            listBoxArchivos.Size = new Size(210, 356);
            listBoxArchivos.TabIndex = 0;
            listBoxArchivos.DrawItem += listBoxArchivos_DrawItem;
            listBoxArchivos.SelectedIndexChanged += listBoxArchivos_SelectedIndexChanged;
            // 
            // btnAgregarArchivos
            // 
            btnAgregarArchivos.Location = new Point(61, 528);
            btnAgregarArchivos.Name = "btnAgregarArchivos";
            btnAgregarArchivos.Size = new Size(120, 23);
            btnAgregarArchivos.TabIndex = 1;
            btnAgregarArchivos.Text = "Agregar archivos";
            btnAgregarArchivos.UseVisualStyleBackColor = true;
            btnAgregarArchivos.Click += btnAgregarArchivos_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.ForeColor = SystemColors.ControlText;
            btnAnterior.Location = new Point(701, 316);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(75, 23);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "Anterior";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(841, 316);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(75, 23);
            btnSiguiente.TabIndex = 3;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnExportar
            // 
            btnExportar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(755, 373);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(116, 50);
            btnExportar.TabIndex = 4;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(0, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 7;
            // 
            // lblValue
            // 
            lblValue.Location = new Point(0, 0);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(100, 23);
            lblValue.TabIndex = 8;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(0, 0);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(75, 23);
            btnCargar.TabIndex = 9;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(61, 570);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(120, 23);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // mediaPlayer
            // 
            mediaPlayer.Enabled = true;
            mediaPlayer.Location = new Point(250, 12);
            mediaPlayer.Name = "mediaPlayer";
            mediaPlayer.OcxState = (AxHost.State)resources.GetObject("mediaPlayer.OcxState");
            mediaPlayer.Size = new Size(400, 600);
            mediaPlayer.TabIndex = 5;
            mediaPlayer.Visible = false;
            mediaPlayer.Enter += mediaPlayer_Enter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(22, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(210, 121);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(250, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(400, 600);
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(739, 278);
            button1.Name = "button1";
            button1.Size = new Size(144, 23);
            button1.TabIndex = 12;
            button1.Text = "Agregar campo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(679, 570);
            button2.Name = "button2";
            button2.Size = new Size(260, 23);
            button2.TabIndex = 13;
            button2.Text = "© Copyright mpsolution. All rights reserved.";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(951, 623);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(mediaPlayer);
            Controls.Add(btnExportar);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(btnAgregarArchivos);
            Controls.Add(btnLimpiar);
            Controls.Add(listBoxArchivos);
            Controls.Add(lblName);
            Controls.Add(lblValue);
            Controls.Add(btnCargar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(967, 662);
            MinimumSize = new Size(967, 662);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Indexador";
            ((System.ComponentModel.ISupportInitialize)mediaPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button button1;
        private Button button2;
    }
}


