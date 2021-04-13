using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

namespace Demo.Helper
{
    public class ExportHelper
    {

        #region 导出 XLS
        MemoryStream DtToMs(DataTable sourceTable, string sheetName)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet1 = hssfworkbook.CreateSheet(sheetName);
            IRow rowhead = sheet1.CreateRow(0);
            // handling header.
            foreach (DataColumn column in sourceTable.Columns)
            {
                ICell headCell = rowhead.CreateCell(column.Ordinal);
                headCell.SetCellValue(column.ColumnName);

                #region 设置自适应行宽
                int CurrentColumn = column.Ordinal;
                int columnWidth = sheet1.GetColumnWidth(CurrentColumn) / 256;//获取当前列宽度  
                int length = Encoding.UTF8.GetBytes(headCell.CString("")).Length;
                if (columnWidth < length + 1)//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符 
                {
                    columnWidth = length + 1;
                }
                sheet1.SetColumnWidth(CurrentColumn, columnWidth * 256);
                #endregion
            }

            // handling value.
            int rowIndex = 1;

            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet1.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    ICell icell = dataRow.CreateCell(column.Ordinal);
                    icell.SetCellValue(row[column].ToString());

                    #region 设置自适应行宽
                    int CurrentColumn = column.Ordinal;
                    int columnWidth = sheet1.GetColumnWidth(CurrentColumn) / 256;//获取当前列宽度  
                    int length = Encoding.UTF8.GetBytes(icell.CString("")).Length;
                    if (columnWidth < length + 1)//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符 
                    {
                        columnWidth = length + 1;
                    }
                    sheet1.SetColumnWidth(CurrentColumn, columnWidth * 256);
                    #endregion
                }
                rowIndex++;
            }
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }

        DataTable DoTb(DataTable dt, int[] remove, string[] contan, string[] headname)
        {
            if (remove != null && remove.Length > 0)
            {//处理删除列
                for (int i = remove.Length; i > 0; i--)
                {
                    //dt.Columns.RemoveAt(0);
                    dt.Columns.RemoveAt(remove[i - 1]);
                }
            }
            if (contan != null && contan.Length > 0)
            {//处理包含列
                int t = 0;
                while (t < dt.Columns.Count)
                {
                    bool flag = false;
                    for (int j = 0; j < contan.Length; j++)
                    {
                        if (String.Compare(dt.Columns[t].ColumnName, contan[j], true) == 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        t++;
                    }
                    else
                    {
                        dt.Columns.Remove(dt.Columns[t].ColumnName);
                    }
                }
            }
            //处理表头
            for (int i = 0; i < headname.Length; i++)
            {
                dt.Columns[i].ColumnName = headname[i];
            }
            return dt;
        }



        private DataTable DoTb(DataTable dt, int[] remove, string[] contan, Dictionary<string, string> headname)
        {

            if (remove != null && remove.Length > 0)
            {//处理删除列
                for (int i = remove.Length; i > 0; i--)
                {
                    //dt.Columns.RemoveAt(0);
                    dt.Columns.RemoveAt(remove[i - 1]);
                }
            }
            if (contan != null && contan.Length > 0)
            {//处理包含列
                int t = 0;
                while (t < dt.Columns.Count)
                {
                    bool flag = false;
                    for (int j = 0; j < contan.Length; j++)
                    {
                        if (String.Compare(dt.Columns[t].ColumnName, contan[j], true) == 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        t++;
                    }
                    else
                    {
                        dt.Columns.Remove(dt.Columns[t].ColumnName);
                    }
                }
            }

            if (headname != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string headerName = headname[dt.Columns[i].ColumnName];
                    if (!string.IsNullOrEmpty(headerName))
                    {
                        dt.Columns[i].ColumnName = headerName;
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fname">指定Excel工作表名称</param>
        /// <param name="header">表头</param>
        /// <returns>Excel工作表</returns>
        public void ExportDataTableToExcel(DataTable sourceTable, string fname, string[] header)
        {
            string filename = string.Format("{0}.xls", fname);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(DtToMs(DoTb(sourceTable, null, null, header), "sheet1").GetBuffer());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fname">指定Excel工作表名称</param>
        /// <param name="header">表头</param>
        /// <param name="remove">排除的列(注意从小到大)</param>
        /// <returns>Excel工作表</returns>
        public void ExportDataTableToExcel(DataTable sourceTable, string fname, string[] header, int[] remove)
        {
            string filename = string.Format("{0}.xls", fname);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(DtToMs(DoTb(sourceTable, remove, null, header), "sheet1").GetBuffer());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="header">表头</param>
        /// <param name="remove">包含的列</param>
        /// <param name="fname">指定Excel工作表名称</param>
        /// <returns>Excel工作表</returns>
        public void ExportDataTableToExcel(DataTable sourceTable, string[] header, string[] contan, string fname)
        {
            string filename = string.Format("{0}.xls", fname);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(DtToMs(DoTb(sourceTable, null, contan, header), "sheet1").GetBuffer());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出表格到EXCEL
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="header"></param>
        /// <param name="fname"></param>
        public void ExportDataTableToExcel(DataTable sourceTable, Dictionary<string, string> header, string fname)
        {
            var columns = (from DataColumn dc in sourceTable.Columns select dc.ColumnName).ToArray();
            foreach (string cname in columns)
            {
                if (header.ContainsKey(cname))
                {
                    sourceTable.Columns[cname].ColumnName = header[cname];
                }
                else
                {
                    sourceTable.Columns.Remove(cname);
                }
            }
            string filename = string.Format("{0}.xls", fname);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(DtToMs(sourceTable, "sheet1").GetBuffer());
            HttpContext.Current.Response.End();

        }


        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="headname">字段别名</param>
        /// <param name="contan">存在值时，只导出这里面的字段</param>
        /// <param name="sheetname"></param>
        /// <param name="fname">文件名称， 不含扩展名</param>
        public void ExportDataTableToExcel(DataTable sourceTable, Dictionary<string, string> headname, string[] contan, string sheetname, string fname)
        {
            string filename = string.Format("{0}.xls", fname);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(DtToMs(DoTb(sourceTable, null, contan, headname), sheetname).GetBuffer());
            HttpContext.Current.Response.End();
        }

        #endregion

        #region 样式操作Demo
        public void CreateFormat()
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = workbook.CreateSheet("Sheet1") as HSSFSheet;
            HSSFRow dataRow = sheet.CreateRow(1) as HSSFRow;
            dataRow = sheet.CreateRow(1) as HSSFRow;
            CellRangeAddress region = new CellRangeAddress(1, 1, 1, 2);
            sheet.AddMergedRegion(region);
            ICell cell = dataRow.CreateCell(1);
            cell.SetCellValue("test");

            ICellStyle style = workbook.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;

            //cell.CellStyle = style;  
            for (int i = region.FirstRow; i <= region.LastRow; i++)
            {
                IRow row = HSSFCellUtil.GetRow(i, sheet);
                for (int j = region.FirstColumn; j <= region.LastColumn; j++)
                {
                    ICell singleCell = HSSFCellUtil.GetCell(row, (short)j);
                    singleCell.CellStyle = style;
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                using (FileStream fs = new FileStream("C:\\TestConsole.xls", FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// 导出Excel,带汇总
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="fname">表名</param>
        /// <param name="header">列头</param>
        /// <param name="remove">移出的列</param>
        /// <param name="TotalDes">汇总说明</param>

        public void ExportExcelAndTotal(DataTable sourceTable, string fname, string[] header, int[] remove, string TotalDes)
        {
            string filename = string.Format("{0}.xls", fname + DateTime.Now.ToString("yyyyMMddhhmmss"));
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(DtToMsFormat(DoTb(sourceTable, remove, null, header), "sheet1", TotalDes, fname).GetBuffer());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 生成Excel
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="sheetName">表名</param>
        /// <param name="TotalDes">汇总列显示</param>
        /// <returns></returns>
        MemoryStream DtToMsFormat(DataTable sourceTable, string sheetName, string TotalDes, string fname)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet1 = hssfworkbook.CreateSheet(sheetName);
            #region 第二行为汇总行
            CellRangeAddress region = new CellRangeAddress(0, 0, 0, sourceTable.Columns.Count - 1); //合并第一行
            sheet1.AddMergedRegion(region);
            IRow rowTotal = sheet1.CreateRow(0);
            ICell Icell = rowTotal.CreateCell(0);
            Icell.SetCellValue(fname + ":" + TotalDes);
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 14;
            font.Boldweight = 14;
            font.Color = HSSFColor.Red.Index;
            style.SetFont(font);
            Icell.CellStyle = style;
            #endregion

            IRow rowhead = sheet1.CreateRow(1);
            // handling header.
            foreach (DataColumn column in sourceTable.Columns)
            {
                ICell headCell = rowhead.CreateCell(column.Ordinal);
                headCell.SetCellValue(column.ColumnName);
                int columnWidth = sheet1.GetColumnWidth(column.Ordinal) / 256;//获取当前列宽度  
                int length = Encoding.UTF8.GetBytes(headCell.CString("")).Length;
                if (columnWidth < length + 1)//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符 
                {
                    columnWidth = length + 1;
                }
                sheet1.SetColumnWidth(column.Ordinal, columnWidth * 256);
            }

            // handling value.

            int rowIndex = 2;

            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet1.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    int currentColumn = column.Ordinal;
                    ICell newCell = dataRow.CreateCell(currentColumn);
                    newCell.SetCellValue(row[column].ToString());

                    int columnWidth = sheet1.GetColumnWidth(currentColumn) / 256;//获取当前列宽度  
                    int length = Encoding.UTF8.GetBytes(newCell.CString("")).Length;
                    if (columnWidth < length + 1)//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符 
                    {
                        columnWidth = length + 1;
                    }
                    sheet1.SetColumnWidth(currentColumn, columnWidth * 256);
                }
                rowIndex++;
            }
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }
        #endregion


        /// <summary> 
        /// 将一组对象导出成EXCEL 
        /// </summary> 
        /// <typeparam name="T">要导出对象的类型</typeparam> 
        /// <param name="objList">一组对象</param> 
        /// <param name="FileName">导出后的文件名</param> 
        /// <param name="columnInfo">列名信息</param> 
        public void ExExcel<T>(List<T> objList, Dictionary<string, string> columnInfo, string FileName)
        {
            if (columnInfo.Count == 0) { return; }
            if (objList.Count == 0) { return; }
            //生成EXCEL的HTML 
            string excelStr = "";
            Type myType = objList[0].GetType();
            //根据反射从传递进来的属性名信息得到要显示的属性 
            List<System.Reflection.PropertyInfo> myPro = new List<System.Reflection.PropertyInfo>();
            foreach (string cName in columnInfo.Keys)
            {
                System.Reflection.PropertyInfo p = myType.GetProperty(cName);
                if (p != null)
                {
                    myPro.Add(p);
                    excelStr += columnInfo[cName] + "\t";
                }
            }
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0) { return; }
            excelStr += "\r";
            foreach (T obj in objList)
            {
                foreach (System.Reflection.PropertyInfo p in myPro)
                {
                    excelStr += p.GetValue(obj, null) + "\t";
                }
                excelStr += "\r";
            }

            //输出EXCEL 
            var rs = System.Web.HttpContext.Current.Response;
            rs.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            rs.AppendHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            rs.ContentType = "application/ms-excel";
            rs.Write(excelStr);
            rs.End();
        }
    }
}
