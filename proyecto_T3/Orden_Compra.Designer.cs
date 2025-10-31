namespace proyecto_T3
{
    partial class Orden_Compra
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
            this.btneliminar = new System.Windows.Forms.Button();
            this.btncerrar = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btnnuevo = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btneditar = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnagregar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtcantidad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtproductos = new System.Windows.Forms.TextBox();
            this.txtprecio_unitario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTimer = new System.Windows.Forms.DateTimePicker();
            this.txtestado = new System.Windows.Forms.TextBox();
            this.txtproveedor = new System.Windows.Forms.TextBox();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.txtid_orden = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(542, 19);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(75, 23);
            this.btneliminar.TabIndex = 4;
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // btncerrar
            // 
            this.btncerrar.Location = new System.Drawing.Point(637, 19);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(75, 23);
            this.btncerrar.TabIndex = 3;
            this.btncerrar.Text = "Cerrar";
            this.btncerrar.UseVisualStyleBackColor = true;
            this.btncerrar.Click += new System.EventHandler(this.btncerrar_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.Location = new System.Drawing.Point(242, 20);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(75, 23);
            this.btnguardar.TabIndex = 2;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.UseVisualStyleBackColor = true;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnnuevo
            // 
            this.btnnuevo.Location = new System.Drawing.Point(63, 19);
            this.btnnuevo.Name = "btnnuevo";
            this.btnnuevo.Size = new System.Drawing.Size(75, 23);
            this.btnnuevo.TabIndex = 0;
            this.btnnuevo.Text = "Nuevo";
            this.btnnuevo.UseVisualStyleBackColor = true;
            this.btnnuevo.Click += new System.EventHandler(this.btnnuevo_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btneliminar);
            this.groupBox4.Controls.Add(this.btncerrar);
            this.groupBox4.Controls.Add(this.btnguardar);
            this.groupBox4.Controls.Add(this.btneditar);
            this.groupBox4.Controls.Add(this.btnnuevo);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 441);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(948, 52);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // btneditar
            // 
            this.btneditar.Location = new System.Drawing.Point(407, 19);
            this.btneditar.Name = "btneditar";
            this.btneditar.Size = new System.Drawing.Size(75, 23);
            this.btneditar.TabIndex = 1;
            this.btneditar.Text = "Editar";
            this.btneditar.UseVisualStyleBackColor = true;
            this.btneditar.Click += new System.EventHandler(this.btneditar_Click);
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(3, 16);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(942, 245);
            this.dgvProductos.TabIndex = 0;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvProductos);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(948, 264);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalles de la Orden:";
            // 
            // btnagregar
            // 
            this.btnagregar.Location = new System.Drawing.Point(776, 29);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(75, 23);
            this.btnagregar.TabIndex = 0;
            this.btnagregar.Text = "Agregar";
            this.btnagregar.UseVisualStyleBackColor = true;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnagregar);
            this.groupBox2.Controls.Add(this.txtcantidad);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtproductos);
            this.groupBox2.Controls.Add(this.txtprecio_unitario);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(948, 77);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agregar Productos:";
            // 
            // txtcantidad
            // 
            this.txtcantidad.Location = new System.Drawing.Point(450, 31);
            this.txtcantidad.Name = "txtcantidad";
            this.txtcantidad.Size = new System.Drawing.Size(65, 20);
            this.txtcantidad.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(391, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Cantidad:";
            // 
            // txtproductos
            // 
            this.txtproductos.Location = new System.Drawing.Point(77, 31);
            this.txtproductos.Name = "txtproductos";
            this.txtproductos.Size = new System.Drawing.Size(293, 20);
            this.txtproductos.TabIndex = 13;
            this.txtproductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtproductos_KeyDown);
            // 
            // txtprecio_unitario
            // 
            this.txtprecio_unitario.Location = new System.Drawing.Point(647, 31);
            this.txtprecio_unitario.Name = "txtprecio_unitario";
            this.txtprecio_unitario.Size = new System.Drawing.Size(81, 20);
            this.txtprecio_unitario.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Productos:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(579, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Precio Unit:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(634, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Estado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fecha de emision:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Proveedor:";
            // 
            // dtpTimer
            // 
            this.dtpTimer.Location = new System.Drawing.Point(429, 32);
            this.dtpTimer.Name = "dtpTimer";
            this.dtpTimer.Size = new System.Drawing.Size(168, 20);
            this.dtpTimer.TabIndex = 9;
            // 
            // txtestado
            // 
            this.txtestado.Location = new System.Drawing.Point(701, 32);
            this.txtestado.Name = "txtestado";
            this.txtestado.Size = new System.Drawing.Size(150, 20);
            this.txtestado.TabIndex = 8;
            // 
            // txtproveedor
            // 
            this.txtproveedor.Location = new System.Drawing.Point(77, 67);
            this.txtproveedor.Name = "txtproveedor";
            this.txtproveedor.Size = new System.Drawing.Size(426, 20);
            this.txtproveedor.TabIndex = 7;
            this.txtproveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtproveedor_KeyDown);
            // 
            // txttotal
            // 
            this.txttotal.Location = new System.Drawing.Point(701, 67);
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(150, 20);
            this.txttotal.TabIndex = 6;
            // 
            // txtid_orden
            // 
            this.txtid_orden.Location = new System.Drawing.Point(77, 32);
            this.txtid_orden.Name = "txtid_orden";
            this.txtid_orden.Size = new System.Drawing.Size(220, 20);
            this.txtid_orden.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(634, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Total:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Orden:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTimer);
            this.groupBox1.Controls.Add(this.txtestado);
            this.groupBox1.Controls.Add(this.txtproveedor);
            this.groupBox1.Controls.Add(this.txttotal);
            this.groupBox1.Controls.Add(this.txtid_orden);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(948, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la Orden";
            // 
            // Orden_Compra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 551);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Orden_Compra";
            this.Text = "Orden_Compra";
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.Button btncerrar;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Button btnnuevo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btneditar;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtcantidad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtproductos;
        private System.Windows.Forms.TextBox txtprecio_unitario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTimer;
        private System.Windows.Forms.TextBox txtestado;
        private System.Windows.Forms.TextBox txtproveedor;
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.TextBox txtid_orden;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}