using System;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Collections.Generic;
using NPOI.SS.Util;

namespace YouGe.Core.Common.Helper
{
    /// <SUMMARY>
    /// ExcelEdit 的摘要说明
    /// </SUMMARY>
    public class NPOIExcelEdit
    {
        public string mFilename;
        public IWorkbook wb;
        public NPOI.SS.UserModel.ISheet ws;
        public NPOIExcelEdit()
        {
            mFilename = "";
            wb = new XSSFWorkbook();
        }

        public void Open(string filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                string ext = Path.GetExtension(filename).ToLower();
                if (ext == ".xlsx")
                    wb = new XSSFWorkbook(fileStream);
                else
                {
                    wb = new HSSFWorkbook(fileStream);

                }
            }
            mFilename = filename;
        }
        /// <summary>
        /// 创建一个Excel对象，该对象为可见的
        /// </summary>
        public void Create(string sheetname = "Sheet1")
        {
            ws = wb.CreateSheet(sheetname);

        }

        public ISheet GetSheet(string SheetName)
        //获取一个工作表
        {
            return (ws = wb.GetSheet(SheetName) ?? wb.CreateSheet(SheetName));
        }

        public ISheet AddSheet(string SheetName)
        //添加一个工作表
        {
            ISheet s = wb.CreateSheet(SheetName);
            return s;
        }

        public void DelSheet(string SheetName)//删除一个工作表
        {
            int index = wb.GetNameIndex(SheetName);
            wb.RemoveSheetAt(index);
        }

        public ISheet ReNameSheet(string OldSheetName, string NewSheetName)//重命名一个工作表一
        {
            int index = wb.GetNameIndex(OldSheetName);
            wb.SetSheetName(index, NewSheetName);
            return wb.GetSheetAt(index);
        }

        public void SetCellValue(ISheet ws, int x, int y, object value)
        //ws：要设值的工作表     X行Y列     value   值
        {
            IRow row = ws.GetRow(x) ?? ws.CreateRow(x);
            ICell cell = row.GetCell(y) ?? row.CreateCell(y);
            string valuetype = value.GetType().Name.ToLower();
            switch (valuetype)
            {
                case "string"://字符串类型   
                case "system.string":
                case "datetime":
                case "system.datetime"://日期类型  
                case "boolean"://布尔型   
                case "system.boolean"://布尔型    
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(value.ToString());
                    break;
                case "byte":
                case "int":
                case "int16":
                case "int32":
                case "int64":
                case "system.int16"://整型   
                case "system.int32":
                case "system.int64":
                case "system.byte":
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToInt32(value));
                    break;
                case "single":
                case "system.single":
                case "double":
                case "system.double":
                case "decimal":
                case "system.decimal":
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(value));
                    break;
                case "dbnull"://空值处理   
                case "system.dbnull"://空值处理   
                    cell.SetCellValue("");
                    break;
                default:
                    cell.SetCellValue(value.ToString());
                    break;
            }
        }

        public void SetCellValue(string ws, int x, int y, object value)
        //ws：要设值的工作表的名称 X行Y列 value 值
        {
            ISheet s = GetSheet(ws);
            SetCellValue(s, x, y, value);
        }

        public ICell GetCell(string ws, int x, int y)
        {
            return GetSheet(ws).GetRow(x).Cells[y];
        }

        public ICell GetCell(ISheet ws, int x, int y)
        {
            return ws.GetRow(x).Cells[y];
        }


        public void SetCellProperty(string ws, int Startx, int Starty, int Endx, int Endy, int size, string fontName, IColor color, int fontSize, HorizontalAlignment HorizontalAlignment)
        //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            SetCellProperty(GetSheet(ws), Startx, Starty, Endx, Endy, size, fontName, color, fontSize, HorizontalAlignment);
        }

        public void SetCellProperty(ISheet ws, int Startx, int Starty, int Endx, int Endy, int size, string fontName, IColor color, int fontSize, HorizontalAlignment HorizontalAlignment)
        //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            ICellStyle style = wb.CreateCellStyle();
            IFont font = wb.CreateFont();
            //font.Color = color.ObjToInt();
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            style.Alignment = HorizontalAlignment;
            style.SetFont(font);
            foreach (ICell cell in GetCellsOfRange(ws, Startx, Starty, Endx, Endy))
            {
                cell.CellStyle = style;
            }
        }

        public IList<ICell> GetCellsOfRange(string ws, int Startx, int Starty, int Endx, int Endy)
        {
            return (GetCellsOfRange(GetSheet(ws), Startx, Starty, Endx, Endy));
        }

        public IList<ICell> GetCellsOfRange(ISheet ws, int Startx, int Starty, int Endx, int Endy)
        {
            IList<ICell> allCell = new List<ICell>();
            for (int i = Startx; i <= Endx; i++)
                for (int j = Starty; j <= Endy; j++)
                {
                    allCell.Add(GetCell(ws, i, j));
                }
            return allCell;
        }

        public void UniteCells(ISheet ws, int firstRow, int lastRow, int firstCol, int lastCol)
        //合并单元格
        {
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            ws.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
        }

        public void UniteCells(string ws, int firstRow, int lastRow, int firstCol, int lastCol)
        //合并单元格
        {
            UniteCells(GetSheet(ws), firstRow, lastRow, firstCol, lastCol);
        }

        public bool SaveAs(string FileName)
        //文档另存为
        {
            mFilename = FileName;
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Close()
        {
            wb.Close();
        }
    }
}