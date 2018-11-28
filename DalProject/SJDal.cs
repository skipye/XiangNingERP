using Infrastructure;
using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DalProject
{
    public class SJDal
    {
        public PagedList<WIP_WOModel> GetPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.WIP_workorder.Where(k => k.delete_flag == false && k.closed_flag == false && k.status >= 0 && k.status <= 2 && k.CRM_contract_detail_id > 0)
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.CRM_contract_detail.CRM_contract_header.SN == SModel.HTSN : true
                            where SModel.status >=0 ? p.status == SModel.status : true
                            where !string.IsNullOrEmpty(SModel.SaleName) ? p.CRM_contract_detail.CRM_contract_header.CRM_customers.name.Contains(SModel.SaleName) : true
                            orderby p.created_time descending
                            select new WIP_WOModel
                            {
                                id = p.id,
                                workorder = p.workorder,
                                SaleName = p.CRM_contract_detail.CRM_contract_header.CRM_customers.name,
                                HTSN = p.CRM_contract_detail.CRM_contract_header.SN,
                                ProductName = p.CRM_contract_detail.SYS_product.name,
                                ProductXL = p.CRM_contract_detail.SYS_product.SYS_product_SN.name,
                                HTId = p.CRM_contract_detail.header_id,
                                JHDateTime = p.CRM_contract_detail.CRM_contract_header.delivery_date,
                                qty = p.qty,
                                length=p.CRM_contract_detail.length,
                                width=p.CRM_contract_detail.width,
                                height=p.CRM_contract_detail.height,
                                status = p.status,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<WIP_WOModel> GetYTPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.WIP_workorder.Where(k => k.delete_flag == false && k.closed_flag == false && k.status >= 0 && k.status <= 2 && k.WIP_contract_id > 0)
                            where SModel.status >= 0 ? p.status == SModel.status : true
                            orderby p.created_time descending
                            select new WIP_WOModel
                            {
                                id = p.id,
                                workorder = p.workorder,
                                WoodName = p.WIP_contract.INV_wood_type.name,
                                //HTSN = p.CRM_contract_detail.CRM_contract_header.SN,
                                ProductName = p.WIP_contract.SYS_product.name,
                                ProductXL = p.WIP_contract.SYS_product.SYS_product_SN.name,
                                Color = p.WIP_contract.color,
                                //JHDateTime = p.CRM_contract_detail.CRM_contract_header.delivery_date,
                                length = p.WIP_contract.length,
                                width = p.WIP_contract.width,
                                height = p.WIP_contract.height,
                                qty = p.qty,
                                status = p.status,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public void Update(SJ_WorkModel Models)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.WIP_workorder.Where(k => k.id == Models.Id).SingleOrDefault();
                tables.BOM_ready_date = DateTime.Now;
                tables.BOM_over_date = DateTime.Now;
                tables.BOM_path = Models.BOM_path;
                tables.paper_path = Models.paper_path;
                tables.status = 2;

                int ProId = 0;
                int CRMId = tables.CRM_contract_detail_id??0;
                int WIPId = tables.WIP_contract_id??0;
                if (CRMId > 0)
                {
                    ProId = tables.CRM_contract_detail.product_id;
                }
                else
                { ProId = tables.WIP_contract.product_id; }

                var PTables = db.SYS_product.Where(k => k.id == ProId).SingleOrDefault();
                PTables.picture = Models.picture;
                PTables.volume = Models.volume;
                PTables.BOM_path = Models.BOM_path;
                PTables.paper_path = Models.paper_path;

                db.SaveChanges();
            }
        }
    }
}
