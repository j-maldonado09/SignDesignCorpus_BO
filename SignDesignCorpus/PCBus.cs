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
    public interface IPCBusRepository : Interfaces.IRepository<PCBus>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class PCBus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class PCBusRepository : IPCBusRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public PCBusRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(PCBus entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO PC_BUS (");
            _strQuery.Append("PC_BUS_NM, ");
            _strQuery.Append("PC_BUS_DSCR");
            _strQuery.Append(") OUTPUT inserted.PC_BUS_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_name, ");
            _strQuery.Append("@prm_description");
            _strQuery.Append(")");

            _queryParams.Clear();
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
            _strQuery.Append("DELETE FROM PC_BUS WHERE ");
            _strQuery.Append("PC_BUS_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(PCBus entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE PC_BUS SET ");
            _strQuery.Append("PC_BUS_NM = @prm_name, ");
            _strQuery.Append("PC_BUS_DSCR = @prm_description ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("PC_BUS_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
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
            _strQuery.Append("PC_BUS_ID AS Id, ");
            _strQuery.Append("PC_BUS_NM AS Name, ");
            _strQuery.Append("PC_BUS_DSCR AS Description ");
            _strQuery.Append("FROM PC_BUS ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("PC_BUS_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("PC_BUS_NM ");
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
        private static void ConvertCase(PCBus entity)
        {
            entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
            entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : "";
        }
    }
}