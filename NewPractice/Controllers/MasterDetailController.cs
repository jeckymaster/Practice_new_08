using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewPractice.Models;
using Newtonsoft.Json;

namespace NewPractice.Controllers
{
    public class MasterDetailController : Controller
    {
        // GET: MasterDetail
        public ActionResult Index()
        {
            DbModel db = new DbModel();
            ViewBag.OrderParty = new SelectList(db.OrderParties, "PartyCode", "PartyName");
            ViewBag.ItemMaster = new SelectList(db.ItemMasters, "ItemCode", "ItemName");
            return View();
        }

        //public JsonResult getItems()
        //{
        //    List<ItemMaster> categories = new List<ItemMaster>();
        //    using (DbModel dc = new DbModel())
        //    {
        //        categories = dc.ItemMasters.OrderBy(a => a.ItemName).ToList();
        //    }
        //    return new JsonResult { Data = categories, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public string LikeFuncFromItemMaster()
        {
            #region DECLARATIONS
            List<OrderDetail> LiCom = new List<OrderDetail>();

            DbModel MDC = new DbModel();

            DataTable DT_Result = new DataTable();

            APIResponse APR = new APIResponse();
            #endregion

            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DbModel"].ConnectionString);
                conn.Open();
                string query = "select ItemCode,ItemName from ItemMasters where ItemName like 'C%'";
                SqlCommand cmd = new SqlCommand(query, conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                //return dt;
                APR.Data = JsonConvert.SerializeObject(dt);

            }
            catch (Exception ex)
            {

            }

            return JsonConvert.SerializeObject(APR);

        }

        public class APIResponse
        {
            public int ServerCode { get; set; }
            public string Data { get; set; }
            public string Status { get; set; }
            public string OtherDetails { get; set; }
            public string UniqueKey { get; set; }
        }


        public string GetRate(int Irate)
        {
            #region DECLARATIONS
            List<ItemMaster> LiCom = new List<ItemMaster>();

            DbModel MDC = new DbModel();

            DataTable DT_Result = new DataTable();

            APIResponse APR = new APIResponse();
            #endregion
            var a="";
            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DbModel"].ConnectionString);
                conn.Open();
                string query = "select ItemRate from ItemMasters where ItemCode = '"+ Irate +"'";
                SqlCommand cmd = new SqlCommand(query, conn);

                //DataTable dt = new DataTable();
                //dt.Load(cmd.ExecuteReader());
                //conn.Close();
                ////return dt;
                //APR.Data = JsonConvert.SerializeObject(dt);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    a = dr["ItemRate"].ToString();

                }

            }
            catch (Exception ex)
            {

            }

            //return JsonConvert.SerializeObject(APR);
            return a;

        }


        public int GetTotal(int Qty, int Rate)
        {
            var a=Qty;
            var TotalAmt = 0;
            int q = (Qty == 0) ? 0 : Qty;
            int r = (Rate == 0) ? 0 : Rate;
            if (q != 0 && r != 0)

            { 
            TotalAmt = Qty*Rate;
            
                //return JsonConvert.SerializeObject(TotalAmt);
            }
            return TotalAmt;
        }

    }
}