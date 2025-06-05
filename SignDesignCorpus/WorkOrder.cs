using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using SignDesignCorpus.ViewModels;
using SignDesignCorpus.HelperModels;

namespace SignDesignCorpus
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IWorkOrderRepository : Interfaces.IRepository<WorkOrderHelperModel>
    {
        public string ReadWorkOrders(int id = -1);
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************
    public class WorkOrder
    {
        // Refer to ViewModels folder
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public WorkOrderRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Create(WorkOrderHelperModel entity)
        {
            //ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO WRK_ORDR (");
            _strQuery.Append("WRK_ORDR_NBR, ");
            _strQuery.Append("REGN_DSTR_CNTR_ID, ");
            _strQuery.Append("MAINT_SECT_ID, ");
            _strQuery.Append("DEPT_ID, ");
            _strQuery.Append("ACCT_ID, ");
            _strQuery.Append("FY, ");
            _strQuery.Append("FUND_ID, ");
            _strQuery.Append("TASK_ID, ");
            _strQuery.Append("PC_BUS_ID, ");
            _strQuery.Append("PROJ_ID, ");
            _strQuery.Append("ACTV_ID, ");
            _strQuery.Append("RES_TYPE_ID, ");
            _strQuery.Append("MAINT_SECT_CNTCT_RQST_ID, ");
            _strQuery.Append("WRK_ORDR_RQST_DT, ");
            _strQuery.Append("MAINT_SECT_SUPV_APRV_ID, ");
            _strQuery.Append("WRK_ORDR_SUPV_APRV_DT, ");
            _strQuery.Append("AREA_ENGR_APRV_ID, ");
            _strQuery.Append("WRK_ORDR_ENGR_APRV_DT, ");
            _strQuery.Append("DIST_CNTCT_APRV_ID, ");
            _strQuery.Append("WRK_ORDR_DIST_APRV_DT, ");
            _strQuery.Append("WRK_ORDR_SIGN_RCVD_DT, ");
            _strQuery.Append("WRK_ORDR_SIGN_INST_DT, ");
            _strQuery.Append("WRK_ORDR_HOLD, ");
            _strQuery.Append("WRK_ORDR_STAT");
            _strQuery.Append(") OUTPUT inserted.WRK_ORDR_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_number, ");
            _strQuery.Append("@prm_dist_center, ");
            _strQuery.Append("@prm_maint_sect, ");
            _strQuery.Append("@prm_dept, ");
            _strQuery.Append("@prm_acct, ");
            _strQuery.Append("@prm_fy, ");
            _strQuery.Append("@prm_fund, ");
            _strQuery.Append("@prm_task, ");
            _strQuery.Append("@prm_pc_bus, ");
            _strQuery.Append("@prm_proj, ");
            _strQuery.Append("@prm_actv, ");
            _strQuery.Append("@prm_res_type, ");
            _strQuery.Append("@prm_maint_sect_cntct_rqst, ");
            _strQuery.Append("@prm_work_order_rqst_date, ");
            _strQuery.Append("@prm_maint_sect_supv_aprv, ");
            _strQuery.Append("@prm_work_order_supv_aprv_date, ");
            _strQuery.Append("@prm_area_engr_aprv, ");
            _strQuery.Append("@prm_work_order_engr_aprv_date, ");
            _strQuery.Append("@prm_dist_cntct_aprv, ");
            _strQuery.Append("@prm_work_order_dist_aprv_date, ");
            _strQuery.Append("@prm_work_order_sign_rcvd_date, ");
            _strQuery.Append("@prm_work_order_sign_inst_date, ");
            _strQuery.Append("@prm_work_order_hold, ");
            _strQuery.Append("@prm_work_order_status");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_number", entity.Number);
            _queryParams.Add("prm_dist_center", entity.MaterialRequestedFromId);
            _queryParams.Add("prm_maint_sect", entity.MaterialRequestedById);
            _queryParams.Add("prm_dept", entity.DepartmentId);
            _queryParams.Add("prm_acct", entity.AccountId);
            _queryParams.Add("prm_fy", entity.FY);
            _queryParams.Add("prm_fund", entity.FundId);
            _queryParams.Add("prm_task", entity.TaskId is null ? DBNull.Value : entity.TaskId);
            _queryParams.Add("prm_pc_bus", entity.PCBusId);
            _queryParams.Add("prm_proj", entity.ProjectId);
            _queryParams.Add("prm_actv", entity.ActivityId);
            _queryParams.Add("prm_res_type", entity.ResTypeId);
            _queryParams.Add("prm_maint_sect_cntct_rqst", entity.RequestedByMaintenanceId);
            _queryParams.Add("prm_work_order_rqst_date", entity.RequestedByMaintenanceDate);
            _queryParams.Add("prm_maint_sect_supv_aprv", entity.ApprovedByMaintenanceId is null ? DBNull.Value : entity.ApprovedByMaintenanceId);
            _queryParams.Add("prm_work_order_supv_aprv_date", entity.ApprovedByMaintenanceDate is null ? DBNull.Value : entity.ApprovedByMaintenanceDate);
            _queryParams.Add("prm_area_engr_aprv", entity.ApprovedByAreaEngineerId is null ? DBNull.Value : entity.ApprovedByAreaEngineerId);
            _queryParams.Add("prm_work_order_engr_aprv_date", entity.ApprovedByAreaEngineerDate is null ? DBNull.Value : entity.ApprovedByAreaEngineerDate);
            _queryParams.Add("prm_dist_cntct_aprv", entity.ApprovedByDistrictId is null ? DBNull.Value : entity.ApprovedByDistrictId);
            _queryParams.Add("prm_work_order_dist_aprv_date", entity.ApprovedByDistrictDate is null ? DBNull.Value : entity.ApprovedByDistrictDate);
            _queryParams.Add("prm_work_order_sign_rcvd_date", entity.SignReceivedDate is null ? DBNull.Value : entity.SignReceivedDate);
            _queryParams.Add("prm_work_order_sign_inst_date", entity.SignInstalledDate is null ? DBNull.Value : entity.SignInstalledDate);
            _queryParams.Add("prm_work_order_hold", entity.IsHold);
            _queryParams.Add("prm_work_order_status", entity.Status is null ? DBNull.Value : entity.Status);


            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            CreateItems(entity, sequenceValue);
            return sequenceValue;
        }

        private void CreateItems(WorkOrderHelperModel entity, int workOrderId)
        {
            int sequenceValue;

            foreach (var item in entity.Items)
            {
                _strQuery.Clear();
                _strQuery.Append("INSERT INTO WRK_ORDR_ITEM (");
                _strQuery.Append("WRK_ORDR_ID, ");
                //_strQuery.Append("NIGP_ID, ");
                _strQuery.Append("NIGP, ");
                _strQuery.Append("SIGN_RDC, ");
                _strQuery.Append("SIGN_QTY, ");
                _strQuery.Append("SIGN_IMG, ");
                _strQuery.Append("SIGN_NM, ");
                _strQuery.Append("SIGN_SZ, ");
                _strQuery.Append("SIGN_CLR, ");
                _strQuery.Append("SIGN_RUSH, ");
                _strQuery.Append("SIGN_NEW, ");
                _strQuery.Append("SPCL_INST_TXT, ");
                _strQuery.Append("SIGN_LAT, ");
                _strQuery.Append("SIGN_LONG, ");
                _strQuery.Append("SIGN_ATTCH, ");
                _strQuery.Append("SIGN_RCVD_DT, ");
                _strQuery.Append("SIGN_INST_DT, ");
                _strQuery.Append("SIGN_INST_IMG");
                _strQuery.Append(") OUTPUT inserted.WRK_ORDR_ITEM_ID ");
                _strQuery.Append("VALUES (");
                _strQuery.Append("@prm_wrk_ordr_id, ");
                _strQuery.Append("@prm_nigp, ");
                _strQuery.Append("@prm_sign_rdc, ");
                _strQuery.Append("@prm_sign_qty, ");
                _strQuery.Append("@prm_sign_img, ");
                _strQuery.Append("@prm_sign_name, ");
                _strQuery.Append("@prm_sign_size, ");
                _strQuery.Append("@prm_sign_color, ");
                _strQuery.Append("@prm_sign_rush, ");
                _strQuery.Append("@prm_sign_new, ");
                _strQuery.Append("@prm_spcl_inst_txt, ");
                _strQuery.Append("@prm_sign_lat, ");
                _strQuery.Append("@prm_sign_long, ");
                _strQuery.Append("@prm_sign_attch, ");
                _strQuery.Append("@prm_sign_rcvd_date, ");
                _strQuery.Append("@prm_sign_inst_date, ");
                _strQuery.Append("@prm_sign_inst_img");
                _strQuery.Append(")");

                _queryParams.Clear();
                _queryParams.Add("prm_wrk_ordr_id", workOrderId);
                _queryParams.Add("prm_nigp", item.NIGP);
                _queryParams.Add("prm_sign_rdc", item.RDC);
                _queryParams.Add("prm_sign_qty", item.Quantity);
                _queryParams.Add("prm_sign_img", item.SignImage);
                _queryParams.Add("prm_sign_name", item.Name);
                _queryParams.Add("prm_sign_size", item.Size);
                _queryParams.Add("prm_sign_color", item.Color);
                _queryParams.Add("prm_sign_rush", item.Rush);
                _queryParams.Add("prm_sign_new", item.New);
                _queryParams.Add("prm_spcl_inst_txt", item.SpecialInstructions);
                _queryParams.Add("prm_sign_lat", item.Latitude);
                _queryParams.Add("prm_sign_long", item.Longitude);
                _queryParams.Add("prm_sign_attch", item.SignAttachment is null ? DBNull.Value : item.SignAttachment);
                _queryParams.Add("prm_sign_rcvd_date", item.ReceivedDate is null ? DBNull.Value : item.ReceivedDate);
                _queryParams.Add("prm_sign_inst_date", item.InstalledDate is null ? DBNull.Value : item.InstalledDate);
                _queryParams.Add("prm_sign_inst_img", item.InstalledImage is null ? DBNull.Value : item.InstalledImage);

                sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);
            }
        }

        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM WRK_ORDR WHERE ");
            _strQuery.Append("WRK_ORDR_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        public void DisposeDBObjects()
        {
            _unitOfWork.ReleaseDBObjects();
        }

        public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT * FROM ");
            _strQuery.Append("(SELECT ");
            _strQuery.Append("WRK_ORDR_ID AS Id, ");
            _strQuery.Append("WRK_ORDR_NBR AS Number, ");
            _strQuery.Append("WRK_ORDR.REGN_DSTR_CNTR_ID AS MaterialRequestedFromId, ");
            _strQuery.Append("REGN_DSTR_CNTR_NM AS MaterialRequestedFromName, ");
            _strQuery.Append("WRK_ORDR.MAINT_SECT_ID AS MaterialRequestedById, ");
            _strQuery.Append("MAINT_SECT.MAINT_SECT_NM AS MaterialRequestedByName, ");
            _strQuery.Append("MAINT_SECT.MAINT_SECT_NBR AS MaterialRequestedByNumber, ");
            //_strQuery.Append("WRK_ORDR_RQST_DT AS RequestedByMaintenanceDate ");

            _strQuery.Append("CASE ");
            _strQuery.Append("WHEN WRK_ORDR_SIGN_INST_DT IS NOT NULL THEN WRK_ORDR_SIGN_INST_DT ");
            _strQuery.Append("WHEN WRK_ORDR_SIGN_RCVD_DT IS NOT NULL THEN WRK_ORDR_SIGN_RCVD_DT ");
            _strQuery.Append("WHEN MAINT_SECT_SUPV_APRV_ID IS NULL AND DIST_CNTCT_APRV_ID IS NULL THEN WRK_ORDR_RQST_DT ");
            _strQuery.Append("WHEN DIST_CNTCT_APRV_ID IS NULL THEN WRK_ORDR_SUPV_APRV_DT ");
            _strQuery.Append("ELSE WRK_ORDR_DIST_APRV_DT ");
            _strQuery.Append("END AS StatusDate, ");

            _strQuery.Append("WRK_ORDR_HOLD AS IsHold, ");
            _strQuery.Append("WRK_ORDR_STAT AS Status ");

            //// Determine the status based on approval and dates values
            //_strQuery.Append("CASE ");
            //_strQuery.Append("WHEN WRK_ORDR_SIGN_INST_DT IS NOT NULL THEN 'INSTALLED' ");
            //_strQuery.Append("WHEN WRK_ORDR_SIGN_RCVD_DT IS NOT NULL THEN 'RECEIVED' ");

            //// If there is NOT a sign that requires area engineer approval
            //_strQuery.Append("WHEN (SELECT COUNT(SIGN_NM) AS result FROM WRK_ORDR_ITEM WHERE WRK_ORDR_ITEM.WRK_ORDR_ID = WRK_ORDR.WRK_ORDR_ID AND (SIGN_NM = 'W12-2' OR SIGN_NM = 'W12-2A')) = 0 THEN ");
            //_strQuery.Append("CASE ");
            //_strQuery.Append("WHEN MAINT_SECT_SUPV_APRV_ID IS NULL AND DIST_CNTCT_APRV_ID IS NULL THEN 'CREATED' ");
            //_strQuery.Append("WHEN DIST_CNTCT_APRV_ID IS NULL THEN 'REQUESTED' ");
            //_strQuery.Append("ELSE 'APPROVED' ");
            //_strQuery.Append("END ");

            //// If there IS a sign that requires area engineer approval
            //_strQuery.Append("WHEN (SELECT COUNT(SIGN_NM) AS result FROM WRK_ORDR_ITEM WHERE WRK_ORDR_ITEM.WRK_ORDR_ID = WRK_ORDR.WRK_ORDR_ID AND (SIGN_NM = 'W12-2' OR SIGN_NM = 'W12-2A')) > 0 THEN ");
            //_strQuery.Append("CASE ");
            //_strQuery.Append("WHEN MAINT_SECT_SUPV_APRV_ID IS NULL AND AREA_ENGR_APRV_ID IS NULL AND DIST_CNTCT_APRV_ID IS NULL THEN 'CREATED' ");
            //_strQuery.Append("WHEN AREA_ENGR_APRV_ID IS NULL AND DIST_CNTCT_APRV_ID IS NULL THEN 'AREA ENGINEER' ");
            //_strQuery.Append("WHEN DIST_CNTCT_APRV_ID IS NULL THEN 'REQUESTED' ");
            //_strQuery.Append("ELSE 'APPROVED' ");
            //_strQuery.Append("END ");

            //_strQuery.Append("END AS Status ");

            _strQuery.Append("FROM WRK_ORDR ");

            _strQuery.Append("INNER JOIN REGN_DSTR_CNTR ON REGN_DSTR_CNTR.REGN_DSTR_CNTR_ID = WRK_ORDR.REGN_DSTR_CNTR_ID ");
            _strQuery.Append("INNER JOIN MAINT_SECT ON MAINT_SECT.MAINT_SECT_ID = WRK_ORDR.MAINT_SECT_ID ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("WRK_ORDR_ID = @prm_id ");
            }

            //_strQuery.Append("ORDER BY ");
            //_strQuery.Append("WRK_ORDR_RQST_DT) queryResult ");
            _strQuery.Append(") queryResult ");
            _strQuery.Append("ORDER BY StatusDate ");
            _strQuery.Append("FOR JSON AUTO");

            // A record with specific "id" is searched.
            _queryParams.Clear();
            if (id != -1)
            {
                _queryParams.Add("prm_id", id);
            }

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

        public string ReadWorkOrders(int id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("WRK_ORDR.WRK_ORDR_ID AS Id, ");
            _strQuery.Append("WRK_ORDR_NBR AS Number, ");
            _strQuery.Append("REGN_DSTR_CNTR_ID AS MaterialRequestedFromId, ");
            _strQuery.Append("MAINT_SECT_ID AS MaterialRequestedById, ");
            _strQuery.Append("DEPT_ID AS DepartmentId, ");
            _strQuery.Append("ACCT_ID AS AccountId, ");
            _strQuery.Append("FY AS FY, ");
            _strQuery.Append("FUND_ID AS FundId, ");
            _strQuery.Append("TASK_ID AS TaskId, ");
            _strQuery.Append("PC_BUS_ID AS PCBusId, ");
            _strQuery.Append("PROJ_ID AS ProjectId, ");
            _strQuery.Append("ACTV_ID AS ActivityId, ");
            _strQuery.Append("RES_TYPE_ID AS ResTypeId, ");
            _strQuery.Append("MAINT_SECT_CNTCT_RQST_ID AS RequestedByMaintenanceId, ");
            _strQuery.Append("WRK_ORDR_RQST_DT AS RequestedByMaintenanceDate, ");
            _strQuery.Append("MAINT_SECT_SUPV_APRV_ID AS ApprovedByMaintenanceId, ");
            _strQuery.Append("WRK_ORDR_SUPV_APRV_DT AS ApprovedByMaintenanceDate, ");
            _strQuery.Append("AREA_ENGR_APRV_ID AS ApprovedByAreaEngineerId, ");
            _strQuery.Append("WRK_ORDR_ENGR_APRV_DT AS ApprovedByAreaEngineerDate, ");
            _strQuery.Append("DIST_CNTCT_APRV_ID AS ApprovedByDistrictId, ");
            _strQuery.Append("WRK_ORDR_DIST_APRV_DT AS ApprovedByDistrictDate, ");
            _strQuery.Append("WRK_ORDR_SIGN_RCVD_DT AS SignReceivedDate, ");
            _strQuery.Append("WRK_ORDR_SIGN_INST_DT AS SignInstalledDate, ");
            _strQuery.Append("WRK_ORDR_HOLD AS IsHold, ");
            _strQuery.Append("Items.WRK_ORDR_ITEM_ID AS ItemId, ");
            //_strQuery.Append("NIGP_ID AS NIGPId, ");
            _strQuery.Append("NIGP AS NIGP, ");
            _strQuery.Append("SIGN_RDC AS RDC, ");
            _strQuery.Append("SIGN_QTY AS Quantity, ");
            _strQuery.Append("SIGN_IMG AS SignImage, ");
            _strQuery.Append("SIGN_NM AS Name, ");
            _strQuery.Append("SIGN_SZ AS Size, ");
            _strQuery.Append("SIGN_CLR AS Color, ");
            _strQuery.Append("SIGN_RUSH AS Rush, ");
            _strQuery.Append("SIGN_NEW AS New, ");
            _strQuery.Append("SPCL_INST_TXT AS SpecialInstructions, ");
            _strQuery.Append("SIGN_LAT AS Latitude, ");
            _strQuery.Append("SIGN_LONG AS Longitude, ");
            _strQuery.Append("SIGN_ATTCH AS SignAttachment, ");
            _strQuery.Append("SIGN_RCVD_DT AS ReceivedDate, ");
            _strQuery.Append("SIGN_INST_DT AS InstalledDate, ");
            _strQuery.Append("SIGN_INST_IMG AS InstalledImage ");
            _strQuery.Append("FROM ");
            _strQuery.Append("WRK_ORDR ");
            _strQuery.Append("INNER JOIN WRK_ORDR_ITEM AS Items ON WRK_ORDR.WRK_ORDR_ID = Items.WRK_ORDR_ID ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("WRK_ORDR.WRK_ORDR_ID = @prm_id ");
                //_strQuery.Append("WRK_ORDR.WRK_ORDR_ID = 13 ");
            }

            _strQuery.Append("FOR JSON AUTO, INCLUDE_NULL_VALUES");

            // A record with specific "id" is searched.
            _queryParams.Clear();
            if (id != -1)
            {
                _queryParams.Add("prm_id", id);
            }

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

        public void Update(WorkOrderHelperModel entity, int id)
        {
            //ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE WRK_ORDR SET ");
            _strQuery.Append("WRK_ORDR_NBR = @prm_number, ");
            _strQuery.Append("REGN_DSTR_CNTR_ID = @prm_dist_center, ");
            _strQuery.Append("MAINT_SECT_ID = @prm_maint_sect, ");
            _strQuery.Append("DEPT_ID = @prm_dept, ");
            _strQuery.Append("ACCT_ID = @prm_acct, ");
            _strQuery.Append("FY = @prm_fy, ");
            _strQuery.Append("FUND_ID = @prm_fund, ");
            _strQuery.Append("TASK_ID = @prm_task, ");
            _strQuery.Append("PC_BUS_ID = @prm_pc_bus, ");
            _strQuery.Append("PROJ_ID = @prm_proj, ");
            _strQuery.Append("ACTV_ID = @prm_actv, ");
            _strQuery.Append("RES_TYPE_ID = @prm_res_type, ");
            _strQuery.Append("MAINT_SECT_CNTCT_RQST_ID = @prm_maint_sect_cntct_rqst, ");
            _strQuery.Append("WRK_ORDR_RQST_DT = @prm_work_order_rqst_date, ");
            _strQuery.Append("MAINT_SECT_SUPV_APRV_ID = @prm_maint_sect_supv_aprv, ");
            _strQuery.Append("WRK_ORDR_SUPV_APRV_DT = @prm_work_order_supv_aprv_date, ");
            _strQuery.Append("AREA_ENGR_APRV_ID = @prm_area_engr_aprv, ");
            _strQuery.Append("WRK_ORDR_ENGR_APRV_DT = @prm_work_order_engr_aprv_date, ");
            _strQuery.Append("DIST_CNTCT_APRV_ID = @prm_dist_cntct_aprv, ");
            _strQuery.Append("WRK_ORDR_DIST_APRV_DT = @prm_work_order_dist_aprv_date, ");
            _strQuery.Append("WRK_ORDR_SIGN_RCVD_DT = @prm_work_order_sign_rcvd_date, ");
            _strQuery.Append("WRK_ORDR_SIGN_INST_DT = @prm_work_order_sign_inst_date, ");
            _strQuery.Append("WRK_ORDR_HOLD = @prm_work_order_hold, ");
            _strQuery.Append("WRK_ORDR_STAT = @prm_work_order_status ");

            _strQuery.Append("WHERE ");
            _strQuery.Append("WRK_ORDR_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_number", entity.Number);
            _queryParams.Add("prm_dist_center", entity.MaterialRequestedFromId);
            _queryParams.Add("prm_maint_sect", entity.MaterialRequestedById);
            _queryParams.Add("prm_dept", entity.DepartmentId);
            _queryParams.Add("prm_acct", entity.AccountId);
            _queryParams.Add("prm_fy", entity.FY);
            _queryParams.Add("prm_fund", entity.FundId);
            _queryParams.Add("prm_task", entity.TaskId is null ? DBNull.Value : entity.TaskId);
            _queryParams.Add("prm_pc_bus", entity.PCBusId);
            _queryParams.Add("prm_proj", entity.ProjectId);
            _queryParams.Add("prm_actv", entity.ActivityId);
            _queryParams.Add("prm_res_type", entity.ResTypeId);
            _queryParams.Add("prm_maint_sect_cntct_rqst", entity.RequestedByMaintenanceId);
            _queryParams.Add("prm_work_order_rqst_date", entity.RequestedByMaintenanceDate);
            _queryParams.Add("prm_maint_sect_supv_aprv", entity.ApprovedByMaintenanceId is null ? DBNull.Value : entity.ApprovedByMaintenanceId);
            _queryParams.Add("prm_work_order_supv_aprv_date", entity.ApprovedByMaintenanceDate is null ? DBNull.Value : entity.ApprovedByMaintenanceDate);
            _queryParams.Add("prm_area_engr_aprv", entity.ApprovedByAreaEngineerId is null ? DBNull.Value : entity.ApprovedByAreaEngineerId);
            _queryParams.Add("prm_work_order_engr_aprv_date", entity.ApprovedByAreaEngineerDate is null ? DBNull.Value : entity.ApprovedByAreaEngineerDate);
            _queryParams.Add("prm_dist_cntct_aprv", entity.ApprovedByDistrictId is null ? DBNull.Value : entity.ApprovedByDistrictId);
            _queryParams.Add("prm_work_order_dist_aprv_date", entity.ApprovedByDistrictDate is null ? DBNull.Value : entity.ApprovedByDistrictDate);
            _queryParams.Add("prm_work_order_sign_rcvd_date", entity.SignReceivedDate is null ? DBNull.Value : entity.SignReceivedDate);
            _queryParams.Add("prm_work_order_sign_inst_date", entity.SignInstalledDate is null ? DBNull.Value : entity.SignInstalledDate);
            _queryParams.Add("prm_work_order_hold", entity.IsHold);
            _queryParams.Add("prm_work_order_status", entity.Status is null ? DBNull.Value : entity.Status);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);

            DeleteItems(id);
            CreateItems(entity, id);
        }

        private void DeleteItems(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM WRK_ORDR_ITEM WHERE ");
            _strQuery.Append("WRK_ORDR_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }
    }
}
