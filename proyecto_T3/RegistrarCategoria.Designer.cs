namespace proyecto_T3
{
    partial class RegistrarCategoria
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
            this.dtgCategoria = new System.Windows.Forms.DataGridView();
            this.btndeshabilitar = new System.Windows.Forms.Button();
            this.btneditar = new System.Windows.Forms.Button();
            this.btnregistrar = new System.Windows.Forms.Button();
            this.checkEstado = new System.Windows.Forms.CheckBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtidCategoria = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCategoria)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgCategoria
            // 
            this.dtgCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCategoria.Location = new System.Drawing.Point(11, 254);
            this.dtgCategoria.Name = "dtgCategoria";
            this.dtgCategoria.Size = new System.Drawing.Size(690, 252);
            this.dtgCategoria.TabIndex = 19;
            this.dtgCategoria.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgCategoria_CellClick);
            // 
            // btndeshabilitar
            // 
            this.btndeshabilitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndeshabilitar.Location = new System.Drawing.Point(362, 158);
            this.btndeshabilitar.Name = "btndeshabilitar";
            this.btndeshabilitar.Size = new System.Drawing.Size(110, 47);
            this.btndeshabilitar.TabIndex = 18;
            this.btndeshabilitar.Text = "Deshabilitar ";
            this.btndeshabilitar.UseVisualStyleBackColor = true;
            this.btndeshabilitar.Click += new System.EventHandler(this.btndeshabilitar_Click);
            // 
            // btneditar
            // 
            this.btneditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btneditar.Location = new System.Drawing.Point(362, 78);
            this.btneditar.Name = "btneditar";
            this.btneditar.Size = new System.Drawing.Size(110, 50);
            this.btneditar.TabIndex = 17;
            this.btneditar.Text = "Editar ";
            this.btneditar.UseVisualStyleBackColor = true;
            this.btneditar.Click += new System.EventHandler(this.btneditar_Click);
            // 
            // btnregistrar
            // 
            this.btnregistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnregistrar.Location = new System.Drawing.Point(362, 9);
            this.btnregistrar.Name = "btnregistrar";
            this.btnregistrar.Size = new System.Drawing.Size(110, 48);
            this.btnregistrar.TabIndex = 16;
            this.btnregistrar.Text = "Registrar ";
            this.btnregistrar.UseVisualStyleBackColor = true;
            this.btnregistrar.Click += new System.EventHandler(this.btnregistrar_Click);
            // 
            // checkEstado
            // 
            this.checkEstado.AutoSize = true;
            this.checkEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEstado.Location = new System.Drawing.Point(135, 189);
            this.checkEstado.Name = "checkEstado";
            this.checkEstado.Size = new System.Drawing.Size(92, 19);
            this.checkEstado.TabIndex = 15;
            this.checkEstado.Text = "Habilitado";
            this.checkEstado.UseVisualStyleBackColor = true;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(135, 108);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(100, 20);
            this.txtDescripcion.TabIndex = 14;
            // 
            // txtidCategoria
            // 
            this.txtidCategoria.Location = new System.Drawing.Point(135, 37);
            this.txtidCategoria.Name = "txtidCategoria";
            this.txtidCategoria.Size = new System.Drawing.Size(100, 20);
            this.txtidCategoria.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Estado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "IdCategoria";
            // 
            // RegistrarCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 526);
            this.Controls.Add(this.dtgCategoria);
            this.Controls.Add(this.btndeshabilitar);
            this.Controls.Add(this.btneditar);
            this.Controls.Add(this.btnregistrar);
            this.Controls.Add(this.checkEstado);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtidCategoria);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RegistrarCategoria";
            this.Text = "RegistrarCategoria";
            ((System.ComponentModel.ISupportInitialize)(this.dtgCategoria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgCategoria;
        private System.Windows.Forms.Button btndeshabilitar;
        private System.Windows.Forms.Button btneditar;
        private System.Windows.Forms.Button btnregistrar;
        private System.Windows.Forms.CheckBox checkEstado;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtidCategoria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}