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
using UnitTestReporter.Business.Interfaces;

namespace UnitTestReporter
{
    public partial class Form1 : Form
    {
        private readonly ILogger logger;
        private readonly ICmdCaller cmdCaller;
        public Form1(ILogger<Form1> _logger, ICmdCaller _cmdCaller)
        {
            logger = _logger;
            cmdCaller = _cmdCaller;
            InitializeComponent();
        }

    }
}
