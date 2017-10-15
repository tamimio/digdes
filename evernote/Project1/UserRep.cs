using System;
using System.Collections.Generic;
using System.Data.SqlClient; //
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;

using evernote.Model;

namespace evernote.DataLayer.sql
{
    public class UserRep : UserRepository
    {
        private string connect;

        public UserRep(string _connect) { connect = _connect; }

        //-----

        public User Create(User _user)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connect))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    _user.ID = Guid.NewGuid();
                    cmd.CommandText = "insert into users (ID, Name) values (@ID, @Name)";
                    cmd.Parameters.AddWithValue("@ID", _user.ID);
                    cmd.Parameters.AddWithValue("@Name", _user.Name);
                    cmd.ExecuteNonQuery();
                    return _user;

                }
            }
        }


        public User Get(Guid _id)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connect))
			{
				sqlConnect.Open();
				using (SqlCommand cmd = sqlConnect.CreateCommand())
				{
					cmd.CommandText = "select id, name from users where ID = @id";
					cmd.Parameters.AddWithValue("@id", _id);

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
            using (SqlConnection sqlConnection = new SqlConnection(connect))
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
