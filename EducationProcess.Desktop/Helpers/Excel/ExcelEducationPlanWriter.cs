using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.DataAccess.Entities;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace EducationProcess.Desktop.Helpers.Excel
{
    public static class ExcelEducationPlanWriter
    {
        public static void ToExcelFile(Discipline[] disciplines, string filePath, bool overwriteFile = true)
        {
            var newFile = @"newbook.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();

                GetHeadersOfEducationPlan(workbook, "Учебный план");
                ISheet sheet = workbook.GetSheet("Учебный план");
                var horisontalTextStyle = GetHorisontalTextStyle(workbook);

                for (int i = 0; i < disciplines.Length; i++)
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

        private static void SetCellValue(ISheet worksheet, int rowPosition, int columnPosition, String value, ICellStyle style = null)
        {
            IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
            ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
            cell.CellStyle = style;
            cell.SetCellValue(value);
        }

        private static ICellStyle GetVerticalTextStyle(IWorkbook workbook)
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

        private static ICellStyle GetHorisontalTextStyle(IWorkbook workbook)
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
        private static void GetHeadersOfEducationPlan(IWorkbook workbook, string sheetName)
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
