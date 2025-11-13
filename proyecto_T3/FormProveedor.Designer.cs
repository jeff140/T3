namespace proyecto_T3
{
    partial class FormProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProveedor));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtRuc = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Razon Social";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "RUC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Telefono";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Direccion";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(164, 63);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(194, 22);
            this.txtRazonSocial.TabIndex = 5;
            // 
            // txtRuc
            // 
            this.txtRuc.Location = new System.Drawing.Point(164, 113);
            this.txtRuc.MaxLength = 11;
            this.txtRuc.Name = "txtRuc";
            this.txtRuc.Size = new System.Drawing.Size(194, 22);
            this.txtRuc.TabIndex = 6;
            this.txtRuc.TextChanged += new System.EventHandler(this.txtRuc_TextChanged);
            this.txtRuc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuc_KeyPress);
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(164, 165);
            this.txtTelefono.MaxLength = 9;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(194, 22);
            this.txtTelefono.TabIndex = 7;
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(164, 220);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(194, 22);
            this.txtDireccion.TabIndex = 8;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.Location = new System.Drawing.Point(33, 320);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(113, 41);
            this.btnRegistrar.TabIndex = 9;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.Location = new System.Drawing.Point(164, 320);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(111, 41);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.Location = new System.Drawing.Point(291, 320);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(113, 41);
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedores.Location = new System.Drawing.Point(449, 100);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.RowHeadersWidth = 51;
            this.dgvProveedores.RowTemplate.Height = 24;
            this.dgvProveedores.Size = new System.Drawing.Size(726, 466);
            this.dgvProveedores.TabIndex = 12;
            this.dgvProveedores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedores_CellClick);
            this.dgvProveedores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(810, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Buscar";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(865, 39);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(181, 22);
            this.txtBuscar.TabIndex = 14;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 41);
            this.button1.TabIndex = 15;
            this.button1.Text = "Nuevo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(788, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "   ";
            // 
            // FormProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1200, 578);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtRuc);
            this.Controls.Add(this.txtRazonSocial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormProveedor";
            this.Text = "FormProveedor";
            this.Load += new System.EventHandler(this.FormProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.TextBox txtRuc;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
    }
}