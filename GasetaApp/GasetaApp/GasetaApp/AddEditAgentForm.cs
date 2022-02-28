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
    public partial class AddEditAgentForm : AgentForm
    {
        public Model1 db { get; set; }
        public Agent agn { get; set; } = null;
        public AddEditAgentForm()
        {
            InitializeComponent();
        }

        private void AddEditAgentForm_Load(object sender, EventArgs e)
        {
            agentTypeBindingSource.DataSource = db.AgentType.ToList();
            if (agn == null)
            {
                agentBindingSource.AddNew();
            }
            else
            {
                agentBindingSource.Add(agn);
            }

        }
    }
}
