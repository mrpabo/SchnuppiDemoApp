using SchnuppiDemoApp.Managers;
using SchnuppiDemoApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using OfficeOpenXml;
using MessageBox = System.Windows.Forms.MessageBox;
using Clipboard = System.Windows.Forms.Clipboard;
using System.Linq;
using System.Windows.Data;
using System;

namespace SchnuppiDemoApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private variables

        LogDataManager logDataManager = new LogDataManager();
        private ObservableCollection<LogData> searchResults = new ObservableCollection<LogData>();
        private SearchFilter searchFilter = new SearchFilter();

        public Models.User _loggedInUser { get; set; }       
        public ObservableCollection<LogData> Items { get; set; }

        private LogData selectedRow;
        public LogData SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
            }
        }


        #endregion

        #region Constructor

        public MainWindowViewModel(Models.User loggedInUser)
        {
            LoggedInUser = loggedInUser;
        }

        #endregion

        #region Properties

        public ObservableCollection<LogData> SearchResults
        {
            get => searchResults;
            set
            {
                if (searchResults != value)
                {
                    searchResults = value;
                    OnPropertyChanged();
                }
            }
        }
        public SearchFilter SearchFilter
        {
            get => searchFilter;
            set
            {
                if (searchFilter != value)
                {
                    searchFilter = value;
                    OnPropertyChanged();
                }
            }
        }
        public Models.User LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                if(_loggedInUser != value)
                {
                    _loggedInUser = value;
                    OnPropertyChanged();
                }
            }

        }

        #endregion

        #region Public Methods

        public void Search()
        {
            var searchResults = logDataManager.GetLogDatas(SearchFilter);

            SearchResults = new ObservableCollection<LogData>(searchResults);
        }
        //public void Search()
        //{
        //    SearchResults = new ObservableCollection<LogData>(
        //        logDataManager.GetLogDatas(SearchFilter)
        //            .Where(log =>

        //                (string.IsNullOrWhiteSpace(SearchFilter.ProducerNumber) || log.ProducerNumber == SearchFilter.ProducerNumber) &&
        //                (!SearchFilter.DateFrom.HasValue || log.TimeStamp >= SearchFilter.DateFrom.Value) &&
        //                (!SearchFilter.DateTo.HasValue || log.TimeStamp <= SearchFilter.DateTo.Value)
        //            )
        //    );
        //}
        public void ExportToCSV(List<LogData> logDataList, string filePath)
        {
            //Überprüfen ob die Liste Daten enthält
            if (logDataList == null || logDataList.Count == 0)
            {
                //Handle den Fall, in dem keine Daten vorhanden sind
                return;
            }
            //Erstelle Inhalt für die CSV - Datei
            StringBuilder csvContent = new StringBuilder();

            //Füge die Überschriften hinzu
            csvContent.AppendLine("ProductionOrder\tProducerNumber\tTimeStamp");

            //Füge Datenzeilen hinzu
            foreach (var logData in logDataList)
            {
                csvContent.AppendLine($"{logData.ProductionOrderNumber}\t{logData.ProducerNumber}\t{logData.TimeStamp:dd.MM.yyyy HH:mm:ss}");
            }
            //Schreib Inhalt in die CSV-Datei
            File.WriteAllText(filePath, csvContent.ToString());
        }
        public void ExportToExcel(List<LogData> logDataList, string filePath)
        {
            //Erstelle eine neue Excel-Datei
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                //Füge ein Arbeitsblatt hinzu
                var worksheet = package.Workbook.Worksheets.Add("LogData");

                //Füge die Überschriften hinzu
                worksheet.Cells["A1"].Value = "ProductionOrder";
                worksheet.Cells["B1"].Value = "ProducerNumber";
                worksheet.Cells["C1"].Value = "DateOrder";

                //Füge Datenzeilen hinzu
                int row = 2;
                foreach (var logData in logDataList)
                {
                    worksheet.Cells[$"A{row}"].Value = logData.ProductionOrderNumber;
                    worksheet.Cells[$"B{row}"].Value = logData.ProducerNumber;
                    worksheet.Cells[$"C{row}"].Value = logData.TimeStamp;

                    row++;
                }
                //Speicher die Excel - Datei
                package.Save();
            }
        }
        public void CopyButton_Click()
        {
            if (SelectedRow != null && SelectedRow != CollectionView.NewItemPlaceholder)
            {
                Clipboard.SetText($"{SelectedRow.ID}, {SelectedRow.DeviceID}, {SelectedRow.TimeStamp}, {SelectedRow.ProductionOrderNumber}, {SelectedRow.ProducerNumber}, {SelectedRow.SerialNumber},{SelectedRow.TesterUserName}, {SelectedRow.MachineName}");
                MessageBox.Show("Ausgewählte Zeile wurde in die Zwischenablage kopiert.");
            }
        }
        public void CopyAllButton()
        {
            string clipboarddata = "";

            foreach (LogData logs in SearchResults)
            {
                clipboarddata += ($"{logs.ID} , {logs.DeviceID}, {logs.TimeStamp}, {logs.ProductionOrderNumber}, {logs.ProducerNumber}, {logs.SerialNumber},{logs.TesterUserName}, {logs.MachineName}");
            }

            Clipboard.SetText(clipboarddata);
            MessageBox.Show("All entries has been copied to the clipboard.");

        }

        #endregion



        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RowEditEnded(LogData item)
        {
            logDataManager.UpdateLogData(item.ID, item);
        }

        #endregion
    }
}