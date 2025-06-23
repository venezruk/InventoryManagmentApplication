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

        public void GetProductData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Products.ToList();
        }

        public void GetRecipeData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Recipes.ToList();
        }

        public void GetSupplierData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Suppliers.ToList();
        }

        public void GetTransferOrdersData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.TransferOrders.ToList();
        }

        public void GetTransferDirectionsData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.TransferDirections.ToList();
        }

        public void GetDocumentsData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Documents.ToList();
        }

        public void GetProcessesData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Processes.ToList();
        }

        public Suppliers? GetSupplierToDelete(int selectedRowNumber) => _dataBase.Suppliers.FirstOrDefault(s => s.supplier_id == selectedRowNumber);

        public void DeleteSupplier(Suppliers Supplier)
        {
            _dataBase.Suppliers.Remove(Supplier);
            _dataBase.SaveChanges();
        }

        public void AddSupplier(Suppliers supplier)
        {
            _dataBase.Suppliers.Add(supplier);
            _dataBase.SaveChanges();
        }

        public void UpdateSuppliers(Suppliers supplier)
        {
            _dataBase.Update(supplier);
            _dataBase.SaveChanges();
        }
    }
}
