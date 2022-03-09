using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace xLevelRewards
{
    public static class DB
    {
        private static string ConnectionString = "server=localhost; database=PS_GameDefs; trusted_connection=true;";

        public static List<LevelReward> ReadLevelRewards()
        {
            List<LevelReward> LevelRewards = new List<LevelReward>();

            SqlConnection Connection = new SqlConnection(ConnectionString);

            try
            {
                Connection.Open();

                SqlCommand Command = new SqlCommand
                {
                    CommandText = "usp_Read_LevelRewards_R",
                    CommandType = CommandType.StoredProcedure,
                    Connection = Connection
                };

                SqlDataReader Reader = Command.ExecuteReader();

                while (Reader.Read())
					LevelRewards.Add(new LevelReward(
                        (int)Reader["Level"],
                        (int)Reader["Faction"],
                        (int)Reader["Family"],
                        (int)Reader["Job"],
                        (int)Reader["Type"],
                        (int)Reader["TypeID"],
                        (int)Reader["Count"]
                        ));

                Reader.Close();
                Reader.Dispose();

                Command.Dispose();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), e.Message);
            }
            finally
            {
                Connection.Close();
                Connection.Dispose();
            }

            return LevelRewards;
        }
    }
}
