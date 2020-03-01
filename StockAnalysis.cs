using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAnalysis
{
    public partial class StockAnalysis : Form
    {
        public StockAnalysis()
        {
            InitializeComponent();

            CacheControlReferences();

            m_Logger = new Logger(m_LogTextBox);
            m_Transactions = new TransactionManager();
        }

        private void button_LoadHistoricalData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Stream fileStream = openFileDialog.OpenFile();

                        m_HistoricalData = new HistoricalData(fileStream);

                        if (m_LoadedFilenameTextBox != null)
                        {
                            m_LoadedFilenameTextBox.Text = openFileDialog.FileName;
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Data Loader Failure");
                        return;
                    }
                }
            }
        }

        private bool Recalculate()
        {
            try
            {
                if (ValidateInputs())
                {
                    if(CalculateResults())
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

            }

            return false;
        }

        private void CacheControlReferences()
        {
            // Cache references to any controls that we will need
            foreach (Control control in this.Controls)
            {
                // We expect to find several TextBox controls
                if (control is TextBox)
                {
                    switch(control.Name)
                    {
                        case "textBox_LoadedHistoricalData":
                            m_LoadedFilenameTextBox = (control as TextBox);
                            break;
                        case "textBox_InitialInvestment":
                            m_InitialInvestmentTextBox = (control as TextBox);
                            break;
                        case "textBox_Rule":
                            m_RuleTextBox = (control as TextBox);
                            break;
                        case "textBox_Frequency":
                            m_FrequencyTextBox = (control as TextBox);
                            break;
                        case "textBox_FinalInvestment":
                            m_FinalInvestmentTextBox = (control as TextBox);
                            break;
                        case "textBox_Growth":
                            m_GrowthTextBox = (control as TextBox);
                            break;
                        case "textBox_Log":
                            m_LogTextBox = (control as TextBox);
                            break;
                        case "textBox_StockCashRatio":
                            m_StockToCashRatioTextBox = (control as TextBox);
                            break;
                        case "textBox_RebalanceTrigger":
                            m_RebalanceTriggerTextBox = (control as TextBox);
                            break;
                        case "textBox_AverageYearlyGrowth":
                            m_AverageYearlyGrowthTextBox = (control as TextBox);
                            break;
                        default:
                            break;
                    }
                }
                else if(control is DateTimePicker)
                {
                    switch(control.Name)
                    {
                        case "dateTimePicker_Start":
                            m_StartDateTimePicker = (control as DateTimePicker);
                            break;
                        case "dateTimePicker_End":
                            m_EndDateTimePicker = (control as DateTimePicker);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private bool ValidateInputs()
        {
            return m_HistoricalData != null;
        }

        private bool CalculateResults()
        {
            // Clear the log
            m_LogTextBox.Clear();

            // Create a list of the relevant monthly entries for the requested range
            List<Tuple<DateTime, decimal>> historicalDataEntryList;
            if(m_HistoricalData.GetDataRange(m_StartDateTimePicker.Value, m_EndDateTimePicker.Value, out historicalDataEntryList) == false)
            {
                m_Logger.LogError("Failed to load historical data for requested range");
                return false;
            }

            // Process the data
            m_Logger.LogInfo("Found " + historicalDataEntryList.Count.ToString() + " data entries in the requested range");
            m_Logger.LogInfo("");

            // Extract parameters from form
            decimal initialCash = decimal.Parse(m_InitialInvestmentTextBox.Text.Replace("$", ""));
            decimal baseStockRatio = decimal.Parse(m_StockToCashRatioTextBox.Text.Replace("%", "")) / 100.0m;
            int frequency = int.Parse(m_FrequencyTextBox.Text);
            decimal rulePercent = decimal.Parse(m_RuleTextBox.Text.Replace("%", "")) / 100.0m;
            decimal rebalanceRatio = decimal.Parse(m_RebalanceTriggerTextBox.Text.Replace("%", "")) / 100.0m;
            DateTime currentDate = historicalDataEntryList[0].Item1;
            decimal currentPrice = historicalDataEntryList[0].Item2;

            // Reset transaction data and perform initial buy in
            m_Transactions.Init(m_Logger, currentDate, initialCash, currentPrice, baseStockRatio, rebalanceRatio);

            decimal oldPrice = currentPrice;
            decimal rulerPercentScalar = 1.0m + rulePercent;

            for (int dataIndex = frequency; dataIndex < historicalDataEntryList.Count; dataIndex += frequency)
            {
                // Build a transaction
                currentDate = historicalDataEntryList[dataIndex].Item1;
                currentPrice = historicalDataEntryList[dataIndex].Item2;
                decimal signalLine = m_Transactions.StockValue * rulerPercentScalar;
                decimal currentStockBalance = m_Transactions.StockShares * currentPrice;
                decimal balance = signalLine - currentStockBalance;
                decimal sharesNeeded = balance / currentPrice;

                m_Transactions.Add(m_Logger, currentDate, currentPrice, sharesNeeded);
            }

            m_Transactions.Complete(m_Logger);

            // Write results to form
            m_FinalInvestmentTextBox.Text = m_Transactions.TotalValue.ToString("C");
            m_GrowthTextBox.Text = m_Transactions.TotalGrowth.ToString("P");
            m_AverageYearlyGrowthTextBox.Text = m_Transactions.AverageYearlyGrowth.ToString("P");

            return true;
        }

        private TextBox m_LoadedFilenameTextBox = null;
        private TextBox m_InitialInvestmentTextBox = null;
        private TextBox m_RuleTextBox = null;
        private TextBox m_FrequencyTextBox = null;
        private TextBox m_FinalInvestmentTextBox = null;
        private TextBox m_GrowthTextBox = null;
        private TextBox m_AverageYearlyGrowthTextBox = null;
        private TextBox m_LogTextBox = null;
        private TextBox m_StockToCashRatioTextBox = null;
        private TextBox m_RebalanceTriggerTextBox = null;

        private DateTimePicker m_StartDateTimePicker = null;
        private DateTimePicker m_EndDateTimePicker = null;

        private HistoricalData m_HistoricalData = null;
        private Logger m_Logger = null;
        private TransactionManager m_Transactions = null;

        private void textBox_Percentage_Validated(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string input = textBox.Text.Trim();
                if (input.Contains("%"))
                {
                    input = input.Replace("%", "");
                }

                string specifier = "P";
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                decimal percentage = decimal.Parse(input);
                textBox.Text = (percentage / 100.0m).ToString(specifier, culture);
            }
        }

        private void textBox_Percentage_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string value = textBox.Text.Trim();
                if (value.Contains("%"))
                {
                    value = value.Replace("%", "");
                }

                NumberStyles style;
                CultureInfo culture;
                decimal percentage;

                style = NumberStyles.Number;
                culture = CultureInfo.CreateSpecificCulture("en-US");
                if (!decimal.TryParse(value, style, culture, out percentage) || percentage <= 0.0m || percentage > 100.0m)
                {
                    MessageBox.Show("Please enter a valid percentage amount.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // prevent the textbox from losing focus
                    e.Cancel = true;
                }
            }
        }

        private void textBox_Currency_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                decimal currency;

                NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                if (!decimal.TryParse(textBox.Text, style, culture, out currency))
                {
                    MessageBox.Show("Please enter a valid currency amount.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // prevent the textbox from losing focus
                    e.Cancel = true;
                }
            }
        }

        private void textBox_Currency_Validated(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string input = textBox.Text.Trim();
                if (input.Contains("$"))
                {
                    input = input.Replace("$", "");
                }

                string specifier = "C";
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                textBox.Text = decimal.Parse(input).ToString(specifier, culture);
            }
        }

        private void button_RunSimulation_Click(object sender, EventArgs e)
        {
            if(Recalculate() == false)
            {
                MessageBox.Show("Check your input", "Stock Analysis Failure");
            }
        }
    }
}
