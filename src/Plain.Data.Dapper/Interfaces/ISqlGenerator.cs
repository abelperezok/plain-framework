using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plain.Data.Dapper.Interfaces
{
    public interface ISqlGenerator
    {
        string GetSqlFindBy();

        string GetSqlAll();

        string GetSqlCount();

        string GetSqlPageAll();

        string GetSqlInsert();

        string GetSqlUpdate();

        string GetSqlDelete();
    }
}
