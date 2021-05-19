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
            ToExcelFile(null, null);
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }

        public static void ToExcelFile(DataTable dataTable, string filePath, bool overwriteFile = true)
        {
            var newFile = @"newbook.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();

                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                var style = workbook.CreateCellStyle();
                style.FillForegroundColor = HSSFColor.LightYellow.Index;
                style.FillPattern = FillPattern.SolidForeground;
                style.WrapText = true;
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Bottom;
                style.BorderBottom = BorderStyle.Thin;
                style.BorderTop = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;

                sheet1.AddMergedRegion(new CellRangeAddress(0, 1, 0, 0));
                var rowIndex = 0;
                IRow row = sheet1.CreateRow(rowIndex);
                row.Height = 30 * 30;
                var cell = row.CreateCell(0);
                cell.CellStyle = style;
                cell.SetCellValue("Наименование дисциплины");
                IRow row2 = sheet1.CreateRow(1);
                var cell22 = row2.CreateCell(0);
                cell22.CellStyle = style;
                sheet1.AutoSizeColumn(0);
                rowIndex++;


                sheet1.AddMergedRegion(new CellRangeAddress(0, 1, 1, 1));
                row.Height = 30 * 30;
                cell = row.CreateCell(1);
                cell.CellStyle = style;
                cell.SetCellValue("Группа");
                sheet1.AutoSizeColumn(1);



                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 2, 12));
                row = sheet1.GetRow(0);
                cell = row.CreateCell(2);
                cell.CellStyle = style;
                cell.SetCellValue("Количество часов по видам");

                row = sheet1.CreateRow(1);
                string[] hoursType = new[]
                {
                    "Лекции", "Прак. зан.", "Лаб. зан.", "Консульт.", "Зачеты", "Экзамены", "Курс. пр.", "Дипл. пр.",
                    "ГЭК", "Рук. пркт.", "Проверка конт. раб."
                };
                for (int i = 0; i < hoursType.Length; i++)
                {
                    cell = row.CreateCell(i + 2);
                    cell.CellStyle = style;
                    cell.SetCellValue(hoursType[i]);
                }

                sheet1.AddMergedRegion(new CellRangeAddress(0, 1, 13, 13));
                row = sheet1.GetRow(0);
                cell = row.CreateCell(13);
                cell.CellStyle = style;
                cell.SetCellValue("Примечание");


                var sheet2 = workbook.CreateSheet("Sheet2");
                var style1 = workbook.CreateCellStyle();
                style1.FillForegroundColor = HSSFColor.Blue.Index2;
                style1.FillPattern = FillPattern.SolidForeground;

                var style2 = workbook.CreateCellStyle();
                style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                style2.FillPattern = FillPattern.SolidForeground;

                var cell2 = sheet2.CreateRow(0).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(0);

                cell2 = sheet2.CreateRow(1).CreateCell(0);
                cell2.CellStyle = style2;
                cell2.SetCellValue(1);



                workbook.Write(fs);
            }
        }
    }
}
