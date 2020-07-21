using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace my_agenda
{
    class agenda
    {
        private SQLiteConnection cn = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader reader = null;
        private DataTable table = null;

        public bool insertar(string nombre, string telefono)
        {
            try
            {
                string query = "INSERT INTO directorio(nombre, telefono) VALUES('" + nombre + "' , '" + telefono + "')";
                cn = conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                if(cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }

        public DataTable consultar()
        {
            try
            {
                nombresColumnas();
                string query = "SELECT * FROM directorio";
                cn = conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(new object[] { reader["id"], reader["nombre"], reader["telefono"] });
                }
                reader.Close();
                cn.Close();
                return table;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                if(cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return table;
        }

        public bool eliminar(int id)
        {
            try
            {
                string query = "DELETE FROM directorio WHERE id='" + id + "'";
                cn = conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                if(cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }

        public DataTable filtrar(string filtro) { return table; }

        public bool actualizar(int id, string nombre, string telefono)
        {
            try
            {
                string query = "UPDATE directorio SET nombre='" + nombre + "', telefono='" + telefono + "' Where id='" + id.ToString() + "'";
                System.Windows.Forms.MessageBox.Show(query);
                cn = conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Ocurrio un Error en el proceso");
            }
            finally
            {
                if(cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }

        private void nombresColumnas()
        {
            table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Nombre");
            table.Columns.Add("Telefono");
        }
    }
}
