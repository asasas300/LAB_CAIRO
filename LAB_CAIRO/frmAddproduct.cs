using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB_CAIRO
{
    public partial class frmAddproduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        private BindingList<ProductClass> showProductList;
        public frmAddproduct()
        {
            InitializeComponent();
            showProductList = new BindingList<ProductClass>();
        }
        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }
        private string Category(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new StringFormatException("Category Name cannot be empty.");
            return input;
        }
        private string Product_Name(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new StringFormatException("Product Name cannot be empty.");
            return input;
        }

        private int Quantity(string input)
        {
            if (!int.TryParse(input, out int result))
                throw new NumberFormatException("Quantity must be a valid number.");
            return result;
        }

        private double SellingPrice(string input)
        {
            if (!double.TryParse(input, out double result))
                throw new CurrencyFormatException("Selling Price must be a valid currency/number.");
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                string[] ListOfProductCategory =
            {
                "Beverages", "Bread/Bakery", "Canned/Jarred Goods",
                "Dairy", "Frozen Goods", "Meat",
                "Personal Care", "Other"
            };

                foreach (string category in ListOfProductCategory)
                {
                    cbCategory.Items.Add(category);
                }
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = Category(cbCategory.Text);
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                                                     _ExpDate, _SellPrice, _Quantity, _Description));

                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "String Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Currency Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Console.WriteLine("Add Product process executed.");
            }
        }
    }
}
