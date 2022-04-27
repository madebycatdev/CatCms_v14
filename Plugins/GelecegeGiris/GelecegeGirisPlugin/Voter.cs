using System;
using System.Data;
using System.Data.SqlClient;

namespace EuroCMS.Plugin.GelecegeGiris
{
    public class Voter
    {
        public int AddVoter(int formID, int AnswerID, string AnswerText, string IP)
        {
            int voterID = 0;
            SqlConnection connection = new SqlConnection("Data Source=10.113.200.21,1645;Initial Catalog=FormBuilder; User Id=fbuser; Password=pWb35G21s!fHx;pooling=true;");
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand insertCommand = new SqlCommand("sp_VoterAddNew", connection, transaction);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@FormID", formID));
            insertCommand.Parameters.Add(new SqlParameter("@IPSource", IP));
            insertCommand.Parameters.Add(new SqlParameter("@VoteDate", DateTime.Now));
            insertCommand.Parameters.Add(new SqlParameter("@StartDate", DateTime.Now));
            insertCommand.Parameters.Add(new SqlParameter("@Validated", true));
            insertCommand.Parameters.Add(new SqlParameter("@UId", DBNull.Value));
            insertCommand.Parameters.Add(new SqlParameter("@VoterID", SqlDbType.Int));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeUId", DBNull.Value));
            insertCommand.Parameters.Add(new SqlParameter("@ProgressSaveDate", DBNull.Value));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeAtPageNumber", DBNull.Value));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeQuestionNumber", DBNull.Value));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeHighestPageNumber", DBNull.Value));
            insertCommand.Parameters["@VoterID"].Direction = ParameterDirection.Output;

            try
            {
                insertCommand.ExecuteNonQuery();
                voterID = Convert.ToInt32(insertCommand.Parameters["@VoterID"].Value);
                if (voterID > 0)
                {
                    SqlCommand command2 = new SqlCommand("sp_VoterAnswersAddNew", connection, transaction);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.Add(new SqlParameter("@AnswerID", AnswerID));
                    command2.Parameters.Add(new SqlParameter("@AnswerText", AnswerText));
                    command2.Parameters.Add(new SqlParameter("@VoterID", voterID));
                    command2.Parameters.Add(new SqlParameter("@SectionNumber", Convert.ToInt32(0)));
                    command2.ExecuteNonQuery();

                }

                transaction.Commit();
                connection.Close();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            return voterID;
        }
        public void AddVoter(int voterID, int formID, int AnswerID, string AnswerText)
        {

            SqlConnection connection = new SqlConnection("Data Source=10.113.200.21,1645;Initial Catalog=FormBuilder; User Id=fbuser; Password=pWb35G21s!fHx;pooling=true;");
            connection.Open();
            SqlCommand command2 = null;
            try
            {
                if (voterID > 0)
                {
                    command2 = new SqlCommand("sp_VoterAnswersAddNew", connection);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.Add(new SqlParameter("@AnswerID", AnswerID));
                    command2.Parameters.Add(new SqlParameter("@AnswerText", AnswerText));
                    command2.Parameters.Add(new SqlParameter("@VoterID", voterID));
                    command2.Parameters.Add(new SqlParameter("@SectionNumber", Convert.ToInt32(0)));
                    command2.ExecuteNonQuery();

                }


                connection.Close();

            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                if (connection != null) { connection.Close(); connection = null; }
                command2 = null;
            }

        }
    }
}