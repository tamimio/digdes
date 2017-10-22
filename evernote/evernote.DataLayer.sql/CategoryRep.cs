using System;
using System.Collections.Generic;
using System.Data.SqlClient; //
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using evernote.Model;
using evernote.DataLayer;

namespace evernote.DataLayer.sql
{
    public class CategoryRep : ICategoryRepository
    {
        private string connectStr;

        public CategoryRep(string connect) { connectStr = connect; }

        //-----

        public Category Create(Guid UserID, string name)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = "insert into categories (ID, Name, UserID) values (@ID, @Name, @UserID)";
                    var newCategory = new Category
                    {
                        ID = Guid.NewGuid(),
                        Name = name
                    };
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Name", newCategory.Name);
                    cmd.Parameters.AddWithValue("@ID", newCategory.ID);
                    cmd.ExecuteNonQuery();

                    return newCategory;
                }
            }
        }

	    public Category Get(short id)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {   
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"select * from Category where ID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new ArgumentException("Category {id} not found.");
                        }
                        return Parse(reader);
                    }
                }
            }
        }

		public void Delete(short CategoryId)
		{
			using (SqlConnection sqlConnect = new SqlConnection(connectStr))
			{
				sqlConnect.Open();
				using (SqlCommand cmd = sqlConnect.CreateCommand())
				{
					cmd.CommandText = @"delete Category where ID=@id";
					cmd.Parameters.AddWithValue("@id", CategoryId);
					cmd.ExecuteNonQuery();
				}
			}
		}


        public IEnumerable<Category> GetNoteCategories(short noteId)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"select c.CategoryId as CategoryId,c.Name as Name from CategoriesNotes as CN join Categories as C on CN.CategoryId=C.CategoryId
                                                where CN.NoteId=@id";
                    cmd.Parameters.AddWithValue("@id", noteId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return Parse(reader);
                        }
                    }
                }
            }
        }
		

        private Category Parse(SqlDataReader reader)
        {
            return new Category
            {
                ID = reader.GetInt16(reader.GetOrdinal("CategoryId")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };
        }

    }
}
