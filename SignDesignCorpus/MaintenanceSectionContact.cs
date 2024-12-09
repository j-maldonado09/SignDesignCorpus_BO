using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataTier;


namespace SignDesignCorpus
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IMaintenanceSectionContactRepository : Interfaces.IRepository<MaintenanceSectionContact>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class MaintenanceSectionContact
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }

    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class MaintenanceSectionContactRepository : IMaintenanceSectionContactRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public MaintenanceSectionContactRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(MaintenanceSectionContact entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO MAINT_SECT_CNTCT (");
            _strQuery.Append("MAINT_SECT_CNTCT_FRST_NM, ");
            _strQuery.Append("MAINT_SECT_CNTCT_LAST_NM, ");
            _strQuery.Append("MAINT_SECT_CNTCT_RL");
            _strQuery.Append(") OUTPUT inserted.MAINT_SECT_CNTCT_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_first_name, ");
            _strQuery.Append("@prm_last_name, ");
            _strQuery.Append("@prm_role");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_first_name", entity.FirstName);
            _queryParams.Add("prm_last_name", entity.LastName);
            _queryParams.Add("prm_role", entity.Role);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM MAINT_SECT_CNTCT WHERE ");
            _strQuery.Append("MAINT_SECT_CNTCT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(MaintenanceSectionContact entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE MAINT_SECT_CNTCT SET ");
            _strQuery.Append("MAINT_SECT_CNTCT_FRST_NM = @prm_first_name, ");
            _strQuery.Append("MAINT_SECT_CNTCT_LAST_NM = @prm_last_name, ");
            _strQuery.Append("MAINT_SECT_CNTCT_ROLE = @prm_role ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("MAINT_SECT_CNTCT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_first_name", entity.FirstName);
            _queryParams.Add("prm_last_name", entity.LastName);
            _queryParams.Add("prm_role", entity.Role);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            string result = null;

            _strQuery.Clear();

            if (id is null)
            {
                return null;
            }

            _strQuery.Append("SELECT ");
            _strQuery.Append("MAINT_SECT_CNTCT_ID AS Id, ");
            _strQuery.Append("MAINT_SECT_CNTCT_FRST_NM AS FirstName, ");
            _strQuery.Append("MAINT_SECT_CNTCT_LAST_NM AS LastName, ");
            _strQuery.Append("MAINT_SECT_CNTCT_RL AS Role, ");
            _strQuery.Append("CONCAT (MAINT_SECT_CNTCT_LAST_NM, ', ', MAINT_SECT_CNTCT_FRST_NM) AS FullName ");
            //_strQuery.Append("PROJ_ID, ");
            //_strQuery.Append("PROJ_NM, ");
            //_strQuery.Append("PROJ_DSCR ");
            _strQuery.Append("FROM MAINT_SECT_CNTCT ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("MAINT_SECT_CNTCT_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("MAINT_SECT_CNTCT_LAST_NM ");
            _strQuery.Append("FOR JSON AUTO");

            // A record with specific "id" is searched.
            _queryParams.Clear();
            if (id != -1)
            {
                _queryParams.Add("prm_id", id);
            }

            result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

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
        private static void ConvertCase(MaintenanceSectionContact entity)
        {
            entity.FirstName = (entity.FirstName != null) ? entity.FirstName.ToUpper() : "";
            entity.LastName = (entity.LastName != null) ? entity.LastName.ToUpper() : "";
            entity.Role = (entity.Role != null) ? entity.Role.ToUpper() : "";
        }
    }
}

