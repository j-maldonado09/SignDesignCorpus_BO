using DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignDesignCorpus
{
    public class WorkOrderReport
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public WorkOrderReport(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string Read()
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT * FROM ");
            _strQuery.Append("(SELECT ");
            _strQuery.Append("WRK_ORDR_ID AS Id, ");
            _strQuery.Append("WRK_ORDR_NBR AS Number, ");
            _strQuery.Append("CONCAT (MAINT_SECT.MAINT_SECT_NBR, '-', WRK_ORDR_ID) AS OrderNumber, ");
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

            _strQuery.Append("CASE ");
            _strQuery.Append("WHEN WRK_ORDR_SIGN_INST_DT IS NOT NULL THEN 'INSTALLED' ");
            _strQuery.Append("WHEN WRK_ORDR_SIGN_RCVD_DT IS NOT NULL THEN 'RECEIVED' ");
            _strQuery.Append("WHEN MAINT_SECT_SUPV_APRV_ID IS NULL AND DIST_CNTCT_APRV_ID IS NULL THEN 'CREATED' ");
            _strQuery.Append("WHEN DIST_CNTCT_APRV_ID IS NULL THEN 'REQUESTED' ");
            _strQuery.Append("ELSE 'APPROVED' ");
            _strQuery.Append("END AS Status ");

            _strQuery.Append("FROM WRK_ORDR ");

            _strQuery.Append("INNER JOIN REGN_DSTR_CNTR ON REGN_DSTR_CNTR.REGN_DSTR_CNTR_ID = WRK_ORDR.REGN_DSTR_CNTR_ID ");
            _strQuery.Append("INNER JOIN MAINT_SECT ON MAINT_SECT.MAINT_SECT_ID = WRK_ORDR.MAINT_SECT_ID ");

            // A record with specific "id" is searched.

            _strQuery.Append("WHERE ");
            _strQuery.Append("(MAINT_SECT_SUPV_APRV_ID IS NULL AND DIST_CNTCT_APRV_ID IS NULL) OR ");
            _strQuery.Append("(MAINT_SECT_SUPV_APRV_ID IS NOT NULL AND DIST_CNTCT_APRV_ID IS NULL) ");

            //_strQuery.Append("ORDER BY ");
            //_strQuery.Append("WRK_ORDR_RQST_DT) queryResult ");
            _strQuery.Append(") queryResult ");
            _strQuery.Append("ORDER BY StatusDate ");
            _strQuery.Append("FOR JSON AUTO");

            // A record with specific "id" is searched.
            _queryParams.Clear();

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

        public void DisposeDBObjects()
        {
            _unitOfWork.ReleaseDBObjects();
        }
    }
}
