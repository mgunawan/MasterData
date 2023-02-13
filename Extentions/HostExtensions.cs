using System.Data;
using MongoDB.Driver.Core.Configuration;
using MySql.Data.MySqlClient;
using Polly;
using Serilog;

namespace MasterData.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating mysql database.");

                    var retry = Policy.Handle<MySqlException>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

                    //if the mysql server container is not created on run docker compose this
                    //migration can't fail for network related exception. The retry options for database operations
                    //apply to transient exceptions

                    retry.Execute(() => ExecuteMigrations(configuration));

                    logger.LogInformation("Migrated mysql database.");
                }
                catch (MySqlException ex)
                {
                    logger.LogError(ex, ex.Message);
                    host.StopAsync();
                }
            }

            return host;
        }

        private static void ExecuteMigrations(IConfiguration configuration)
        {
            using var connection = new MySqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = new MySqlCommand
            {
                Connection = connection
            };

            //command.CommandText = "DROP TABLE IF EXISTS product";
            //command.ExecuteNonQuery();
            //SeedDataPool(connection);

            bool alterTable = false;
            string curVersion = configuration.GetValue<string>("DatabaseSettings:Version");
            if (TableExist(command, "db_migration"))
            {
                command.CommandText = "SELECT version from db_migration order by id desc limit 1";
                var rd = command.ExecuteReader();
                var tb = new DataTable();
                tb.Load(rd);
                if (tb.Rows.Count > 0)
                {
                    if (tb.Rows[0]["version"].ToString() != curVersion)
                        alterTable = true;
                }
            }
            else
            {
                if (!TableExist(command, "mst_pool"))
                {
                    command.CommandText = @"CREATE TABLE mst_pool (id varchar(5) NOT NULL PRIMARY KEY,
                                                            id2 varchar(5) NOT NULL,
                                                            name varchar(30),
                                                            address varchar(255),
                                                            telephone varchar(30),
                                                            area_id varchar(5),
                                                            is_active bit,
                                                            status_pool int,
                                                            parent_id varchar(5),
                                                            active_on date,
                                                            non_active_on date,                                                            
                                                            create_on date,
                                                            update_on date,
                                                            user_id varchar(30),
                                                            computer_name varchar(100))";
                    command.ExecuteNonQuery();
                    SeedDataPool(connection);
                }


                if (!TableExist(command, "mst_vm_servicetype"))
                {
                    command.CommandText = @"CREATE TABLE mst_vm_servicetype (id varchar(5) NOT NULL PRIMARY KEY,
                                                            name varchar(30),
                                                            available_for varchar(255))";
                    command.ExecuteNonQuery();
                    SeedDataServiceType(connection);
                }

                if (!TableExist(command, "mst_area"))
                {
                    command.CommandText = @"CREATE TABLE mst_area (id varchar(5) NOT NULL PRIMARY KEY, 
                                                    name VARCHAR(30) NOT NULL)";
                    command.ExecuteNonQuery();
                    SeedDataArea(connection);
                }

                if (!TableExist(command, "mst_company"))
                {
                    command.CommandText = @"CREATE TABLE mst_company (id varchar(10) NOT NULL,
                                                            id2 varchar(10)  NOT NULL,
                                                            name varchar(30),
                                                            is_active bit,
                                                            PRIMARY KEY (id, id2));";
                    command.ExecuteNonQuery();
                    SeedDataCompany(connection);
                }

                if (!TableExist(command, "mst_pool_company"))
                {
                    command.CommandText = @"CREATE TABLE mst_pool_company (
                                                pool_id varchar(5) NOT NULL,                                                
                                                company_id varchar(10) NOT NULL,
                                                is_induk bit NOT NULL DEFAULT 0,
                                                service_type int,
                                                is_active bit NOT NULL DEFAULT 1,
                                                create_on datetime,
                                                update_on datetime,
                                                PRIMARY KEY (pool_id, company_id, service_type));";
                    command.ExecuteNonQuery();
                }

                if (!TableExist(command, "mst_media"))
                {
                    command.CommandText = @"CREATE TABLE mst_media (id varchar(5) NOT NULL PRIMARY KEY,
                                                            name varchar(30),
                                                            description varchar(255),
                                                            is_active bit NOT NULL DEFAULT 1,
                                                            index_cmb int NOT NULL DEFAULT 1)";
                    command.ExecuteNonQuery();
                    SeedDataMedia(connection);
                }

                if (!TableExist(command, "mst_background"))
                {
                    command.CommandText = @"CREATE TABLE mst_background (id varchar(5) NOT NULL PRIMARY KEY,
                                                            name varchar(50),
                                                            is_active bit NOT NULL DEFAULT 1,
                                                            index_cmb int NOT NULL DEFAULT 1)";
                    command.ExecuteNonQuery();
                    SeedDataworkBackground(connection);
                }

                if (!TableExist(command, "db_migration"))
                {
                    command.CommandText = @"CREATE TABLE db_migration (Id SERIAL PRIMARY KEY, 
                                                    version VARCHAR(255) NOT NULL,
                                                    execute_at Varchar(255))";
                    command.ExecuteNonQuery();
                }

                //update migration
                command.CommandText = $"INSERT into db_migration (version, execute_at) values ('{curVersion}','{DateTime.Now.ToString("yyyyMMdd HHmmss")}');";
                command.ExecuteNonQuery();
            }

            if (alterTable)
            {
                //CURRENT V 1


                //if (!TableExist(command, "mst_media"))
                //{
                //    command.CommandText = @"CREATE TABLE mst_media (id varchar(5) NOT NULL PRIMARY KEY,
                //                                            name varchar(30),
                //                                            description varchar(255),
                //                                            is_active bit NOT NULL DEFAULT 1,
                //                                            index_cmb int NOT NULL DEFAULT 1)";
                //    command.ExecuteNonQuery();
                //    SeedDataMedia(connection);
                //}

                //string upgradeTable = $@"ALTER TABLE mst_pool_company " +
                //    "ADD createon datetime," +
                //    "ADD updateon datetime;";
                //command.CommandText = upgradeTable;
                //command.ExecuteNonQuery();

                //update migration
                command.CommandText = $"INSERT into db_migration (version, execute_at) values ('{curVersion}','{DateTime.Now.ToString("yyyyMMdd HHmmss")}');";
                command.ExecuteNonQuery();
            }
        }

        private static void SeedDataworkBackground(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        private static void SeedDataMedia(MySqlConnection conn)
        {
            string fileName = @".\Extentions\init_data_media.sql";
            string data = System.IO.File.ReadAllText(fileName);
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = data;
                cmd.ExecuteNonQuery();
            }
        }

        private static void SeedDataServiceType(MySqlConnection conn)
        {
            string fileName = @".\Extentions\init_data_servicetype.sql";
            string data = System.IO.File.ReadAllText(fileName);
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = data;
                cmd.ExecuteNonQuery();
            }
        }

        private static void SeedDataCompany(MySqlConnection conn)
        {
            string fileName = @".\Extentions\init_data_company.sql";
            //using (MySqlConnection connt = conn)
            //{
            string data = System.IO.File.ReadAllText(fileName);
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = data;
                cmd.ExecuteNonQuery();
            }
            //}
        }

        private static void SeedDataArea(MySqlConnection conn)
        {
            string fileName = @".\Extentions\init_data_area.sql";
            //using (MySqlConnection connt = conn)
            //{
            string data = System.IO.File.ReadAllText(fileName);
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = data;
                cmd.ExecuteNonQuery();
            }
            //}
        }

        private static void SeedDataPool(MySqlConnection conn)
        {
            string fileName = @".\Extentions\init_data_pool.sql";
            //using (MySqlConnection connt = conn)
            //{
            string data = System.IO.File.ReadAllText(fileName);
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = data;
                cmd.ExecuteNonQuery();
            }
            //}
        }

        private static bool TableExist(MySqlCommand cmd, string tableName)
        {
            string sqlQuery = "SELECT COUNT(*) Jml FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = '" + tableName + "'";
            cmd.CommandText = sqlQuery;
            var dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return Convert.ToInt16(dt.Rows[0]["Jml"].ToString()) > 0;
        }
    }
}
