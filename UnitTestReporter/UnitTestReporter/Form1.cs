using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTestReporter
{
    public partial class Form1 : Form
    {
        private readonly ILogger logger;
        public Form1(ILogger<Form1> _logger)
        {
            logger = _logger;
            InitializeComponent();
        }

    }
}
