using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryManagmentApplication
{
    /// <summary>
    /// Логика взаимодействия для LogisticianMainWindow.xaml
    /// </summary>
    public partial class LogisticianMainWindow : Window
    {
        DataBaseService dataBaseService = new DataBaseService();

        public LogisticianMainWindow()
        {
            InitializeComponent();

            dataBaseService.GetWarehouseData(WarehousesTable);

            dataBaseService.GetMaterialData(MaterialsTable);

            dataBaseService.GetProductData(ProductsTable);

            dataBaseService.GetRecipeData(RecipesTable);

            dataBaseService.GetSupplierData(SuppliersTable);

            dataBaseService.GetTransferOrdersData(TransferOrdersTable);

            dataBaseService.GetTransferDirectionsData(TransferDirectionsTable);

            dataBaseService.GetDocumentsData(DocumentsTable);

            dataBaseService.GetProcessesData(ProcessesTable);

            SuppliersTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(SuppliersTableKeyDown),
            handledEventsToo: true);
        }

        private void SuppliersTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                var answer = MessageBox.Show("Вы действительно хотите удалить строку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {

                    var selectedSupplier = (Suppliers)SuppliersTable.SelectedItem;

                    if (selectedSupplier != null)
                    {
                        int selectedRowNumber = selectedSupplier.supplier_id;

                        var supplierToDelete = dataBaseService.GetSupplierToDelete(selectedRowNumber);

                        if (supplierToDelete != null)
                        {
                            dataBaseService.DeleteSupplier(supplierToDelete);

                            MessageBox.Show("Информация успешно удалена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        dataBaseService.GetSupplierData(SuppliersTable);
                    }
                }
                else
                {
                    MessageBox.Show("Данные не были удалены", "Отмена очистки", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

        private void IsDataEditing(object sender, DataGridCellEditEndingEventArgs e)
        {
            var supplier = SuppliersTable.SelectedItem as Suppliers;

            if (supplier != null)
            {
                dataBaseService.UpdateSuppliers(supplier);
            }
        }

        private void AddingSupplierClick(object sender, RoutedEventArgs e)
        {
            var newSupplier = new Suppliers()
            {
                supplier_name = "Название поставщика",
                contact_info = "Контактная информация"
            };

            dataBaseService.AddSupplier(newSupplier);

            dataBaseService.GetSupplierData(SuppliersTable);

            SuppliersTable.Items.Refresh();
        }
    }
}
