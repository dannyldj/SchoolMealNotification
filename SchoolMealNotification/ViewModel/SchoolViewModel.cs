using Prism.Commands;
using SchoolMealNotification.Manager;
using SchoolMealNotification.Model;
using SchoolMealNotification.Model.School;
using SchoolMealNotification.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolMealNotification.ViewModel
{
    public class SchoolViewModel : INotifyPropertyChanged
    {
        WebManager webManager = new WebManager();

        private string _searchingSchool;

        public string SearchingSchool
        {
            get { return _searchingSchool; }
            set { _searchingSchool = value; }
        }

        private SchoolModel _schoolModel;

        public SchoolModel Schools
        {
            get { return _schoolModel; }
            set { _schoolModel = value; }
        }

        private Row _selectedSchool;

        public Row SelectedSchool
        {
            get { return _selectedSchool; }
            set { _selectedSchool = value; }
        }

        //public bool IsSearchEnabled
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(SearchingSchool);
        //    }
        //}

        public DelegateCommand SearchCommand { get; private set; }
        public ICommand SelectCommand { get; private set; }
        public ObservableCollection<Row> ListSchool { get; set; }
        public SchoolViewModel()
        {
            Schools = new SchoolModel();

            SearchCommand = new DelegateCommand(OnSearch); //ObservesCanExecute(() => IsSearchEnabled)
            SelectCommand = new DelegateCommand(OnSelect);
            ListSchool = new ObservableCollection<Row>();
        }

        private void OnSearch()
        {
            ListSchool.Clear();
            QParamModel[] queryParams = new QParamModel[3];
            queryParams[0] = new QParamModel { key = "KEY", value = Settings.Default.apiKey };
            queryParams[1] = new QParamModel { key = "Type", value = "json" };
            queryParams[2] = new QParamModel { key = "SCHUL_NM", value = SearchingSchool };
            Schools = webManager.webRequest<SchoolModel>(queryParams, "schoolInfo");

            try
            {
                // 하나만 호출했지만 원인불명으로 schoolInfo 리스트가 나누어져 각각 head와 row를 포함함
                foreach (Row row in Schools.schoolInfo[1].row)
                {
                    if (row != null)
                    {
                        ListSchool.Add(row);
                    }
                }
            }
            catch
            {
                MessageBox.Show("학교 정보가 없습니다", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnSelect()
        {
            Settings.Default.localCode = SelectedSchool.ATPT_OFCDC_SC_CODE;
            Settings.Default.schoolCode = SelectedSchool.SD_SCHUL_CODE;
            Settings.Default.schoolName = SelectedSchool.SCHUL_NM;
            Settings.Default.Save();
        }

        //private bool CanSearch()
        //{
        //    return !string.IsNullOrEmpty(SearchingSchool);
        //}
        //private bool CanSelect()
        //{
        //    return SelectedSchool != null;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
