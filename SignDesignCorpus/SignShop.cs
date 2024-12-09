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
    public interface ISignShopRepository : Interfaces.IRepository<SignShop>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class SignShop
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class SignShopRepository : ISignShopRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public SignShopRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(SignShop entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO SIGN_SHOP (");
            _strQuery.Append("SIGN_SHOP_NM, ");
            _strQuery.Append("SIGN_SHOP_CITY_NM, ");
            _strQuery.Append("SIGN_SHOP_STATE_NM, ");
            _strQuery.Append("SIGN_SHOP_ZIPCODE, ");
            _strQuery.Append("SIGN_SHOP_PHN_NBR, ");
            _strQuery.Append("SIGN_SHOP_EML_ADDR");
            _strQuery.Append(") OUTPUT inserted.SIGN_SHOP_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_name, ");
            _strQuery.Append("@prm_city, ");
            _strQuery.Append("@prm_state, ");
            _strQuery.Append("@prm_zipcode, ");
            _strQuery.Append("@prm_phone, ");
            _strQuery.Append("@prm_email");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_city", entity.City);
            _queryParams.Add("prm_state", entity.State);
            _queryParams.Add("prm_zipcode", entity.ZipCode);
            _queryParams.Add("prm_phone", entity.Phone);
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
            _strQuery.Append("DELETE FROM SIGN_SHOP WHERE ");
            _strQuery.Append("SIGN_SHOP_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(SignShop entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE SIGN_SHOP SET ");
            _strQuery.Append("SIGN_SHOP_NM = @prm_name, ");
            _strQuery.Append("SIGN_SHOP_CITY_NM = @prm_city, ");
            _strQuery.Append("SIGN_SHOP_STATE_NM = @prm_state, ");
            _strQuery.Append("SIGN_SHOP_ZIPCODE = @prm_zipcode, ");
            _strQuery.Append("SIGN_SHOP_PHN_NBR = @prm_phone, ");
            _strQuery.Append("SIGN_SHOP_EML_ADDR = @prm_email ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("SIGN_SHOP_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_city", entity.City);
            _queryParams.Add("prm_state", entity.State);
            _queryParams.Add("prm_zipcode", entity.ZipCode);
            _queryParams.Add("prm_phone", entity.Phone);
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
            _strQuery.Append("SIGN_SHOP_ID AS Id, ");
            _strQuery.Append("SIGN_SHOP_NM AS Name, ");
            _strQuery.Append("SIGN_SHOP_CITY_NM AS City, ");
            _strQuery.Append("SIGN_SHOP_STATE_NM AS State, ");
            _strQuery.Append("SIGN_SHOP_ZIPCODE AS ZipCode, ");
            _strQuery.Append("SIGN_SHOP_PHN_NBR AS Phone, ");
            _strQuery.Append("SIGN_SHOP_EML_ADDR AS Email ");
            _strQuery.Append("FROM SIGN_SHOP ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("SIGN_SHOP_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("SIGN_SHOP_NM ");
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
        private static void ConvertCase(SignShop entity)
        {
            entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
            entity.City = (entity.City != null) ? entity.City.ToUpper() : "";
            entity.State = (entity.State != null) ? entity.State.ToUpper() : "";
            entity.ZipCode = (entity.ZipCode != null) ? entity.ZipCode.ToUpper() : "";
            entity.Phone = (entity.Phone!= null) ? entity.Phone.ToUpper() : "";
            entity.Email = (entity.Email != null) ? entity.Email.ToUpper() : "";
        }
    }
}

