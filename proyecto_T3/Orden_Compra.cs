using CapaEntidad;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace proyecto_T3
{
    public partial class Orden_Compra : Form
    {
        private int idOrdenActual = 0;
        public Orden_Compra()
        {
            InitializeComponent();
            ConfigurarControles();
            ConfigurarDataGridView();
            DeshabilitarControles();
            ConfigurarAutoCompletarProveedor();
            CargarSugerenciasProducto();

        }


            private void ConfigurarControles()
            {
                txtid_orden.Enabled = false;
                txttotal.Enabled = false;
                txtestado.Enabled = false;
                txtestado.Text = "Activo";
                dtpTimer.Value = DateTime.Now;
            txtprecio_unitario.Enabled = true;


            // ✅ Autocompletado para producto
            txtproductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtproductos.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CargarSugerenciasProducto();
        }
        private void CargarSugerenciasProducto()
        {
            try
            {
                var productos = logProducto.Instancia.ListarProducto();
                AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

                foreach (var p in productos)
                {
                    lista.Add(p.nombreProducto);
                    lista.Add(p.idProducto.ToString());
                }

                txtproductos.AutoCompleteCustomSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar sugerencias de productos: " + ex.Message);
            }
        }
        private void ConfigurarDataGridView()
            {
                dgvProductos.AutoGenerateColumns = false;
                dgvProductos.Columns.Clear();
                dgvProductos.AllowUserToAddRows = false;

                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "idDetalleOrden",
                    HeaderText = "ID Detalle",
                    Name = "idDetalleOrden",
                    Visible = false
                });

                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "idProducto",
                    HeaderText = "ID Producto",
                    Name = "idProducto",
                    Width = 80
                });

                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "nombreProducto",
                    HeaderText = "Producto",
                    Name = "nombreProducto",
                    Width = 250
                });

                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "cantidad",
                    HeaderText = "Cantidad",
                    Name = "cantidad",
                    Width = 80
                });

                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "precioUnitario",
                    HeaderText = "Precio Unit.",
                    Name = "precioUnitario",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "subtotal",
                    HeaderText = "Subtotal",
                    Name = "subtotal",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                {
                    HeaderText = "Acción",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true,
                    Name = "btnEliminar",
                    Width = 80
                };
                dgvProductos.Columns.Add(btnEliminar);
            }

            private void HabilitarControles()
            {
                txtproveedor.Enabled = true;
                dtpTimer.Enabled = true;
                groupBox2.Enabled = true;
                txtproductos.Enabled = true;
                txtcantidad.Enabled = true;
            }

            private void DeshabilitarControles()
            {
                txtproveedor.Enabled = false;
                dtpTimer.Enabled = false;
                groupBox2.Enabled = false;
                txtproductos.Enabled = false;
                txtcantidad.Enabled = false;
            }

            private void LimpiarCampos()
            {
                txtid_orden.Clear();
                txtproveedor.Clear();
                txttotal.Text = "0.00";
                txtestado.Text = "Activo";
                dtpTimer.Value = DateTime.Now;

                txtproductos.Clear();
                txtprecio_unitario.Clear();
                txtcantidad.Clear();

                dgvProductos.DataSource = null;
                idOrdenActual = 0;
            }

            private void btnnuevo_Click(object sender, EventArgs e)
             {
            try
            {
                // Si aún no hay orden activa
                if (idOrdenActual == 0)
                {
                    LimpiarCampos();
                    HabilitarControles();

                    // ✅ Habilitamos el campo proveedor para buscarlo por nombre
                    txtproveedor.Enabled = true;
                    txtproductos.Enabled = false;
                    txtcantidad.Enabled = false;

                    btnguardar.Enabled = false;
                    btncerrar.Enabled = true;
                    btnnuevo.Enabled = true;
                    btneditar.Enabled = false;
                    btneliminar.Enabled = false;

                    
                    txtproveedor.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear orden: " + ex.Message);
            }
        }

        private void txtproductos_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarProducto();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }

            private void BuscarProducto()
            {
                try
                {
                    if (string.IsNullOrEmpty(txtproductos.Text))
                        return;

                    var productos = logProducto.Instancia.ListarProducto();
                    var producto = productos.FirstOrDefault(p =>
                        p.nombreProducto.ToLower().Contains(txtproductos.Text.ToLower()) ||
                        p.idProducto.ToString() == txtproductos.Text);

                    if (producto != null)
                    {
                        txtproductos.Text = producto.nombreProducto;
                        txtproductos.Tag = producto.idProducto;
                        txtprecio_unitario.Text = producto.precioUnitario.ToString("N2");
                        txtcantidad.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Producto no encontrado");
                        txtproductos.Clear();
                        txtprecio_unitario.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar producto: " + ex.Message);
                }
            }
        private void BuscarProveedor()
        {
            try
            {
                if (string.IsNullOrEmpty(txtproveedor.Text))
                    return;

                var proveedores = logProveedor.Instancia.ListarProveedor();
                var proveedor = proveedores.FirstOrDefault(p =>
                    p.razonSocial.ToLower().Contains(txtproveedor.Text.ToLower()) ||
                    p.idProveedor.ToString() == txtproveedor.Text);

                if (proveedor != null)
                {
                    txtproveedor.Text = proveedor.razonSocial;
                    txtproveedor.Tag = proveedor.idProveedor;

                    // ✅ Ahora creamos la orden automáticamente
                    entOrden_Compra orden = new entOrden_Compra();
                    orden.fechaEmision = dtpTimer.Value;
                    orden.idProveedor = proveedor.idProveedor;
                    orden.total = 0;
                    orden.estado = true;

                    idOrdenActual = logOrden_Compra.Instancia.InsertarOrdenCompra(orden);
                    txtid_orden.Text = idOrdenActual.ToString();

                    txtproveedor.Enabled = false; // Bloqueamos para no cambiarlo luego
                    txtproductos.Enabled = true;
                    txtcantidad.Enabled = true;
                    btnguardar.Enabled = true;

                    
                    txtproductos.Focus();
                }
                else
                {
                    MessageBox.Show("Proveedor no encontrado");
                    txtproveedor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar proveedor: " + ex.Message);
            }
        }
        private void txtproveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarProveedor();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
            {
                try
                {
                    if (idOrdenActual == 0)
                    {
                        MessageBox.Show("Primero debe crear una orden con el botón 'Nuevo'");
                        return;
                    }

                    if (string.IsNullOrEmpty(txtproductos.Text) || txtproductos.Tag == null)
                    {
                        MessageBox.Show("Seleccione un producto válido");
                        txtproductos.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtcantidad.Text) || !int.TryParse(txtcantidad.Text, out int cantidad) || cantidad <= 0)
                    {
                        MessageBox.Show("Ingrese una cantidad válida");
                        txtcantidad.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtprecio_unitario.Text) || !decimal.TryParse(txtprecio_unitario.Text, out decimal precioUnitario) || precioUnitario <= 0)
                    {
                        MessageBox.Show("Precio unitario no válido");
                        return;
                    }

                    int idProducto = Convert.ToInt32(txtproductos.Tag);
                    decimal subtotal = cantidad * precioUnitario;

                    // Verificar si el producto ya existe
                    var detallesActuales = logDetalleOrdenCompra.Instancia.ListarDetalleOrdenCompra(idOrdenActual);
                    if (detallesActuales.Any(d => d.idProducto == idProducto))
                    {
                        MessageBox.Show("El producto ya está en la orden. Elimínelo primero si desea agregarlo nuevamente.");
                        return;
                    }

                    // Insertar detalle directamente en BD
                    entDetalleOrdenCompra detalle = new entDetalleOrdenCompra();
                    detalle.idOrdenCompra = idOrdenActual;
                    detalle.idProducto = idProducto;
                    detalle.cantidad = cantidad;
                    detalle.precioUnitario = precioUnitario;
                    detalle.subtotal = subtotal;

                    logDetalleOrdenCompra.Instancia.InsertarDetalleOrdenCompra(detalle);

                    // Actualizar total en la orden
                    ActualizarTotalOrden();

                    // Recargar grid
                    CargarDetalles();

                    LimpiarCamposDetalle();
                    MessageBox.Show("Producto agregado exitosamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar producto: " + ex.Message);
                }
            }

            private void ActualizarTotalOrden()
            {
                try
                {
                    var detalles = logDetalleOrdenCompra.Instancia.ListarDetalleOrdenCompra(idOrdenActual);
                    decimal total = detalles.Sum(d => d.subtotal);

                    // Actualizar en BD
                    entOrden_Compra orden = new entOrden_Compra();
                    orden.idOrdenCompra = idOrdenActual;
                    orden.fechaEmision = dtpTimer.Value;
                orden.idProveedor = Convert.ToInt32(txtproveedor.Tag);

                orden.total = total;
                    orden.estado = true;

                    logOrden_Compra.Instancia.EditarOrdenCompra(orden);

                    // Actualizar en pantalla
                    txttotal.Text = total.ToString("N2");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar total: " + ex.Message);
                }
            }

            private void CargarDetalles()
            {
                try
                {
                    if (idOrdenActual == 0) return;

                    var detalles = logDetalleOrdenCompra.Instancia.ListarDetalleOrdenCompra(idOrdenActual);

                    // Obtener nombres de productos
                    var productos = logProducto.Instancia.ListarProducto();
                    var detallesConNombre = detalles.Select(d => new
                    {
                        d.idDetalleOrden,
                        d.idProducto,
                        nombreProducto = productos.FirstOrDefault(p => p.idProducto == d.idProducto)?.nombreProducto ?? "Producto no encontrado",
                        d.cantidad,
                        d.precioUnitario,
                        d.subtotal
                    }).ToList();

                    dgvProductos.DataSource = detallesConNombre;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar detalles: " + ex.Message);
                }
            }

            private void LimpiarCamposDetalle()
            {
                txtproductos.Clear();
                txtproductos.Tag = null;
                txtprecio_unitario.Clear();
                txtcantidad.Clear();
                txtproductos.Focus();
            }

            private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    if (e.RowIndex < 0) return;

                    if (e.ColumnIndex == dgvProductos.Columns["btnEliminar"].Index)
                    {
                        DialogResult result = MessageBox.Show("¿Está seguro de eliminar este producto?",
                            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            int idDetalleOrden = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["idDetalleOrden"].Value);

                            logDetalleOrdenCompra.Instancia.EliminarDetalleOrdenCompra(idDetalleOrden);

                            ActualizarTotalOrden();
                            CargarDetalles();

                            MessageBox.Show("Producto eliminado");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            private void btnguardar_Click(object sender, EventArgs e)
            {
                try
                {
                    if (idOrdenActual == 0)
                    {
                        MessageBox.Show("No hay orden activa");
                        return;
                    }

                    var detalles = logDetalleOrdenCompra.Instancia.ListarDetalleOrdenCompra(idOrdenActual);
                    if (detalles.Count == 0)
                    {
                        MessageBox.Show("Debe agregar al menos un producto a la orden");
                        return;
                    }

                    MessageBox.Show($"Orden de compra {idOrdenActual} guardada exitosamente con {detalles.Count} producto(s) y total de S/. {txttotal.Text}");
                    btncerrar_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }

            private void btneditar_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Para editar, seleccione una orden de la lista de órdenes");
            }

            private void btneliminar_Click(object sender, EventArgs e)
            {
                try
                {
                    if (idOrdenActual == 0)
                    {
                        MessageBox.Show("No hay orden seleccionada");
                        return;
                    }

                    DialogResult result = MessageBox.Show("¿Está seguro de eliminar esta orden de compra?",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        entOrden_Compra orden = new entOrden_Compra();
                        orden.idOrdenCompra = idOrdenActual;

                        logOrden_Compra.Instancia.DeshabilitarOrdenCompra(orden);
                        MessageBox.Show("Orden eliminada exitosamente");
                        btncerrar_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        private void ConfigurarAutoCompletarProveedor()
        {
            try
            {
                var proveedores = logProveedor.Instancia.ListarProveedor();
                AutoCompleteStringCollection listaProveedores = new AutoCompleteStringCollection();

                // Agrega la razón social de cada proveedor a la lista
                foreach (var p in proveedores)
                {
                    listaProveedores.Add(p.razonSocial);
                }

                // Configura el TextBox para usar autocompletado
                txtproveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtproveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtproveedor.AutoCompleteCustomSource = listaProveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores para autocompletar: " + ex.Message);
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
            {
                LimpiarCampos();
                DeshabilitarControles();
                btnguardar.Enabled = false;
                btncerrar.Enabled = false;
                btnnuevo.Enabled = true;
                btneditar.Enabled = true;
                btneliminar.Enabled = true;
            }
        }
    }