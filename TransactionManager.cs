using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalysis
{
    class TransactionManager
    {
        public decimal TotalValue {  get { return m_CashBalance + StockValue; } }

        public decimal TotalGrowth { get { return ((m_CashBalance + StockValue) - m_InitialCash) / m_InitialCash; } }

        public decimal AverageYearlyGrowth { get; private set; }

        public decimal StockValue { get; private set; }

        public decimal StockShares { get; private set; }

        public void Init(Logger logger, DateTime date, decimal cash, decimal stockPrice, decimal targetRatio, decimal rebalanceRatio)
        {
            m_CurrentYear = date.Year;
            m_InitialCash = cash;
            m_CashBalance = cash;
            StockValue = 0;
            StockShares = 0;
            m_TargetRatio = targetRatio;
            m_RebalanceRatio = rebalanceRatio;
            m_RebalanceRequired = false;

            m_TotalRebalancesRequired = 0;
            m_TotalRebalancesExecutes = 0;
            m_TotalCashShorts = 0;
            m_YearlyBalances.Clear();
            m_YearlyBalances.Add(cash);
            AverageYearlyGrowth = 0;

            logger.LogInfo("Starting cash: " + cash.ToString("C"));
            logger.LogInfo("");

            StockShares = 0;

            // Initial buy in
            Add(logger, date, stockPrice, (cash * targetRatio) / stockPrice);
        }

        public void Add(Logger logger, DateTime date, decimal stockPrice, decimal shares)
        {
            if (stockPrice > 0)
            {
                EventLogInfo eventLogInfo = new EventLogInfo();

                // Update the stock value to the current price
                StockValue = StockShares * stockPrice;

                if (m_CurrentYear != date.Year)
                {
                    m_YearlyBalances.Add(StockValue + m_CashBalance);
                    m_CurrentYear = date.Year;
                }

                decimal transactionCost = stockPrice * shares;

                if (shares < 0)
                {
                    // Sell
                    m_CashBalance -= transactionCost;
                    StockShares += shares;
                    StockValue = StockShares * stockPrice;

                    if (m_CashBalance >= (m_RebalanceRatio * (StockValue + m_CashBalance)))
                    {
                        m_RebalanceRequired = true;
                        eventLogInfo.CreateRebalanceFlagged();
                        //logger.LogInfo("Rebalance flagged for next buy");
                        ++m_TotalRebalancesRequired;
                    }
                }
                else if (shares > 0)
                {
                    // Buy

                    if (m_RebalanceRequired)
                    {
                        // We have excess cash, so adjust the buy order so that we are rebalanced
                        //logger.LogInfo("Rebalance required");

                        decimal sharesAdjustedBy = 0;
                        decimal targetCash = (StockValue + m_CashBalance) * (1 - m_TargetRatio);
                        if ((m_CashBalance - transactionCost) > targetCash)
                        {
                            decimal adjustedShares = targetCash / stockPrice;
                            sharesAdjustedBy = adjustedShares - shares;
                            //logger.LogInfo("Share purchase increased by " + (adjustedShares - shares).ToString("0.##"));
                            shares = adjustedShares;
                            transactionCost = targetCash;
                        }
                        m_RebalanceRequired = false;
                        //logger.LogInfo("Rebalance corrected");
                        eventLogInfo.CreateRebalanceTriggered(sharesAdjustedBy);
                        ++m_TotalRebalancesExecutes;
                    }

                    if (m_CashBalance <= transactionCost)
                    {
                        // We do not have enough cash to purchases the requested amount of stock
                        //logger.LogInfo("*Insufficient Cash* Requested: " + transactionCost.ToString("C") + " Current: " + m_CashBalance.ToString("C"));
                        decimal adjustedShares = m_CashBalance / stockPrice;
                        decimal sharesAdjustedBy = (m_CashBalance > 0) ? shares - adjustedShares : 0;
                        //logger.LogInfo("Share purchase decreased by " + (shares - adjustedShares).ToString("0.##"));
                        eventLogInfo.CreateCashShort(transactionCost, m_CashBalance, sharesAdjustedBy);
                        shares = adjustedShares;
                        transactionCost = m_CashBalance;
                        ++m_TotalCashShorts;
                    }
                    m_CashBalance -= transactionCost;
                    StockShares += shares;
                    StockValue = StockShares * stockPrice;
                }

                LogTransaction(logger, shares, StockShares, date, stockPrice, StockValue, m_CashBalance, eventLogInfo);
            }
        }

        public void Complete(Logger logger)
        {
            logger.LogInfo("Rebalances required: " + m_TotalRebalancesRequired.ToString());
            logger.LogInfo("Rebalances executed: " + m_TotalRebalancesExecutes.ToString());
            logger.LogInfo("Months cash reserve was short: " + m_TotalCashShorts.ToString());

            logger.LogInfo("");

            if (m_YearlyBalances.Count > 1)
            {
                logger.LogInfo("Total Fund Growth");

                decimal accumulator = 0.0m;
                decimal previousYear = m_YearlyBalances[0];
                for (int index = 1; index < m_YearlyBalances.Count; ++index)
                {
                    decimal difference = m_YearlyBalances[index] - previousYear;
                    decimal yearlyGrowth = (difference / previousYear);

                    logger.LogInfo("Year " + index.ToString() + ": " + yearlyGrowth.ToString("P"));

                    accumulator += yearlyGrowth;
                    previousYear = m_YearlyBalances[index];
                }

                AverageYearlyGrowth = (accumulator / (m_YearlyBalances.Count - 1));
            }
        }

        private void LogTransaction(Logger logger, decimal shares, decimal totalShares, DateTime date, decimal stockPrice, decimal stock, decimal cash, EventLogInfo eventLogInfo)
        {
            logger.LogInfo("Transaction : " + date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString());
            logger.LogInfo("Stock Price : " + stockPrice.ToString("C"));

            if (eventLogInfo.RebalanceFlagged)
            {
                logger.LogInfo("Rebalance flagged for next buy");
            }

            if(eventLogInfo.RebalanceTriggered)
            {
                logger.LogInfo("Rebalance required");
                logger.LogInfo("Share purchase increased by " + eventLogInfo.AdjustedSharesForRebalance.ToString("0.##"));
                logger.LogInfo("Rebalance corrected");
            }

            if (eventLogInfo.CashShort)
            {
                logger.LogWarn("Insufficient Cash Requested: " + eventLogInfo.CashRequestedForCashShort.ToString("C") + " Current: " + eventLogInfo.CashAvailableForCashShort.ToString("C"));
                logger.LogWarn("Share purchase decreased by " + eventLogInfo.AdjustedSharesForCashShort.ToString("0.##"));
            }

            if (shares != 0)
            {
                decimal adjustedShares = (shares > 0) ? shares : -shares;
                logger.LogInfo(((shares > 0) ? "Bought " : "Sold ") + adjustedShares.ToString("0.##") + " shares");
                logger.LogInfo("Total Shares: " + totalShares.ToString("0.##"));
                logger.LogInfo("Total Stock: " + stock.ToString("C") + " (" + (stock / (stock + cash)).ToString("P") + ")");
                logger.LogInfo("Total Cash: " + cash.ToString("C") + " (" + (cash / (stock + cash)).ToString("P") + ")");
                logger.LogInfo("Total Assets: " + (stock + cash).ToString("C"));
            }

            logger.LogInfo("");
        }

        private int m_CurrentYear = 0;
        private decimal m_InitialCash = 0;
        private decimal m_CashBalance = 0;
        private decimal m_TargetRatio = 0;
        private decimal m_RebalanceRatio = 0;
        private bool m_RebalanceRequired = false;

        private class EventLogInfo
        {
            public bool RebalanceFlagged { get; private set; }
            public bool RebalanceTriggered { get; private set; }
            public bool CashShort { get; private set; }
            public decimal AdjustedSharesForRebalance { get; private set; }
            public decimal CashRequestedForCashShort { get; private set; }
            public decimal CashAvailableForCashShort { get; private set; }
            public decimal AdjustedSharesForCashShort { get; private set; }

            public EventLogInfo()
            {
                RebalanceFlagged = false;
                RebalanceTriggered = false;
                CashShort = false;
            }

            public void CreateRebalanceFlagged()
            {
                RebalanceFlagged = true;
            }

            public void CreateRebalanceTriggered(decimal sharesAdjustedBy)
            {
                RebalanceTriggered = true;
                AdjustedSharesForRebalance = sharesAdjustedBy;
            }

            public void CreateCashShort(decimal cashRequested, decimal cashAvailable, decimal sharesAdjustedBy)
            {
                CashShort = true;
                CashRequestedForCashShort = cashRequested;
                CashAvailableForCashShort = cashAvailable;
                AdjustedSharesForCashShort = sharesAdjustedBy;
            }
        }

        // Stats
        private int m_TotalRebalancesRequired = 0;
        private int m_TotalRebalancesExecutes = 0;
        private int m_TotalCashShorts = 0;
        private List<decimal> m_YearlyBalances = new List<decimal>();
    }
}
