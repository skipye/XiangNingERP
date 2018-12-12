using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DalProject;
using ModelProject;

namespace ServiceProject
{
    public class ToExcel
    {
        private static readonly ToExcelDal EDal = new ToExcelDal();
        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="dt">数据集合</param>
        /// <param name="FileName">文件名</param>
        public void CreateExcel(DataTable dt, string FileName)
        {
            if (dt.Rows.Count > 0)
            {
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                string colHeaders = "", ls_item = "";
                DataRow[] myRow = dt.Select();
                int i = 0;
                int cl = dt.Columns.Count;
                //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加\n
                    {
                        colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                    }
                    else
                    {
                        colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                    }

                }
                System.Web.HttpContext.Current.Response.Write(colHeaders);
                //向HTTP输出流中写入取得的数据信息

                //逐行处理数据
                foreach (DataRow row in myRow)
                {
                    //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据
                    for (i = 0; i < cl; i++)
                    {
                        if (i == (cl - 1))//最后一列，加\n
                        {
                            ls_item += row[i].ToString() + "\n";
                        }
                        else
                        {
                            ls_item += row[i].ToString() + "\t";
                        }

                    }
                    System.Web.HttpContext.Current.Response.Write(ls_item);
                    ls_item = "";

                }
                System.Web.HttpContext.Current.Response.End();
                System.Web.HttpContext.Current.Response.Clear();
            }
        }
        public List<HRExcelModel> GetMouthHRList(string TypeId)
        {
            try { return EDal.GetMouthHRList(TypeId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcelOut()
        {
            try {
                return EDal.HRToExcel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
