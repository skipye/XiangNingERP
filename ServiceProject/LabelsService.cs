using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalProject;
using System.Data;

namespace ServiceProject
{
    public class LabelsService
    {
        private static readonly LabelsDal SDal = new LabelsDal();
        public PagedList<LabelsModel> GetPageList(SLabelsModel SModel, int PageIndex, int PageSize)
        {
            try { return SDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelsModel> GetWorkLabelsList(SLabelsModel SModel)
        {
            try { return SDal.GetWorkLabelsList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<LabelsModel> GetDeliveryList(SLabelsModel SModel)
        {
            try { return SDal.GetDeliveryList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Add(LabelsModel Models)
        {
            try
            {
                SDal.Add(Models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //编辑尺寸
        public bool editStyle(LabelsModel models)
        {
            try
            {
                SDal.editStyle(models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LabelsModel GetDetailById(int Id)
        {
            try { return SDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool TransferMore(string ListId, int inv_id)
        {
            try
            {
                SDal.TransferMore(ListId, inv_id); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //绑定合同操作
        public bool CheckLabels(string ListId, int CRM_Id)
        {
            try
            {
                SDal.CheckLabels(ListId, CRM_Id); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //送货维修操作
        public bool DeliveryMore(string ListId,string DeliverTime)
        {
            try
            {
                SDal.DeliveryMore(ListId, DeliverTime); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckDelivery(string ListId, string OrderNum, string DeliverTime)
        {
            try
            {
                SDal.CheckDelivery(ListId, OrderNum, DeliverTime); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckMore(string ListId, int inv_id)
        {
            try
            {
                SDal.CheckMore(ListId,inv_id); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckedDeliveryMore(string ListId, bool IsCW)
        {
            try
            {
                SDal.CheckedDeliveryMore(ListId, IsCW); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteDeliveryMore(string ListId)
        {
            try
            {
                SDal.DeleteDelivery(ListId); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            try
            {
                return SDal.GetProNameDrolistBySNAndArea(ProSN, ProProArea); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddWork(WIP_WOXQModel Models)
        {
            try
            {
                SDal.AddWork(Models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //返修
        public bool AddReWork(LabelsModel models)
        {
            try
            {
                SDal.AddReWork(models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcel(SLabelsModel SModel)
        {
            try
            {
                return SDal.ToExcel(SModel); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToCWExcel(SLabelsModel SModel)
        {
            try
            {
                return SDal.ToCWExcel(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToRKExcel(SLabelsModel SModel)
        {
            try
            {
                return SDal.ToRKExcel(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToDeliveryExcelOut(SLabelsModel SModel)
        {
            try
            {
                return SDal.ToDeliveryExcelOut(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //打印送货单
        public CRM_HTZModel PrintDelivery(string ListId)
        {
            try
            {
                return SDal.PrintDelivery(ListId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
