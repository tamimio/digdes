using System;
using System.Collections.Generic;
using System.Data.SqlClient; //
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using evernote.Model;
//using evernote.DataLayer;

namespace evernote.DataLayer.sql
{
    public class CategoryRep : CategoryRepository
    {
        private string connect;

        public CategoryRep(string _connect) { connect = _connect; }

        //-----

        public Category Create(Guid _UserID, string _name)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connect))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = "insert into categories (ID, Name, UserID) values (@ID, @Name, @UserID)";
                    var newCategory = new Category
                    {
                        ID = Guid.NewGuid(),
                        Name = _name
                    };
                    cmd.Parameters.AddWithValue("@UserID", _UserID);
                    cmd.Parameters.AddWithValue("@Name", newCategory.Name);
                    cmd.Parameters.AddWithValue("@ID", newCategory.ID);
                    cmd.ExecuteNonQuery();

                    return newCategory;
                }
            }
        }



    }
}
