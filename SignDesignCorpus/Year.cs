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
    public interface IYearRepository : Interfaces.IRepository<Year>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class Year
    {
        [Required]
        public int FiscalYear { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class YearRepository : IYearRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public YearRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(Year entity)
        {
            //ConvertCase(entity);

            //_strQuery.Clear();
            //_strQuery.Append("INSERT INTO FY (");
            //_strQuery.Append("FY_NM, ");
            //_strQuery.Append("FY_DSCR");
            //_strQuery.Append(") OUTPUT inserted.FY_ID ");
            //_strQuery.Append("VALUES (");
            //_strQuery.Append("@prm_name, ");
            //_strQuery.Append("@prm_description");
            //_strQuery.Append(")");

            //_queryParams.Clear();
            //_queryParams.Add("prm_name", entity.Name);
            //_queryParams.Add("prm_description", entity.Description);

            //int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            //return sequenceValue;
            return 0;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            //_strQuery.Clear();
            //_strQuery.Append("DELETE FROM FY WHERE ");
            //_strQuery.Append("FY_ID = @prm_id");

            //_queryParams.Clear();
            //_queryParams.Add("prm_id", id);

            //_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(Year entity, int id)
        {
            //ConvertCase(entity);

            //_strQuery.Clear();
            //_strQuery.Append("UPDATE FY SET ");
            //_strQuery.Append("FY_NM = @prm_name, ");
            //_strQuery.Append("FY_DSCR = @prm_description ");
            //_strQuery.Append("WHERE ");
            //_strQuery.Append("FY_ID = @prm_id");

            //_queryParams.Clear();
            //_queryParams.Add("prm_id", id);
            //_queryParams.Add("prm_name", entity.Name);
            //_queryParams.Add("prm_description", entity.Description);

            //_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("FY_NBR AS FiscalYear ");
            _strQuery.Append("FROM FY ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("FY_NBR = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("FY_NBR ");
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
        private static void ConvertCase(Year entity)
        {
            //entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
            //entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : "";
        }
    }
}

