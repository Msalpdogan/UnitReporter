using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Windows.Forms;
using UnitTestReporter.Business.Interfaces;
using UnitTestReporter.Core.Configuration;

namespace UnitTestReporter
{
    public partial class Form1 : Form
    {
        private readonly ILogger logger;
        private readonly ICmdCaller cmdCaller;
        private readonly CommonSettings options;

        public Form1(ILogger<Form1> _logger, ICmdCaller _cmdCaller, IOptions<CommonSettings> _options)
        {
            logger = _logger;
            cmdCaller = _cmdCaller;
            options = _options.Value;
            InitializeComponent();
            var a = options.MinLogLevel;
        }

    }
}
