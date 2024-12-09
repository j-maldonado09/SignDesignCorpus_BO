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
    public interface IDistrictContactRepository : Interfaces.IRepository<DistrictContact>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class DistrictContact
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }

    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class DistrictContactRepository : IDistrictContactRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public DistrictContactRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(DistrictContact entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO DIST_CNTCT (");
            _strQuery.Append("DIST_CNTCT_FRST_NM, ");
            _strQuery.Append("DIST_CNTCT_LAST_NM");
            _strQuery.Append(") OUTPUT inserted.DIST_CNTCT_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_first_name, ");
            _strQuery.Append("@prm_last_name");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_first_name", entity.FirstName);
            _queryParams.Add("prm_last_name", entity.LastName);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM DIST_CNTCT WHERE ");
            _strQuery.Append("DIST_CNTCT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(DistrictContact entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE DIST_CNTCT SET ");
            _strQuery.Append("DIST_CNTCT_FRST_NM = @prm_first_name, ");
            _strQuery.Append("DIST_CNTCT_LAST_NM = @prm_last_name ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("DIST_CNTCT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_first_name", entity.FirstName);
            _queryParams.Add("prm_last_name", entity.LastName);

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
            _strQuery.Append("DIST_CNTCT_ID AS Id, ");
            _strQuery.Append("DIST_CNTCT_FRST_NM AS FirstName, ");
            _strQuery.Append("DIST_CNTCT_LAST_NM AS LastName, ");
            _strQuery.Append("CONCAT (DIST_CNTCT_LAST_NM, ', ', DIST_CNTCT_FRST_NM) AS FullName ");
            _strQuery.Append("FROM DIST_CNTCT ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("DIST_CNTCT_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("DIST_CNTCT_LAST_NM ");
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
        private static void ConvertCase(DistrictContact entity)
        {
            entity.FirstName = (entity.FirstName != null) ? entity.FirstName.ToUpper() : "";
            entity.LastName = (entity.LastName != null) ? entity.LastName.ToUpper() : "";
        }
    }
}

