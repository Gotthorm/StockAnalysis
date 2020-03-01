namespace StockAnalysis
{
    partial class StockAnalysis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_LoadHistoricalData = new System.Windows.Forms.Button();
            this.textBox_LoadedHistoricalData = new System.Windows.Forms.TextBox();
            this.label_StartDate = new System.Windows.Forms.Label();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_InitialInvestment = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Rule = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Frequency = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_FinalInvestment = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Growth = new System.Windows.Forms.TextBox();
            this.textBox_Log = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_StockCashRatio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_RebalanceTrigger = new System.Windows.Forms.TextBox();
            this.button_RunSimulation = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_AverageYearlyGrowth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_LoadHistoricalData
            // 
            this.button_LoadHistoricalData.Location = new System.Drawing.Point(13, 13);
            this.button_LoadHistoricalData.Name = "button_LoadHistoricalData";
            this.button_LoadHistoricalData.Size = new System.Drawing.Size(75, 23);
            this.button_LoadHistoricalData.TabIndex = 0;
            this.button_LoadHistoricalData.Text = "Load";
            this.button_LoadHistoricalData.UseVisualStyleBackColor = true;
            this.button_LoadHistoricalData.Click += new System.EventHandler(this.button_LoadHistoricalData_Click);
            // 
            // textBox_LoadedHistoricalData
            // 
            this.textBox_LoadedHistoricalData.Location = new System.Drawing.Point(95, 15);
            this.textBox_LoadedHistoricalData.Name = "textBox_LoadedHistoricalData";
            this.textBox_LoadedHistoricalData.ReadOnly = true;
            this.textBox_LoadedHistoricalData.Size = new System.Drawing.Size(893, 20);
            this.textBox_LoadedHistoricalData.TabIndex = 1;
            // 
            // label_StartDate
            // 
            this.label_StartDate.AutoSize = true;
            this.label_StartDate.Location = new System.Drawing.Point(12, 75);
            this.label_StartDate.Name = "label_StartDate";
            this.label_StartDate.Size = new System.Drawing.Size(29, 13);
            this.label_StartDate.TabIndex = 3;
            this.label_StartDate.Text = "Start";
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Location = new System.Drawing.Point(47, 69);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_Start.TabIndex = 4;
            this.dateTimePicker_Start.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(47, 95);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_End.TabIndex = 6;
            this.dateTimePicker_End.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "End";
            // 
            // textBox_InitialInvestment
            // 
            this.textBox_InitialInvestment.Location = new System.Drawing.Point(134, 146);
            this.textBox_InitialInvestment.Name = "textBox_InitialInvestment";
            this.textBox_InitialInvestment.Size = new System.Drawing.Size(100, 20);
            this.textBox_InitialInvestment.TabIndex = 7;
            this.textBox_InitialInvestment.Text = "$10,000.00";
            this.textBox_InitialInvestment.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Currency_Validating);
            this.textBox_InitialInvestment.Validated += new System.EventHandler(this.textBox_Currency_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Initial Investment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Rule";
            // 
            // textBox_Rule
            // 
            this.textBox_Rule.Location = new System.Drawing.Point(134, 172);
            this.textBox_Rule.Name = "textBox_Rule";
            this.textBox_Rule.Size = new System.Drawing.Size(100, 20);
            this.textBox_Rule.TabIndex = 9;
            this.textBox_Rule.Text = "3.00%";
            this.textBox_Rule.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Percentage_Validating);
            this.textBox_Rule.Validated += new System.EventHandler(this.textBox_Percentage_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Frequency (months)";
            // 
            // textBox_Frequency
            // 
            this.textBox_Frequency.Location = new System.Drawing.Point(134, 198);
            this.textBox_Frequency.Name = "textBox_Frequency";
            this.textBox_Frequency.Size = new System.Drawing.Size(100, 20);
            this.textBox_Frequency.TabIndex = 11;
            this.textBox_Frequency.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Final Investment";
            // 
            // textBox_FinalInvestment
            // 
            this.textBox_FinalInvestment.Location = new System.Drawing.Point(134, 276);
            this.textBox_FinalInvestment.Name = "textBox_FinalInvestment";
            this.textBox_FinalInvestment.ReadOnly = true;
            this.textBox_FinalInvestment.Size = new System.Drawing.Size(100, 20);
            this.textBox_FinalInvestment.TabIndex = 13;
            this.textBox_FinalInvestment.Text = "$0.00";
            this.textBox_FinalInvestment.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Currency_Validating);
            this.textBox_FinalInvestment.Validated += new System.EventHandler(this.textBox_Currency_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Total Growth";
            // 
            // textBox_Growth
            // 
            this.textBox_Growth.Location = new System.Drawing.Point(134, 302);
            this.textBox_Growth.Name = "textBox_Growth";
            this.textBox_Growth.ReadOnly = true;
            this.textBox_Growth.Size = new System.Drawing.Size(100, 20);
            this.textBox_Growth.TabIndex = 15;
            this.textBox_Growth.Text = "0.00%";
            this.textBox_Growth.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Percentage_Validating);
            this.textBox_Growth.Validated += new System.EventHandler(this.textBox_Percentage_Validated);
            // 
            // textBox_Log
            // 
            this.textBox_Log.Location = new System.Drawing.Point(253, 69);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Log.Size = new System.Drawing.Size(735, 253);
            this.textBox_Log.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Stock to Cash Ratio";
            // 
            // textBox_StockCashRatio
            // 
            this.textBox_StockCashRatio.Location = new System.Drawing.Point(134, 224);
            this.textBox_StockCashRatio.Name = "textBox_StockCashRatio";
            this.textBox_StockCashRatio.Size = new System.Drawing.Size(100, 20);
            this.textBox_StockCashRatio.TabIndex = 18;
            this.textBox_StockCashRatio.Text = "80.00%";
            this.textBox_StockCashRatio.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Percentage_Validating);
            this.textBox_StockCashRatio.Validated += new System.EventHandler(this.textBox_Percentage_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Rebalance Trigger";
            // 
            // textBox_RebalanceTrigger
            // 
            this.textBox_RebalanceTrigger.Location = new System.Drawing.Point(134, 250);
            this.textBox_RebalanceTrigger.Name = "textBox_RebalanceTrigger";
            this.textBox_RebalanceTrigger.Size = new System.Drawing.Size(100, 20);
            this.textBox_RebalanceTrigger.TabIndex = 20;
            this.textBox_RebalanceTrigger.Text = "30.00%";
            this.textBox_RebalanceTrigger.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Percentage_Validating);
            this.textBox_RebalanceTrigger.Validated += new System.EventHandler(this.textBox_Percentage_Validated);
            // 
            // button_RunSimulation
            // 
            this.button_RunSimulation.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_RunSimulation.Location = new System.Drawing.Point(438, 345);
            this.button_RunSimulation.Name = "button_RunSimulation";
            this.button_RunSimulation.Size = new System.Drawing.Size(125, 25);
            this.button_RunSimulation.TabIndex = 22;
            this.button_RunSimulation.Text = "Run Simulation";
            this.button_RunSimulation.UseVisualStyleBackColor = true;
            this.button_RunSimulation.Click += new System.EventHandler(this.button_RunSimulation_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 331);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Average Yearly Growth";
            // 
            // textBox_AverageYearlyGrowth
            // 
            this.textBox_AverageYearlyGrowth.Location = new System.Drawing.Point(134, 328);
            this.textBox_AverageYearlyGrowth.Name = "textBox_AverageYearlyGrowth";
            this.textBox_AverageYearlyGrowth.ReadOnly = true;
            this.textBox_AverageYearlyGrowth.Size = new System.Drawing.Size(100, 20);
            this.textBox_AverageYearlyGrowth.TabIndex = 23;
            this.textBox_AverageYearlyGrowth.Text = "0.00%";
            // 
            // StockAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 382);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_AverageYearlyGrowth);
            this.Controls.Add(this.button_RunSimulation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_RebalanceTrigger);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_StockCashRatio);
            this.Controls.Add(this.textBox_Log);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_Growth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_FinalInvestment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Frequency);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Rule);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_InitialInvestment);
            this.Controls.Add(this.dateTimePicker_End);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker_Start);
            this.Controls.Add(this.label_StartDate);
            this.Controls.Add(this.textBox_LoadedHistoricalData);
            this.Controls.Add(this.button_LoadHistoricalData);
            this.Name = "StockAnalysis";
            this.Text = "Stock Analysis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_LoadHistoricalData;
        private System.Windows.Forms.TextBox textBox_LoadedHistoricalData;
        private System.Windows.Forms.Label label_StartDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_InitialInvestment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Rule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Frequency;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_FinalInvestment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Growth;
        private System.Windows.Forms.TextBox textBox_Log;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_StockCashRatio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_RebalanceTrigger;
        private System.Windows.Forms.Button button_RunSimulation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_AverageYearlyGrowth;
    }
}

