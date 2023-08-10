using NewPractice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPractice.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            DbModel db = new DbModel();

            OrderMaster om = new OrderMaster();

            ViewBag.OrderParty = new SelectList(db.OrderParties, "PartyCode", "PartyName");

            ViewBag.ItemMaster = new SelectList(db.ItemMasters, "ItemCode", "ItemName");

            return View(om);
        }

        public string ListData1()
        {
            #region DECLARATIONS
            List<OrderDetail> LiCom = new List<OrderDetail>();

            DbModel MDC = new DbModel();

            DataTable DT_Result = new DataTable();

            APIResponse APR = new APIResponse();
            #endregion

            try
            {
                //OrderDetail od = new OrderDetail();

                //string sqlstring = "select * from OrderDetails";

                //SqlCommand cmd = new SqlCommand(sqlstring);

                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DbModel"].ConnectionString);
                conn.Open();
                //string query = "SELECT od.OrderDetailsNo, od.OrderNo, im.ItemName,od.ItemQty, od.ItemRate, od.TotalAmt FROM OrderDetails od left join ItemMasters im on od.ItemCode=im.ItemCode";
                string query = "SELECT * from OrderMasters";
                SqlCommand cmd = new SqlCommand(query, conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                APR.Data = JsonConvert.SerializeObject(dt);

                

                //od = JsonConvert.SerializeObject(cmd);

                //DT_Result = LF.FillDataTable(sqlstring, MDC.Database.Connection.ConnectionString);

                //APR.Status = "SUCCESS"; 

                //APR.Data = JsonConvert.SerializeObject(DT_Result);


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

        public string GetRate(int Irate)
        {
            #region DECLARATIONS
            List<ItemMaster> LiCom = new List<ItemMaster>();

            DbModel MDC = new DbModel();

            DataTable DT_Result = new DataTable();

            APIResponse APR = new APIResponse();
            #endregion
            var a = "";
            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DbModel"].ConnectionString);
                conn.Open();
                string query = "select ItemRate from ItemMasters where ItemCode = '" + Irate + "'";
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

        public string SaveData(OrderMaster om, string ListOrderDetails,int pid)
        {
            APIResponse APR = new APIResponse();

            DbModel db = new DbModel();

            OrderMaster OM = new OrderMaster();

            OM = om;

            List<OrderDetail> ListOD = new List<OrderDetail>();
            
            try
            {
                //om.OrderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(om.ObjectOrderDetails);

                ListOD = JsonConvert.DeserializeObject<List<OrderDetail>>(ListOrderDetails);
                //var parid = om.PartyCode;
                for (int i = 0; i <= ListOD.Count - 1; i++)
                {

                    ListOD[i].OrderNo = om.OrderNo;

                    var item = ListOD[i];

                    if (item.OrderDetailsNo == 0)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Added;

                    }
                    else
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                
                }

                //om.PartyCode = parid;
                om.EntryDate = DateTime.Now;

                if (om.OrderNo == 0)
                {
                    db.Entry(om).State = EntityState.Added;
                }
                else
                {
                    db.Entry(om).State = EntityState.Modified;
                }

                db.SaveChanges();
            }

            catch(Exception ex)
            { 

            }
            return JsonConvert.SerializeObject(APR); ;
        }



        public string ListDataHistory()
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
                //string query = "SELECT od.OrderDetailsNo, od.OrderNo, im.ItemName,od.ItemQty, od.ItemRate, od.TotalAmt FROM OrderDetails od left join ItemMasters im on od.ItemCode=im.ItemCode";
                string query = "select om.OrderDate,om.PartyCode,op.PartyName,om.EntryDate from OrderMasters om left join OrderParties op on om.PartyCode=op.PartyCode";
                SqlCommand cmd = new SqlCommand(query, conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                APR.Data = JsonConvert.SerializeObject(dt);


            }
            catch (Exception ex)
            {


            }

            return JsonConvert.SerializeObject(APR);

        }






        //public string EditBatch(int id)
        //{
        //    #region DECLARATIONS AND ASSIGNMENT

        //    APIResponse APR = new APIResponse();

        //    LibraryFunctions LF = new LibraryFunctions();

        //    #endregion

        //    try
        //    {
        //        Batch M = GetBatchDetail(id);

        //        M.ObjectBatchDetails = JsonConvert.SerializeObject(M.BatchDetails);

        //        APR.Status = "SUCCESS";
        //        APR.Data = HTMLHelpers.RenderRazorViewToString(this.ControllerContext, "NewForm", M);
        //    }
        //    catch (Exception ex)
        //    {
        //        APR.Status = "ERROR";
        //        APR.Data = LF.GetErrorMessageFromException(ex);
        //    }

        //    return JsonConvert.SerializeObject(APR);
        //}

        //public string DeleteBatch(int id)
        //{
        //    #region DECLARATIONS AND ASSIGNMENTS

        //    APIResponse APR = new APIResponse();

        //    LibraryFunctions LF = new LibraryFunctions();

        //    #endregion

        //    try
        //    {
        //        using (MyDbContext MDC = new MyDbContext(0, false))
        //        {
        //            string SqlString = $"DELETE FROM safedb.Batches WHERE Code = {id};\n" +
        //                               $"DELETE FROM safedb.BatchDetails WHERE BatchId = {id};";

        //            MDC.Database.ExecuteSqlCommand(SqlString);

        //            APR.Status = "SUCCESS";
        //            APR.Data = HTMLHelpers.RenderRazorViewToString(this.ControllerContext, "NewForm", NewObject());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        APR.Status = "ERROR";
        //        APR.Data = LF.GetErrorMessageFromException(ex);
        //    }

        //    return JsonConvert.SerializeObject(APR);
        //}


        //public Department NewObject()
        //{
        //    #region DECLARATIONS

        //    User U = new User();

        //    Department D = new Department();

        //    ProjectLibrary PL = new ProjectLibrary();

        //    DateTime LocateDate = DateTime.Now;

        //    GeneralMethods GM = new GeneralMethods();


        //    #endregion

        //    U = GM.GetUserCookie_UserObject(Request);

        //    LocateDate = DateTime.Now; // PL.ConvertToLocalDate(DateTime.Now, U.TimeZoneName);

        //    D = new Department()
        //    {
        //        ClientId = U.ClientId,
        //        CompanyId = U.CurrentCompanyId,
        //        BranchId = U.CurrentBranchId,
        //        SubBranchId = U.CurrentBranchId,
        //        Dt = LocateDate,
        //        EntryDate = LocateDate,
        //        EntryByUserId = U.Code,
        //    };

        //    return D;
        //}

        //public string GetFreezeDepartmentsHistory()
        //{
        //    List<DepartmentFrozenHistory> L = new List<DepartmentFrozenHistory>();

        //    var JsonData = "";

        //    MyDbContext DBM = new MyDbContext(0, false);

        //    U = GM.GetUserCookie_UserObject(Request);

        //    var listuser = String.Join(",", U.ListSelfDepartmentIds);

        //    /*string sqlstring = "select * from safedb.DepartmentFrozenHistories";*/ /*where Code IN(" + listuser +")";*/

        //    string sqlstring = "select d.Code, d.Name, dh.FrozenDate, u.FirstName, dh.IsFrozen from safedb.DepartmentFrozenHistories dh left join safedb.Departments d on dh.DepartmentId = d.Code left join safedb.Users u on dh.FrozenByUserId = u.Code";

        //    L = DBM.Database.SqlQuery<DepartmentFrozenHistory>(sqlstring).ToList();

        //    if (L.Count > 0)
        //    {
        //        JsonData = JsonConvert.SerializeObject(L);

        //    }
        //    else
        //    {
        //        return "";
        //    }

        //    return "{\"records\":" + JsonData + "}";
        //}


        //public string UpdateDepartments(string M)//(List<int> ids)
        //{
        //    MyDbContext MDC = new MyDbContext(0, false);

        //    APIResponse APR = new APIResponse();

        //    Department d = new Department();

        //    LibraryFunctions LF = new LibraryFunctions();

        //    U = GM.GetUserCookie_UserObject(Request);

        //    try
        //    {

        //        List<DeptFrozen> dp = new List<DeptFrozen>();

        //        dp = JsonConvert.DeserializeObject<List<DeptFrozen>>(M);

        //        string SqlString = "";

        //        string SqlDeptHistory = "";


        //        for (int i = 0; i < dp.Count; i++)
        //        {
        //            int UCode = (dp[i].IsFrozen == true) ? U.Code : 0;

        //            SqlString += "update safedb.Departments set IsFrozen = " + (dp[i].IsFrozen == true ? 1 : 0) + ", FrozenDate = Getdate(), FrozenByUserId = " + UCode + " where Code = " + dp[i].recid + "; \n ";

        //            SqlDeptHistory += "Insert Into safedb.DepartmentFrozenHistories (DepartmentId, IsFrozen, FrozenByUserId, FrozenDate) Values (" + dp[i].recid + "," + (dp[i].IsFrozen == true ? 1 : 0) + ", " + U.Code + ",GetDate()) ; \n";
        //        }

        //        if (SqlString != "")
        //        {
        //            MDC.Database.ExecuteSqlCommand(SqlString);

        //            MDC.Database.ExecuteSqlCommand(SqlDeptHistory);

        //            APR.Status = "SUCCESS";
        //        }


        //    }

        //    catch (Exception ex)
        //    {
        //        APR.Status = "ERROR";
        //        APR.Data = ex.Message;

        //    }

        //    return JsonConvert.SerializeObject(APR);
        //}
    }
}