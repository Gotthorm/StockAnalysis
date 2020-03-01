using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StockAnalysis
{
    class HistoricalData
    {
        public HistoricalData(Stream inputStream)
        {
            try
            {
                using (StreamReader reader = new StreamReader(inputStream))
                {
                    // Expect the first line to be the column headings
                    string lineData = reader.ReadLine();
                    if(lineData == null)
                    {
                        throw new Exception("Failed to read column headings");
                    }

                    string[] columnHeadings = lineData.Split(',');

                    // I need a column named "Date"
                    int dateColumnIndex = -1;
                    for (int index = 0; index < columnHeadings.Length; ++index)
                    {
                        if (columnHeadings[index].ToLower() == "date")
                        {
                            dateColumnIndex = index;
                            break;
                        }
                    }

                    // I need a column named "Adj Close" or "Close"
                    int closeColumnIndex = -1;
                    for (int index = 0; index < columnHeadings.Length; ++index)
                    {
                        if (columnHeadings[index].ToLower() == "adj close")
                        {
                            closeColumnIndex = index;
                            break;
                        }
                        else if (columnHeadings[index].ToLower() == "close")
                        {
                            closeColumnIndex = index;
                        }
                    }

                    if (dateColumnIndex == -1 || closeColumnIndex == -1)
                    {
                        throw new Exception("Failed to find 'date' and 'close' colums");
                    }

                    if(ReadData(reader, dateColumnIndex, closeColumnIndex) == false)
                    {
                        throw new Exception("Error reading data");
                    }
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    
        public bool GetDataRange(DateTime start, DateTime end, out List<Tuple<DateTime, decimal>> data)
        {
            data = new List<Tuple<DateTime, decimal>>();

            int year = start.Year;
            int month = start.Month;
            int day = start.Day;

            foreach (Tuple<DateTime, decimal> entry in m_Data)
            {
                if(entry.Item1.Year < end.Year || (entry.Item1.Year == end.Year && entry.Item1.Month <= end.Month))
                //if(entry.Item1 <= end)
                {
                    if (entry.Item1 >= start)
                    {
                        if (ProcessDataEntry(day, month, year, entry, ref data))
                        {
                            if (++month > 12)
                            {
                                month = 1;
                                year++;
                            }
                        }
                    }
                    continue;
                }

                break;
            }

            return true;
        }

        private bool ProcessDataEntry(int day, int month, int year, Tuple<DateTime, decimal> entry, ref List<Tuple<DateTime, decimal>> data)
        {
            int days = System.DateTime.DaysInMonth(year, month);
            if(day > days)
            {
                day = days;
            }

            if(year == entry.Item1.Year && month == entry.Item1.Month && day <= entry.Item1.Day)
            {
                data.Add(new Tuple<DateTime, decimal>(entry.Item1, entry.Item2));
                return true;
            }

            return false;
        }

        private bool ReadData(StreamReader reader, int dateIndex, int closeIndex)
        {
            string lineData = reader.ReadLine();

            while(lineData != null)
            {
                string[] fields = lineData.Split(',');
                if(dateIndex < fields.Length && closeIndex < fields.Length)
                {
                    try
                    {
                        DateTime date = DateTime.Parse(fields[dateIndex]);
                        decimal cost = decimal.Parse(fields[closeIndex], System.Globalization.NumberStyles.Number);
                        m_Data.Add(new Tuple<DateTime, decimal>(date, cost));
                    }
                    catch(Exception)
                    {
                        return false;
                    }
                }

                lineData = reader.ReadLine();
            }

            return m_Data.Count > 0;
        }

        private List<Tuple<DateTime, decimal>> m_Data = new List<Tuple<DateTime, decimal>>();
    }
}
