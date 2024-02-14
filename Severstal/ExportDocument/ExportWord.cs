using Microsoft.Office.Interop.Word;
using Severstal.Core;
using Severstal.Core.Contracts;
using Severstal.Data;
using Severstal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Severstal.ExportDocument
{
    public class ExportWord : IExport
    {

        public void ExportFileWord (List<LoadReport> dataRusultLoad)
        {   
            if (dataRusultLoad == null)
            {
                MessageBox.Show("Нет данных для обработки!");
                return;
            }

            var wordApplication = new Microsoft.Office.Interop.Word.Application();
            wordApplication.Visible = true;
            var wordDocument = wordApplication.Documents.Add();

            try
            {
                InsertDataWord(wordDocument, dataRusultLoad);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при экспорте: {ex.Message}");
            }
            finally
            {
                Marshal.ReleaseComObject(wordApplication);
                Marshal.ReleaseComObject(wordApplication);
            }
        }


        public void InsertDataWord(Document wordDocument, List<LoadReport> dataRusultLoad)
        {
            // Получаем уникальные отделы и сортируем
            var uniqueDepartments = dataRusultLoad
                .GroupBy(x => x.DepartmentName)
                .OrderByDescending(group => group.Sum(x => x.TaskNumber))
                .Select(group => group.Key)
                .ToList();

            // Создаем таблицу с количеством строк, равным числу уникальных отделов с сотрудниками плюс одна строка для заголовка
            var table = wordDocument.Tables.Add(wordDocument.Range(), dataRusultLoad.Count + uniqueDepartments.Count + 1, 2);

            // Добавляем заголовки в таблицу
            table.Rows[1].Cells[1].Range.Text = "Отдел";
            table.Rows[1].Cells[2].Range.Text = "Количество задач";

            int currentRow = 2;

            // Заполняем таблицу данными
            foreach (var departmentName in uniqueDepartments)
            {
                // Сортируем
                var departmentTasks = dataRusultLoad
                    .Where(x => x.DepartmentName == departmentName)
                    .OrderByDescending(x => x.TaskNumber)
                    .ToList();

                // Фон и жирность текста для заголовков отделов
                table.Rows[currentRow].Cells[1].Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
                table.Rows[currentRow].Cells[1].Range.Font.Bold = 1;

                table.Rows[currentRow].Cells[2].Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
                table.Rows[currentRow].Cells[2].Range.Font.Bold = 1;
                
                // Заполняем данными
                table.Rows[currentRow].Cells[1].Range.Text = $"{departmentName}";
                table.Rows[currentRow].Cells[2].Range.Text = $"{departmentTasks.Sum(item => item.TaskNumber)}";

                currentRow++;
                

                foreach (var employee in departmentTasks)
                {
                    // Проверки на пустоту с точкой
                    string nameSuffix = string.IsNullOrEmpty(employee.Name) ? string.Empty : ".";
                    string fathersNameSuffix = string.IsNullOrEmpty(employee.FathersName) ? string.Empty : ".";

                    table.Rows[currentRow].Cells[1].Range.Text = $"{employee.Surname.Replace(" ", "")} {employee.Name?.FirstOrDefault()}{nameSuffix}{employee.FathersName?.FirstOrDefault()}{fathersNameSuffix}";
                    table.Rows[currentRow].Cells[2].Range.Text = $"{employee.TaskNumber}".ToString();
                    currentRow++;
                }
            }

            // Применяем стили
            ApplyTableStyles(table);
        }

        private void ApplyTableStyles(Table table)
        {
            // Устанавливаем все границы и толщину текста
            table.Rows[1].Range.Font.Bold = 1;
            table.Rows[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Borders.Enable = 1;

            // Фон для заголовка
            foreach (Cell cell in table.Rows[1].Cells)
            {
                cell.Range.Shading.BackgroundPatternColor = WdColor.wdColorGray40;
                cell.Range.Font.Color = WdColor.wdColorWhite;
            }

            // Убираем интервалы
            foreach (Row row in table.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    cell.Range.ParagraphFormat.SpaceAfter = 0f;
                }
            }

            // Центрируем значения в колонке "Количество задач"
            foreach (Row row in table.Rows)
            {
                row.Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            }
        }
    }
}