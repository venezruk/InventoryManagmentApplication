using System.Windows.Controls;

namespace InventoryManagmentApplication
{
    public class DataBaseService
    {
        private static readonly ApplicationContext _dataBase = new ApplicationContext();

        public void GetWarehouseData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Warehouses.ToList();
        }

        public void GetMaterialData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Materials.ToList();
        }
    }
}
