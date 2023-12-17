using System.Windows.Forms;
using WinForms.Data;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private INewContext newContext;
        public Form1(INewContext dbContext)
        {
            InitializeComponent();
            newContext = new NewContext();


            //
            newContext = dbContext;


        }
        public List<object> ViewTopSalesPersons(int year, int rows)
        {
            //Wyczyszcenie kolumn i wierszy przed kolejnym zapytaniem
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            //Dodanie kolumn
            dataGridView1.Columns.Add("SalesPersonID", "ID_sprzedawcy");
            dataGridView1.Columns.Add("TotalOrders", "Sumaryczna_iloœæ_zamówieñ");
            dataGridView1.Columns.Add("TotalDue", "Sumaryczna_wartoœæ_zamówieñ");
            dataGridView1.Columns.Add("CurrencyRateIDSet", "Zamówienia_z_ustawionym_kursem_waluty");
            dataGridView1.Columns.Add("CurrencyRateIDNotSet", "Zamówienia_z_nieustawionym_kursem_waluty");



            //wy³uskanie z bazy danych odpowiednich informacji
            var data = newContext.SalesOrderHeaders
            .Where(x => x.OrderDate.Year == year)
            .GroupBy(x => x.SalesPersonID)
            .Select(group => new
            {
                SalesPersonID = group.Key,
                TotalOrders = group.Count(),
                TotalDue = group.Sum(order => order.TotalDue),
                CurrencyRateIDSet = group.Count(order => order.CurrencyRateID.HasValue),
                CurrencyRateIDNotSet = group.Count(order => !order.CurrencyRateID.HasValue)
            })
                .OrderByDescending(result => result.TotalOrders)
                .Take(rows)
                .ToList();
            
            //wypisanie wszystkich danych do dataGridView1
            foreach (var item in data.Where(i => i != null))
            {
                dataGridView1.Rows.Add(item.SalesPersonID, item.TotalOrders, item.TotalDue, item.CurrencyRateIDSet, item.CurrencyRateIDNotSet);
            }

            //dostosowanie okna do iloœci wierszy
            int totalRowsHeight = dataGridView1.ColumnHeadersHeight + dataGridView1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            this.Height = totalRowsHeight + 200; dataGridView1.AutoResizeColumns();
            dataGridView1.Height = dataGridView1.ColumnHeadersHeight + (dataGridView1.RowCount * 25);
            
            return data.Cast<object>().ToList();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(comboBox1.SelectedItem);
            int rows = Convert.ToInt32(numericUpDown2.Value);
            ViewTopSalesPersons(year, rows +1);
        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}