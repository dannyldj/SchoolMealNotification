using Prism.Commands;
using SchoolMealNotification.Manager;
using SchoolMealNotification.Model;
using SchoolMealNotification.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolMealNotification.ViewModel
{
    public class MealViewModel : INotifyPropertyChanged
    {
        WebManager webManager = new WebManager();
        MealModel mealModel = new MealModel();

        private int _viewMode;

        public int viewMode
        {
            get { return _viewMode; }
            set
            {
                _viewMode = value;
                OnPropertyChanged("viewMode");
            }
        }

        public static string dateFormat = "yyyyMMdd";
        public DateTime dateInput { get; set; }
        public DelegateCommand loadCommand { get; set; }
        public ICommand switchViewCommand { get; }
        public ObservableCollection<Row> meals { get; set; }
        public MealViewModel()
        {
            viewMode = 0;

            loadCommand = new DelegateCommand(onLoad, canLoad);
            switchViewCommand = new DelegateCommand(onSwitchView);
            meals = new ObservableCollection<Row>();
            dateInput = DateTime.Today;
        }

        private void onLoad()
        {
            meals.Clear();
            QParamModel[] queryParams = new QParamModel[5];
            queryParams[0] = new QParamModel { key = "KEY", value = Settings.Default.apiKey };
            queryParams[1] = new QParamModel { key = "Type", value = "json" };
            queryParams[2] = new QParamModel { key = "ATPT_OFCDC_SC_CODE", value = Settings.Default.localCode };
            queryParams[3] = new QParamModel { key = "SD_SCHUL_CODE", value = Settings.Default.schoolCode };
            queryParams[4] = new QParamModel { key = "MLSV_YMD", value = dateInput.ToString(dateFormat) };
            mealModel = webManager.webRequest<MealModel>(queryParams, "hub/mealServiceDietInfo");

            // 급식이 없는 날의 정보를 요청할 경우 예외 처리
            try
            {
                // 하나만 호출했지만 원인불명으로 mealServiceDietInfo 리스트가 나누어져 각각 head와 row를 포함함
                foreach (Row row in mealModel.mealServiceDietInfo[1].row)
                {
                    if (row != null)
                    {
                        // 정규식으로 필요한 데이터를 변환
                        row.DDISH_NM = Regex.Replace(row.DDISH_NM, @"[\d\.]", "");
                        row.DDISH_NM = row.DDISH_NM.Replace("<br/>", "\n");
                        row.ORPLC_INFO = Regex.Replace(row.ORPLC_INFO, @"[\d\.]", "");
                        row.ORPLC_INFO = row.ORPLC_INFO.Replace("<br/>", "\n");
                        row.CAL_INFO = row.CAL_INFO.Replace("<br/>", "\n");
                        row.NTR_INFO = row.NTR_INFO.Replace("<br/>", "\n");
                        meals.Add(row);
                    }
                }
            }
            catch
            {
                MessageBox.Show("급식 정보가 없습니다", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool canLoad()
        {
            bool isValid = dateInput.ToString(dateFormat).Length == 8;

            if (!isValid)
            {
                MessageBox.Show("Invalid Date", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return isValid;
        }

        private void onSwitchView()
        {
            bool temp = !Convert.ToBoolean(viewMode);
            viewMode = Convert.ToInt32(temp);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
