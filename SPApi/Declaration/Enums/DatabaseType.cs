namespace SPApi.Declaration.Enums
{
    public enum DB_MODE
    {
        DATABASE,
        GATEWAY
    }

    public enum DB_CONNECTION_TYPE
    {
        SELECTER,
        UPLOADER
    }

    public enum DB_PROVIDER
    {
        NONE,
        AS400_ODBC,
        AS400_OLEDB,
        ORACLE,
        MSSQL,
        MYSQL,
        SQLITE3
    }
}
