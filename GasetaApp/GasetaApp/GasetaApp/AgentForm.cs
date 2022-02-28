using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasetaApp
{
    public partial class AgentForm : Form
    {
        Model1 db = new Model1();
        public AgentForm()
        {
            InitializeComponent();
        }

        private void AgentForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database_New_GasetaDataSet.Agent". При необходимости она может быть перемещена или удалена.
            this.agentTableAdapter.Fill(this.database_New_GasetaDataSet.Agent);

            List<string> listParam = new List<string>();
            listParam.Add("по типу");
            listParam.Add("по приоритету");
            comboBox1.DataSource = listParam;

            List<string> listFiltr = new List<string>();
            listFiltr = db.AgentType.Select(a => a.Title).ToList();
            listFiltr.Insert(0, "Все типы");
            filtrCombo.DataSource = listFiltr;
            agentTypeBindingSource.DataSource = db.AgentType.ToList();
            agentBindingSource1.DataSource = db.Agent.ToList();

        }

        private void agentBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Agent agn = (Agent)agentBindingSource.Current;
            if (agn.Logo != null && agn.Logo != "")
            {
                string str = agn.Logo.Substring(1);
                pictureBox1.Image = Image.FromFile(str);
            }
            else
            {
                pictureBox1.Image = Image.FromFile(@"agents\picture.png");
            }
        }

        private void filtrCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filtrCombo.Text == "Все типы")
            {
                agentBindingSource.DataSource = db.Agent.ToList();
            }
            else
            {
                agentBindingSource.DataSource = db.Agent.Where(a => a.AgentType.Title == filtrCombo.Text).ToList();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "по типу")
            {
                agentBindingSource.DataSource = db.Agent.OrderBy(a => a.AgentTypeID).ToList();
            }
            else
            {
                agentBindingSource.DataSource = db.Agent.OrderBy(a => a.Priority).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEditAgentForm form = new AddEditAgentForm();
            form.db = db;
            form.agn = null;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = db.Agent.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Agent agn = (Agent)agentBindingSource.Current;
            AddEditAgentForm form = new AddEditAgentForm();
            form.db = db;
            form.agn = agn;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = db.Agent.ToList();
            }
        }
    }
}
