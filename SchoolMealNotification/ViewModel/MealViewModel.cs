using Prism.Commands;
using SchoolMealNotification.Manager;
using SchoolMealNotification.Model;
using SchoolMealNotification.Model.Meal;
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

        private int _viewMode;

        public int ViewMode
        {
            get { return _viewMode; }
            set
            {
                _viewMode = value;
                onPropertyChanged("viewMode");
            }
        }

        private string _viewStatus;

        public string ViewStatus
        {
            get { return _viewStatus; }
            set 
            {
                _viewStatus = value;
                onPropertyChanged("viewStatus");
            }
        }

        private MealModel _mealModel;

        public MealModel Meals
        {
            get { return _mealModel; }
            set { _mealModel = value; }
        }

        public static string dateFormat = "yyyyMMdd";
        public DateTime DateInput { get; set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand SwitchViewCommand { get; private set; }
        public ObservableCollection<Row> ListMeal { get; set; }
        public MealViewModel()
        {
            ViewMode = 0;
            ViewStatus = "간략히";
            Meals = new MealModel();

            LoadCommand = new DelegateCommand(OnLoad);
            SwitchViewCommand = new DelegateCommand(OnSwitchView);
            ListMeal = new ObservableCollection<Row>();
            DateInput = DateTime.Today;
        }

        private void OnLoad()
        {
            ListMeal.Clear();
            QParamModel[] queryParams = new QParamModel[5];
            queryParams[0] = new QParamModel { key = "KEY", value = Settings.Default.apiKey };
            queryParams[1] = new QParamModel { key = "Type", value = "json" };
            queryParams[2] = new QParamModel { key = "ATPT_OFCDC_SC_CODE", value = Settings.Default.localCode };
            queryParams[3] = new QParamModel { key = "SD_SCHUL_CODE", value = Settings.Default.schoolCode };
            queryParams[4] = new QParamModel { key = "MLSV_YMD", value = DateInput.ToString(dateFormat) };
            Meals = webManager.webRequest<MealModel>(queryParams, "mealServiceDietInfo");

            // 급식이 없는 날의 정보를 요청할 경우 예외 처리
            try
            {
                // 하나만 호출했지만 원인불명으로 mealServiceDietInfo 리스트가 나누어져 각각 head와 row를 포함함
                foreach (Row row in Meals.mealServiceDietInfo[1].row)
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
                        ListMeal.Add(row);
                    }
                }
            }
            catch
            {
                MessageBox.Show("급식 정보가 없습니다", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSwitchView()
        {
            bool temp = !Convert.ToBoolean(ViewMode);
            ViewMode = Convert.ToInt32(temp);

            if(ViewMode == 0)
            {
                ViewStatus = "간략히";
            }
            else
            {
                ViewStatus = "자세히";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void onPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
