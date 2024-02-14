using Severstal.Core;
using Severstal.Data;
using Severstal.ExportDocument;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Severstal
{
    public partial class Main : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IService _service;

        public Main(IServiceProvider serviceProvider, IService service)
        {
            _serviceProvider = serviceProvider;
            _service = service;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _service.startDataProcessing();
        }
    }
}
