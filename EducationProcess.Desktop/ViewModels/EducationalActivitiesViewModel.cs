using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

using Range = System.Range;

namespace EducationProcess.Desktop.ViewModels
{
    public class SemestreInfo
    {
        public string DisciplineName { get; set; }
        public string GroupName { get; set; }
        public short LectionLessonHours { get; set; }
        public short PracticalLessonHours { get; set; }
        public short LaboratoryLessonHours { get; set; }
        public short ConsultationHours { get; set; }
        public short TestHours { get; set; }
        public short ExamHours { get; set; }
        public short CourseworkProjectHours { get; set; }
        public short DiplomaProjectHours { get; set; }
        public short SecHours { get; set; }
        public short PracticeHeadHours { get; set; }
        public short ControlWorkVerificationHours { get; set; }
        public string Note { get; set; }

        public SemestreInfo(Discipline discipline, string groupName)
        {
            DisciplineName = discipline.Name;
            GroupName = groupName;
            LectionLessonHours = discipline.LectionLessonHours;
            PracticalLessonHours = discipline.PracticalLessonHours;
            LaboratoryLessonHours = discipline.LaboratoryLessonHours;
            ConsultationHours = discipline.ConsultationHours;
            TestHours = discipline.TestHours;
            ExamHours = discipline.ExamHours;
            CourseworkProjectHours = discipline.CourseworkProjectHours;
            DiplomaProjectHours = discipline.DiplomaProjectHours;
            SecHours = discipline.SecHours;
            PracticeHeadHours = discipline.PracticeHeadHours;
            ControlWorkVerificationHours = discipline.ControlWorkVerificationHours;
            Note = discipline.Note;
        }
    }
    public class EducationalActivitiesViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public ObservableCollection<SemestreInfo> SemesterDisciplines { get; set; }
        public ObservableCollection<SemestreInfo> SemesterResult { get; set; }
        public ObservableCollection<string> AcademicYears { get; set; }
        public EducationalActivitiesViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            SemesterDiscipline[] disciplines = new EducationProcessContext().SemesterDisciplines
                .Where(x => x.Semester.Number == 1)
                .Include(x => x.Group)
                .Include(x => x.Discipline)
                .Include(x => x.Employee)
                .ToArray();
            List<SemestreInfo> semestreInfos = new List<SemestreInfo>();
            foreach (var discipline in disciplines)
            {
                semestreInfos.Add(new SemestreInfo(discipline.Discipline, discipline.GroupName));
            }

            Discipline resultDiscipline = new Discipline() { Name = "Итого: " };
            foreach (var semestreInfo in semestreInfos)
            {
                resultDiscipline.ConsultationHours += semestreInfo.ConsultationHours;
                resultDiscipline.ControlWorkVerificationHours += semestreInfo.ControlWorkVerificationHours;
                resultDiscipline.CourseworkProjectHours += semestreInfo.CourseworkProjectHours;
                resultDiscipline.DiplomaProjectHours += semestreInfo.DiplomaProjectHours;
                resultDiscipline.ExamHours += semestreInfo.ExamHours;
                resultDiscipline.LaboratoryLessonHours += semestreInfo.LaboratoryLessonHours;
                resultDiscipline.LectionLessonHours += semestreInfo.LectionLessonHours;
                resultDiscipline.PracticalLessonHours += semestreInfo.PracticalLessonHours;
                resultDiscipline.PracticeHeadHours += semestreInfo.PracticeHeadHours;
                resultDiscipline.SecHours += semestreInfo.SecHours;
                resultDiscipline.TestHours += semestreInfo.TestHours;
            }

            semestreInfos.Add((new SemestreInfo(resultDiscipline, null)));
            string[] academicYears = new string[] { "2020-2021", "2019-2020", "2018-2019" };

            SemesterDisciplines = new ObservableCollection<SemestreInfo>(semestreInfos);     
            AcademicYears = new ObservableCollection<string>(academicYears);
            ConvertToExcelCommand = new RelayCommand(null, _ => ConvertDataToExcel());

        }



        public RelayCommand ConvertToExcelCommand { get; set; }

        private void ConvertDataToExcel()
        {
            var data = ToDataTable(SemesterDisciplines.ToList());
            ToExcelFile(data, "test.xlsx");
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    //inserting property values to data table rows
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check data table
            return dataTable;
        }

        public static void ToExcelFile(DataTable dataTable, string filePath, bool overwriteFile = true)
        {
            if (File.Exists(filePath) && overwriteFile)
                File.Delete(filePath);

            using (var connection = new OleDbConnection())
            {
                connection.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};" +
                                              "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    var columnNames = (from DataColumn dataColumn in dataTable.Columns select dataColumn.ColumnName).ToList();
                    var tableName = !string.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TABLE [{tableName}] ({string.Join(",", columnNames.Select(c => $"[{c}] VARCHAR").ToArray())});";
                    command.ExecuteNonQuery();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var rowValues = (from DataColumn column in dataTable.Columns select (row[column] != null && row[column] != DBNull.Value) ? row[column].ToString() : string.Empty).ToList();
                        command.CommandText = $"INSERT INTO [{tableName}]({string.Join(",", columnNames.Select(c => $"[{c}]"))}) VALUES ({string.Join(",", rowValues.Select(r => $"'{r}'").ToArray())});";
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }
    }
}
