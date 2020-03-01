using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAnalysis
{
    class Logger
    {
        public Logger(TextBox textBox)
        {
            m_TextBox = textBox;
        }

        public void LogInfo(string message)
        {
            AppendLine(message);
        }

        public void LogWarn(string message)
        {
            // TODO: Change text color
            AppendLine(message);
        }

        public void LogError(string message)
        {
            // TODO: Change text color
            AppendLine(message);
        }

        public void AppendLine(string value)
        {
            if (m_TextBox.Text.Length == 0)
            {
                m_TextBox.Text = value;
            }
            else
            {
                m_TextBox.AppendText(System.Environment.NewLine + value);
            }
        }

        private TextBox m_TextBox = null;
    }
}
