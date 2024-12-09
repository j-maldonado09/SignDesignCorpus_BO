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
    public interface IFundRepository : Interfaces.IRepository<Fund>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class Fund
    {
        public int Id { get; set; }
        public string Number { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class FundRepository : IFundRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public FundRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(Fund entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO FUND (");
            _strQuery.Append("FUND_NBR, ");
            _strQuery.Append("FUND_NM, ");
            _strQuery.Append("FUND_DSCR");
            _strQuery.Append(") OUTPUT inserted.FUND_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_number, ");
            _strQuery.Append("@prm_name, ");
            _strQuery.Append("@prm_description");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_number", entity.Number);
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_description", entity.Description);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM FUND WHERE ");
            _strQuery.Append("FUND_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(Fund entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE FUND SET ");
            _strQuery.Append("FUND_NBR = @prm_number, ");
            _strQuery.Append("FUND_NM = @prm_name, ");
            _strQuery.Append("FUND_DSCR = @prm_description ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("FUND_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_number", entity.Number);
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_description", entity.Description);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("FUND_ID AS Id, ");
            _strQuery.Append("FUND_NBR AS Number, ");
            _strQuery.Append("FUND_NM AS Name, ");
            _strQuery.Append("FUND_DSCR AS Description ");
            _strQuery.Append("FROM FUND ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("FUND_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("FUND_NM ");
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
        private static void ConvertCase(Fund entity)
        {
            entity.Number = (entity.Number != null) ? entity.Number.ToUpper() : "";
            entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
            entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : "";
        }
    }
}
