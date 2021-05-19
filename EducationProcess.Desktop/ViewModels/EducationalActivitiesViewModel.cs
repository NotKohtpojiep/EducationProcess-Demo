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
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

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
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();

            ToExcelFile(disciplines, null);
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }

        public void ToExcelFile(Discipline[] disciplines, string filePath, bool overwriteFile = true)
        {
            var newFile = @"newbook.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();

                GetHeadersOfEducationPlan(workbook, "Учебный план");
                ISheet sheet = workbook.GetSheet("Учебный план");
                var horisontalTextStyle = GetHorisontalTextStyle(workbook);

                for(int i = 0; i < disciplines.Length; i++)
                {
                    int index = i + 8;
                    SetCellValue(sheet, index, 1, disciplines[i].DisciplineIndex, horisontalTextStyle);
                    SetCellValue(sheet, index, 2, disciplines[i].Name, horisontalTextStyle);
                    SetCellValue(sheet, index, 3, disciplines[i].ExamHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 4, disciplines[i].ExamHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 5, disciplines[i].LectionLessonHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 6, disciplines[i].PracticalLessonHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 7, disciplines[i].CourseworkProjectHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 8, disciplines[i].ConsultationHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 9, disciplines[i].ControlWorkVerificationHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 10, disciplines[i].ControlWorkVerificationHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 11, disciplines[i].ConsultationHours.ToString(), horisontalTextStyle);
                    SetCellValue(sheet, index, 12, disciplines[i].DiplomaProjectHours.ToString(), horisontalTextStyle);
                }

                workbook.Write(fs);
            }
        }

        private void SetCellValue(ISheet worksheet, int rowPosition, int columnPosition, String value, ICellStyle style = null)
        {
            IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
            ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
            cell.CellStyle = style;
            cell.SetCellValue(value);
        }

        private ICellStyle GetVerticalTextStyle(IWorkbook workbook)
        {
            var verticalTextStyle = workbook.CreateCellStyle();
            verticalTextStyle.WrapText = true;
            verticalTextStyle.Rotation = 90;
            verticalTextStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            verticalTextStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Bottom;
            verticalTextStyle.BorderBottom = BorderStyle.Thin;
            verticalTextStyle.BorderTop = BorderStyle.Thin;
            verticalTextStyle.BorderLeft = BorderStyle.Thin;
            verticalTextStyle.BorderRight = BorderStyle.Thin;
            verticalTextStyle.BorderDiagonal = BorderDiagonal.Both;

            return verticalTextStyle;
        }

        private ICellStyle GetHorisontalTextStyle(IWorkbook workbook)
        {
            var horizontalTextStyle = workbook.CreateCellStyle();
            horizontalTextStyle.WrapText = true;
            horizontalTextStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            horizontalTextStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Bottom;
            horizontalTextStyle.BorderBottom = BorderStyle.Thin;
            horizontalTextStyle.BorderTop = BorderStyle.Thin;
            horizontalTextStyle.BorderLeft = BorderStyle.Thin;
            horizontalTextStyle.BorderRight = BorderStyle.Thin;

            return horizontalTextStyle;
        }
        private void GetHeadersOfEducationPlan(IWorkbook workbook, string sheetName)
        {
            var verticalTextStyle = GetVerticalTextStyle(workbook);
            var horizontalTextStyle = GetHorisontalTextStyle(workbook);

            // Полотно
            ISheet sheet = workbook.CreateSheet(sheetName);

            // Первая строка "Учебный план"
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 2, 26));
            SetCellValue(sheet, 0, 2, "Учебный план");

            // Вторая строка "для специальности ... гг."
            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 2, 26));
            SetCellValue(sheet, 1, 2, "для специальности 09.02.07 Информационные системы и программирование. 2019-2020 г");

            const int headerRow = 2;
            int headerColumn = 1;

            const int stepDown = 4;


            // Колонка индекс
            sheet.AddMergedRegion(new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow, headerColumn, "Индекс", verticalTextStyle);

            headerColumn++;
            // Колонка наименование дисциплин
            sheet.AddMergedRegion(new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow, headerColumn, "Наименование циклов, дисциплин, профессиональных модулей, МДК, практик", horizontalTextStyle);

            headerColumn++;
            // Колонка форма промежуточной аттестации
            sheet.AddMergedRegion(new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow, headerColumn, "Форма промежуточной аттестации (зачеты, дифференцированные зачеты)", verticalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            sheet.AddMergedRegion(new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow, headerColumn, "Объем образовательной нагрузки", verticalTextStyle);

            headerColumn++;
            // Колонка форма самостоятельная учебная нагрузка
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 1, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow + 1, headerColumn, "Самостоятельная учебная нагрузка", verticalTextStyle);

            // Плитка учебная нагрузка обучающихся
            sheet.AddMergedRegion(new CellRangeAddress(headerRow, headerRow, headerColumn, headerColumn + 7));
            SetCellValue(sheet, headerRow, headerColumn, "Учебная нагрузка обучающихся (час.)", horizontalTextStyle);


            headerColumn++;
            // Колонка Всего учебных занятий
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 3, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow + 3, headerColumn, "Всего учебных занятий", verticalTextStyle);

            // Плитка взаимодействии с преподавателем
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 6));
            SetCellValue(sheet, headerRow + 1, headerColumn, "Во взаимодействии с преподавателем", horizontalTextStyle);

            // Плитка нагрузка на дисциплины и МДК
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 2, headerRow + 2, headerColumn, headerColumn + 3));
            SetCellValue(sheet, headerRow + 2, headerColumn, "Нагрузка на дисциплины и МДК", horizontalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки

            SetCellValue(sheet, headerRow + 4, headerColumn, "Теоретическое обучение", verticalTextStyle);
            // Плитка нагрузка на дисциплины и МДК
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 3, headerRow + 3, headerColumn, headerColumn + 2));
            SetCellValue(sheet, headerRow + 3, headerColumn, "в т.ч. по учебным дисциплинам и МДК", horizontalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            SetCellValue(sheet, headerRow + 4, headerColumn, "Лаб. и практ. занятия", verticalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            SetCellValue(sheet, headerRow + 4, headerColumn, "Курсовых работ (проектов)", verticalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow + 2, headerColumn, "По практике производственной и учебной", verticalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow + 2, headerColumn, "Консультации", verticalTextStyle);

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            sheet.AddMergedRegion(new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn));
            SetCellValue(sheet, headerRow + 2, headerColumn, "Промежуточная аттестация", verticalTextStyle);


            for (int i = 1; i <= headerColumn; i++)
            {
                SetCellValue(sheet, headerRow + 5, i, i.ToString(), horizontalTextStyle);
            }
        }
    }
}
