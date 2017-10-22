using System;
using System.Collections.Generic;
using System.Data.SqlClient; 
using System.Text;
using System.Threading.Tasks;

using evernote.Model;

namespace evernote.DataLayer.sql
{
    public class UserRep : IUserRepository
    {
		private string connectStr;
		private ICategoryRepository catRep;

        public UserRep(string connect) { connectStr = connect; }
		public UserRep(string connectionString, ICategoryRepository categoriesRepository)
        {
            connectStr = connectionString;
            catRep = categoriesRepository;
        }

        //-----

        public User Create(User user)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    user.ID = Guid.NewGuid();
                    cmd.CommandText = "insert into users (ID, Name) values (@ID, @Name)";
                    cmd.Parameters.AddWithValue("@ID", user.ID);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.ExecuteNonQuery();
                    return user;

                }
            }
        }


        public User Get(Guid id)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
			{
				sqlConnect.Open();
				using (SqlCommand cmd = sqlConnect.CreateCommand())
				{
					cmd.CommandText = "select id, name from users where ID = @id";
					cmd.Parameters.AddWithValue("@id", id);

					using (var reader = cmd.ExecuteReader())
					{
						if (!reader.Read())
							throw new ArgumentException("User with id {id} not found");

						var user = new User
						{
							ID = reader.GetGuid(reader.GetOrdinal("id")),
							Name = reader.GetString(reader.GetOrdinal("name"))
						};
						return user;
					}
				}
			}
		}


        public void Delete(Guid _id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectStr))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = "delete from users where ID = @id";
                    
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.ExecuteNonQuery();
                    
                }
            }

        }


    }
}
