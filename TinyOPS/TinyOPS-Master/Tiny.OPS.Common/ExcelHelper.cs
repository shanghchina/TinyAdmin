using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Tiny.OPS.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public static byte[] SaveExcel<T>(List<T> List)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("数据导出");

                worksheet.Cells.Style.ShrinkToFit = true;

                var properties = typeof(T).GetProperties();

                var colIndex = 1;
                for (int i = 0; i < properties.Count(); i++)
                {
                    if (properties[i].CustomAttributes.Any(p => p.AttributeType == typeof(NoExportAttribute)))
                        continue;
                    var _col = properties[i].GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();
                    worksheet.Cells[1, colIndex].Value = _col?.Name ?? properties[i].Name;

                    #region  样式
                    worksheet.Cells[1, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                    worksheet.Cells[1, colIndex].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                    worksheet.Cells[1, colIndex].Style.Font.Bold = true;//字体为粗体
                    worksheet.Cells[1, colIndex].Style.Font.Name = "微软雅黑";//字体
                    worksheet.Cells[1, colIndex].Style.Font.Size = 12;//字体大小
                    worksheet.Cells[1, colIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, colIndex].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 255));
                    worksheet.Row(1).Height = 25;
                    worksheet.Column(colIndex).Width = 20;
                    #endregion

                    colIndex++;
                }

                int rowIndex = 2;
                foreach (T entity in List)
                {
                    colIndex = 1;
                    for (int i = 0; i < properties.Count(); i++)
                    {
                        if (properties[i].CustomAttributes.Any(p => p.AttributeType == typeof(NoExportAttribute)))
                            continue;
                        if (properties[i].PropertyType == typeof(Decimal) || properties[i].PropertyType == typeof(Decimal?))
                            worksheet.Cells[rowIndex, colIndex].Value = Convert.ToDouble(properties[i].GetValue(entity, null));
                        else if (properties[i].PropertyType == typeof(int))
                            worksheet.Cells[rowIndex, colIndex].Value = Convert.ToInt32(properties[i].GetValue(entity, null));
                        else if (properties[i].PropertyType == typeof(DateTime) || properties[i].PropertyType == typeof(DateTime?))
                        {
                            string _format = "yyyy-MM-dd hh:mm:ss";
                            if (properties[i].CustomAttributes.Any(p => p.AttributeType == typeof(DateFormatAttribute)))
                            {
                                var _attr = properties[i].GetCustomAttributes(typeof(DateFormatAttribute), false).Cast<DateFormatAttribute>().FirstOrDefault();
                                _format = _attr.Format;
                            }

                            worksheet.Cells[rowIndex, colIndex].Style.Numberformat.Format = _format;
                            var _date = properties[i].GetValue(entity, null);
                            if (_date != null)
                                worksheet.Cells[rowIndex, colIndex].Value = Convert.ToDateTime(properties[i].GetValue(entity, null));
                            else
                                worksheet.Cells[rowIndex, colIndex].Value = properties[i].GetValue(entity, null)?.ToString() ?? string.Empty;
                        }
                        else
                            worksheet.Cells[rowIndex, colIndex].Value = properties[i].GetValue(entity, null)?.ToString() ?? string.Empty;

                        #region 样式
                        worksheet.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                        worksheet.Cells[rowIndex, colIndex].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                        worksheet.Cells[rowIndex, colIndex].Style.Font.Name = "微软雅黑";//字体
                        worksheet.Cells[rowIndex, colIndex].Style.Font.Size = 12;//字体大小
                        worksheet.Row(rowIndex).Height = 25;
                        #endregion 

                        colIndex++;
                    }
                    rowIndex++;
                }

                return package.GetAsByteArray();
            }
        }

        #region 导入数据
        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoExportAttribute : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateFormatAttribute : System.Attribute
    {
        public string Format { get; set; }
    }
    public class ExcelColumnName
    {
       

    }

}
