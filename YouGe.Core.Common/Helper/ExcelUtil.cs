﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using YouGe.Core.Commons.Helper;

namespace YouGe.Core.Common.Helper
{
    public class ExcelUtil
    {
        /// <summary>
        /// 读取Excel多Sheet数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">Sheet名</param>
        /// <returns></returns>
        public static DataSet ReadExcelToDataSet(string filePath, string sheetName = null)
        {
            if (!File.Exists(filePath))
            {
                Log4NetHelper.Error("未找到文件 " + filePath);
                
                return null;
            }
            //获取文件信息
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = WorkbookFactory.Create(fs);
            //获取sheet信息
            ISheet sheet = null;
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    Log4NetHelper.Error(filePath + "未找到sheet " + sheetName);
                    
                    return null;
                }
                DataTable dt = ReadExcelFunc(workbook, sheet);
                ds.Tables.Add(dt);
            }
            else
            {
                //遍历获取所有数据
                int sheetCount = workbook.NumberOfSheets;
                for (int i = 0; i < sheetCount; i++)
                {
                    sheet = workbook.GetSheetAt(i);
                    if (sheet != null)
                    {
                        DataTable dt = ReadExcelFunc(workbook, sheet);
                        ds.Tables.Add(dt);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 读取Excel信息
        /// </summary>
        /// <param name="workbook">工作区</param>
        /// <param name="sheet">sheet</param>
        /// <returns></returns>
        private static DataTable ReadExcelFunc(IWorkbook workbook, ISheet sheet)
        {
            DataTable dt = new DataTable();
            //获取列信息
            IRow cells = sheet.GetRow(sheet.FirstRowNum);
            int cellsCount = cells.PhysicalNumberOfCells;
            int emptyCount = 0;
            int cellIndex = sheet.FirstRowNum;
            List<string> listColumns = new List<string>();
            bool isFindColumn = false;
            while (!isFindColumn)
            {
                emptyCount = 0;
                listColumns.Clear();
                for (int i = 0; i < cellsCount; i++)
                {
                    if (string.IsNullOrEmpty(cells.GetCell(i).StringCellValue))
                    {
                        emptyCount++;
                    }
                    listColumns.Add(cells.GetCell(i).StringCellValue);
                }
                //这里根据逻辑需要，空列超过多少判断
                if (emptyCount == 0)
                {
                    isFindColumn = true;
                }
                cellIndex++;
                cells = sheet.GetRow(cellIndex);
            }

            foreach (string columnName in listColumns)
            {
                if (dt.Columns.Contains(columnName))
                {
                    //如果允许有重复列名，自己做处理
                    continue;
                }
                dt.Columns.Add(columnName, typeof(string));
            }
            //开始获取数据
            int rowsCount = sheet.PhysicalNumberOfRows;
            cellIndex += 1;
            DataRow dr = null;
            for (int i = cellIndex; i < rowsCount; i++)
            {
                cells = sheet.GetRow(i);
                dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //这里可以判断数据类型
                    switch (cells.GetCell(j).CellType)
                    {
                        case CellType.String:
                            dr[j] = cells.GetCell(j).StringCellValue;
                            break;
                        case CellType.Numeric:
                            dr[j] = cells.GetCell(j).NumericCellValue.ToString();
                            break;
                        case CellType.Unknown:
                            dr[j] = cells.GetCell(j).StringCellValue;
                            break;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="entities">数据实体</param>
        /// <param name="dicColumns">列对应关系,如Name->姓名</param>
        /// <param name="title">标题</param>
        /// <returns></returns>

        public static MemoryStream ExportExcelToStram<T>(List<T> entities, string sheetName,Dictionary<string, string> dicColumns=null, string title = null)
        {
            if (entities.Count <= 0)
            {
                return null;
            }
            PropertyInfo[] properties = entities[0].GetType().GetProperties();
            if (dicColumns == null) //如果没有，就用实体模型的字段名
            {
                dicColumns = new Dictionary<string, string>();
                foreach (PropertyInfo p in properties)
                {
                    dicColumns.Add(p.Name, p.Name);
                }
            }
            //HSSFWorkbook => xls
            //XSSFWorkbook => xlsx
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);//名称自定义
            IRow cellsColumn = null;
            IRow cellsData = null;
            //获取实体属性名
            
            int cellsIndex = 0;
            //标题
            if (!string.IsNullOrEmpty(title))
            {
                ICellStyle style = workbook.CreateCellStyle();
                //边框  
                style.BorderBottom = BorderStyle.Dotted;
                style.BorderLeft = BorderStyle.Hair;
                style.BorderRight = BorderStyle.Hair;
                style.BorderTop = BorderStyle.Dotted;
                //水平对齐  
                style.Alignment = HorizontalAlignment.Left;

                //垂直对齐  
                style.VerticalAlignment = VerticalAlignment.Center;

                //设置字体
                IFont font = workbook.CreateFont();
                font.FontHeightInPoints = 10;
                font.FontName = "微软雅黑";
                style.SetFont(font);

                IRow cellsTitle = sheet.CreateRow(0);
                cellsTitle.CreateCell(0).SetCellValue(title);
                cellsTitle.RowStyle = style;
                //合并单元格
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, dicColumns.Count - 1));
                cellsIndex = 2;
            }
            //列名
            cellsColumn = sheet.CreateRow(cellsIndex);
            int index = 0;
            Dictionary<string, int> columns = new Dictionary<string, int>();
            foreach (var item in dicColumns)
            {
                cellsColumn.CreateCell(index).SetCellValue(item.Value);
                columns.Add(item.Value, index);
                index++;
            }
            cellsIndex += 1;
            //数据
            foreach (var item in entities)
            {
                cellsData = sheet.CreateRow(cellsIndex);
                for (int i = 0; i < properties.Length; i++)
                {
                    if (!dicColumns.ContainsKey(properties[i].Name)) continue;
                    //这里可以也根据数据类型做不同的赋值，也可以根据不同的格式参考上面的ICellStyle设置不同的样式
                    object[] entityValues = new object[properties.Length];
                    entityValues[i] = properties[i].GetValue(item);
                    //获取对应列下标
                    index = columns[dicColumns[properties[i].Name]];
                    cellsData.CreateCell(index).SetCellValue(entityValues[i].ToString());
                }
                cellsIndex++;
            }                    
             MemoryStream ds = new MemoryStream();
             using (MemoryStream ms = new MemoryStream())
             {
                 workbook.Write(ms);
                ms.CopyTo(ds);
                 ms.Close();
            }
            return ds;
        }
        /// <summary>
        /// 导出成excel byte字节流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="sheetName"></param>
        /// <param name="dicColumns"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static byte[] ExportExcelTobyte<T>(List<T> entities, string sheetName, Dictionary<string, string> dicColumns, string title = null)
        {              
            MemoryStream ms = ExportExcelToStram(entities, sheetName, dicColumns, title);
            byte[] buffer = ms.GetBuffer();
            ms.Close();
            return buffer;
        }
        /// <summary>
        /// 导出excel 到指定的文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="sheetName"></param>
        /// <param name="dicColumns"></param>
        /// <param name="fileName">指定的文件</param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool ExportExcelToFile<T>(List<T> entities, string sheetName, Dictionary<string, string> dicColumns,string filePath,string fileName, string title = null)
        {
           
            MemoryStream ms = ExportExcelToStram(entities, sheetName, dicColumns, title);
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, System.IO.FileAccess.Write))
            {
                bool isOk = false;
                if (ms.Length == 0) return isOk;
                byte[] bytes = new byte[ms.Length];               
                int  counts = ms.Read(bytes, 0, (int)ms.Length);                
                file.Write(bytes, 0, bytes.Length);               
                file.Close();
                ms.Close();
                if(counts>0)
                {
                    return true;
                }
                return false;
            }                         
           
        }

        
    }
}

