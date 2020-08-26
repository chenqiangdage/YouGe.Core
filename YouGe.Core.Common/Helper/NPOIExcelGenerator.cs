using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace YouGe.Core.Common.Helper
{
    public class NPOIExcelGenerator : NPOIExcelEdit
    {

        public NPOIExcelGenerator()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 将一个泛型类集合数据插入EXCEL表中
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="alldata">泛型类集合</param>
        /// <param name="caption">放在EXCEL第一行的标题数据</param>
        /// <param name="infostr">放在EXCEL第二行开始的附加数据信息，一个数据放一行</param>
        /// <param name="ws">EXCEL表格名称，默认为"Sheet1"</param>
        /// <param name="startX">EXCEL表格数据的起始行数，默认为第1行开始，在EXCEL中用1、2、3、4表示</param>
        /// <param name="startY">EXCEL表格数据的起始列数，默认为第1行开始，在EXCEL中用A、B、C、D表示</param>
        /// <returns>返回所有数据占据的行数</returns>
        public int InsertIListData<T>(IList<T> alldata, String caption, String[] infostr, string ws, int startX, int startY)
        {
            ISheet s = wb.GetSheet(ws);
            if (s == null)
                s = wb.CreateSheet(ws);
            IRow row;
            //使用原始名称，“否”为使用中文，“是”为使用英文
            bool originname = true;

            //返回值为数据占据的行数
            Type t = typeof(T);
            PropertyInfo[] myPropertyInfo = t.GetProperties();
            String classname = t.Name;
            int nowX = 0, nowY = 0;
            if (alldata == null) return 0;
            //这里插入标题
            if (caption.Length > 0)
            {
                SetCellValue(ws, startX + nowX, startY, caption);
            }
            //这里开始插入附加信息infostr
            for (int i = 0; i < infostr.Length; i++)
            {
                SetCellValue(ws, startX + nowX++, startY + nowY, infostr[i]);
            }
            //这里开始插入具体列表信息
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                string temp = myPropertyInfo[i].PropertyType.ToString();
                if (temp.Substring(temp.Length - 2) == "[]")
                    continue;
                SetCellValue(ws, startX + nowX, startY + nowY, myPropertyInfo[i].Name);
                for (int j = 0; j < alldata.Count; j++)
                {
                    SetCellValue(ws, startX + j + nowX + 1, startY + nowY, myPropertyInfo[i].GetValue(alldata[j], null));
                }
                nowY++;
            }
            //这里是将标题行合并并且居中
            if (caption.Length > 0)
            {
                UniteCells(ws, startX, startY, startX, startY + nowY - 1);
            }
            //这里将附加信息合并并且居中
            if (infostr.Length > 0)
            {
                for (int i = 0; i < nowX; i++)
                {
                    UniteCells(ws, startX + i, startY, startX + i, startY + nowY - 1);
                }
            }
            return (alldata.Count + nowX + 1);
        }

        /// <summary>
        /// 将一个泛型类集合数据插入EXCEL表中
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="alldata">泛型类集合</param>
        /// <param name="caption">放在EXCEL第一行的标题数据</param>
        /// <param name="ws">EXCEL表格名称，默认为"Sheet1"</param>
        /// <param name="startX">EXCEL表格数据的起始行数，默认为第1行开始，在EXCEL中用1、2、3、4表示</param>
        /// <param name="startY">EXCEL表格数据的起始列数，默认为第1行开始，在EXCEL中用A、B、C、D表示</param>
        /// <returns>返回所有数据占据的行数</returns>
        public int InsertIListData<T>(IList<T> alldata, String caption, string ws, int startX, int startY)
        {
            String[] infostr = { };
            return (InsertIListData(alldata, caption, infostr, ws, startX, startY));
        }

        public int InsertIListData<T>(IList<T> alldata)
        {
            String caption = "", ws = "Sheet1";
            int startX = 1, startY = 1;
            String[] infostr = { };
            return (InsertIListData(alldata, caption, infostr, ws, startX, startY));
        }

        /*public void InsertIListDataAsync<T>(IList<T> alldata)
        {
            InsertIListDataAsy(alldata);
            return ;
        }*/

        public async void InsertDatatableAsync(System.Data.DataTable dt)
        {
            var t = await Task.Run(() => { return (InsertDatatable(dt)); });
            return;
        }


        public int InsertDatatable(System.Data.DataTable dt)
        {
            return InsertDatatable(dt, "Sheet1");
        }

        public int InsertDatatable(System.Data.DataTable dt, string ws)
        {
            if (dt == null || dt.Rows.Count == 0) return 0;
            long totalCount = dt.Rows.Count;
            int x = 0, y = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                SetCellValue(ws, x, y + i, dt.Columns[i].ColumnName);
            }
            x++;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    SetCellValue(ws, x + j, y + i, dt.Rows[j][i]);
                }
            }
            return dt.Rows.Count + 1;
        }

        public async void InsertIListDataAsync<T>(IList<T> alldata)
        {
            var t = await Task.Run(() => { return (InsertIListData(alldata)); });
            return;
        }

    }
}