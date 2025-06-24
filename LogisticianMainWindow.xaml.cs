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

            // Загрузка данных из базы данных в таблицы //

            dataBaseService.GetWarehouseData(WarehousesTable);

            dataBaseService.GetMaterialData(MaterialsTable);

            dataBaseService.GetRouteData(RoutesTable);

            dataBaseService.GetSupplierData(SuppliersTable);

            dataBaseService.GetProductData(ProductsTable);

            dataBaseService.GetOrderData(OrdersTable);

            dataBaseService.GetTransferOrdersShipmentData(OrdersShipmentTable);

            dataBaseService.GetShipmentDocumentData(ShipmentDocumentsTable);

            dataBaseService.GetTransferOrdersOnTheWayData(OrdersOnTheWayTable);

            dataBaseService.GetOnTheWayDocumentData(OnTheWayDocumentsTable);

            dataBaseService.GetTransferOrdersAcceptanceData(OrdersAcceptanceTable);

            dataBaseService.GetAcceptanceDocumentData(AcceptanceDocumentsTable);

            // //

            // Корректная обработка захвата и фокуса для интерактивной работы с таблицами //

            RoutesTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(RoutesTableKeyDown),
            handledEventsToo: true);

            SuppliersTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(SuppliersTableKeyDown),
            handledEventsToo: true);

            // // 
        }

        private void SuppliersTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                var answer = MessageBox.Show("Вы действительно хотите удалить информацию о поставщике?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

        private void IsSuppliersEditing(object sender, DataGridCellEditEndingEventArgs e)
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

        private void AddingRouteClick(object sender, RoutedEventArgs e)
        {
            var newRoute = new Routes()
            {
                warehouse_sender_id = 1,

                warehouse_recipient_id = 1,
            };

            dataBaseService.AddRoute(newRoute);

            dataBaseService.GetRouteData(RoutesTable);

            RoutesTable.Items.Refresh();
        }

        public void IsRoutesEditing(object sender, DataGridCellEditEndingEventArgs e)
        {
            var route = RoutesTable.SelectedItem as Routes;

            if (route != null)
            {
                int firstwarehouse = route.warehouse_sender_id;
                int secondwarehouse = route.warehouse_recipient_id;

                if (firstwarehouse == secondwarehouse) MessageBox.Show("Не стоит создавать маршрут в один и тот же склад.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning); 

                else
                {
                    if (!dataBaseService.IsWarehouseExist(firstwarehouse)) MessageBox.Show("Выбранный склад-отправитель не был найден в списке складов.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                    else
                    {
                        if (!dataBaseService.IsWarehouseExist(secondwarehouse)) MessageBox.Show("Выбранный склад-получатель не был найден в списке складов.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                        
                        else
                        {
                            dataBaseService.UpdateRoutes(route);
                        }

                    }

                }
            }
        }

        private void RoutesTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                var answer = MessageBox.Show("Вы действительно хотите удалить информацию о маршруте?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {

                    var selectedRoute = (Routes)RoutesTable.SelectedItem;

                    if (selectedRoute != null)
                    {
                        int selectedRowNumber = selectedRoute.route_id;

                        var routeToDelete = dataBaseService.GetRouteToDelete(selectedRowNumber);

                        if (routeToDelete != null)
                        {
                            dataBaseService.DeleteRoute(routeToDelete);

                            MessageBox.Show("Информация успешно удалена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        dataBaseService.GetRouteData(RoutesTable);
                    }
                }
                else
                {
                    MessageBox.Show("Данные не были удалены", "Отмена очистки", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }
    }
}
