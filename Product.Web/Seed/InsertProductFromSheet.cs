using Domain.Service.Repository;
using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Web.Seed
{
    public class InsertProductFromSheet
    {
        private ICommandRepository<Domain.Aggregate.Products> _repostiory;

        public InsertProductFromSheet(ICommandRepository<Domain.Aggregate.Products> repostiory)
        {
            _repostiory = repostiory;
        }
        public async Task Insert()
        {
            var products = new List<Domain.Aggregate.Products>();
            string con =
  @"Provider=Microsoft.ACE.OLEDB.12.0;;Data Source=.\Data\stock-2020.xlsx;" +
  @"Extended Properties='Excel 12.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter d = new OleDbDataAdapter("SELECT * from [Sheet1$]", con);
                d.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    if (!string.IsNullOrEmpty(row["rasma"].ToString()))
                    {
                        var total = GetColumValue(row["M"]) + GetColumValue(row["L"]) + GetColumValue(row["XL"])
                            + GetColumValue(row["2XL"]) + GetColumValue(row["3XL"]);
                        var product = new Domain.Aggregate.Products(row["Name"] + " " + row["rasma"].ToString(), 160, (short)total, 0, 1);
                        product.GetNewProductNumber();
                        if (GetColumValue(row["M"]) != 0)
                        {
                            product.AddProductSize("M", "20*20", GetColumValue(row["M"]));
                        }
                        if (GetColumValue(row["L"]) != 0)
                        {
                            product.AddProductSize("L", "20*20", GetColumValue(row["L"]));
                        }
                        if (GetColumValue(row["XL"]) != 0)
                        {
                            product.AddProductSize("XL", "20*20", GetColumValue(row["XL"]));
                        }
                        if (GetColumValue(row["2XL"]) != 0)
                        {
                            product.AddProductSize("2XL", "20*20", GetColumValue(row["2XL"]));
                        }
                        if (GetColumValue(row["3XL"]) != 0)
                        {
                            product.AddProductSize("3XL", "20*20", GetColumValue(row["3XL"]));
                        }
                        products.Add(product);
                    }
                }
            }

            _repostiory.CreateRange(products, "");
            await _repostiory.SaveAsync();
        }
        private int GetColumValue(object value)
        {
            return DBNull.Value == value ? 0 : (int)(double)value;
        }

    }
}
