using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaElEnegije.SQL
{
    class Connection
    {
        public class ConnectionParams
        {
            //TODO: Ukoliko koristite SUBP u VM, a Visual Studio van VM promenite localhost sa IP adresom VM
            public static readonly string LOCAL_DATA_SOURCE = "//localhost:1521/xe";
            public static readonly string CLASSROOM_DATA_SOURCE = "192.168.1.3";

            //TODO: promeniti username i password
            public static readonly string USER_ID = "C##Srdjan2";
            public static readonly string PASSWORD = "ftn";
        }

        public class ConnectionUtil_Pooling : IDisposable
        {
            private static IDbConnection instance = null;

            public static IDbConnection GetConnection()
            {
                if (instance == null || instance.State == System.Data.ConnectionState.Closed)
                {
                    OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
                    ocsb.DataSource = Connection.ConnectionParams.LOCAL_DATA_SOURCE;
                    ocsb.UserID = Connection.ConnectionParams.USER_ID;
                    ocsb.Password = Connection.ConnectionParams.PASSWORD;
                    //https://docs.oracle.com/database/121/ODPNT/featConnecting.htm#ODPNT163
                    ocsb.Pooling = true;
                    ocsb.MinPoolSize = 1;
                    ocsb.MaxPoolSize = 10;
                    ocsb.IncrPoolSize = 3;
                    ocsb.ConnectionLifeTime = 5;
                    ocsb.ConnectionTimeout = 30;
                    instance = new OracleConnection(ocsb.ConnectionString);

                }
                return instance;
            }


            public void Dispose()
            {
                if (instance != null)
                {
                    instance.Close();
                    instance.Dispose();
                }

            }
        }

            public static void AddParameter(IDbCommand command, string name, DbType type)
            {
                IDbDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = name;
                parameter.DbType = type;
                command.Parameters.Add(parameter);
            }

            public static void AddParameter(IDbCommand command, string name, DbType type, ParameterDirection direction)
            {
                IDbDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = name;
                parameter.DbType = type;
                parameter.Direction = direction;
                command.Parameters.Add(parameter);
            }

            public static void AddParameter(IDbCommand command, string name, DbType type, int size)
            {
                IDbDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = name;
                parameter.DbType = type;
                parameter.Size = size;
                command.Parameters.Add(parameter);
            }

            public static void SetParameterValue(IDbCommand command, string name, Object value)
            {
                DbParameter parameter = (DbParameter)command.Parameters[name];
                //TODO: Ovde se moze dodati provera da li postoji parametar sa prosledjenim nazivom
                //Ako ne postoji trebalo bi baciti odgovarajuci izuzetak i obraditi ga na adekvatnom mestu
                parameter.Value = value;
            }

            public static object GetParameterValue(IDbCommand command, string name)
            {
                DbParameter parameter = (DbParameter)command.Parameters[name];
                return parameter.Value;
            }


        }
    }

