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
                var mainFont = CreateMainFontStyle(workbook, false);
                var horisontalTextStyle = CreateHorisontalTextStyle(workbook, mainFont);


                for (int i = 0; i < disciplines.Length; i++)
                {
                    int index = i + 8;
                    for (int j = 1; j <= 12; j++)
                    {
                        SetCellStyle(sheet, index, j, horisontalTextStyle);
                    }

                    SetCellValue(sheet, index, 1, disciplines[i].DisciplineIndex);
                    SetCellValue(sheet, index, 2, disciplines[i].Name);
                    SetCellValue(sheet, index, 3, disciplines[i].ExamHours);
                    SetCellValue(sheet, index, 4, disciplines[i].ExamHours);
                    SetCellValue(sheet, index, 5, disciplines[i].LectionLessonHours);
                    SetCellValue(sheet, index, 6, disciplines[i].PracticalLessonHours);
                    SetCellValue(sheet, index, 7, disciplines[i].CourseworkProjectHours);
                    SetCellValue(sheet, index, 8, disciplines[i].ConsultationHours);
                    SetCellValue(sheet, index, 9, disciplines[i].ControlWorkVerificationHours);
                    SetCellValue(sheet, index, 10, disciplines[i].ControlWorkVerificationHours);
                    SetCellValue(sheet, index, 11, disciplines[i].ConsultationHours);
                    SetCellValue(sheet, index, 12, disciplines[i].DiplomaProjectHours);
                }

                workbook.Write(fs);
            }
        }

        private static void SetRowHeight(ISheet worksheet, int rowPosition, short height)
        {
            IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
            dataRow.Height = height;
        }

        private static void SetCellStyle(ISheet worksheet, int rowPosition, int columnPosition, ICellStyle style)
        {
            IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
            ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
            cell.CellStyle = style;
        }
        private static void SetCellValue(ISheet worksheet, int rowPosition, int columnPosition, string value)
        {
            IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
            ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
            cell.SetCellValue(value);
        }

        private static void SetCellValue(ISheet worksheet, int rowPosition, int columnPosition, double value)
        {
            IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
            ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
            cell.SetCellValue(value);
        }

        private static IFont CreateMainFontStyle(IWorkbook workbook, bool isBold)
        {
            var font = workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.FontName = "Times New Roman";
            font.IsBold = isBold;

            return font;
        }

        private static void AddMergedStyledRegion(ISheet sheet, CellRangeAddress cellRange, ICellStyle style)
        {
            for (int row = cellRange.FirstRow; row <= cellRange.LastRow; row++)
            {
                for (int column = cellRange.FirstColumn; column <= cellRange.LastColumn; column++)
                {
                    SetCellStyle(sheet, row, column, style);
                }
            }
            sheet.AddMergedRegion(cellRange);
        }

        private static ICellStyle CreateVerticalTextStyle(IWorkbook workbook, IFont? font = null)
        {
            var verticalTextStyle = workbook.CreateCellStyle();
            verticalTextStyle.WrapText = true;
            verticalTextStyle.Rotation = 90;
            verticalTextStyle.Alignment = HorizontalAlignment.Left;
            verticalTextStyle.VerticalAlignment = VerticalAlignment.Bottom;
            verticalTextStyle.BorderBottom = BorderStyle.Thin;
            verticalTextStyle.BorderTop = BorderStyle.Thin;
            verticalTextStyle.BorderLeft = BorderStyle.Thin;
            verticalTextStyle.BorderRight = BorderStyle.Thin;
            verticalTextStyle.BorderDiagonal = BorderDiagonal.Both;
            if (font != null)
                verticalTextStyle.SetFont(font);

            return verticalTextStyle;
        }

        private static ICellStyle CreateHorisontalTextStyle(IWorkbook workbook, IFont? font = null)
        {
            var horizontalTextStyle = workbook.CreateCellStyle();
            horizontalTextStyle.WrapText = true;
            horizontalTextStyle.Alignment = HorizontalAlignment.Left;
            horizontalTextStyle.VerticalAlignment = VerticalAlignment.Center;
            horizontalTextStyle.BorderBottom = BorderStyle.Thin;
            horizontalTextStyle.BorderTop = BorderStyle.Thin;
            horizontalTextStyle.BorderLeft = BorderStyle.Thin;
            horizontalTextStyle.BorderRight = BorderStyle.Thin;
            if (font != null)
                horizontalTextStyle.SetFont(font);

            return horizontalTextStyle;
        }
        private static void GetHeadersOfEducationPlan(IWorkbook workbook, string sheetName)
        {
            IFont mainBoldFont = CreateMainFontStyle(workbook, true);
            IFont mainFont = CreateMainFontStyle(workbook, false);

            var verticalBoldTextStyle = CreateVerticalTextStyle(workbook, mainBoldFont);
            var horizontalBoldTextStyle = CreateHorisontalTextStyle(workbook, mainBoldFont);

            var verticalTextStyle = CreateVerticalTextStyle(workbook, mainFont);
            var horizontalTextStyle = CreateHorisontalTextStyle(workbook, mainFont);

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
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow, headerColumn, "Индекс");

            headerColumn++;
            // Плитка наименование дисциплин
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), horizontalBoldTextStyle);
            SetCellValue(sheet, headerRow, headerColumn, "Наименование циклов, дисциплин, профессиональных модулей, МДК, практик");

            headerColumn++;
            // Колонка форма промежуточной аттестации
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow, headerColumn, "Форма промежуточной аттестации (зачеты, дифференцированные зачеты)");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow, headerColumn, "Объем образовательной нагрузки");

            headerColumn++;
            // Колонка форма самостоятельная учебная нагрузка
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 1, headerColumn, "Самостоятельная учебная нагрузка");

            // Плитка учебная нагрузка обучающихся
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow, headerColumn, headerColumn + 7), horizontalBoldTextStyle);
            SetCellValue(sheet, headerRow, headerColumn, "Учебная нагрузка обучающихся (час.)");


            headerColumn++;
            // Колонка Всего учебных занятий
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 3, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 3, headerColumn, "Всего учебных занятий");

            // Плитка взаимодействии с преподавателем
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 6), horizontalBoldTextStyle);
            SetCellValue(sheet, headerRow + 1, headerColumn, "Во взаимодействии с преподавателем");

            // Плитка нагрузка на дисциплины и МДК
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 2, headerColumn, headerColumn + 3), horizontalTextStyle);
            SetCellValue(sheet, headerRow + 2, headerColumn, "Нагрузка на дисциплины и МДК");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 4, headerColumn, "Теоретическое обучение");
            // Плитка нагрузка на дисциплины и МДК
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 3, headerRow + 3, headerColumn, headerColumn + 2), horizontalBoldTextStyle);
            SetCellValue(sheet, headerRow + 3, headerColumn, "в т.ч. по учебным дисциплинам и МДК");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 4, headerColumn, "Лаб. и практ. занятия");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 4, headerColumn, "Курсовых работ (проектов)");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 2, headerColumn, "По практике производственной и учебной");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 2, headerColumn, "Консультации");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 2, headerColumn, "Промежуточная аттестация");

            headerColumn++;
            // Плитка Распределение учебной нагрузки по курсам и семестрам
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow, headerColumn, headerColumn + 13), horizontalBoldTextStyle);
            SetCellValue(sheet, headerRow, headerColumn, "Распределение учебной нагрузки по курсам и семестрам (час. в семестр)");

            // Плитка I Курс
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 1), horizontalTextStyle);
            SetCellValue(sheet, headerRow + 1, headerColumn, "I курс");

            // Колонка форма объема образовательной нагрузки
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 4, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 2, headerColumn, "1 сем.**нед.17");

            headerColumn++;
            // Колонка форма объема образовательной нагрузки
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 4, headerColumn, headerColumn), verticalBoldTextStyle);
            SetCellValue(sheet, headerRow + 2, headerColumn, "2 сем.**22нед.");

            headerColumn++;

            for (int i = 2; i <= 4; i++)
            {
                AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 3), horizontalTextStyle);
                SetCellValue(sheet, headerRow + 1, headerColumn, $"{i} курс");

                for (int j = 1; 0 <= j; j--)
                {
                    int semNumber = i * 2 - j;
                    AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 3, headerColumn, headerColumn + 1), horizontalTextStyle);
                    SetCellValue(sheet, headerRow + 2, headerColumn, $"{semNumber} сем.");

                    // Колонка форма объема образовательной нагрузки
                    SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
                    SetCellValue(sheet, headerRow + 4, headerColumn, "Во взаимодействии ");
                    sheet.SetColumnWidth(headerColumn, 1500);

                    // Колонка форма объема образовательной нагрузки
                    SetCellStyle(sheet, headerRow + 4, headerColumn + 1, verticalBoldTextStyle);
                    SetCellValue(sheet, headerRow + 4, headerColumn + 1, "с/р");
                    sheet.SetColumnWidth(headerColumn + 1, 1500);
                    headerColumn += 2;
                }
            }


            // Определение ширины для колонок
            sheet.SetColumnWidth(1, 3500);
            sheet.SetColumnWidth(2, 11000);
            sheet.SetColumnWidth(3, 3200);
            // Определение высоты для строки
            SetRowHeight(sheet, 0, 400);
            SetRowHeight(sheet, 1, 400);
            SetRowHeight(sheet, 2, 700);
            SetRowHeight(sheet, 5, 600);

            for (int i = 1; i <= headerColumn - 1; i++)
            {
                SetCellStyle(sheet, headerRow + 5, i, horizontalBoldTextStyle);
                SetCellValue(sheet, headerRow + 5, i, i);
            }
        }
    }
}
