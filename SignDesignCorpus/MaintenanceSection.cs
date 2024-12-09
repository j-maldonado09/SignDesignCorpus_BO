using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataTier;
using SignDesignCorpus.ViewModels;

namespace SignDesignCorpus
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IMaintenanceSectionRepository : Interfaces.IRepository<MaintenanceSection>
    {
        public string ReadMaintenanceSectionParameters(int? id = -1);
        public void UpdateMaintenanceSectionParameters(MaintenanceSectionParametersViewModel entity, int id);
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class MaintenanceSection
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }
        public string Email { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class MaintenanceSectionRepository : IMaintenanceSectionRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public MaintenanceSectionRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(MaintenanceSection entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO MAINT_SECT (");
            _strQuery.Append("MAINT_SECT_NM, ");
            _strQuery.Append("MAINT_SECT_NBR, ");
            _strQuery.Append("MAINT_SECT_ST_NBR, ");
            _strQuery.Append("MAINT_SECT_CITY_NM, ");
            _strQuery.Append("MAINT_SECT_STATE_CD, ");
            _strQuery.Append("MAINT_SECT_ZIPCODE, ");
            _strQuery.Append("MAINT_SECT_EML_ADDR");
            _strQuery.Append(") OUTPUT inserted.MAINT_SECT_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_name, ");
            _strQuery.Append("@prm_number, ");
            _strQuery.Append("@prm_street, ");
            _strQuery.Append("@prm_city, ");
            _strQuery.Append("@prm_state, ");
            _strQuery.Append("@prm_zipcode, ");
            _strQuery.Append("@prm_email");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_number", entity.Number);
            _queryParams.Add("prm_street", entity.Street);
            _queryParams.Add("prm_city", entity.City);
            _queryParams.Add("prm_state", entity.State);
            _queryParams.Add("prm_zipcode", entity.ZipCode);
            _queryParams.Add("prm_email", entity.Email);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM MAINT_SECT WHERE ");
            _strQuery.Append("MAINT_SECT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(MaintenanceSection entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE MAINT_SECT SET ");
            _strQuery.Append("MAINT_SECT_NM = @prm_name, ");
            _strQuery.Append("MAINT_SECT_NBR = @prm_number, ");
            _strQuery.Append("MAINT_SECT_ST_NBR = @prm_street, ");
            _strQuery.Append("MAINT_SECT_CITY_NM = @prm_city, ");
            _strQuery.Append("MAINT_SECT_STATE_CD = @prm_state, ");
            _strQuery.Append("MAINT_SECT_ZIPCODE = @prm_zipcode, ");
            _strQuery.Append("MAINT_SECT_EML_ADDR = @prm_email ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("MAINT_SECT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_number", entity.Number);
            _queryParams.Add("prm_street", entity.Street);
            _queryParams.Add("prm_city", entity.City);
            _queryParams.Add("prm_state", entity.State);
            _queryParams.Add("prm_zipcode", entity.ZipCode);
            _queryParams.Add("prm_email", entity.Email);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("MAINT_SECT_ID AS Id, ");
            _strQuery.Append("MAINT_SECT_NM AS Name, ");
            _strQuery.Append("MAINT_SECT_NBR AS Number, ");
            _strQuery.Append("MAINT_SECT_ST_NBR AS Street, ");
            _strQuery.Append("MAINT_SECT_CITY_NM AS City, ");
            _strQuery.Append("MAINT_SECT_STATE_CD AS State, ");
            _strQuery.Append("MAINT_SECT_ZIPCODE AS ZipCode, ");
            _strQuery.Append("MAINT_SECT_EML_ADDR AS Email ");
            _strQuery.Append("FROM MAINT_SECT ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("MAINT_SECT_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("MAINT_SECT_NM ");
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

        // ---------------------------------------------------------------------------------------------
        //        Release database resources.       
        // ---------------------------------------------------------------------------------------------
        public void DisposeDBObjects()
        {
            _unitOfWork.ReleaseDBObjects();
        }

        // ---------------------------------------------------------------------------------------------
        //               Convert to upper case specific fields before CRUD operation.
        // ---------------------------------------------------------------------------------------------
        private static void ConvertCase(MaintenanceSection entity)
        {
            entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
            entity.Number = (entity.Number != null) ? entity.Number.ToUpper() : "";
            entity.Street = (entity.Street != null) ? entity.Street.ToUpper() : "";
            entity.City = (entity.City != null) ? entity.City.ToUpper() : "";
            entity.State = (entity.State != null) ? entity.State.ToUpper() : "";
            entity.ZipCode = (entity.ZipCode != null) ? entity.ZipCode.ToUpper() : "";
            entity.Email = (entity.Email != null) ? entity.Email.ToUpper() : "";
        }

        // ---------------------------------------------------------------------------------------------
        //            
        // ---------------------------------------------------------------------------------------------
        public string ReadMaintenanceSectionParameters(int? id = -1)
        {
            if (id == null)
                return null;

            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("MAINT_SECT_ID AS Id, ");
            _strQuery.Append("MAINT_SECT_DEPT_ID AS DepartmentId, ");
            _strQuery.Append("MAINT_SECT_ACCT_ID AS AccountId, ");
            _strQuery.Append("MAINT_SECT_FUND_ID AS FundId, ");
            _strQuery.Append("MAINT_SECT_TASK_ID AS TaskId, ");
            _strQuery.Append("MAINT_SECT_PC_BUS_ID AS PCBusId, ");
            _strQuery.Append("MAINT_SECT_PROJ_ID AS ProjectId, ");
            _strQuery.Append("MAINT_SECT_ACTV_ID AS ActivityId, ");
            _strQuery.Append("MAINT_SECT_RES_TYPE_ID AS ResTypeId ");
            _strQuery.Append("FROM MAINT_SECT ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("MAINT_SECT_ID = @prm_id ");
            }

            //_strQuery.Append("ORDER BY ");
            //_strQuery.Append("MAINT_SECT_NM ");
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

        // ---------------------------------------------------------------------------------------------
        //              
        // ---------------------------------------------------------------------------------------------
        public void UpdateMaintenanceSectionParameters(MaintenanceSectionParametersViewModel entity, int id)
        {
            _strQuery.Clear();
            _strQuery.Append("UPDATE MAINT_SECT SET ");
            _strQuery.Append("MAINT_SECT_DEPT_ID = @prm_dept_id, ");
            _strQuery.Append("MAINT_SECT_ACCT_ID = @prm_acct_id, ");
            _strQuery.Append("MAINT_SECT_FUND_ID = @prm_fund_id, ");
            _strQuery.Append("MAINT_SECT_TASK_ID = @prm_task_id, ");
            _strQuery.Append("MAINT_SECT_PC_BUS_ID = @prm_pc_bus_id, ");
            _strQuery.Append("MAINT_SECT_PROJ_ID = @prm_proj_id, ");
            _strQuery.Append("MAINT_SECT_ACTV_ID = @prm_actv_id, ");
            _strQuery.Append("MAINT_SECT_RES_TYPE_ID = @prm_res_type_id ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("MAINT_SECT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_dept_id", entity.DepartmentId is null ? DBNull.Value : entity.DepartmentId);
            _queryParams.Add("prm_acct_id", entity.AccountId is null ? DBNull.Value : entity.AccountId);
            _queryParams.Add("prm_fund_id", entity.FundId is null ? DBNull.Value : entity.FundId);
            _queryParams.Add("prm_task_id", entity.TaskId is null ? DBNull.Value : entity.TaskId);
            _queryParams.Add("prm_pc_bus_id", entity.PCBusId is null ? DBNull.Value : entity.PCBusId);
            _queryParams.Add("prm_proj_id", entity.ProjectId is null ? DBNull.Value : entity.ProjectId);
            _queryParams.Add("prm_actv_id", entity.ActivityId is null ? DBNull.Value : entity.ActivityId);
            _queryParams.Add("prm_res_type_id", entity.ResTypeId is null ? DBNull.Value : entity.ResTypeId);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }
    }
}

