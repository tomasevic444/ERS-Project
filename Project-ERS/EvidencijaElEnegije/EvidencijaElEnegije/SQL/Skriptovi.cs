using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.SQL
{
    class Skriptovi
    {
        private bool ExistsById(DateTime dat, string potrosnja, potrosnjaPoSatu pps, IDbConnection connection)
        {
            string query = "select * from podatak where datum=:datum and potrosnja=:potrosnja and sat=:sat and  oblast=:oblast";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                Connection.AddParameter(command, "datum", DbType.Date);
                Connection.AddParameter(command, "potrosnja", DbType.String);
                Connection.AddParameter(command, "sat", DbType.Int32);
                Connection.AddParameter(command, "oblast", DbType.String);
                command.Prepare();
                Connection.SetParameterValue(command, "sat", pps.sat);
                Connection.SetParameterValue(command, "oblast", pps.oblast);
                Connection.SetParameterValue(command, "datum", dat.Date);
                Connection.SetParameterValue(command, "potrosnja", potrosnja);
                return command.ExecuteScalar() != null;
            }
        }


        public void dodajElement(potrosnjaPoSatu pps, Dan d)
        {
            DateTime datum = new DateTime(d.godina, d.mesec, d.dan);
            Console.WriteLine(datum.Date);
            string queryInsert = "insert into podatak (load, datum, potrosnja, sat, oblast) values (:load, :datum, :potrosnja, :sat, :oblast)";
            string queryUpdate = "update podatak set load=:load where datum=:datum and potrosnja=:potrosnja and sat=:sat and  oblast=:oblast";

            string potrosnja;
            if (d.ostvarena)
                potrosnja = "Ostvarena";
            else
                potrosnja = "Prognozirana";

            using (IDbConnection connection = Connection.ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using(IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = ExistsById(datum, potrosnja, pps, connection) ? queryUpdate : queryInsert;
                    Connection.AddParameter(command, "load", DbType.Int32);
                    Connection.AddParameter(command, "datum", DbType.Date);
                    Connection.AddParameter(command, "potrosnja", DbType.String);
                    Connection.AddParameter(command, "sat", DbType.Int32);
                    Connection.AddParameter(command, "oblast", DbType.String);
                    command.Prepare();
                    Connection.SetParameterValue(command, "sat", pps.sat);
                    Connection.SetParameterValue(command, "load", pps.load);
                    Connection.SetParameterValue(command, "oblast", pps.oblast);
                    Connection.SetParameterValue(command, "datum", datum.Date);
                    Connection.SetParameterValue(command, "potrosnja", potrosnja);
                    command.ExecuteNonQuery();
                }

            }




        }


    }
}
