using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using evernote.Model;
using evernote.DataLayer;

namespace evernote.DataLayer.sql
{
    public class NotesRepository : INoteRepository
    {
        private string connectStr;

        public NotesRepository(string connect) { connectStr = connect; }

        public Note Create(Note note)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"insert into Notes (UserId,Header,Content,DateCreated,DateUpdated) values (@userid,@header,@content,CAST(GETDATE() as datetime2(0)),CAST(GETDATE() as datetime2(0)))                                                
                                                select NoteId, DateCreated from Notes where noteid=SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("@userid", note.User_ID);
                    cmd.Parameters.AddWithValue("@header", note.Header);
                    cmd.Parameters.AddWithValue("@content", note.Content??"null");
                    using (var reader=cmd.ExecuteReader())
                    {
                        if(!reader.Read())
                            throw new ArgumentException("Note hasn't benn created.");
                        note.ID = reader.GetInt16(reader.GetOrdinal("NoteId"));
                        note.Create = reader.GetDateTime(reader.GetOrdinal("DateCreated"));
                        return note;
                    }
                }
            }
        }

        public Note Update(Note note)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"update Notes set Content=@content, Header=@header, DateUpdated=CAST(GETDATE() as datetime2(0))";
                    cmd.Parameters.AddWithValue("@uid", note.User_ID);
                    cmd.Parameters.AddWithValue("@header", note.Header);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.ExecuteNonQuery();
                    return note;
                }
            }
        }

        public void Delete(short noteId)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"delete from Notes where NoteId = @id";
                    cmd.Parameters.AddWithValue("@id", noteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AddCategory(short noteid, short categoryid)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"insert into CategoriesNotes(CategoryId,NoteId) values (@catId,@noteId)
                                                insert CategoriesUsers (CategoryId,UserId) values (@catId,(select UserId from Notes where NoteId=@noteId))";
                    cmd.Parameters.AddWithValue("@catId", categoryid);
                    cmd.Parameters.AddWithValue("@noteId", noteid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddShares(short noteid, short userid)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText = @"insert Shares (UserId, NoteId) values (@userId,@noteId)";
                    cmd.Parameters.AddWithValue("@userId", userid);
                    cmd.Parameters.AddWithValue("@noteId", noteid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Note> GetByCategory(short categoryid)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectStr))
            {
                sqlConnect.Open();
                using (SqlCommand cmd = sqlConnect.CreateCommand())
                {
                    cmd.CommandText =@"select n.NoteId as NoteId, n.DateCreated as DateCreated, n.DateUpdated as DateUpdated, n.UserId as UserId, n.Text as Text, n.Title as Title
                                                from CategoriesNotes as cn join Notes as n on cn.NoteId=n.NoteId where cn.CategoryId=@id";
                    cmd.Parameters.AddWithValue("@id", categoryid);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return parser(reader);
                        }
                    }
                }
            }
        }


      
       


        private Note parser(SqlDataReader reader)
        {
            return new Note
            {
                ID = reader.GetInt16(reader.GetOrdinal("NoteId")),
                User_ID = reader.GetInt16(reader.GetOrdinal("UserId")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                Header = reader.GetString(reader.GetOrdinal("Header")),
                Create = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                Update = reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
            };
        }

    }
}
