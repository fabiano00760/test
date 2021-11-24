using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace CadAluno
{
    public partial class Form1 : Form
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;uid=root;pwd='';database=2021_02_seg;SslMode=none");

        public Form1()
        {
            InitializeComponent();
        }

        private void Salvar(object sender, EventArgs e)
        {
            string sql = "";
            string msg = "";
            if (textBoxMatricula.Text.Equals("")){
                sql = $"insert into aluno2(nome,cpf) values('{textBoxNome.Text}','{textBoxCPF.Text}')";
                msg = "Dados inseridos com sucesso!";
            }
            else{
                sql = $"update aluno2 set nome='{textBoxNome.Text}', cpf='{textBoxCPF.Text}' where matricula={textBoxMatricula.Text}";
                msg = "Dados atualizados com sucesso!";
            }
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show(msg);
            }catch(Exception ex){
                MessageBox.Show("Problema ao salvar aluno: " + ex.Message, "Academia de Ginástica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }finally { 
                conexao.Close();
                comando.Dispose();
            }
        }

        private void Consultar(object sender, EventArgs e)
        {
            textBoxMatricula.Enabled = true;
            buttonBuscar.Enabled = true;
            buttonSalvar.Enabled = false;
            buttonExcluir.Enabled = false;
            textBoxMatricula.Text = "";
            textBoxNome.Text = "";
            textBoxCPF.Text = "";
            textBoxNome.Enabled = false;
            textBoxCPF.Enabled = false;
        }

        private void Buscar(object sender, EventArgs e)
        {
            string sql = $"select * from aluno2 where matricula='{textBoxMatricula.Text}'";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            try
            {
                conexao.Open();
                MySqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    textBoxNome.Text = dr["nome"].ToString();
                    textBoxCPF.Text = dr["cpf"].ToString();

                    textBoxMatricula.Enabled = false;
                    textBoxNome.Enabled = true;
                    textBoxCPF.Enabled = true;
                    buttonSalvar.Enabled = true;
                    buttonExcluir.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Registro não encontrado!");
                    textBoxNome.Text = "";
                    textBoxCPF.Text = "";
                }
            }catch (Exception ex){
                MessageBox.Show("Problema ao consultar aluno: " + ex.Message, "Academia de Ginástica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
                comando.Dispose();
            }
        }

        private void Excluir(object sender, EventArgs e)
        {
            string sql = $"delete from aluno2 where matricula={textBoxMatricula.Text}";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();

                MessageBox.Show("Dados excluídos com sucesso!");
                textBoxMatricula.Text = "";
                textBoxNome.Text = "";
                textBoxCPF.Text = "";
                buttonExcluir.Enabled = false;
                buttonBuscar.Enabled = false;
            }catch (Exception ex){
                MessageBox.Show("Problema ao excluir aluno: " + ex.Message, "Academia de Ginástica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }finally{
                conexao.Close();
                comando.Dispose();
            }
        }

        private void Novo(object sender, EventArgs e)
        {
            textBoxMatricula.Text = "";
            textBoxNome.Text = "";
            textBoxCPF.Text = "";

            textBoxMatricula.Enabled = false;
            textBoxNome.Enabled = true;
            textBoxCPF.Enabled = true;
            buttonBuscar.Enabled = false;
            buttonSalvar.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
